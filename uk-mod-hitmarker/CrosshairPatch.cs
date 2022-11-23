﻿using UnityEngine;

namespace HitmarkerMod
{
    public class CrosshairPatch
    {
        static Hitmarker hitmarker;

        public static void ApplyHooks()
        {
            On.Crosshair.Start += Crosshair_Start;
        }

        private static void Crosshair_Start(On.Crosshair.orig_Start orig, Crosshair self)
        {
            orig(self);

            var assetBundle = AssetHelper.EmbeddedAssetBundle;

            if (hitmarker == null)
            {
                hitmarker = Object.Instantiate(assetBundle.LoadAsset<GameObject>("HitmarkerPlaceholder")).AddComponent<Hitmarker>();
            }

            hitmarker.transform.SetParent(self.transform.parent);
            hitmarker.transform.localPosition = Vector3.zero;
        }

        public static void RemoveHooks()
        {
            On.Crosshair.Start -= Crosshair_Start;
            Object.Destroy(hitmarker);
        }
    }
}