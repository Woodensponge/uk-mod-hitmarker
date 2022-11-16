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

            if (embeddedAssetBundle != null)
            {
                Debug.LogError("Hitmarker: Tried loading the embedded AssetBundle already loaded.");
                return null;
            }

            var assembly = Assembly.GetExecutingAssembly();

            Stream stream = assembly.GetManifestResourceStream("HitmarkerMod.Resources.AssetBundles.hitmarker");
            if (stream == null)
            {
                Debug.LogError("Hitmarker: Cannot load default AssetBundle embedded in the dll.");
            }

            var assetBundle = AssetBundle.LoadFromStream(stream);
            if (assetBundle == null)
            {
                Debug.LogError("Hitmarker: Obtained embedded AssetBundle file, but could not create it. I have no idea why.");
                return null;
            }

            embeddedAssetBundle = assetBundle;
            return embeddedAssetBundle;
        }
    }
}
