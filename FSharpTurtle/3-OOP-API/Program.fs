// Learn more about F# at http://fsharp.org

open System
open TurtleAPI

let drawPolygon n = 
    let angle = 180.0 - (360.0/float n) 
    let api = TurtleApi()

    // define a function that draws one side
    let drawOneSide() = 
        api.Exec "Move 100.0"
        api.Exec (sprintf "Turn %f" angle)

    // repeat for all sides
    for i in [1..n] do
        drawOneSide()

let triggerError() = 
    let api = TurtleApi()
    api.Exec "Move bad"

[<EntryPoint>]
let main argv =
    drawPolygon 5 |> ignore
    triggerError()
    0 // return an integer exit code
