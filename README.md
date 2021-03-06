# Fable SignalR Minimal App

This is a minimal Fable App intended to utilize basic SignalR functionality in lieu of [Microsoft's Serverless SignalR Chat Demo built on Vue](https://azure-samples.github.io/signalr-service-quickstart-serverless-chat/demo/chat-v2/).  This is meant to provide the basics of Connecting to and receving Push Communication from an operating SignalR Service.

## Requirements

* Should have set up a SignalR Service to connect to as per the [Microsoft Tutorial](https://docs.microsoft.com/en-us/azure/azure-signalr/signalr-quickstart-azure-functions-csharp).
* [dotnet SDK](https://www.microsoft.com/net/download/core) 2.1 or higher
* [node.js](https://nodejs.org) with [npm](https://www.npmjs.com/)
* An F# editor like Visual Studio, Visual Studio Code with [Ionide](http://ionide.io/) or [JetBrains Rider](https://www.jetbrains.com/rider/).

## Building and running the app

* Install JS dependencies: `npm install`
* Start Webpack dev server: `npx webpack-dev-server` or `npm start`
* After the first compilation is finished, in your browser open: http://localhost:8080/

Any modification you do to the F# code will be reflected in the web page after saving.
