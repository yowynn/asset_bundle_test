using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public abstract class Test : MonoBehaviour
{
    [Serializable]
    public struct AssetBundlePair
    {
        public string Key;
        public AssetBundle Value;
    }
    public abstract string Tag { get; }
    private List<AssetBundle> abs = new List<AssetBundle>();
    public List<AssetBundlePair> LoadedAssetBundles = new List<AssetBundlePair>();
    private GameObject show = null;

    public AssetBundle LoadAB(string abname, string tag)
    {
        tag = tag ?? Tag;
        Debug.Log($"LoadAB: {tag}/{abname}");
        var ab = AssetBundle.LoadFromFile($"AssetBundles/{tag}/{abname}");
        abs.Add(ab);
        LoadedAssetBundles.Add(new AssetBundlePair { Key = abname, Value = ab });
        return ab;
    }

    public AssetBundle LoadAB(string abname)
    {
        string tag = Tag;
        var strs = abname.Split("/");
        if (strs.Length == 2)
        {
            tag = strs[0];
            abname = strs[1];
        }
        Debug.Log($"LoadAB: {tag}/{abname}");
        var ab = AssetBundle.LoadFromFile($"AssetBundles/{tag}/{abname}");
        abs.Add(ab);
        LoadedAssetBundles.Add(new AssetBundlePair { Key = abname, Value = ab });
        return ab;
    }

    public void Show(AssetBundle ab = null, string assetName = "prefab1")
    {
        if (show == null)
        {
            ab = ab ?? abs[0];
            //foreach (var name in ab.GetAllAssetNames()) Debug.Log(name);
            var prefab = ab.LoadAsset<GameObject>(assetName);
            show = Instantiate(prefab) as GameObject;
            show.GetComponentInChildren<TextMesh>().text = Tag;
        }
    }

    public virtual void Start()
    {
        Debug.Log("Start");
    }

    public virtual void OnDestroy()
    {
        foreach(var ab in abs)
        {
            ab.Unload(true);
        }
        if (show != null)
        {
            DestroyImmediate(show);
        }
    }

    public virtual void OnValidate()
    {
        Debug.Log("OnValidate");
        var removed = new List<AssetBundle>();
        foreach (var ab in abs)
        {
            var exist = false;
            foreach (var pair in LoadedAssetBundles)
            {
                if (pair.Value == ab)
                {
                    exist = true;
                    break;
                }
            }
            if (!exist)
            {
                removed.Add(ab);
            }
        }
        foreach (var ab in removed)
        {
            abs.Remove(ab);
            ab.Unload(true);
        }
    }
}
