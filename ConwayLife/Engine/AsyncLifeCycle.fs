namespace Engine

module AsyncLifeSycle =
    open Rule
    open Domain.Types

    let conwayLoop cfg = async {
      let rec start config prevConfigs = async {
        let next = computeNextStep config
        match checkFinish next prevConfigs with
        | Option.None -> 
            return Item(next, start next (next::prevConfigs))
        | Option.Some reason -> return Ended reason
      }

      return! start cfg []
    }
