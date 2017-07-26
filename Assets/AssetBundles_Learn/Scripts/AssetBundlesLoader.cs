using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/*
https://www.one-tab.com/page/A1rUTxPVR-2wOXtgLGh6LQ
============================================================================================================================
4种方式加载 AssetBundle

1.AssetBundle.LoadFromMemoryAsync, 需要传入一组AssetBundle的字节信息。如果是LZMA压缩格式，则加载的时候就解压缩；如果是LZ4，则加载的时候还是压缩状态。

2.AssetBundle.LoadFromFile, 能够高效快速的从本地加载已解压的bundle。当bundle已解压或者是LZ4压缩格式(按块解压)，则直接加载。如果是LZMA压缩格式，则在加载进内存前，要先完全解压。
	这个方法在unity5.3以及之前的版本，无法加载streaming assets的文件

3.WWW.LoadFromCacheOrDownload， 用于从远程服务器下载 或 从本地加载 bundle。 从远程服务器下载下来以后，会开启一个工作线程来解压并缓存。如果目标bundle已经解压并缓存了，
则这个方法同 AssetBundle.LoadFromFile 是一样的效果。
由于这个方法会在WWW对象中缓存bundle字节数据，所以要保持bundle尽量小，最多几兆。并且保证一次只加载一个bundle，避免内存高峰。
如果缓存空间不足，则会自动最近最少用到的bundle, 直到足以缓存当前bundle。如果实在没法腾出空间了（硬盘空间满 或者 bundle都在使用中）,则会把当前bundle数据放进内存中。
有个version参数。
	即将被遗弃，使用UnityWebRequest代替

4.UnityWebRequest, 优点是更灵活，可能减少不必要的内存使用。比www好。 （不懂为啥 >_<!

============================================================================================================================
从 AssetBundle 加载 Asset

bundle.LoadAsset, LoadAllAssets，以及他们的异步加载 LoadAssetAsync, LoadAllAssetsAsync
同步加载的API直接返回 Unity.Object ，异步加载返回 AssetBundleRequest

============================================================================================================================
加载 AssetBundle Manifests 依赖描述文件
如果某个 gameobject 引用了 某个texture， 则texture是否也一起打进同一个bundle？
两个gameobject同时应用了 某个texture, 是两个包中都有这个texture 还是 先到先得？
或者其他情况
如果texture单独打成bundle， 那么就需要依赖文件来保证 texture 的 bundle 先加载

============================================================================================================================
管理 已加载的AssetBundle
1. 当 Object 从场景中移除时，Unity并不会自动清理（unload）。而是定时清理，但是也可以手动清理。

2. 必须正确处理load和unload, 不恰当的处理 AssetBundle 可能导致 内存中有两份同样的bundle,或者texutre丢失等
	所以重点在于什么时候调用 AssetBundle.Unload(bool)

3. AssetBundle.Unload(bool), true的时候会卸载掉从该AssetBundle加载的所有Object, false则卸载掉 the header information of the AssetBundle being called（什么意思？>.<!!）
	选true的话，是把所有依赖都卸载掉， 还是说，某个依赖是从别的bundle加载的，那么这个就不会一同卸载掉？

4. 大部分情况都应该使用 Unload(true)，避免存在两个相同的引用Object副本
当必须使用Unload(false)的时候， 还需要调用Resources.UnloadUnusedAssets (这个方法具体什么用， 这样做和Unload(true) 有何区别？)
	Unload(false) 会断开AssetBundle 和 生成的引用Obejct的链接， 当下载再次AssetBundle.LoadAsset的时候，就会多产生一个引用Object
	所以Unload, 在两种情况下分别会卸载哪些东西？>_<!
 */

public class AssetBundlesLoader 
{
	public Dictionary<string, AssetBundle> bundleDict = new Dictionary<string, AssetBundle>();

	private static AssetBundlesLoader instance;

	public static AssetBundlesLoader Instance
	{
		get
		{
			if(instance == null)
			{
				instance = new AssetBundlesLoader();
			}

			return instance;
		}
	}

	private AssetBundlesLoader(){}

	public IEnumerator LoadFromMemoryAsync(string path)
	{
		AssetBundleCreateRequest createRequest = AssetBundle.LoadFromMemoryAsync(File.ReadAllBytes(path));

		yield return createRequest;

		AssetBundle bundle = createRequest.assetBundle;
		addBundle(path, bundle);

		yield return null;
	}

	public AssetBundle LoadFromMemorySync(string path)
	{
		AssetBundle bundle = AssetBundle.LoadFromMemory(File.ReadAllBytes(path));
		addBundle(path, bundle);

		return bundle;
	}

	public AssetBundle LoadFromFile(string path)
	{
		AssetBundle bundle = AssetBundle.LoadFromFile(path);
		addBundle(path, bundle);

		return bundle;
	}

	//可能官方案例不是最佳实践？ www不用dispose么？
	public IEnumerator LoadFromCacheOrDownload(string url, int version)
	{
		while(!Caching.ready)
			yield return null;
		
		WWW www = WWW.LoadFromCacheOrDownload(url, version);
		yield return www;

		if(!string.IsNullOrEmpty(www.error))
		{
			Debug.LogError(www.error);
			yield return null;
		}

		addBundle(url, www.assetBundle);
	}

	public IEnumerator UnityWebRequest(string uri, string version)
	{
		UnityEngine.Networking.UnityWebRequest request = UnityEngine.Networking.UnityWebRequest.GetAssetBundle(uri);

		yield return request.Send();

		AssetBundle bundle = UnityEngine.Networking.DownloadHandlerAssetBundle.GetContent(request);

		addBundle(uri, bundle);

		yield return null;
	}

	public AssetBundleManifest GetAssetBundleManifest(string path, string name)
	{
		AssetBundle bundle = LoadFromFile(path);
		AssetBundleManifest manifest = bundle.LoadAsset<AssetBundleManifest>(name);
		return manifest;

		// //根据依赖列表，加载依赖
		// string[] dependencies = manifest.GetAllDependencies("bundleName");
		// foreach(var dependency in dependencies)
		// {
		// 	LoadFromFile(dependency);
		// }
	}












	public T GetRes<T> (string path, string name) where T : Object
	{
		if(bundleDict.ContainsKey(path))
		{
			return bundleDict[path].LoadAsset<T>(name);
		}
		else
		{
			AssetBundle bundle = LoadFromMemorySync(path);
			return bundle.LoadAsset<T>(name);
		}
	}









	private void addBundle(string path, AssetBundle bundle)
	{
		if(bundle != null)
		{
			bundleDict[path] = bundle;
		}
		else
		{
			Debug.LogError(string.Format("path={0}, bundle=null", path));
		}
	}

	private void unloadBundle(string path)
	{
		bundleDict.Remove(path);
	}
}
