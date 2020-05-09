module Connection

open Fable.Import
open Fable.Core
open Elmish

module SignalR =
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

open SignalR
open State

/// Starts the SignalR Hub Connection and registers its handlers and orientation as an Elmish subscription.
let hubConnection initial = 
    let sub dispatch =
        ///Handles Push Messages from the Server and dispatches them as Elmish messages.
        let onLiveOperatorMessage (msg:obj) =
            match msg with
            | :? ChatMessage -> 
                (downcast msg : ChatMessage)
                |> RemoteChatMessage
                |> Service
                |> dispatch
            | _ ->
                {sender = "No Sender"; text = "No Text"}
                |> RemoteChatMessage
                |> Service
                |> dispatch
        ///Establishes the SignalR Hub Connection
        let connection = 
            MyConnectionBuilder()
                .withUrl("http://localhost:7071/api")
                .configureLogging(LogLevel.Information)
                .build()
        connection.on("newMessage", onLiveOperatorMessage)
        connection.start() |> ignore
    Cmd.ofSub sub
