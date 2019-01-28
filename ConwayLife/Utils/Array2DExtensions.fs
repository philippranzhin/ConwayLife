namespace Utils

module Array2DExtensions = 
    let contains index arr =
        let i1, i2 = index
        let length1 = Array2D.length1 arr
        let length2 = Array2D.length2 arr

        i1 >= 0 && i1 < length1 && i2 >= 0 && i2 < length2

    let tryGet index arr =
        let i1, i2 = index
        match contains index arr with
        | true -> Some arr.[i1, i2]
        | false -> None

    let asSeq arr = 
        let l1 = Array2D.length1 arr
        let l2 = Array2D.length2 arr

        seq {
            for i in 0..l1 do
                for j in 0..l2 do
                    yield ((i, j),tryGet (i, j) arr)
        }
