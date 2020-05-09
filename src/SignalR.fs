module SignalR

open System
open Fable.Core
open Fable.Core.JsInterop
open Fable.Core.JS
open Browser

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