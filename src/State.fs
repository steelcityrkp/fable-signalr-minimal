/// Manages Model's State update behavior and associated messaging
module State
open Model

type InitialModelArguments = {
  HubConnectionURI : string
}
let init (args:InitialModelArguments) : Model = {
    Connection = {
      ConnectionURI = args.HubConnectionURI
      Connected = false }
    ReceivedMessages = List.empty
    Counter = 0
}
type ClientBehaviorMsg =
| Local of LocalBehaviorMsg
| Command of RemoteCommandMsg
and LocalBehaviorMsg =
| Increment
| Decrement
and RemoteCommandMsg =
| SendChatMessage of ChatMessage
let processClientBehaviorMsg (msg:ClientBehaviorMsg) (model:Model) =
  match msg with
    | Local lMsg ->
      match lMsg with
      | Increment -> {model with Counter = model.Counter + 1}
      | Decrement -> {model with Counter = model.Counter - 1}
    | Command rcMsg ->
      match rcMsg with
      | SendChatMessage chat ->
        model

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
        {model with Connection = {model.Connection with Connected = true}}
    | Push pMsg ->
      match pMsg with
      | ReceivedChatMessage chat -> 
        {model with ReceivedMessages = chat :: model.ReceivedMessages}

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