const webpack = require('webpack');
var output = '/wwwroot';

module.exports = {
	mode: 'development',
	entry: {
		bundle: './GraphiQL/app.js',
	},
	output: {
		path: __dirname + output,
		filename: '[name].js',
	},
	resolve: {
		extensions: ['flow', '.mjs', 'jsx', '.js', '.json'],
	},
	module: {
		rules: [
			{
				test: /\.css$/,
				use: ['style-loader', 'css-loader'],
			},
			{
				test: /\.html$/,
				use: [
					{
						loader: 'html-loader',
					},
				],
			},
			{
				test: /\.m?js$/,
				exclude: /(node_modules|bower_components)/,
				use: {
					loader: 'babel-loader',
					options: {
						presets: ['@babel/preset-env'],
					},
				},
			},
		],
	},
	plugins: [
		new webpack.DefinePlugin({
			development: {
				GRAPHQL_NO_NAME_WARNING: true,
			},
			process: {
				env: 'development',
			},
		}),
		new webpack.ContextReplacementPlugin(
			/graphql-language-service-interface[\/\\]dist/,
			/\.js$/
		),
	],
};
