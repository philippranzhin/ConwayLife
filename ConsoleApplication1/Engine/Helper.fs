namespace Engine

module Helper =
    open Domain
    open Domain.Types


    let private prev point range =
        let i = point - 1
        if i < 0 then range
        else i

    let next point range =
        let i = point + 1
        if i > range then 0
        else i

    let private neighborIndexes index config =
        let i, j = index
        let l1, l2 = GameConfig.length config
        let prevI = prev i (l1-1)
        let prevJ = prev j (l2-1)

        let nextI = next i (l1-1)
        let nextJ = next j (l2-1)

        [
            prevI, prevJ; 
            prevI, j;
            prevI, nextJ;
            i, prevJ;
            i, nextJ;
            nextI, prevJ;
            nextI, j;
            nextI, nextJ;
        ]

    let getNeighbors index (config: GameConfig) = 
        let boxNeighborFromIndex (x, y) = (GameConfig.tryGet (x,y) config), x, y

        neighborIndexes index config |> List.map boxNeighborFromIndex

    let countOfAlive (cells: (CellState option*int*int) list) =
        let counter count (item: CellState option*int*int) = 
            let cell,_,_  = item
            match cell with
            | Some c -> 
                match c with
                | CellState.Alive -> count+1
                | _ -> count
            | _ -> count

        List.fold counter 0 cells
