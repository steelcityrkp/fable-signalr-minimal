module Model

type ChatMessage = {
    sender : string
    text : string
}
type Model = {
    Connected : bool
    ReceivedMessages : ChatMessage list
    Counter : int
}