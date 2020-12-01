using AbyssBatteries.Items;
using HarmonyLib;

namespace AbyssBatteries.Patches
{
    [HarmonyPatch(typeof(PDAScanner), nameof(PDAScanner.Unlock))]
    public static class PDAScanner_Unlock_Patch
    {
        [HarmonyPrefix]
        public static bool Prefix(ref PDAScanner.EntryData entryData, ref bool unlockBlueprint)
        {
            if (entryData.key == TechType.GhostLeviathan)
            {
                unlockBlueprint = true;
                entryData.blueprint = GhostPiece.TechTypeID;
                string key = "EncyDesc_GhostLeviathan";
                #region Garbage
                #region intro
                string intro = "This creature is approaching the size limit for sustainable organic lifeforms, and has been designated leviathan class. Adults of the species have been encountered exclusively around the edges of the volcanic crater which supports life on this part of the planet, and react with extreme aggression on approach.";
                #endregion
                #region first Part
                string firstPart = "\n\n1. Hammerhead:\nCartilaginous extensions of the creature's skull form a hammerhead which protects the ghost leviathan as it performs devastating ramming attacks.";
                #endregion
                #region second Part
                string secondPart = "\n\n2. Jaws:\nWhile fully capable of tearing through the flesh of any creature in range, all evidence indicates that mature ghost leviathans feed on microscopic lifeforms in the waters around the edges of the inhabited zone. Their vicious attacks on interlopers to their domain are not predatory in nature, but territorial. A creature so vast requires a huge expanse of water to satisfy its daily calorie requirements.";
                #endregion
                #region third Part
                string thirdPart = "\n\n3. Torso:\nIts muscled interior body is surrounded by a translucent outer membrane, suggesting adaptation for deep, low-light environments. When threatened it can tense its entire body before lashing out with incredible speed.";
                #endregion
                #region fourth Part
                string fourthPart = "\n\n4. Lifecycle:\nProbable migratory behavior. This specimen was likely born far from the area where it was encountered.";
                #endregion
                #region fifth Part
                string fifthPart = "\n\n5. Nodes: \nMysterious electrical nodes line its entire body, surging with an unknown orange energy. Possible applications for high-end battery crafting detected.";
                #endregion
                #region outro
                string outro = "\n\nAssessment: Extreme threat - Avoid the crater edge";
                #endregion
                #endregion
                if (Language.main.currentLanguage == "English")
                {
                    Language.main.strings[key] = intro + firstPart + secondPart + thirdPart + fourthPart + fifthPart + outro;
                }
            }
            else if (entryData.key == TechType.GhostLeviathanJuvenile)
            {
                unlockBlueprint = true;
                entryData.blueprint = GhostPiece.TechTypeID;
                string key = "EncyDesc_GhostLeviathanJuvenile";
                #region Garbage
                #region intro
                string intro = "This large predator has adapted to live in deep waters and dark cave systems, attacking anything and everything in its quest to grow larger.";
                #endregion
                #region first Part
                string firstPart = "\n\n1. Torso:\nSoft outer membrane and elongated body enable superior navigation of tight cave environments. Displays some similarities to other eel-like predators in the area, however the ghost leviathan has covered over the electrical prongs on its inner torso with a taut, transparent membrane which delivers superior maneuverability.";
                #endregion
                #region second Part
                string secondPart = "\n\n2. Diet:\nIn its juvenile state this leviathan feeds on larger herbivores, and unfortunate members of its own species. They display a remarkable rate of growth which shows no signs of stopping, suggesting that they must abandon their hatching grounds before they grow too large and make for more open waters.";
                #endregion
                #region third Part
                string thirdPart = "\n\n3. Nodes: \nMysterious electrical nodes line its entire body, surging with an unknown orange energy. Possible applications for high-end battery crafting detected.";
                #endregion
                #region outro
                string outro = "\n\nAssessment: Avoid";
                #endregion
                #endregion
                if (Language.main.currentLanguage == "English")
                {
                    Language.main.strings[key] = intro + firstPart + secondPart + thirdPart + outro;
                }
            }
            return true;
        }
    }
}
