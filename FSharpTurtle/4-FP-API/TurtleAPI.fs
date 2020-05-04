module TurtleAPI

open Common.Common
open Common.Result
open Turtle
open TurtleError

type TurtleApi() =

    let move = Turtle.move log
    let turn = Turtle.turn log
    let penDown = Turtle.penDown log
    let penUp = Turtle.penUp log
    let setColor = Turtle.setColor log

    let mutable state = initialTurtleState

    /// Update the mutable state value
    let updateState newState =
        state <- newState

    /// Execute the command string, and return a Result
    /// Exec : commandStr:string -> Result<unit,ErrorMessage>
    member this.Exec (commandStr:string) = 
        let tokens = commandStr.Split(' ') |> List.ofArray |> List.map trimString
    
        // lift current state to Result
        let stateR = returnR state
    
        // calculate the new state
        let newStateR = 
            match tokens with
            | [ "Move"; distanceStr ] -> 
                // get the distance as a Result
                let distanceR = validateDistance distanceStr 
    
                // call "move" lifted to the world of Results
                lift2R move distanceR stateR
    
            | [ "Turn"; angleStr ] -> 
                let angleR = validateAngle angleStr 
                lift2R turn angleR stateR
    
            | [ "Pen"; "Up" ] -> 
                returnR (penUp state)
    
            | [ "Pen"; "Down" ] -> 
                returnR (penDown state)
    
            | [ "SetColor"; colorStr ] -> 
                let colorR = validateColor colorStr
                lift2R setColor colorR stateR
    
            | _ -> 
                Error (InvalidCommand commandStr)
    
        // Lift `updateState` into the world of Results and
        // call it with the new state.
        mapR updateState newStateR
    
        // Return the final result (output of updateState)
    
    

