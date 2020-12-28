using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
namespace QuitToDesktopBZ.Patches
{
    [HarmonyPatch(typeof(IngameMenu))]
    public static class IngameMenu_Patch
    {
        static Button quitButton;
        static GameObject quitConfirmation;

        [HarmonyPostfix]
        [HarmonyPatch(nameof(IngameMenu.Open))]
        public static void Postfix(IngameMenu __instance)
        {
            if (GameModeUtils.IsPermadeath())
                return;
            if (__instance != null && quitButton == null)
            {
                // make a new Confirmation Menu 
                var confirmationMenuPrefab = __instance.gameObject.FindChild("QuitConfirmation");
                quitConfirmation = GameObject.Instantiate(confirmationMenuPrefab, __instance.gameObject.FindChild("QuitConfirmation").transform.parent);
                quitConfirmation.name = "QuitToDesktopConfirmation";

                // get the no button from the confirmation menu and add the needed listener
                var noButtonPrefab = quitConfirmation.gameObject.transform.Find("ButtonNo").GetComponent<Button>();
                noButtonPrefab.onClick.RemoveAllListeners();
                noButtonPrefab.onClick.AddListener(() => { __instance.Close(); });

                // get the yes button from the confirmation menu and add the needed listener
                var yesButtonPrefab = quitConfirmation.gameObject.transform.Find("ButtonYes").GetComponent<Button>();
                yesButtonPrefab.onClick.RemoveAllListeners();
                yesButtonPrefab.onClick.AddListener(() => { __instance.QuitGame(true); });


                // make a new button
                var buttonPrefab = __instance.quitToMainMenuButton.gameObject.GetComponent<Button>();
                quitButton = GameObject.Instantiate(buttonPrefab, __instance.quitToMainMenuButton.transform.parent);
                quitButton.name = "ButtonQuitToDesktop";
                quitButton.onClick.RemoveAllListeners();
                quitButton.onClick.AddListener(() => { __instance.gameObject.FindChild("QuitConfirmationWithSaveWarning").SetActive(false); });
                quitButton.onClick.AddListener(() => { __instance.gameObject.FindChild("QuitConfirmation").SetActive(false); });
                quitButton.onClick.AddListener(() => { quitConfirmation.SetActive(true); });

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
