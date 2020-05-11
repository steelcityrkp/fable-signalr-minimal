/// Defines the Model at the basis of State updation behavior and View Rendering
module Model

/// Matching definition from Service in Azure Serverless SignalR Demo
type ChatMessage = {
    sender : string
    text : string}

type Session = {
    Sender : string
    ConnectionURI : string
    Connected : bool}
type Display = {
    ReceivedMessages : ChatMessage list
    Counter : int}
type Input = {
    NewChatMessage : string}

type Model = {
    Session : Session
    Display : Display
    Input : Input}