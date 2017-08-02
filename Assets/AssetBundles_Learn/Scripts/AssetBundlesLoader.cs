using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/*
https://www.one-tab.com/page/A1rUTxPVR-2wOXtgLGh6LQ

所有的manifest信息都可以在主bundle中获取, 因此打包得到的manifest只用来编辑器预览. 实际使用中,要获取依赖信息,只要读取 主bundle 为 AssetBundleManifest 类型即可.
主bundle.name是个空字符串, 如果根据name缓存的话, 需要注意.
加载manifest的asset name 是 AssetBundleManifest
============================================================================================================================
4种方式加载 AssetBundle

1.AssetBundle.LoadFromMemoryAsync, 需要传入一组AssetBundle的字节信息。如果是LZMA压缩格式，则加载的时候就解压缩；如果是LZ4，则加载的时候还是压缩状态。

2.AssetBundle.LoadFromFile, 能够高效快速的从本地加载已解压的bundle。当bundle已解压或者是LZ4压缩格式(按块解压)，则直接加载。如果是LZMA压缩格式，则在加载进内存前，要先完全解压。
	这个方法在unity5.3以及之前的版本，无法加载streaming assets的文件

3.WWW.LoadFromCacheOrDownload， 用于从远程服务器下载 或 从本地加载 bundle。 从远程服务器下载下来以后，会开启一个工作线程来解压并缓存。如果目标bundle已经解压并缓存了，
则这个方法同 AssetBundle.LoadFromFile 是一样的效果。
由于这个方法会在WWW对象中缓存bundle字节数据，所以要保持bundle尽量小，并且保证一次只加载一个bundle，避免内存高峰。
如果缓存空间不足，则会Unload自动最近最少用到的bundle, 直到足以缓存当前bundle。如果实在没法腾出空间了（硬盘空间满 或者 bundle都在使用中）,则会把当前bundle数据放进内存中。
有个version参数。
	即将被遗弃，使用UnityWebRequest代替

4.UnityWebRequest, 优点是更灵活，可能减少不必要的内存使用。比www好。 （不懂为啥 >_<!

============================================================================================================================
从 AssetBundle 加载 Asset

bundle.LoadAsset, LoadAllAssets，以及他们的异步加载 LoadAssetAsync, LoadAllAssetsAsync
同步加载的API直接返回 Unity.Object ，异步加载返回 AssetBundleRequest

============================================================================================================================
加载 AssetBundle Manifests 依赖描述文件
如果某个 gameobject 引用了 某个texture， 则texture是否也一起打进同一个bundle？如果texture有独立的Bundle。则gameobject的bundle会自动引用，否则一同打进一个bundle
两个gameobject同时应用了 某个texture, 是两个包中都有这个texture 还是 先到先得？如上
或者其他情况
如果texture单独打成bundle， 那么就需要依赖文件来保证 texture 的 bundle 先加载？ 对的，而且单独打通常是必要的，这就是为什么说要把常用共享资源分离出来为另一个bundle，这样会减少bundle大小

============================================================================================================================
管理 已加载的AssetBundle
1. 当 Object 从场景中移除时，Unity并不会自动清理（unload）。而是定时清理，但是也可以手动清理。

2. 必须正确处理load和unload, 不恰当的处理 AssetBundle 可能导致 内存中有两份同样的bundle,或者texutre丢失等
	所以重点在于什么时候调用 AssetBundle.Unload(bool)

3. AssetBundle.Unload(bool), true的时候会卸载掉从该AssetBundle加载的所有Object, false则卸载掉 the header information of the AssetBundle being called（什么意思？>.<!!）

4. 大部分情况都应该使用 Unload(true)，避免存在两个相同的引用Object副本
当必须使用Unload(false)的时候， 还需要调用Resources.UnloadUnusedAssets (这个方法具体什么用， 这样做和Unload(true) 有何区别？ 清理掉不被使用的资源；前者处理内存中的所有资源，后者只处理从Bundle加载进内存的资源)
	Unload(false) 会断开AssetBundle 和 生成的引用Obejct的链接， 当下载再次AssetBundle.LoadAsset的时候，就会多产生一个引用Object

当bundle中有依赖其他bundle的时候，unload会对表现产生影响，
如果2个bundle, x 和 y 同时依赖另一个bundle z
1.如果不先加载z， 那x,y能正常加载么？也就是z会被自动加载不？ 必须先加载依赖项
2.场景中同时存在了 x,y, 对x作unload(true), y会出现异常么？ unload(false)呢？x,y之前没有联系，所以没影响
3.场景中存在x， 对x作unload(true),此时 z会被卸载么？即y能正常加载么？ unload(false)呢？如上，无关，只有 【被依赖项z】 发生改变时，才会影响到【依赖项x,y】

PS!: bundle会存在多个资源， 所以Unload是针对n个资源，以上讨论的都是1个资源，并且有依赖其他资源的情况, 所以只要z bundle正常的情况下，x,y怎么Unload都不会对对方造成影响
如果x bundle包含了 a,b资源， y bundle包含了c资源， 且c资源依赖于a资源
1.先加载 y bundle， 能不能正常显示？不能，必须先加载x,才有c资源
2.先加载 x bundle，再加载y bundle， 对 x 作unload(true), y bundle还正常不？ c资源被unlaod了，所以不正常
3.先加载 x bundle, 再加载y bundle, 对 x 作unload(false), y bundle应该是正常的， 然后再对 y bundle 做 Unload, 此时 c 资源会被 unload 吗？是否要用Resources.unloadunused
也就是， Unload是否只针对从本Bundle生成的资源？ 
是的！y bundle不会管 x bundle 中的资源 c， 所以c不会被unload，c迷路了，这个时候就要用Resources.UnloadUnusedAssets()了
就算是Destroy(y),也没法 清除资源 c 的缓存， 必须得用Unload(参照1)

============================================================================================================================
补丁
Unity没有提供差异检测功能,需要自己实现一套
需要判断哪些补丁是需要替换的, 1.需要当前本地下载完成的bundle列表以及版本信息  2.远程服务器的bundle列表以及版本信息

============================================================================================================================
可能出现的问题
1.Asset资源重复: 正确处理依赖关系,合理分包加载卸载
2.Sprite Altas 精灵图集重复 : 图集会在每一个引用到他的bundle中都有一个副本, 保证所有引用到同一个图集的精灵都打在同一个bundle中.
3.Android Texture 安卓纹理 : 所有安卓设备支持ETC1的纹理,但是ETC1不支持透明通道. 如果应用不需支持opengl es2, 则可以使用ETC2, 所有opengl es3的设备都支持ETC2.
其他特殊情况,可以通过设定变体Variant来区分纹理格式, 需要特殊区分的纹理应该单独放在一个bundle中, 这样方便单独更新.

============================================================================================================================
热更新的情况: http://lvmingbei.hatenablog.com/entry/2016/04/25/185738
1.版本控制. 文件是否存在, hash值是否一致, size是否一致
2.断点续传
3.设备休眠处理
4.程序切换
5.网络连接异常
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

			return instance; }
	}

	private AssetBundlesLoader(){}

	

	public IEnumerator LoadFromMemoryAsync(string path)
	{
		AssetBundleCreateRequest createRequest = AssetBundle.LoadFromMemoryAsync(File.ReadAllBytes(path));

		yield return createRequest;

		AssetBundle bundle = createRequest.assetBundle;
		addBundle(bundle.name, bundle);
	}

	public AssetBundle LoadFromMemorySync(string path)
	{
		AssetBundle bundle = AssetBundle.LoadFromMemory(File.ReadAllBytes(path));
		addBundle(bundle.name, bundle);

		return bundle;
	}

	public AssetBundle LoadFromFile(string path)
	{
		AssetBundle bundle = AssetBundle.LoadFromFile(path);
		addBundle(bundle.name, bundle);

		return bundle;
	}

	public IEnumerator LoadFromFileAsync(string path)
	{
		AssetBundleCreateRequest createRequest = AssetBundle.LoadFromFileAsync(path);
		yield return createRequest;

		AssetBundle bundle = createRequest.assetBundle;
		addBundle(bundle.name, bundle);
	}

	public IEnumerator LoadFromCacheOrDownload(string url, int version)
	{
		while(!Caching.ready)
			yield return null;
		
		using(WWW www = WWW.LoadFromCacheOrDownload(url, version))
		{
			yield return www;

			if(!string.IsNullOrEmpty(www.error))
			{
				Debug.LogError(www.error);
				yield break;
			}

			addBundle(www.assetBundle.name, www.assetBundle);
		}
	}

	public IEnumerator UnityWebRequest(string uri, uint version)
	{
		using(UnityEngine.Networking.UnityWebRequest request = UnityEngine.Networking.UnityWebRequest.GetAssetBundle(uri, version))
		{
			yield return request.Send();
			
			if(request.isError)
			{
				Debug.Log(request.error);
				Debug.Log(request.responseCode);
			}

			AssetBundle bundle = UnityEngine.Networking.DownloadHandlerAssetBundle.GetContent(request);

			addBundle(bundle.name, bundle);
		}

		yield return null;
	}









	public T GetRes<T> (string bundleName, string assetName) where T : Object
	{
		if(bundleDict.ContainsKey(bundleName))
		{
			return bundleDict[bundleName].LoadAsset<T>(assetName);
		}
		else
		{
			AssetBundle bundle = LoadFromMemorySync(bundleName);
			return bundle.LoadAsset<T>(assetName);
		}
	}









	private void addBundle(string bundleName, AssetBundle bundle)
	{
		//主bundle的name是空字符串
		bundleName = bundleName == "" ? "Bundles" : bundleName;

		if(bundle != null)
		{
			bundleDict[bundleName] = bundle;
			Debug.Log(string.Format("load bundle name={0}", bundle.name));
		}
		else
		{
			Debug.LogError(string.Format("name={0}, bundle=null", bundleName));
		}
	}

	public void UnloadBundle(string name, bool unloadAll)
	{
		if(bundleDict.ContainsKey(name))
		{
			bundleDict[name].Unload(unloadAll);
			bundleDict.Remove(name);
		}
	}
}
