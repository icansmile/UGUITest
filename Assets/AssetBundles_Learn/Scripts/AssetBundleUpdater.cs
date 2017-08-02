using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetBundleUpdater : MonoBehaviour
{
	private Dictionary<string, string> localBundleMD5Dict;
	private Dictionary<string, string> serverBundleMD5Dict;
	private List<string> diffBundleNameList;

	private string localVersionFilePath;
	private string localBundlesDir;

	//正常情况由服务器返回
	private string serverVersionFilePath;
	private string serverBundlesDir;

	void Start()
	{
		localVersionFilePath = Application.persistentDataPath + "/Res/versionInfo.txt";
		localBundlesDir = Application.persistentDataPath + "/Res/Bundles";
		serverVersionFilePath = "file:///" + Application.dataPath + "/../Res/versionInfo.txt";
		serverBundlesDir = "file:///" + Application.dataPath + "/../Res/Bundles";

		localBundleMD5Dict = new Dictionary<string, string>();
		serverBundleMD5Dict = new Dictionary<string, string>();
		diffBundleNameList = new List<string>();

		StartCoroutine(updateVersion());
	}
		
	/// <summary>
	/// 读取local version
	/// 读取server version
	/// 对比local 和 server version, 筛选出需要更新的bundle
	/// 更新local version
	/// </summary>
	/// <returns></returns>
	IEnumerator updateVersion()
	{	
		if(!Directory.Exists(localBundlesDir))
			Directory.CreateDirectory(localBundlesDir);

		//读取本地version
		Stream localVersionFileStream = File.Open(localVersionFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);

		StreamReader reader = new StreamReader(localVersionFileStream);
		// Debug.Log(reader.ReadToEnd());
		localBundleMD5Dict = readVersionFile(localVersionFileStream);

		//读取服务器version
		WWW www = new WWW(serverVersionFilePath);
		yield return www;
		if(string.IsNullOrEmpty(www.error))
		{
			MemoryStream memStream = new MemoryStream(www.bytes);
			serverBundleMD5Dict = readVersionFile(memStream);
			memStream.Close();
		}
		else
		{
			Debug.Log("www error" + www.error);
		}

		//获取差异文件列表
		diffBundleNameList = getBundleNeedUpdateList();

		//有差异文件需要下载
		if(diffBundleNameList.Count > 0)
		{
			yield return StartCoroutine(downloadRes(diffBundleNameList));
		}

		localVersionFileStream.SetLength(0);
		localVersionFileStream.Write(www.bytes, 0, www.bytes.Length);
		localVersionFileStream.Flush();
		localVersionFileStream.Close();
	}

	/// <summary>
	/// 下载资源
	/// </summary>
	/// <param name="bundleNames"></param>
	/// <returns></returns>
	IEnumerator downloadRes(List<string> bundleNames)
	{
		for(int i = 0; i < bundleNames.Count; ++i)
		{
			using(WWW www = new WWW(serverBundlesDir + "/" + bundleNames[i]))
			{
				Debug.Log(www.url);
				yield return www;
				if(string.IsNullOrEmpty(www.error))
				{
					replaceLocalFile(localBundlesDir + "/" + bundleNames[i], www.bytes);
				}
				else
				{
					Debug.Log("www error" + www.error);
				}
			}
		}

		yield return null;
	}

	/// <summary>
	/// 替换或者添加指定路径的文件
	/// </summary>
	/// <param name="path"></param>
	/// <param name="data"></param>
	void replaceLocalFile(string path, byte[] data)
	{
		Debug.Log("更新文件 -- " + path);
		FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
		fs.SetLength(0);
		fs.Write(data, 0, data.Length);
		fs.Flush();
		fs.Close();
	}

	/// <summary>
	/// 解析versionInfo
	/// </summary>
	Dictionary<string, string> readVersionFile(Stream fileStream)
	{
		Dictionary<string, string> bundleMD5Dict = new Dictionary<string, string>();

		fileStream.Position = 0;
		StreamReader reader = new StreamReader(fileStream);

		//版本号
		string version = reader.ReadLine();

		//bundle name & md5
		while(true)
		{
			string bundleName = reader.ReadLine();

			if(string.IsNullOrEmpty(bundleName)) break;

			string md5 = reader.ReadLine();

			bundleMD5Dict.Add(bundleName, md5);
		}

		return bundleMD5Dict;
	}

	/// <summary>
	/// 获取差异文件列表, 通过比对versionInfo
	/// </summary>
	/// <returns></returns>
	List<string> getBundleNeedUpdateList()
	{
		List<string> diffBundleNames = new List<string>();

		var dict = serverBundleMD5Dict.GetEnumerator();
		while(dict.MoveNext())
		{
			string serverBundleName = dict.Current.Key;
			string serverBundleMD5 = dict.Current.Value;
			
			//打资源包的时候预先保证versioninfo没有重复资源记录
			if(localBundleMD5Dict.ContainsKey(serverBundleName))
			{
				if(localBundleMD5Dict[serverBundleName] != serverBundleMD5)
				{
					//改动资源
					diffBundleNames.Add(serverBundleName);
				}
			}
			else
			{
				//新增资源
				diffBundleNames.Add(serverBundleName);
			}
		}

		return diffBundleNames;
	}

	/// <summary>
	/// 写入versionInfo
	/// </summary>
	void writeVersionFile(Dictionary<string, string> bundleMD5Dict)
	{
	}
}
