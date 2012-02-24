#light

module Travel

open System
open System.Collections.Generic

[<Measure>] type ft
[<Measure>] type m

type Distance =
  | English of float<ft>
  | Metric of float<m>

type Direction = 
    | North of Distance
    | South of Distance
    | East of Distance
    | West of Distance

type Navigation = 
    | Vector of Direction
    | History of IDictionary<DateTime, Direction>
    | Path of IList<Direction>

let SampleDistance = 
    Metric(10.0<m>)

let SampleDirection = 
    North(SampleDistance)

let SampleVector = 
    Vector(SampleDirection)

let SampleHistory = 
    History(Map<DateTime,Direction>([(DateTime.Now, SampleDirection)]))

let SamplePath = 
    Path([|SampleDirection|] :> IList<Direction>)
