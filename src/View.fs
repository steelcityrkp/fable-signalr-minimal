module View
open Fable.React
open Fable.React.Props
open Model
open State

let view (model:Model) dispatch =
  div []
      [ button [ OnClick (fun _ -> dispatch (Increment |> Client)) ] [ str "+" ]
        div [] [ str (string model) ]
        button [ OnClick (fun _ -> dispatch (Decrement |> Client)) ] [ str "-" ] ]