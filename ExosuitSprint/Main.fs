namespace ExosuitSprint

open ExosuitSprint
open Microsoft.FSharp.Quotations.Patterns

module Main =
    let getModuleType() = function
    | PropertyGet (_, propertyInfo, _) -> propertyInfo.DeclaringType
    | _ -> failwith "Failed getting the declared type"
    
    let boostModule = BoostModule("BoostModule", "Boost Module", "Boosts the horizontal velocity of the Prawn Suit.")
     
    