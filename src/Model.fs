/// Defines the Model at the basis of State updation behavior and View Rendering
module Model

type Session = {
    Sender : string
    ConnectionURI : string
    Connected : bool
}
type ChatMessage = {
    sender : string
    text : string
}

type Model = {
    Session : Session
    ReceivedMessages : ChatMessage list
    Counter : int
}