module Connection

open SignalR
open Fable.Core.JsInterop
open Fable.Import
open Fable.Core
open Elmish

// type IConnection =
//     abstract initConnection : unit -> unit
//     abstract newMessageHandler : 'a -> unit

// [<Import("*", "./Connection.js")>]
// let myLib: IConnection = jsNative

let initialize () = 
    //hubconnect.start()
    //clientLib.HubConnection.start()
    //do myLib.initConnection()
    let test = ignore

    let connection = 
        MyConnectionBuilder()
            .withUrl("http://localhost:7071/api")
            .configureLogging(LogLevel.Information)
            .build()

    connection.on("newMessage", test)
    
    connection.start()
    

let operatorConnection initial = 
    let sub dispatch =
        //Handles push messages from the server and relays them into Elmish messages.
        let onLiveOperatorMessage msg =
            // let msg = msg.data |> decode<{| Payload : string |}>
            // msg.Payload
            // |> decode<WebSocketServerMessage>
            // |> ReceivedFromServer
            msg
            |> Msg.Remote
            |> dispatch

        // Naive web socket connection management
        //let ws = WebSocket.Create "ws://localhost:8085/channel"
        //ws.onmessage <- onLiveOperatorMessage

        let connection = 
            MyConnectionBuilder()
                .withUrl("http://localhost:7071/api")
                .configureLogging(LogLevel.Information)
                .build()
        connection.on("newMessage", onLiveOperatorMessage)
        connection.start() |> ignore
    Cmd.ofSub sub
