// Learn more about F# at http://fsharp.org

open System
open Common.Common

module Turtle =
    type TurtleState = {
        position : Position
        angle : float<Degrees>
        color : PenColor
        penState : PenState
    }

    let initialTurtleState = {
           position = initialPosition
           angle = 0.0<Degrees>
           color = initialColor
           penState = initialPenState
    }
    
    let move log distance state =
           log (sprintf "Move %0.1f" distance)
           // calculate new position
           let newPosition = calcNewPosition distance state.angle state.position 
           // draw line if needed
           if state.penState = Down then
               dummyDrawLine log state.position newPosition state.color
           // update the state
           {state with position = newPosition}

    let turn log angle state =
        log (sprintf "Turn %0.1f" angle)
        // calculate new angle
        let newAngle = (state.angle + angle) % 360.0<Degrees>
        // update the state
        {state with angle = newAngle}

    let penUp log state =
        log "Pen up" 
        {state with penState = Up}

    let penDown log state =
        log "Pen down" 
        {state with penState = Down}

    let setColor log color state =
        log (sprintf "SetColor %A" color)
        {state with color = color}


/// Function to log a message
let log message =
    printfn "%s" message 

// versions with log baked in (via partial application)
let move = Turtle.move log
let turn = Turtle.turn log
let penDown = Turtle.penDown log
let penUp = Turtle.penUp log
let setColor = Turtle.setColor log

let drawTriangle() = 
    Turtle.initialTurtleState
    |> move 100.0 
    |> turn 120.0<Degrees>
    |> move 100.0 
    |> turn 120.0<Degrees>
    |> move 100.0 
    |> turn 120.0<Degrees>

let drawPolygon n = 
    let angle = 180.0 - (360.0/float n) 
    let angleDegrees = angle * 1.0<Degrees>

    // define a function that draws one side
    let oneSide state sideNumber = 
        state
        |> move 100.0 
        |> turn angleDegrees 

    // repeat for all sides
    [1..n] 
    |> List.fold oneSide Turtle.initialTurtleState


[<EntryPoint>]
let main argv =
    drawPolygon 5
    0 // return an integer exit code
