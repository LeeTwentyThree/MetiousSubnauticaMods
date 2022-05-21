[<QModManager.API.ModLoading.QModCore>]
module ExosuitSprint.QPatch

open System.IO
open System.Reflection
open FMOD
open HarmonyLib
open QModManager.API.ModLoading
open SMLHelper.V2.Handlers
open SMLHelper.V2.Utility
open UnityEngine

let config = OptionsPanelHandler.Main.RegisterModOptions<config>()

let MakeSound (bundle: AssetBundle, name, loop) =
    let sound = CustomSoundHandler.RegisterCustomSound(name, bundle.LoadAsset<AudioClip>(name), AudioUtils.BusPaths.PlayerSFXs)
    let modes =
        match loop with
        | true -> MODE.DEFAULT ||| MODE._2D ||| MODE.ACCURATETIME ||| MODE.LOOP_NORMAL
        | false -> MODE.DEFAULT ||| MODE._2D ||| MODE.ACCURATETIME
    sound.setMode(modes) |> ignore
    sound
    
[<QModPatch>]
let Load () =
    config.Load()
    if config.makeModule then Main.boostModule.Patch()
    let assetBundle = HorizontalJet.Bundle
    MakeSound(assetBundle, "exosuit_boost_start", false) |> ignore
    MakeSound(assetBundle, "exosuit_boost_stop", false) |> ignore
    MakeSound(assetBundle, "exosuit_boost_loop", true) |> ignore
    Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), "Metious_ExosuitSprint") |> ignore