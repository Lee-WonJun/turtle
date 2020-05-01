namespace Common

open System

module Common =
    /// An alias for a float
    type Distance = float
    
    /// Use a unit of measure to make it clear that the angle is in degrees, not radians
    type [<Measure>] Degrees
    
    /// An alias for a float of Degrees
    type Angle  = float<Degrees>
    
    /// Enumeration of available pen states
    type PenState = Up | Down
    
    /// Enumeration of available pen colors
    type PenColor = Black | Red | Blue
    
    /// A structure to store the (x,y) coordinates
    type Position = {x:float; y:float}
    
    // round a float to two places to make it easier to read
    let round2 (flt:float) = Math.Round(flt,2)
    
    /// calculate a new position from the current position given an angle and a distance
    let calcNewPosition (distance:Distance) (angle:Angle) currentPos = 
        // Convert degrees to radians with 180.0 degrees = 1 pi radian
        let angleInRads = angle * (Math.PI/180.0) * 1.0<1/Degrees> 
        // current pos
        let x0 = currentPos.x
        let y0 = currentPos.y
        // new pos
        let x1 = x0 + (distance * cos angleInRads)
        let y1 = y0 + (distance * sin angleInRads)
        // return a new Position
        {x=round2 x1; y=round2 y1}
    
    /// Default initial state
    let initialPosition,initialColor,initialPenState = 
        {x=0.0; y=0.0}, Black, Down
    
    let dummyDrawLine log oldPos newPos color =
        // for now just log it
        log (sprintf "...Draw line from (%0.1f,%0.1f) to (%0.1f,%0.1f) using %A" oldPos.x oldPos.y newPos.x newPos.y color)
    
    /// trim a string 
    let trimString (str:string) = str.Trim()