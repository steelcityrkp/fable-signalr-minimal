module App
open Elmish
open Elmish.React
open State
open Connection
open View
let args = {
    Sender = "Jah"
    HubConnectionURI = "http://localhost:7071/api"}
Program.mkSimple init update view
|> Program.withReactSynchronous "elmish-app"
|> Program.withSubscription Subscription.hubConnection
|> Program.withConsoleTrace
|> Program.runWith args