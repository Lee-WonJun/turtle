// Learn more about F# at http://fsharp.org

open System
open TurtleAPI
open Common.Result

let drawTriangle() = 
    let api = TurtleApi()
    result {
        do! api.Exec "Move 100"
        do! api.Exec "Turn 120"
        do! api.Exec "Move 100"
        do! api.Exec "Turn 120"
        do! api.Exec "Move 100"
        do! api.Exec "Turn 120"
        }
    // back home at (0,0) with angle 0
        
let drawThreeLines() = 
    let api = TurtleApi()
    result {

    // draw black line 
    do! api.Exec "Pen Down"
    do! api.Exec "SetColor Black"
    do! api.Exec "Move 100"
    // move without drawing
    do! api.Exec "Pen Up"
    do! api.Exec "Turn 90"
    do! api.Exec "Move 100"
    do! api.Exec "Turn 90"
    // draw red line 
    do! api.Exec "Pen Down"
    do! api.Exec "SetColor Red"
    do! api.Exec "Move 100"
    // move without drawing
    do! api.Exec "Pen Up"
    do! api.Exec "Turn 90"
    do! api.Exec "Move 100"
    do! api.Exec "Turn 90"
    // back home at (0,0) with angle 0
    // draw diagonal blue line 
    do! api.Exec "Pen Down"
    do! api.Exec "SetColor Blue"
    do! api.Exec "Turn 45"
    do! api.Exec "Move 100"
    }

let drawPolygon n = 
    let angle = 180.0 - (360.0/float n) 
    let api = TurtleApi()

    // define a function that draws one side
    let drawOneSide() = result {
        do! api.Exec "Move 100.0"
        do! api.Exec (sprintf "Turn %f" angle)
        }

    // repeat for all sides
    result {
        for i in [1..n] do
            do! drawOneSide() 
    }

let triggerError() = 
    let api = TurtleApi()
    api.Exec "Move bad"

[<EntryPoint>]
let main argv =
    drawPolygon 5 |> ignore
    0 // return an integer exit code
