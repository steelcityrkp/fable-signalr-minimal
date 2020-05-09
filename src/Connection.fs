module Connection
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
module Subscription =
    open Elmish
    open SignalR
    open Model
    open State
    /// Starts the SignalR Hub Connection and registers its handlers and orientation as an Elmish subscription.
    let hubConnection initial = 
        let sub dispatch =
            let dispatchConnected () = 
                Connected 
                |> Service 
                |> dispatch
            ///Handles Push Messages from the Server and dispatches them as Elmish messages.
            let onHubMessage (msg:obj) =
                msg :?> ChatMessage
                |> RemoteChatMessage
                |> Service
                |> dispatch
            let connection = 
                MyConnectionBuilder()
                    .withUrl(initial.Connection.ConnectionURI)
                    .configureLogging(LogLevel.Information)
                    .build()
            do connection.on("newMessage", onHubMessage)
            do Promise.iter 
                dispatchConnected 
                (connection.start()) 
                |> ignore
        Cmd.ofSub sub