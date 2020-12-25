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
				var prefab = __instance.quitToMainMenuButton.transform.parent.GetChild(0).gameObject.GetComponent<Button>();
				quitButton = GameObject.Instantiate(prefab, __instance.quitToMainMenuButton.transform.parent);
				quitButton.name = "ButtonQuitToDesktop";
				quitButton.onClick.RemoveAllListeners();
				quitButton.onClick.AddListener(() => { __instance.QuitGame(true); });

				IEnumerable<TextMeshProUGUI> texts = quitButton.GetComponents<TextMeshProUGUI>().Concat(quitButton.GetComponentsInChildren<TextMeshProUGUI>());
				foreach (TextMeshProUGUI text in texts)
				{
					text.text = "Quit to Desktop";
				}

				texts = __instance.quitToMainMenuButton.GetComponents<TextMeshProUGUI>().Concat(__instance.quitToMainMenuButton.GetComponentsInChildren<TextMeshProUGUI>());
				foreach (TextMeshProUGUI text in texts)
				{
					text.text = "Quit to Main Menu";
				}
			}
		}
    }
}
