using UnityEditor;
using System.IO;

public class CreateAssetBundles
{
   [MenuItem("Assets/Build AssetBundles")]
   static void BuildAllAssetBundles()
    {
        string assetBundleDirectoty = "Assets/StreamingAssets/AssetBundles";
        if (!Directory.Exists(assetBundleDirectoty))
        {
            Directory.CreateDirectory(assetBundleDirectoty);
        }
        BuildPipeline.BuildAssetBundles(assetBundleDirectoty,
            BuildAssetBundleOptions.None,
            BuildTarget.StandaloneWindows);
    }
}
