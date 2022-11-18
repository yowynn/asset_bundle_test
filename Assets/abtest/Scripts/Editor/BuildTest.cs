using UnityEngine;
using UnityEditor;
using System.IO;

public class BuildAssetBundlesBuildMapExample : MonoBehaviour
{
    [MenuItem("Test/Build Asset Bundles Using BuildMap")]
    static void BuildMapABs()
    {
        var assetBundleDirectory = "Assets/AssetBundles";
        var abnames = new string[]
        {
            "mat.v1",
            //"mat.v2",
            "prefab",
        };
        if (!Directory.Exists(assetBundleDirectory))
        {
            Directory.CreateDirectory(assetBundleDirectory);
        }
        AssetBundleBuild[] buildMap = new AssetBundleBuild[abnames.Length];
        for (var i = 0; i < abnames.Length; i++)
        {
            string variant = null;
            var abname = abnames[i];
            var assetPaths = AssetDatabase.GetAssetPathsFromAssetBundle(abname);
            var ss = abname.Split('.');
            if (ss.Length == 2)
            {
                abname = ss[0];
                //variant = ss[1];
            }
            AssetBundleBuild build = new AssetBundleBuild();
            build.assetBundleName = abname;
            build.assetBundleVariant = variant;
            Debug.Log($"name: {abname}, variant: {variant}");
            build.assetNames = assetPaths;
            buildMap[i] = build;
        }
        BuildPipeline.BuildAssetBundles(assetBundleDirectory, buildMap, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
    }
}