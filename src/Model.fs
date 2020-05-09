/// Defines the Model at the basis of State updation behavior and View Rendering
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