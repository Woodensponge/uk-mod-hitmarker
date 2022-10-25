using UnityEngine;
using System.IO;
using System.Reflection;

namespace HitmarkerMod
{
    public class AssetHelper
    {
        static AssetBundle embeddedAssetBundle;

        public static AssetBundle EmbeddedAssetBundle { get => embeddedAssetBundle; }

        public static AssetBundle LoadEmbeddedAssetBundle()
        {
            Debug.Log("Hitmarker: Loading Embedded AssetBundle");

            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "HitmarkerMod.Resources.AssetBundles.hitmarker";

            Stream stream = assembly.GetManifestResourceStream(resourceName);
            if (stream == null)
            {
                Debug.LogError("Hitmarker: Cannot load default AssetBundle embedded in the dll.");
            }

            embeddedAssetBundle = AssetBundle.LoadFromStream(stream);
            if (embeddedAssetBundle == null)
            {
                Debug.LogError("Hitmarker: Obtained Embedded AssetBundle file, but could not create it. Probably already loaded.");
            }

            return embeddedAssetBundle;
        }
    }
}
