module Connection

open Fable.Core.JsInterop
open Fable.Import
open Fable.Core
open Elmish

type IConnection =
    abstract initConnection : unit -> unit
    abstract newMessageHandler : 'a -> unit

[<Import("*", "./Connection.js")>]
let myLib: IConnection = jsNative

let initialize () = 
    do myLib.initConnection()