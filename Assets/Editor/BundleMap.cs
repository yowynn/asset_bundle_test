using UnityEditor;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace AssetBundleTest
{
    [CreateAssetMenu(fileName = "BuildMap", menuName = "BuildMap")]
    [System.Serializable]
    public class BundleMap : ScriptableObject
    {
        [Serializable]
        public struct SubMap
        {
            public string assetBundleName;
            public string assetBundleVariant;
            public UnityEngine.Object[] assetObjects;
        }

        public string Outpath = "AssetBundles/default";
        public BuildTarget BuildTarget = BuildTarget.StandaloneWindows;
        public Dictionary<string, string> s;
        public SubMap[] MapList;

        public void Build()
        {
            if (!Directory.Exists(Outpath))
            {
                Directory.CreateDirectory(Outpath);
            }
            var buildList = new List<AssetBundleBuild>(MapList.Length);
            foreach(var map in MapList)
            {
                List<string> assetNames = new List<string>(map.assetObjects.Length);
                foreach (var asset in map.assetObjects)
                {
                    var assetName = AssetDatabase.GetAssetPath(asset);
                    assetNames.Add(assetName);
                }
                var build = new AssetBundleBuild();
                build.assetBundleName = map.assetBundleName;
                if (!String.IsNullOrEmpty(map.assetBundleVariant))
                {
                    build.assetBundleVariant = map.assetBundleVariant;
                }
                build.assetNames = assetNames.ToArray();
                buildList.Add(build);
            }
            BuildPipeline.BuildAssetBundles(Outpath, buildList.ToArray(), BuildAssetBundleOptions.None, BuildTarget);
        }
        [MenuItem("Assets/Build This Map")]
        public static void BuildHotfixAssetBundle()
        {
            BundleMap map = Selection.objects?[0] as BundleMap;
            map.Build();
        }
        [MenuItem("Assets/Build This Map", true)]
        public static bool BuildHotfixAssetBundleValidate()
        {
            UnityEngine.Object[] objects = Selection.objects;
            if (objects == null) return false;
            if (objects.Length != 1) return false;
            BundleMap map = objects[0] as BundleMap;
            if (map == null) return false;
            return true;
        }
    }
}