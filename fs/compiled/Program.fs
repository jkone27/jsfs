open System
open Modules
open FSharp.Control.Tasks.V2


[<EntryPoint>]
let main argv =
    task {
        let! petName = PetsClient.getNameByIdAsync(1)
        printfn "Hello world %s" petName
    } 
    |> Async.AwaitTask
    |> Async.RunSynchronously
    0
    