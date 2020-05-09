module Model

type ChatMessage = {
    sender : string
    text : string
}
type Model = {
    Connected : bool
    Messages : ChatMessage list
    Counter : int
}