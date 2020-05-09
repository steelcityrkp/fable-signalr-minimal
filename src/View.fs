/// Manages functional reactive View Rendering
module View
open Fable.React
open Fable.React.Props
open Model
open State

let connectionDisplay connected dispatch = 
  div [ Class "col-sm" ] 
      [ hr  []
        match connected with
        | false ->
          p [] 
            [ str "Not Connected" ]
        | true -> 
          p [] 
            [ str "Connected" ] ]

let counterDisplay counter dispatch =
  div [ Class "col-sm" ] 
      [ hr  []
        div [ Class "btn-group"
              Role "group" ]
            [ button  
                [ Type "button"
                  Class "btn btn-secondary"
                  OnClick (fun _ -> dispatch (Increment |> Local |> Client)) ]
                [ str "+" ]
              button  
                [ Type "button"
                  Class "btn btn-secondary" ]
                [ str (string counter) ]
              button  
                [ Type "button"
                  Class "btn btn-secondary"
                  OnClick (fun _ -> dispatch (Decrement |> Local |> Client)) ]
                [ str "-" ] ] ]

let chatInputFormDisplay dispatch = 
  div [ Class "col-sm" ]
      [ hr [ ]
        form 
          []
          [ input 
              [ Type "text"
                Id "message-box"
                Placeholder "Type message here..."
                AutoComplete "off"
                Class "form-control" ] ] ]

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
        div [ Class "row" ]
            [ connectionDisplay model.Connection.Connected dispatch ]
        div [ Class "row" ] 
            [ counterDisplay model.Counter dispatch ]
        div [ Class "row" ] 
            [ chatInputFormDisplay dispatch ]
        div [ Class "row" ] 
            [ chatDisplay model.ReceivedMessages dispatch ] ]
