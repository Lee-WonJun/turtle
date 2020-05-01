module TurtleAPI

open Common.Common
open Turtle
open TurtleException
 
/// Function to log a message
let log message =
    printfn "%s" message 

type TurtleApi() =

    let turtle = Turtle(log)

    member this.Exec (commandStr:string) = 
        let tokens = commandStr.Split(' ') |> List.ofArray |> List.map trimString
        match tokens with
        | [ "Move"; distanceStr ] -> 
            let distance = validateDistance distanceStr 
            turtle.Move distance 
        | [ "Turn"; angleStr ] -> 
            let angle = validateAngle angleStr
            turtle.Turn angle  
        | [ "Pen"; "Up" ] -> 
            turtle.PenUp()
        | [ "Pen"; "Down" ] -> 
            turtle.PenDown()
        | [ "SetColor"; colorStr ] -> 
            let color = validateColor colorStr 
            turtle.SetColor color
        | _ -> 
            let msg = sprintf "Instruction '%s' is not recognized" commandStr
            raise (TurtleApiException msg)

