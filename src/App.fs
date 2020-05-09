module App

(**
 The famous Increment/Decrement ported from Elm.
 You can find more info about Elmish architecture and samples at https://elmish.github.io/
*)

open Elmish
open Elmish.React
open Fable.React
open Fable.React.Props

open State
open Connection

// MODEL
type Model = int
let init() : Model = 0

// UPDATE
let update (msg:Msg) (model:Model) =
    match msg with
    | Client cmsg ->
      match cmsg with
      | Increment -> model + 1
      | Decrement -> model - 1
    | Service smsg ->
      match smsg with
      | RemoteChatMessage rcm ->
        model

// VIEW (rendered with React)
let view (model:Model) dispatch =
  div []
      [ button [ OnClick (fun _ -> dispatch (Increment |> Client)) ] [ str "+" ]
        div [] [ str (string model) ]
        button [ OnClick (fun _ -> dispatch (Decrement |> Client)) ] [ str "-" ] ]

// App
Program.mkSimple init update view
|> Program.withReactSynchronous "elmish-app"
|> Program.withSubscription hubConnection
|> Program.withConsoleTrace
|> Program.run
