using UMM;
using UnityEngine;
using UnityEngine.UI;
//TODO: Move to another file later
namespace HitmarkerMod
{
    [UKPlugin("Hitmarker Mod", "1.0.0", "Adds a customizeable hitmarker to the game. Hitmarkers are both optionally auditory and visual.", true, true)]
    public class HitmarkerMod : UKMod
    {
        static GameObject hitmarker;

        public override void OnModLoaded()
        {
            //TODO: Move to another file later
            On.NewMovement.Start += NewMovement_Start;
            Debug.Log("Hitmarker: you're balls");

            AssetHelper.LoadEmbeddedAssetBundle();
        }

        private void NewMovement_Start(On.NewMovement.orig_Start orig, NewMovement self)
        {
            orig(self);

            Debug.Log("Hitmarker: balls sniffer");
            var assetBundle = AssetHelper.EmbeddedAssetBundle;

            if (assetBundle == null)
            {
                Debug.Log("Hitmarker: cock");
                orig(self);
                return;
            }

            hitmarker = Instantiate(assetBundle.LoadAsset<GameObject>("HitmarkerPlaceholder"));
            if (self == null)
            {
                Debug.Log("FUCK");
            }
            

            Debug.Log("Hitmarker: piss");
            hitmarker.transform.SetParent(GameObject.Find("Canvas").transform);
            hitmarker.transform.localPosition = Vector3.zero;
            Debug.Log("Hitmarker: fucking nice");
        }

        public override void OnModUnload()
        {

        }
    }
}
