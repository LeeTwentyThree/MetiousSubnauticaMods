namespace ExosuitSprint

open ExosuitSprint
open SMLHelper.V2.Json
open SMLHelper.V2.Options
open SMLHelper.V2.Options.Attributes
open UnityEngine

[<Menu("Prawn Suit Boosting Module")>]
type config () =
    inherit ConfigFile()
    
    
    [<Toggle("Make Module", Tooltip = "Adds the boosting feature into a module.\nIf unchecked, the feature will be available on any Prawn suit.")>]
    [<OnChange("MakeModuleEvent")>]
    member val makeModule = false with get, set
    
    
    member private this.MakeModuleEvent(e: ToggleChangedEventArgs) =
        this.makeModule <- e.Value
        
        let exosuits = Object.FindObjectsOfType<Exosuit>()
        
        if e.Value then
            Main.boostModule.Patch()
            exosuits |> Seq.filter (fun e -> e.modules.GetCount(Main.boostModule.TechType) <= 0)
                     |> Seq.iter (fun e -> Object.Destroy(e.GetComponent<HorizontalJet>()))
            
            "Patched Boost Module" |> ErrorMessage.AddMessage
        else
            exosuits |> Seq.iter (fun e -> e.gameObject.EnsureComponent<HorizontalJet>() |> ignore)
            Main.boostModule.Unpatch()
            "Unpatched Boost Module" |> ErrorMessage.AddMessage