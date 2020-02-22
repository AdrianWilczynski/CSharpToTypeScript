//@ts-check

const MonacoWebpackPlugin = require('monaco-editor-webpack-plugin');
const path = require('path');

/**@type {import('webpack').Configuration}*/
const config = {
    mode: 'development',
    entry: {
        index: './Scripts/index.ts'
    },
    resolve: {
        extensions: ['.ts', '.js']
    },
    module: {
        rules: [
            {
                test: /\.ts$/,
                use: 'ts-loader',
                exclude: /node_modules/
            },
            {
                test: /\.css$/,
                use: ['style-loader', 'css-loader']
            },
            {
                test: /\.ttf$/,
                use: ['file-loader']
            }
        ]
    },
    plugins: [
        new MonacoWebpackPlugin()
    ],
    output: {
        filename: '[name].js',
        publicPath: 'dist/',
        path: path.resolve(__dirname, 'wwwroot/dist/')
    }
};

module.exports = config;