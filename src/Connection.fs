/// Manages live Service Connection
module Connection
/// Manages SignalR JS Client InterOp
module SignalR =
    open Fable.Import
    open Fable.Core
    type LogLevel =
    | Trace = 0
    | Debug = 1
    | Information = 2
    | Warning = 3
    | Error = 4
    | Critical = 5
    | None = 6
    type IConnection =
        abstract start : unit -> JS.Promise<unit>
        abstract on : methodName:string * newMethod:(obj -> unit) -> unit
    type IConnectionBuilder =
        abstract withUrl : string -> IConnectionBuilder
        abstract configureLogging : LogLevel -> IConnectionBuilder
        abstract build : unit -> IConnection
    [<ImportDefault("@microsoft/signalr")>]
    [<Emit("new signalR.HubConnectionBuilder()")>]
    let MyConnectionBuilder() : IConnectionBuilder = jsNative    
/// Manages Elmish Subscription for live Connection messaging
module Subscription =
    open Elmish
    open SignalR
    open Model
    open State
    /// Starts the SignalR Hub Connection and registers its handlers and orientation as an Elmish Subscription.
    let hubConnection initial = 
        let sub dispatch =
            let dispatchConnected () = 
                Connected 
                |> Session
                |> Service
                |> dispatch
            ///Handles Push Messages from the Service and dispatches them as Elmish messages.
            let onHubMessage (msg:obj) =
                msg :?> ChatMessage
                |> ReceivedChatMessage
                |> Push
                |> Service
                |> dispatch
            let connection = 
                MyConnectionBuilder()
                    .withUrl(initial.Session.ConnectionURI)
                    .configureLogging(LogLevel.Information)
                    .build()
            do connection.on("newMessage", onHubMessage)
            do Promise.iter 
                dispatchConnected 
                (connection.start()) 
                |> ignore
        Cmd.ofSub sub