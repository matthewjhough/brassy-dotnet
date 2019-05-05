using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using brassy_api.src.Data;
using brassy_api.src.Message;
using brassy_api.src.Mood;
using brassy_api.src.Operations;
using GraphiQl;
using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Transports.AspNetCore;
using GraphQL.Server.Transports.WebSockets;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace brassy_api {
    public class Startup {
        public Startup (IHostingEnvironment env) {
            var builder = new ConfigurationBuilder ()
                .SetBasePath (env.ContentRootPath)
                .AddJsonFile ("appsettings.json", optional : true, reloadOnChange : true)
                .AddJsonFile ($"appsettings.{env.EnvironmentName}.json", optional : true)
                .AddEnvironmentVariables ();
            Configuration = builder.Build ();
            Env = env;
        }

        public IConfigurationRoot Configuration { get; }
        private IHostingEnvironment Env { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services.AddScoped<IDocumentExecuter, DocumentExecuter> ();

            services.AddTransient<IMessageRepository, MessageRepository> ()
                .AddTransient<MessageSchema> ()
                .AddTransient<Subscription> ()
                .AddTransient<MessageInputType> ()
                .AddSingleton<MoodType> ();

            services
                .AddDbContext<BrassyContext> (options => options.UseSqlServer (Configuration["ConnectionStrings:BrassyDatabaseConnection"]));

            services.AddGraphQL (options => {
                    options.EnableMetrics = true;
                })
                .AddWebSockets ()
                .AddDataLoader ();

            services.AddMvc ();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (
            IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory,
            BrassyContext db
        ) {
            loggerFactory.AddConsole (Configuration.GetSection ("Logging"));
            loggerFactory.AddDebug ();

            app.UseWebSockets ();
            app.UseGraphQLWebSockets<MessageSchema> ("/graphql");
            app.UseStaticFiles ();
            app.UseMvc ();
            db.EnsureSeedData ();
        }
    }
}