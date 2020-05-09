module App

(**
 The famous Increment/Decrement ported from Elm.
 You can find more info about Elmish architecture and samples at https://elmish.github.io/
*)

open Elmish
open Elmish.React
open Fable.React
open Fable.React.Props

// MODEL

type Model = int



let init() : Model = 0

// UPDATE

let update (msg:Msg.Msg) (model:Model) =
    match msg with
    | Increment -> model + 1
    | Decrement -> model - 1

// VIEW (rendered with React)

let view (model:Model) dispatch =

  div []
      [ button [ OnClick (fun _ -> dispatch Msg.Increment) ] [ str "+" ]
        div [] [ str (string model) ]
        button [ OnClick (fun _ -> dispatch Msg.Decrement) ] [ str "-" ] ]

//Connection.initialize() |> ignore

// App
Program.mkSimple init update view
|> Program.withReactSynchronous "elmish-app"
|> Program.withSubscription Connection.operatorConnection
|> Program.withConsoleTrace
|> Program.run
