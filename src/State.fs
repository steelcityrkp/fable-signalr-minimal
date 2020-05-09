module State

type ChatMessage = {
    sender : string
    text : string
}

type ServicePushMsg =
| RemoteChatMessage of ChatMessage

type ClientBehaviorMsg =
| Increment
| Decrement

type Msg =
| Client of ClientBehaviorMsg
| Service of ServicePushMsg