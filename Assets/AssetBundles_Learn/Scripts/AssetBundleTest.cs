using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetBundleTest : MonoBehaviour {

	string bundlesDir;

	// Use this for initialization
	void Start () 
	{
		bundlesDir = Application.dataPath + "/AssetBundles_Learn/Bundles";
	}

	/// <summary>
	/// OnGUI is called for rendering and handling GUI events.
	/// This function can be called multiple times per frame (one call per event).
	/// </summary>
	void OnGUI()
	{
		GUILayout.BeginHorizontal();
		if(GUILayout.Button("下载tex_bg1(被依赖项)"))
		{
			StartCoroutine(AssetBundlesLoader.Instance.LoadFromCacheOrDownload("file:///" + bundlesDir + "/tex_bg1", 0));
		}

		if(GUILayout.Button("下载bg1"))
		{
			StartCoroutine(AssetBundlesLoader.Instance.LoadFromCacheOrDownload("file:///" + bundlesDir + "/bg1", 0));
		}

		if(GUILayout.Button("下载bg1n2"))
		{
			StartCoroutine(AssetBundlesLoader.Instance.LoadFromCacheOrDownload("file:///" + bundlesDir + "/bg1n2", 0));
		}
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		if(GUILayout.Button("显示tex_bg1"))
		{
			GameObject go = AssetBundlesLoader.Instance.GetRes<GameObject>("tex_bg1", "bg1");
			Instantiate(go);
		}

		if(GUILayout.Button("显示bg1"))
		{
			GameObject go = AssetBundlesLoader.Instance.GetRes<GameObject>("bg1", "bg1");
			Instantiate(go);
		}

		if(GUILayout.Button("显示bg1n2"))
		{
			GameObject go = AssetBundlesLoader.Instance.GetRes<GameObject>("bg1n2", "bg1n2");
			Instantiate(go);
		}
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		if(GUILayout.Button("完全卸载tex_bg1"))
		{
			AssetBundlesLoader.Instance.UnloadBundle("tex_bg1", true);
		}

		if(GUILayout.Button("卸载bg1"))
		{
			AssetBundlesLoader.Instance.UnloadBundle("bg1", false);
		}

		if(GUILayout.Button("完全卸载bg1n2"))
		{
			AssetBundlesLoader.Instance.UnloadBundle("bg1n2", true);
		}
		GUILayout.EndHorizontal();

		GUILayout.Label("x拥有 bg3(prefab) + bg4(texture) \ny拥有 bg4(prefab,引用texture bg4)");
		GUILayout.BeginHorizontal();
		if(GUILayout.Button("下载x"))
		{
			StartCoroutine(AssetBundlesLoader.Instance.LoadFromCacheOrDownload("file:///" + bundlesDir + "/x", 0));
		}

		if(GUILayout.Button("显示x(prefab bg3)"))
		{
			GameObject go = AssetBundlesLoader.Instance.GetRes<GameObject>("x", "bg3");
			Instantiate(go);
		}

		if(GUILayout.Button("卸载x"))
		{
			AssetBundlesLoader.Instance.UnloadBundle("x", false);
		}

		if(GUILayout.Button("下载y"))
		{
			StartCoroutine(AssetBundlesLoader.Instance.LoadFromCacheOrDownload("file:///" + bundlesDir + "/y", 0));
		}

		if(GUILayout.Button("显示y(prefab bg4)"))
		{
			GameObject go = AssetBundlesLoader.Instance.GetRes<GameObject>("y", "bg4");

			Instantiate(go);
		}

		if(GUILayout.Button("卸载y"))
		{
			AssetBundlesLoader.Instance.UnloadBundle("y", true);
			// Resources.UnloadUnusedAssets();
		}

		if(GUILayout.Button("Destroy y"))
		{
			Destroy(GameObject.Find("bg4(Clone)"));
		}

		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		if(GUILayout.Button("加载主bundle"))
		{
			StartCoroutine(AssetBundlesLoader.Instance.LoadFromCacheOrDownload("file:///" + bundlesDir + "/Bundles", 0));
		}

		if(GUILayout.Button("加载主bundle manifest"))
		{
			AssetBundleManifest manifest = AssetBundlesLoader.Instance.GetRes<AssetBundleManifest>("Bundles", "AssetBundleManifest");
			foreach(string mf in manifest.GetAllDependencies("y"))
			{
				Debug.Log(mf);
			}
		}

		GUILayout.EndHorizontal();
	}
}
