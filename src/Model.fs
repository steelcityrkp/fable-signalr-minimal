module Model

type HubConnection = {
    ConnectionURI : string
    Connected : bool
}

type ChatMessage = {
    sender : string
    text : string
}
type Model = {
    Connection : HubConnection
    ReceivedMessages : ChatMessage list
    Counter : int
}