using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace QuitToDesktopBZ.Patches
{
    [HarmonyPatch(typeof(IngameMenu))]
    public static class IngameMenu_Patch
    {
        static Button quitButton;

        [HarmonyPostfix]
        [HarmonyPatch(nameof(IngameMenu.Open))]
        public static void Postfix(IngameMenu __instance)
        {
            if (GameModeUtils.IsPermadeath())
                return;
            if (__instance != null && quitButton == null)
            {
                // make a new button
                var prefab = __instance.quitToMainMenuButton.transform.parent.GetChild(0).gameObject.GetComponent<Button>();
                quitButton = GameObject.Instantiate(prefab, __instance.quitToMainMenuButton.transform.parent);
                quitButton.name = "ButtonQuitToDesktop";
                quitButton.onClick.RemoveAllListeners();
                quitButton.onClick.AddListener(() => { __instance.QuitGame(true); });

                IEnumerable<TextMeshProUGUI> texts = quitButton.GetComponents<TextMeshProUGUI>().Concat(quitButton.GetAllComponentsInChildren<TextMeshProUGUI>());

                foreach (var text in texts)
                {
                    text.text = "Quit to Desktop"; // change our Button text to Quit To Desktop
                }

                texts = __instance.quitToMainMenuButton.GetComponents<TextMeshProUGUI>().Concat(__instance.quitToMainMenuButton.GetAllComponentsInChildren<TextMeshProUGUI>());

                foreach (var text in texts)
                {
                    text.text = "Quit to Main Menu"; // change the Quit button text into Quit to Main Menu
                }
            }
        }
    }
}
