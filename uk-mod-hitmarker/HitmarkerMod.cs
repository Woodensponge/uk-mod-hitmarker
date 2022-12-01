using UMM;
using UnityEngine;
using BepInEx;

namespace HitmarkerMod
{
#if UKMOD
    [UKPlugin("Hitmarker Mod", "1.0.0", "Adds a customizeable hitmarker to the game. Hitmarkers are both optionally auditory and visual.", true, true)]
    public class HitmarkerMod : UKMod
    {
        public override void OnModLoaded()
        {
            if (AssetHelper.EmbeddedAssetBundle == null)
            {
                AssetHelper.LoadEmbeddedAssetBundle();
            }

            DeliverDamagePatch.ApplyHooks();
            CrosshairPatch.ApplyHooks();
        }

        public override void OnModUnload()
        {
            DeliverDamagePatch.RemoveHooks();
            CrosshairPatch.RemoveHooks();
        }
    }
#endif
#if BEPINEX
    [BepInPlugin("woodensponge.ultrakill.hitmarker", "Hitmarker Mod", "1.0.0")]
    [BepInProcess("ULTRAKILL.exe")]
    public class HitmarkerMod : BaseUnityPlugin
    {
        public void Awake()
        {
            if (AssetHelper.EmbeddedAssetBundle == null)
            {
                AssetHelper.LoadEmbeddedAssetBundle();
            }

            DeliverDamagePatch.ApplyHooks();
            CrosshairPatch.ApplyHooks();
        }

        public void OnDestroy()
        {
            DeliverDamagePatch.RemoveHooks();
            CrosshairPatch.RemoveHooks();
        }
    }

#endif
}
