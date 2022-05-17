namespace ExosuitSprint

open System.Collections
open System.IO
open System.Reflection
open SMLHelper.V2.Assets
open SMLHelper.V2.Crafting
open UnityEngine

type public BoostModule =
    inherit Equipable
    
    member private this.PostPatch() =
        if uGUI.isMainLevel && not uGUI.isLoading then
            // To make in-game patching unlock it correctly
            if KnownTech.Contains(TechType.Workbench) then KnownTech.Add(this.TechType) |> ignore
            if CraftTree.initialized then CraftTree.initialized <- false
            Language.main.strings.["Tooltip_" + this.ClassID] <- this.Description
            // Removes or adds the module's prefab on runtime
            CraftData.RebuildDatabase()
            
    
    new(classId, displayName, description) as this = {inherit Equipable(classId, displayName, description)} then
        this.OnFinishedPatching <- Spawnable.PatchEvent(this.PostPatch)
    
    override this.GetBlueprintRecipe() =
        TechData(
            craftAmount = 1,
            Ingredients = ( [Ingredient(TechType.Sulphur, 2); Ingredient(TechType.Titanium, 5); Ingredient(TechType.Lithium, 1) ] |> ResizeArray ))
        
    override this.GetGameObject() =
        Object.Instantiate<GameObject>(CraftData.GetPrefabForTechType(TechType.ExosuitThermalReactorModule))
        
    override this.GetGameObjectAsync(gameObject: IOut<GameObject>) =
        seq {
           let task = CraftData.GetPrefabForTechTypeAsync(TechType.ExosuitJetUpgradeModule)
           yield task
           let obj = Object.Instantiate<GameObject>(task.GetResult())
           obj |> gameObject.Set
        } :?> IEnumerator
        
    override this.EquipmentType = EquipmentType.VehicleModule
    override this.RequiredForUnlock = TechType.Workbench;
    override this.CategoryForPDA = TechCategory.Workbench;
    override this.GroupForPDA = TechGroup.Workbench;
    override this.FabricatorType = CraftTree.Type.Workbench;
    override this.StepsToFabricatorTab = [|"ExosuitMenu"|];
    override this.AssetsFolder = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Assets");