using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var abPrefab = AssetBundle.LoadFromFile("AssetBundles/StandaloneWindows/prefab");
        //var abPrefab = AssetBundle.LoadFromFile("Assets/AssetBundles/prefab");
        var abMat = AssetBundle.LoadFromFile("AssetBundles/StandaloneWindows/mat");
        //var abMatV1 = AssetBundle.LoadFromFile("AssetBundles/StandaloneWindows/mat.v1");
        //var abMatV2 = AssetBundle.LoadFromFile("AssetBundles/StandaloneWindows/mat.v2");
        //var abMatp = AssetBundle.LoadFromFile("Assets/AssetBundles/mat");
        //var abMatV1p = AssetBundle.LoadFromFile("Assets/AssetBundles/mat.v1");
        //var abMatV2p = AssetBundle.LoadFromFile("Assets/AssetBundles/mat.v2");
        var prefab = abPrefab.LoadAsset<GameObject>("prefab1");
        GameObject.Instantiate(prefab);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
