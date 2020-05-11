/// Manages Model's State update behavior and associated messaging
module State
open Model


module RemoteCommander = 
  open Fable.Core
  open Fetch
  let postChat session txt = async {
    let uri = 
      session.ConnectionURI 
      |> sprintf "%s/api/messages"
    let body = {
      sender = session.Sender
      text = txt}
    let props = [ Method HttpMethod.POST
                  Body (body |> unbox) ]
    do JS.console.log uri
    do JS.console.log body
    let! response = 
      fetch uri props
      |> Async.AwaitPromise 
    return response }

type InitialModelArguments = {
  Sender : string
  HubConnectionURI : string
}
let init (args:InitialModelArguments) : Model = {
    Session = {
      Sender = args.Sender
      ConnectionURI = args.HubConnectionURI
      Connected = false}
    Display = {
      ReceivedMessages = List.empty
      Counter = 0}
    Input = {
      NewChatMessage = ""}}

type ClientBehaviorMsg =
| Local of LocalBehaviorMsg
| Command of RemoteCommandMsg
and LocalBehaviorMsg =
| Increment
| Decrement
| NewChatMessageValueChange of string
and RemoteCommandMsg =
| SendNewChatMessage
let processClientBehaviorMsg (msg:ClientBehaviorMsg) (model:Model) =
  match msg with
    | Local lMsg ->
      match lMsg with
      | Increment -> 
        {model with Display = {
                model.Display with Counter = model.Display.Counter + 1}}
      | Decrement ->
        {model with Display = {
                model.Display with Counter = model.Display.Counter - 1}}
      | NewChatMessageValueChange txt ->
        {model with Input = {
                model.Input with NewChatMessage = txt}}
    | Command rcMsg ->
      match rcMsg with
      | SendNewChatMessage ->
        do RemoteCommander.postChat model.Session model.Input.NewChatMessage
           |> Async.Ignore
           |> Async.Start
        {model with Input = {
                model.Input with NewChatMessage = ""}}

type ServiceRemoteMsg =
| Session of ConnectionSessionMsg
| Push of ServicePushMsg
and ConnectionSessionMsg =
| Connected
and ServicePushMsg =
| ReceivedChatMessage of ChatMessage
let processServiceRemoteMsg (msg:ServiceRemoteMsg) (model:Model) =
  match msg with
    | Session sMsg ->
      match sMsg with
      | Connected -> 
        {model with Session = {
                model.Session with Connected = true}}
    | Push pMsg ->
      match pMsg with
      | ReceivedChatMessage chat -> 
        {model with Display = { 
                model.Display with ReceivedMessages = chat :: model.Display.ReceivedMessages}}

type Msg =
| Client of ClientBehaviorMsg
| Service of ServiceRemoteMsg
let update (msg:Msg) (model:Model) =
    match msg with
    | Client cMsg -> 
      model
      |> processClientBehaviorMsg cMsg
    | Service sMsg ->
      model
      |> processServiceRemoteMsg sMsg