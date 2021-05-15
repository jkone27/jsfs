open System
open Modules


[<EntryPoint>]
let main argv =
    
    let petName = PetsClient.getNameByIdAsync(1).GetAwaiter().GetResult()

    printfn "Hello world %s" petName
    0 // return an integer exit code