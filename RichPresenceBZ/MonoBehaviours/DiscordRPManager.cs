using System;

namespace RichPresenceBZ.MonoBehaviours
{
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using DiscordGameSDK;
    using static Constants;
    
    public class DiscordRPManager : MonoBehaviour
    {
        #region Static
        private static string _sceneName;
        private static bool _initialized;

        public static void Initialize()
        {
            SceneManager.sceneLoaded += (scene, _) =>
            {
                _sceneName = scene.name;
                
                if (_initialized)
                    return;
                
                var rpManager = new GameObject("DiscordRPManager", typeof(DiscordRPManager), typeof(SceneCleanerPreserve));
                DontDestroyOnLoad(rpManager);
                _initialized = true;
            };
        }
        #endregion

        private Discord _client;
        private ActivityManager _activityManager;
        private Activity _activity;
        
        private PlayerState _state;


        private void Awake()
        {
            _client = new Discord(904891494375239710, (ulong)CreateFlags.Default);
            _activityManager = _client.GetActivityManager();
            _activity.Assets = new ActivityAssets();
            var currentEpochTime = DateTime.UtcNow - new DateTime(1970, 1, 1);
            _activity.Timestamps = new ActivityTimestamps
            {
                Start = (long)currentEpochTime.TotalSeconds
            };
        }

        private void Update()
        {
            UpdateState();
            UpdateActivity();
            _client.RunCallbacks();
        }

        private void UpdateActivity()
        {
            if (_state is not PlayerState.Playing)
            {
                _activity.Details = _state == PlayerState.Menu ? "In Menu" : "Loading";
                _activity.State = string.Empty;
                _activity.Assets.LargeImage = kImageBelowZeroMain;
                _activity.Assets.SmallImage = string.Empty;
                _activity.Assets.SmallText = string.Empty;
                
                _activityManager.UpdateActivity(_activity, result =>
                {
                    if (result is not Result.Ok)
                    {
                        QModManager.Utility.Logger.Log(QModManager.Utility.Logger.Level.Error, $"Update Activity failed with error code: {result}");
                    }
                });
                return;
            }
            
            if (!Player.main)
                return;

            var biome = BiomeUtils.GetDisplayName(Player.main.GetBiomeString(), out var isVanillaDisplayName);
            var biomeString = BiomeUtils.GetBiomeString(Player.main.GetBiomeString());
            _activity.Details = isVanillaDisplayName ? biome : $"At {biome}";
            _activity.Assets.LargeImage = biomeString;
            _activity.Assets.LargeText = biome;
            
            if (Player.main.IsInSub())
            {
                _activity.State = "In Base";
                _activity.Assets.SmallImage = string.Empty;
                _activity.Assets.SmallText = string.Empty;
            }
            else if (Player.main.GetVehicle() is not null)
            {
                _activity.State = "In Prawn Suit";
                _activity.Assets.SmallImage = kImageExosuit;
                _activity.Assets.SmallText = "Prawn Suit";
            }
            else if (Player.main.inHovercraft)
            {
                _activity.State = "On Snowfox";
                _activity.Assets.SmallImage = kImageHoverbike;
                _activity.Assets.SmallText = "Snowfox";
            }
            else if (Player.main.inSeatruckPilotingChair || Player.main.IsPilotingSeatruck())
            {
                _activity.State = "In Seatruck";
                _activity.Assets.SmallImage = kImageSeatruck;
                _activity.Assets.SmallText = "Seatruck";
            }
            else if (Player.main.IsUnderwaterForSwimming())
            {
                _activity.State = "Swimming";
                _activity.Assets.SmallImage = kImageSwimming;
                _activity.Assets.SmallText = string.Empty;
            }
            else if (!Player.main.IsSwimming())
            {
                _activity.State = "On Foot";
                _activity.Assets.SmallImage = string.Empty;
                _activity.Assets.SmallText = string.Empty;
            }

            _activity.State += $" (Depth: {Mathf.Round(Player.main.GetDepth())})";

            _activityManager.UpdateActivity(_activity, result =>
            {
                if (result is not Result.Ok)
                {
                    QModManager.Utility.Logger.Log(QModManager.Utility.Logger.Level.Error, $"Update Activity failed with error code: {result}");
                }
            });
        }

        private void UpdateState()
        {
            var scene = _sceneName.ToLower();
            if (scene.StartsWith("start") || scene.Contains("menu"))
                _state = PlayerState.Menu;
            else if (uGUI._main is not null && uGUI.isLoading)
                _state = PlayerState.Loading;
            else
                _state = PlayerState.Playing;
        }

        private enum PlayerState
        {
            Menu,
            Loading,
            Playing,
        }
    }
}