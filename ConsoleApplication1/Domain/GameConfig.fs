namespace Domain

[<RequireQualifiedAccess>]
module GameConfig =

    open Utils.Array2DExtensions
    open Types
    open CellState

    let private value (GameConfig config) = config
        
    let tryGet index config =
        tryGet index (value config)
    
    let map mapping config = 
        GameConfig (Array2D.mapi mapping <| value config)

    let length config = 
        let arr = value config
        (Array2D.length1 arr, Array2D.length2 arr)

    let asSeq config = 
        let arr = value config
        asSeq arr

    let aliveExisits config = 
        config
        |> asSeq
        |> Seq.exists (fun (a,cell) -> 
                        match cell with
                        | Some c -> isAlive c
                        | _ -> false)
