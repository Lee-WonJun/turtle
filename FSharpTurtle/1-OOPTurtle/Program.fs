// Learn more about F# at http://fsharp.org

open System
open Common.Common

type Turtle(log) =

    let mutable currentPosition = initialPosition 
    let mutable currentAngle = 0.0<Degrees>
    let mutable currentColor = initialColor
    let mutable currentPenState = initialPenState
    
    member this.Move(distance) =
        log (sprintf "Move %0.1f" distance)

        let newPosition = calcNewPosition distance currentAngle currentPosition 

        if currentPenState = Down then
            dummyDrawLine log currentPosition newPosition currentColor
        currentPosition <- newPosition

    member this.Turn(angle) =
        log (sprintf "Turn %0.1f" angle)
        let newAngle = (currentAngle + angle) % 360.0<Degrees>
        currentAngle <- newAngle 

    member this.PenUp() =
        log "Pen up" 
        currentPenState <- Up

    member this.PenDown() =
        log "Pen down" 
        currentPenState <- Down

    member this.SetColor(color) =
        log (sprintf "SetColor %A" color)
        currentColor <- color


/// Function to log a message
let log message =
    printfn "%s" message 

let drawTriangle() = 
    let turtle = Turtle(log)
    turtle.Move 100.0 
    turtle.Turn 120.0<Degrees>
    turtle.Move 100.0 
    turtle.Turn 120.0<Degrees>
    turtle.Move 100.0
    turtle.Turn 120.0<Degrees>
    // back home at (0,0) with angle 0

let drawPolygon n = 
    let angle = 180.0 - (360.0/float n) 
    let angleDegrees = angle * 1.0<Degrees>
    let turtle = Turtle(log)

    // define a function that draws one side
    let drawOneSide() = 
        turtle.Move 100.0 
        turtle.Turn angleDegrees 

    // repeat for all sides
    for i in [1..n] do
        drawOneSide()

[<EntryPoint>]
let main argv =
    drawPolygon 5
    0 // return an integer exit code
