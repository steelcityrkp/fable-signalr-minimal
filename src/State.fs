module State
open Model

type ServiceRemoteMsg =
| Connected of unit
| RemoteChatMessage of ChatMessage
type ClientBehaviorMsg =
| Increment
| Decrement
type Msg =
| Client of ClientBehaviorMsg
| Service of ServiceRemoteMsg

let init() : Model = {
    Connected = false
    Messages = List.empty
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
      | RemoteChatMessage rcm ->
        model