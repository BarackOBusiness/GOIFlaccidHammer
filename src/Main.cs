using BepInEx;
using BepInEx.Configuration;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Flaccid
{
    [BepInProcess("GettingOverIt.exe")]
    [BepInPlugin(GUID, Name, Version)]
    public class Flacid : BaseUnityPlugin
    {
        internal const string GUID = "goi.fun.flaccid";
        internal const string Name = "Flaccid Hammer";
        internal const string Version = "1.0.0";

        private ConfigEntry<Preset> difficulty;
        private ConfigEntry<float> floppiness;

        private enum Preset: int {
            Teraflop = 20,
            Gigaflop = 24,
            Megaflop = 28,
            Kiloflop = 32,
            Custom
        }
    
        private void Awake() {
            difficulty = Config.Bind(
                "", "Difficulty", Preset.Gigaflop,
                "A few preset configurations of flaccidity, more flop means more difficulty; hope you know your SI prefixes."
            );
            floppiness = Config.Bind(
                "", "Custom Floppiness", 26.0f,
                "A custom value for how rigid the hammer pole is, lower is more flaccid, higher is more rigid."
            );

            SceneManager.sceneLoaded += OnSceneLoad;
            Logger.LogInfo("Five, hundred, flops.");
        }

        private void OnSceneLoad(Scene scene, LoadSceneMode mode) {
            if (scene.name == "Mian") {
                var joint = GameObject.Find("Player/Hub/Slider/Handle").GetComponent<FixedJoint2D>();
                float flopValue = difficulty.Value != Preset.Custom? (float)difficulty.Value : floppiness.Value;
                joint.frequency = Mathf.Clamp(flopValue, 0f, 70f);
            }
        }
    }
}
