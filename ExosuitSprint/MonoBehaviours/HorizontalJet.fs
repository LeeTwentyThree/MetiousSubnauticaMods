namespace ExosuitSprint

open System.IO
open System.Reflection
open ExosuitSprint
open UnityEngine

type HorizontalJet () =
    inherit MonoBehaviour()
    
    let FModAsset path =
        let asset = ScriptableObject.CreateInstance<FMODAsset>()
        asset.path <- path
        asset
        
    static let _bundle = AssetBundle.LoadFromFile(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Assets", "exosuitsprint"))
    static let mutable _speedFXPrefab: GameObject = null
    
    
    let mutable _horizontalJetActive = false
    let mutable _verticalJetActive = false
    let mutable _lastMovementDirection: Vector3 = Vector3.zero
    let mutable _exosuit: Exosuit = null
    
    let mutable _speedFX: GameObject = null
    
    let mutable _boostSound: FMOD_CustomLoopingEmitter = null
    
    static member Bundle with get() = _bundle
    member this.HorizontalJetActive with get() = _horizontalJetActive
    member this.VerticalJetActive with get() = _verticalJetActive
    member this.IsGround with get() = _exosuit.onGround
    
    member private this.Awake() =
        if _speedFXPrefab.Null() then
            _speedFXPrefab <- _bundle.LoadAsset<GameObject>("ScreenSpeedFX")
        this.InitFX(_speedFXPrefab)
        _exosuit <- this.GetComponent<Exosuit>()
        _boostSound <- this.gameObject.AddComponent<FMOD_CustomLoopingEmitter>()
        _boostSound.SetAsset(FModAsset "exosuit_boost_loop")
        _boostSound.assetStart <- FModAsset "exosuit_boost_start"
        _boostSound.assetStop <- FModAsset "exosuit_boost_stop"
        
    member private this.OnEnable() =
        _exosuit.hasInitStrings <- false
        
    member private this.Update() =
        _lastMovementDirection <- if AvatarInputHandler.main.IsEnabled() then GameInput.GetMoveDirection() else Vector3.zero
        let sprintHeld = GameInput.GetButtonHeld(GameInput.Button.Sprint)
        if (_lastMovementDirection.y > 0.0f || sprintHeld) && _exosuit.IsPowered() && _exosuit.liveMixin.IsAlive() then
            _horizontalJetActive <- sprintHeld
            _verticalJetActive <- _lastMovementDirection.y > 0.0f
            
            _exosuit.thrustPower <- Mathf.Clamp01(_exosuit.thrustPower - Time.deltaTime * _exosuit.thrustConsumption)
        else
            _horizontalJetActive <- false
            _verticalJetActive <- false
            let multiplier =
                if _exosuit.onGround then 4.0f
                elif not(_lastMovementDirection.x = 0.0f) || not(_lastMovementDirection.z = 0.0f) then -0.7f
                else 0.7f
            _exosuit.thrustPower <- Mathf.Clamp01(_exosuit.thrustPower + Time.deltaTime * _exosuit.thrustConsumption * multiplier)
            
        this.UpdateSounds()
            
    member private this.FixedUpdate() =
        if _exosuit.IsUnderwater() && _exosuit.thrustPower > 0.0f then
            if _horizontalJetActive then
                let multiplier = 0.8f + _exosuit.thrustPower * 0.2f
                let normalizedDir =
                    _lastMovementDirection.WithY(0.0f)
                    |> (fun v -> if v.sqrMagnitude > 0.0f then this.transform.TransformDirection(v).normalized else this.transform.forward)
                    |> (fun v -> if _verticalJetActive then v else v + Vector3.up * Mathf.Clamp(MainCamera.camera.transform.forward.y, -0.75f, 0.75f))
                    |> (fun v -> if _exosuit.jumpJetsUpgraded then v * 7.5f else v * 5.5f)
                    
                _exosuit.useRigidbody.AddForce(normalizedDir * multiplier, ForceMode.Acceleration)
                "Applying boost" |> Logging.Log
                    
            
    member private this.OnDisable() =
        _exosuit.hasInitStrings <- false
        
    member private this.OnDestroy() =
        Object.Destroy(_boostSound)
        
    member private this.UpdateSounds() =
        let jetActive = this.HorizontalJetActive && _exosuit.thrustPower > 0.0f
        match jetActive with
        | true -> if not _boostSound.playing then _boostSound.Play()
        | false -> if _boostSound.playing then _boostSound.Stop()
        
    member private this.InitFX(speedFXPrefab: GameObject) =
        if speedFXPrefab.Null() then
            nullArg "speedFX"
            
        if _speedFX.Null() then
            _speedFX <- Object.Instantiate<GameObject>(_speedFXPrefab, this.transform)
            _speedFX.transform.localPosition <- Vector3.zero.WithY(0.55f);
            _speedFX.GetComponentInChildren<Renderer>().materials
            |> Seq.iter (fun m -> m.shader <- Shader.Find("UWE/Particles/UBER"))