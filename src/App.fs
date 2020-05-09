module App
open Elmish
open Elmish.React
open State
open Connection
open View
Program.mkSimple init update view
|> Program.withReactSynchronous "elmish-app"
|> Program.withSubscription hubConnection
|> Program.withConsoleTrace
|> Program.run