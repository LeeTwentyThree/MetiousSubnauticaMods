using System.IO;
using System.Linq;
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
        private Color _savedColor = Color.white;
        private Light _light;
        private Renderer[] _renderers;
        private Renderer _particleRenderer;

        private bool _dirty;
        
        private void Start()
        {
            _light = GetComponentInChildren<Light>(true);
            _renderers = transform.GetChild(0).GetComponentsInChildren<Renderer>(true);
            _particleRenderer = _renderers.First(r => r.material.shader.name.Contains("Particle"));
            
            PrefabIdentifier prefabIdentifier = GetComponentInParent<PrefabIdentifier>();
            if ((prefabIdentifier ??= GetComponent<PrefabIdentifier>()) is null)
                return;
            
            string saveFolder = Main.GetSaveFolderPath();
            string filePath = Main.Combine(saveFolder, prefabIdentifier.Id + ".json");
            if (File.Exists(filePath))
            {
                ColorInfo colorInfo;
                using (StreamReader reader = new StreamReader(filePath))
                {
                    var serializer = new JsonSerializer();
                    colorInfo = serializer.Deserialize(reader, typeof(ColorInfo)) as ColorInfo;
                }
                
                if (colorInfo != null)
                {
                    _savedColor = new Color(colorInfo.RedLevel, colorInfo.GreenLevel, colorInfo.BlueLevel);
                    _dirty = true;
                    return;
                }
                Logger.Log(Logger.Level.Warn,"colorInfo is null");
            }
        }

        private void LateUpdate()
        {
            if (_dirty)
            {
                _light.color = new Color(_savedColor.r, _savedColor.g, _savedColor.b);
                foreach (var renderer in _renderers)
                {
                    foreach (var material in renderer.materials)
                    {
                        material.SetColor(ShaderPropertyID._GlowColor, _savedColor);
                    }

                    _particleRenderer.material.color = new Color(_savedColor.r, _savedColor.g, _savedColor.b, 0.45f);
                }

                _dirty = false;
            }
        }

        public void OnHandClick(GUIHand hand) {}

        public void OnHandHover(GUIHand hand)
        {
            if (!enabled)
                return;
            
            if (_light == null)
            {
                Logger.Log(Logger.Level.Error, "Light Component is missing!");
                return;
            }

            var reticle = HandReticle.main;
            reticle.SetIcon(HandReticle.IconType.Hand, 1f);

            if (Input.GetKeyDown(KeyCode.R))
            {
                if (_light.color.r >= 1.0f)
                    _light.color = new Color(0f, _light.color.g, _light.color.b);
                else
                    _light.color = new Color(_light.color.r + 0.1f, _light.color.g, _light.color.b);

                _savedColor.r = _light.color.r;
                _dirty = true;
                ErrorMessage.AddDebug($"Spotlight: Red levels updated ({_light.color.r:0.00}/1)");
            }
            else if (Input.GetKeyDown(KeyCode.G))
            {
                if (_light.color.g >= 1.0f)
                    _light.color = new Color(_light.color.r, 0f, _light.color.b);
                else
                    _light.color = new Color(_light.color.r, _light.color.g + 0.1f, _light.color.b);

                _savedColor.g = _light.color.g;
                _dirty = true;
                ErrorMessage.AddDebug($"Spotlight: Green levels updated ({_light.color.g:0.00}/1)");
            }
            else if (Input.GetKeyDown(KeyCode.B))
            {
                if (_light.color.b >= 1.0f)
                    _light.color = new Color(_light.color.r, _light.color.g, 0f);
                else
                    _light.color = new Color(_light.color.r, _light.color.g, _light.color.b + 0.1f);

                _savedColor.b = _light.color.b;
                _dirty = true;
                ErrorMessage.AddDebug($"Spotlight: Blue levels updated ({_light.color.b:0.00}/1)");
            }
            
            reticle.SetInteractText($"Press \"R\" to change the Red Levels, current red Level: {_light.color.r:0.00}\nPress \"G\" to change the Green Levels, current Green Level: {_light.color.g:0.00}\nPress \"B\" to change the Blue Levels, current Blue level: {_light.color.b:0.00}");
        }
        public void OnProtoSerialize(ProtobufSerializer serializer)
        {
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
                _savedColor = new Color(colorInfo.RedLevel, colorInfo.GreenLevel, colorInfo.BlueLevel);
            }
        }

        public void OnProtoDeserialize(ProtobufSerializer serializer)
        {
        }
    }
}
