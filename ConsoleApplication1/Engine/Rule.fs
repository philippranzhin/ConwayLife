namespace Engine

module Rule =
    open Domain
    open Domain.Types

    let private handleCell index cell config = 
        let aliveCount = 
            Helper.getNeighbors index config
            |> Helper.countOfAlive
        
        match cell with
        | CellState.Alive -> 
                    let continueLife =
                        aliveCount >= Constants.MinLivingNeighborsToContinueLife &&
                        aliveCount <= Constants.MaxLivingNeighborsToContinueLife
                    match continueLife with
                    | true -> CellState.Alive
                    | false -> CellState.Dead
        | CellState.Dead ->
                    match aliveCount with
                    | Constants.LivingNeighborsForOriginLife -> CellState.Alive
                    | _ -> CellState.Dead
            
    
    let checkFinish config (prevConfigs: GameConfig list) = 
        if not <| GameConfig.aliveExisits config then Some FinishReason.NoLiveCells
        elif (not <| List.isEmpty prevConfigs) && (List.head prevConfigs) = config then Some FinishReason.StateNotChanged
        // elif List.exists (fun c -> c = config) prevConfigs then Some FinishReason.StateAlreadyBeen
        else None


    let computeNextStep (config: GameConfig) =
       GameConfig.map (fun i j sell -> handleCell (i, j) sell config) config
