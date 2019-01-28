namespace Components

open Domain.Types
open System.Windows.Forms

type App(conwaySeq: ConwaySeq) as this =
    inherit Form(WindowState = FormWindowState.Maximized)

    let mutable konwaySeq = conwaySeq
    let updateTimer = new Timer(Interval = Constants.PulseInterval);
    let scene = new Scene()
 
    do 
        this.Controls.Add scene
        updateTimer.Tick 
        |> Observable.add (fun e -> this.Render() |> Async.StartImmediate) 
        updateTimer.Start()

    member this.Run() =
      Application.Run this
    
    member this.Render() = async {
         let! step = konwaySeq
         match step with
         | Item (cfg, next) ->
            scene.Update cfg
            konwaySeq <- next
         | Ended reason ->
            updateTimer.Stop()
            match reason with
            | NoLiveCells -> 
                        MessageBox.Show("All cells dead") |> ignore
            | FinishReason.StateAlreadyBeen ->
                        MessageBox.Show("This config already been") |> ignore
            | FinishReason.StateNotChanged -> 
                        MessageBox.Show("Configuration not changed") |> ignore
            Application.Exit()

    }
