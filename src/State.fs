module State
open Model

type ServiceRemoteMsg =
| Connected
| RemoteChatMessage of ChatMessage
type ClientBehaviorMsg =
| Increment
| Decrement
type Msg =
| Client of ClientBehaviorMsg
| Service of ServiceRemoteMsg

let init() : Model = {
    Connected = false
    ReceivedMessages = List.empty
    Counter = 0
}
let update (msg:Msg) (model:Model) =
    match msg with
    | Client cmsg ->
      match cmsg with
      | Increment -> {model with Counter = model.Counter + 1}
      | Decrement -> {model with Counter = model.Counter - 1}
    | Service smsg ->
      match smsg with
      | RemoteChatMessage rcm -> { model with ReceivedMessages = rcm :: model.ReceivedMessages }
      | Connected -> {model with Connected = true}