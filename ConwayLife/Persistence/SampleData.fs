namespace Persistence

module SampleData =
    open Domain.Types

    let gliderConfig = [(0,1);(1,2);(2,0);(2,1);(2,2)];
    let glider x y = Array2D.init x y (fun i j -> 
                                                match List.contains (i,j) gliderConfig with
                                                | true -> CellState.Alive
                                                | false -> Dead)
