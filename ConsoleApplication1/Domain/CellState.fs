namespace Domain

module CellState =
    open Types

    let isAlive state = 
        match state with
        | Alive -> true
        | _ -> false
