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

type ServiceRemoteMsg =
| Connected
| RemoteChatMessage of ChatMessage
type ClientBehaviorMsg =
| Increment
| Decrement
type Msg =
| Client of ClientBehaviorMsg
| Service of ServiceRemoteMsg

let update (msg:Msg) (model:Model) =
    match msg with
    | Client cmsg ->
      match cmsg with
      | Increment -> {model with Counter = model.Counter + 1}
      | Decrement -> {model with Counter = model.Counter - 1}
    | Service smsg ->
      match smsg with
      | RemoteChatMessage rcm -> { model with ReceivedMessages = rcm :: model.ReceivedMessages }
      | Connected -> {model with Connection = {model.Connection with Connected = true}}