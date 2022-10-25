using UMM;
using UnityEngine;
//TODO: Move to another file later
namespace HitmarkerMod
{
    [UKPlugin("Hitmarker Mod", "1.0.0", "Adds a customizeable hitmarker to the game. Hitmarkers are both optionally auditory and visual.", true, true)]
    public class HitmarkerMod : UKMod
    {
        public override void OnModLoaded()
        {
            //TODO: Move to another file later
            On.NewMovement.Start += NewMovement_Start;
            Debug.Log("Hitmarker: you're balls");

            AssetHelper.LoadEmbeddedAssetBundle();
        }

        private void NewMovement_Start(On.NewMovement.orig_Start orig, NewMovement self)
        {
            Debug.Log("Hitmarker: balls sniffer");
            var assetBundle = AssetHelper.EmbeddedAssetBundle;

            if (assetBundle == null)
            {
                Debug.Log("Hitmarker: cock");
                orig(self);
                return;
            }

            var prefab = assetBundle.LoadAsset<GameObject>("MyBalls");


            if (Instantiate(prefab) != null)
            {
                Debug.Log("Hitmarker: :)");
            }

            orig(self);
        }

        public override void OnModUnload()
        {

        }
    }
}
