using UMM;
using UnityEngine;

namespace HitmarkerMod
{
    [UKPlugin("Hitmarker Mod", "1.0.0", "Adds a customizeable hitmarker to the game. Hitmarkers are both optionally auditory and visual.", true, true)]
    public class HitmarkerMod : UKMod
    {
        public override void OnModLoaded()
        {
            if (AssetHelper.EmbeddedAssetBundle == null)
            {
                AssetHelper.LoadEmbeddedAssetBundle();
            }

            GunPatch.ApplyHooks();
            CrosshairPatch.ApplyHooks();
        }

        public override void OnModUnload()
        {
            GunPatch.RemoveHooks();
            CrosshairPatch.RemoveHooks();
        }
    }
}
