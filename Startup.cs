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
            // Add GraphQL services and configure options
            services.AddGraphQL (options => {
                    options.EnableMetrics = true;
                })
                .AddWebSockets () // Add required services for web socket support
                .AddDataLoader (); // Add required services for DataLoader support
            // services.AddTransient<Query> ();
            // services.AddTransient<Mutation> ();
            // services.AddTransient<Subscription> ();
            services.AddTransient<IMessageRepository, MessageRepository> ();
            services.AddTransient<MessageSchema> ();
            services.AddSingleton<MessageInputType> ();
            services.AddSingleton<MoodType> ();
            services.AddDbContext<BrassyContext> (options => options.UseSqlServer (Configuration["ConnectionStrings:BrassyDatabaseConnection"]));
            services.AddMvc ();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, BrassyContext db) {
            loggerFactory.AddConsole (Configuration.GetSection ("Logging"));
            loggerFactory.AddDebug ();
            // this is required for websockets support
            app.UseWebSockets ();

            // use websocket middleware for MessageSchema at path /graphql
            app.UseGraphQLWebSockets<MessageSchema> ("/graphql");
            app.UseGraphiQl ();
            app.UseStaticFiles ();
            app.UseMvc ();

            db.EnsureSeedData ();
        }
    }
}