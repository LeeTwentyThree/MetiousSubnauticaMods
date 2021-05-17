using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using QModManager.Utility;
using System.IO;
using Logger = QModManager.Utility.Logger;
using UnityEngine;
#if SN1
using Oculus.Newtonsoft.Json;
using Oculus.Newtonsoft.Json.Serialization;
#elif BZ
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
#endif
namespace ColorizableSpotlight.MonoBehaviours
{
    public class ColoringController : HandTarget, IHandTarget, IProtoEventListener
    {
        Color savedColor = Color.white;
        public void Start()
        {
            PrefabIdentifier prefabIdentifier = GetComponentInParent<PrefabIdentifier>();
            if ((prefabIdentifier ??= GetComponent<PrefabIdentifier>()) is null)
                return;
            
            ColorInfo colorInfo;
            string saveFolder = Main.GetSaveFolderPath();
            string filePath = Main.Combine(saveFolder, prefabIdentifier.Id + ".json");
            using (StreamReader reader = new StreamReader(filePath))
            {
                var serializer = new JsonSerializer();
                colorInfo = serializer.Deserialize(reader, typeof(ColorInfo)) as ColorInfo;
            }
            if (File.Exists(filePath))
            {
                if (colorInfo != null)
                {
                    savedColor = new Color(colorInfo.RedLevel, colorInfo.GreenLevel, colorInfo.BlueLevel);
                    return;
                }
                Logger.Log(Logger.Level.Warn,"[Spotlight Colorizable] ColorInfo is null");
                return;
            }
            
            Logger.Log(Logger.Level.Warn, "saved file doesn't exist");
        }

        public void Update()
        {
            Light light = this.gameObject.GetComponentInChildren<Light>();
            Renderer[] renderers = this.gameObject.GetComponentsInChildren<Renderer>();

            light.color = new Color(savedColor.r, savedColor.g, savedColor.b);
            foreach (var renderer in renderers)
            {
                foreach (var material in renderer.materials)
                {
                    material.SetColor(ShaderPropertyID._GlowColor, savedColor);
                }
            }
        }

        public void OnHandClick(GUIHand hand)
        {}

        public void OnHandHover(GUIHand hand)
        {
            if (!enabled)
                return;
            
            Light light = this.gameObject.GetComponentInChildren<Light>();
            Renderer[] renderers = this.gameObject.GetComponentsInChildren<Renderer>();
            if (light == null)
            {
                Logger.Log(Logger.Level.Error, "[ColorizableSpotlight] Light Component is missing!");
                return;
            }

            var reticle = HandReticle.main;
            reticle.SetIcon(HandReticle.IconType.Hand, 1f);

            if (Input.GetKeyDown(KeyCode.R))
            {
                if (light.color.r >= 1.0f)
                    light.color = new Color(0f, light.color.g, light.color.b);
                else
                    light.color = new Color(light.color.r + 0.1f, light.color.g, light.color.b);

                savedColor.r = light.color.r;
                ErrorMessage.AddDebug($"Spotlight: Red levels updated ({light.color.r:0.00}/1)");
            }
            else if (Input.GetKeyDown(KeyCode.G))
            {
                if (light.color.g >= 1.0f)
                    light.color = new Color(light.color.r, 0f, light.color.b);
                else
                    light.color = new Color(light.color.r, light.color.g + 0.1f, light.color.b);

                savedColor.g = light.color.g;
                ErrorMessage.AddDebug($"Spotlight: Green levels updated ({light.color.g:0.00}/1)");
            }
            else if (Input.GetKeyDown(KeyCode.B))
            {
                if (light.color.b >= 1.0f)
                    light.color = new Color(light.color.r, light.color.g, 0f);
                else
                    light.color = new Color(light.color.r, light.color.g, light.color.b + 0.1f);

                savedColor.b = light.color.b;
                ErrorMessage.AddDebug($"Spotlight: Blue levels updated ({light.color.b:0.00}/1)");
            }
            foreach (var renderer in renderers)
            {
                foreach (var material in renderer.materials)
                {
                    material.SetColor(ShaderPropertyID._GlowColor, new Color(light.color.r, light.color.g, light.color.b));
                }
            }
            reticle.SetInteractText($"Press \"R\" to change the Red Levels, current red Level: {light.color.r:0.00}\nPress \"G\" to change the Green Levels, current Green Level: {light.color.g:0.00}\nPress \"B\" to change the Blue Levels, current Blue level: {light.color.b:0.00}");
        }
        public void OnProtoSerialize(ProtobufSerializer serializer)
        {
            if (!enabled)
                return;

            PrefabIdentifier prefabIdentifier = GetComponentInParent<PrefabIdentifier>();
            if ((prefabIdentifier ??= GetComponent<PrefabIdentifier>()) is null)
            {
                return; // return if we couldn't find a PrefabIdentifier component anywhere.
            }
            string saveFolder = Main.GetSaveFolderPath();
            if (!Directory.Exists(saveFolder))
                Directory.CreateDirectory(saveFolder);


            Light light = this.gameObject.GetComponentInChildren<Light>();

            ColorInfo colorInfo = new ColorInfo()
            {
                RedLevel = light.color.r,
                GreenLevel = light.color.g,
                BlueLevel = light.color.b,
            };
            if (light != null)
            {
                string filePath = Main.Combine(saveFolder, prefabIdentifier.Id + ".json");
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.Write(JsonConvert.SerializeObject(colorInfo, Formatting.Indented));
                }
                savedColor = new Color(colorInfo.RedLevel, colorInfo.GreenLevel, colorInfo.BlueLevel);
            }
        }

        public void OnProtoDeserialize(ProtobufSerializer serializer)
        {
        }
    }
}
