module Main

open Domain.Types
open Persistence.SampleData
open Domain
open Components
open Engine

[<EntryPoint>]
let main argv = 
    let loop = AsyncLifeSycle.conwayLoop(GameConfig (glider Constants.GliderSizeX Constants.GliderSizeY))
    let app = new App(loop)
    app.Run()
    0
