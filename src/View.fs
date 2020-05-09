module View
open Fable.React
open Fable.React.Props
open Model
open State

let connectionDisplay connected dispatch = 
  div [ Class "col-sm" ] 
      [ match connected with
        | false ->
          p [] 
            [ str "Not Connected" ]
        | true -> 
          p [] 
            [ str "Connected" ] ]

let counterDisplay counter dispatch =
  div [ Class "col-sm" ] 
      [ div [ Class "btn-group"
              Role "group" ]
            [ button  
                [ Type "button"
                  Class "btn btn-secondary"
                  OnClick (fun _ -> dispatch (Increment |> Client)) ]
                [ str "+" ]
              button  
                [ Type "button"
                  Class "btn btn-secondary" ]
                [ str (string counter) ]
              button  
                [ Type "button"
                  Class "btn btn-secondary"
                  OnClick (fun _ -> dispatch (Decrement |> Client)) ]
                [ str "-" ] ] ]

let chatMessageDisplay message dispatch =
  div [ Class "col-sm" ]
      [ hr  []
        div []
            [ div [ Style 
                        [ Display DisplayOptions.InlineBlock
                          PaddingLeft "12px" ] ]
                  [ div []
                        [ span [ Class "text-info small" ]
                                [ strong [ ]
                                [ str message.sender ] ] ]
                    div []
                        [ str message.text ] ] ] ]

let chatDisplay messages dispatch = 
  div [] 
      [ for chat in messages -> chatMessageDisplay chat dispatch ]

let view (model:Model) dispatch =
  div [ Class "container" ]
      [ h3  []
            [ str "Fable-Elmish SignalR Serverless Chat" ]
        hr  []
        div [ Class "row" ] 
            [ connectionDisplay model.Connection.Connected dispatch ]
        div [ Class "row" ] 
            [ counterDisplay model.Counter dispatch ]
        div [ Class "row" ] 
            [ chatDisplay model.ReceivedMessages dispatch ] ]
