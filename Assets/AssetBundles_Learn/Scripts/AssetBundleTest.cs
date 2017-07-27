using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetBundleTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// OnGUI is called for rendering and handling GUI events.
	/// This function can be called multiple times per frame (one call per event).
	/// </summary>
	void OnGUI()
	{
		if(GUILayout.Button("下载"))
		{
			StartCoroutine(AssetBundlesLoader.Instance.LoadFromCacheOrDownload("https://git.oschina.net/nick.c/assetbundles/blob/master/bundles/bg", 0));
		}

		if(GUILayout.Button("显示"))
		{
			GameObject go = AssetBundlesLoader.Instance.GetRes<GameObject>("bg", "bg1");
			Instantiate(go);
		}
	}
}
