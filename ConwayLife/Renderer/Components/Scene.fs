namespace Components

open Domain
open Domain.Types
open System.Windows.Forms
open System.Drawing

type Scene() as this =
    inherit PictureBox()
    
    let mutable config: GameConfig Option = None
    do
        this.Dock <- DockStyle.Fill
        this.Paint.Add (fun s -> this.Render s.Graphics)

    member this.Update (cfg: GameConfig) =
        config <- Some cfg
        this.Refresh()


    member private this.Render (graphics: Graphics) =
        match config with
        | None -> ignore()
        | Some cfg ->
            let x, y = GameConfig.length cfg
            let koeffI = this.Width/x
            let koeffJ = this.Height/y
            cfg
            |> GameConfig.map (fun i j state -> 
                      match state with
                      | Alive -> 
                            use brush = new SolidBrush(Styles.PointColor)
                            this.DrawPoint graphics brush (i*koeffI,j*koeffJ) (float32 <| (koeffI+koeffJ)/4)
                            state
                      | Dead -> state
                      ) 
            |> ignore
        
    member private this.DrawPoint (graphics: Graphics) (brush: Brush) (location: int*int) (radius: float32) =
        let x,y = location
        graphics.FillEllipse(brush, float32(x), float32(y), radius, radius)
