module App
open Elmish
open Elmish.React
open State
open Connection
open View



// Pulls from the Local Storage Values set in Browser prior to the App Initialization
let initialArgs = {
    Sender = Browser.Dom.window.localStorage.getItem("Sender")
    HubConnectionURI = Browser.Dom.window.localStorage.getItem("ServiceURI")}

let startApplication args = 
    Program.mkSimple init update view
    |> Program.withReactSynchronous "elmish-app"
    |> Program.withSubscription Subscription.hubConnection
    |> Program.withConsoleTrace
    |> Program.runWith args

do startApplication initialArgs