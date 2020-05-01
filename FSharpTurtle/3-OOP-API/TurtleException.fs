module TurtleException

open Common.Common

exception TurtleApiException of string

// convert the distance parameter to a float, or throw an exception
let validateDistance distanceStr =
    try
        float distanceStr 
    with
    | ex -> 
        let msg = sprintf "Invalid distance '%s' [%s]" distanceStr  ex.Message
        raise (TurtleApiException msg)

// convert the angle parameter to a float<Degrees>, or throw an exception
let validateAngle angleStr =
    try
        (float angleStr) * 1.0<Degrees> 
    with
    | ex -> 
        let msg = sprintf "Invalid angle '%s' [%s]" angleStr ex.Message
        raise (TurtleApiException msg)
        
// convert the color parameter to a PenColor, or throw an exception
let validateColor colorStr =
    match colorStr with
    | "Black" -> Black
    | "Blue" -> Blue
    | "Red" -> Red
    | _ -> 
        let msg = sprintf "Color '%s' is not recognized" colorStr
        raise (TurtleApiException msg)
