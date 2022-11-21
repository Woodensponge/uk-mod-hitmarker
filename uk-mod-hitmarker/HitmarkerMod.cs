using UMM;
using UnityEngine;

//TODO: Move to another file later
namespace HitmarkerMod
{
    [UKPlugin("Hitmarker Mod", "1.0.0", "Adds a customizeable hitmarker to the game. Hitmarkers are both optionally auditory and visual.", true, true)]
    public class HitmarkerMod : UKMod
    {
        GameObject hitmarker;

        public override void OnModLoaded()
        {
            //TODO: Move to another file later
            On.NewMovement.Start += NewMovement_Start;

            if (AssetHelper.EmbeddedAssetBundle == null)
            {
                AssetHelper.LoadEmbeddedAssetBundle();
            }
        }

        public override void OnModUnload()
        {
            Destroy(hitmarker);
        }

        private void NewMovement_Start(On.NewMovement.orig_Start orig, NewMovement self)
        {
            orig(self);

            var assetBundle = AssetHelper.EmbeddedAssetBundle;

            hitmarker = Instantiate(assetBundle.LoadAsset<GameObject>("HitmarkerPlaceholder"));

            hitmarker.transform.SetParent(GameObject.Find("Canvas").transform);
            hitmarker.transform.localPosition = Vector3.zero;
        }
    }
}
