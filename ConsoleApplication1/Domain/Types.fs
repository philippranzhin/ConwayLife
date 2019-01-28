namespace Domain

module Types =
    type CellState = Alive | Dead

    type GameConfig = GameConfig of CellState[,]

    type FinishReason = 
        | StateNotChanged
        | NoLiveCells
        | StateAlreadyBeen

    type StepResult =
        | Continue of GameConfig
        | Finish of FinishReason
  
    type AsyncSeq<'a, 'b> = Async<AsyncSeqInner<'a, 'b>> 
    and AsyncSeqInner<'a, 'b> =
      | Ended of 'b
      | Item of 'a * AsyncSeq<'a, 'b>

    type ConwaySeq = AsyncSeq<GameConfig, FinishReason>
