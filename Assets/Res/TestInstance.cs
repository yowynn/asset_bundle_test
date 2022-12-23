using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInstance : Test
{
    public string _tag = "undefined";
    public override string Tag => _tag;

    public string[] preloadAssetBundles = new string[]
    {
        "prefab",
        "mat1",
        "mat1a",
        "mat2",
        "tex",
    };
    public override void Start()
    {
        foreach (var bundleName in preloadAssetBundles)
        {
            LoadAB(bundleName);
        } 
        Show(null, "prefab1.prefab");
    }
}
