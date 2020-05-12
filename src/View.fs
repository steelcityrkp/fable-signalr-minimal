/// Manages functional reactive View Rendering
module View
open Fable.Core.JsInterop
open Fable.React
open Fable.React.Props
open Model
open State

let connectionDisplay connected dispatch = 
  div [ Class "col-sm" ]
      [ hr  []
        p   []
            [str "SignalR Hub Connection Status"]
        match connected with
        | false ->
          div [Class "alert alert-warning"]
            [ str "Not Connected" ]
        | true -> 
          div [Class "alert alert-success"]
            [ str "Connected" ] ]

let counterDisplay counter dispatch =
  div [ Class "col-sm" ] 
      [ hr  []
        p   []
            [str "Local Counter - Classic Elmish Messaging Example"]
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

let chatInputFormDisplay chatInput dispatch = 
  let dispatchInputValueChange (ev:Browser.Types.Event) = 
    !!ev.target?value 
    |> NewChatMessageValueChange 
    |> Local
    |> Client
    |> dispatch
  let dispatchInputFormSubmission (ev:Browser.Types.Event) =
    do ev.preventDefault() //Prevent Default Form Submission Refresh Behavior!
    SendNewChatMessage 
    |> Command 
    |> Client 
    |> dispatch
  div [ Class "col-sm" ]
      [ hr  [ ]
        p   []
            [str "New Chat Message Input"]
        form 
            [ 
              OnSubmit dispatchInputFormSubmission]
            [ input 
                [ Type "text"
                  Id "message-box"
                  Placeholder "Type message here..."
                  AutoComplete "off"
                  Class "form-control"
                  Value chatInput
                  OnChange dispatchInputValueChange ] ] ]

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

let chatReceivedMessagesDisplay messages dispatch = 
  div [] 
      [ for chat in messages -> chatMessageDisplay chat dispatch ]

let view (model:Model) dispatch =
  div [ Class "container" ]
      [ br  []
        h3  []
            [ img [ Src "fable.png" 
                    HTMLAttr.Width "50px" ]
              img [ Src "functions.png"
                    HTMLAttr.Width "50px" ]  
              str "Fable-Elmish SignalR Serverless Chat" ]        
        div [ Class "row" ]
            [ connectionDisplay model.Session.Connected dispatch ]
        div [ Class "row" ] 
            [ counterDisplay model.Display.Counter dispatch ]
        div [ Class "row" ] 
            [ chatInputFormDisplay model.Input.NewChatMessage dispatch ]
        div [ Class "row" ] 
            [ chatReceivedMessagesDisplay model.Display.ReceivedMessages dispatch ] ]
