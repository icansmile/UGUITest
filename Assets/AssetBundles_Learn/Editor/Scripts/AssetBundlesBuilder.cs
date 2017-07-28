using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
// https://www.one-tab.com/page/5x_2GrqsSKGk_Eg2_n7a4w
// 打包组织方式 :
// 1.按逻辑实体, 如 UI, 角色, 场景, 经常出现在游戏中的资源, 方便热更新单独的模块
// 2.按资源类型. 如 audio, shader, 方便对应多平台资源
// 3.按运行时需求. 如同时需要使用到的
// 准则 :
// 1.分离 常用 和 不常用
// 2.多个ab包都会用到的资源, 可以单独分离出来放在一个新的 [共享AB包]
// 3.分离 经常加载 和 不常加载
// 4.组合经常使用的散包
// 5.统一物体的不同版本,使用变体Variants

/*
BuildAssetBundleOptions:
	BuildAssetBundleOptions.None : 默认使用LZMA格式压缩, 使用前完全解压缩整个assetbundle, 解压多个小的散文件会稍慢, 下载完成后用LZ4重新压缩
	BuildAssetBundleOptions.UncompressedAssetBundle : 不压缩, 第一次加载会快些
	BuildAssetBundleOptions.ChunkBasedCompressed : 比LZMA大, 但是按 块 解压缩,所以加载比LZMA快

BuildTarget


Build后会获得 2*(n+1) 个文件, 分别是
	1. 对应资源的 AssetBundlName, AssetBundleName.manifest : 这是资源的集合,实际运行时需要加载的(不需要对应manifest)
	2. AssetBundle文件所在文件夹名字, AssetBundle文件所在文件夹名字.manifest  :  资源依赖信息
	3. 主bundle保存了所有的manifest(主要是各个bundle的依赖关系)

 */
public class AssetBundlesBuilder : EditorWindow
{
	[MenuItem("AssetBundles/Build")]
	private static void Build()
	{
		BuildPipeline.BuildAssetBundles(Application.dataPath + "/AssetBundles_Learn/Bundles", BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
		AssetDatabase.Refresh();
	}

	[MenuItem("AssetBundles/Tool/SetMaterial")]
	private static void SetMaterial()
	{
		string resDir = "Assets/AssetBundles_Learn/Res";
		string[] matPaths = Directory.GetFiles(Application.dataPath + "/../" + resDir + "/Materials");

		foreach(string matPath in matPaths)
		{
			if(matPath.Contains(".meta")) continue;

			string matName = matPath.Substring(matPath.LastIndexOf('/') + 1);

			Material mat = AssetDatabase.LoadAssetAtPath<Material>(resDir + "/Materials/" + matName);
			Texture tex = AssetDatabase.LoadAssetAtPath<Texture>(resDir + "/Textures/" + matName.Replace(".mat", ".jpg"));

			if(tex != null)
			{
				mat.mainTexture = tex;
				Debug.Log(string.Format("set mat {0}", mat.name));

				GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
				cube.GetComponent<MeshRenderer>().material = mat;

				PrefabUtility.CreatePrefab(resDir + "/Prefabs/" + matName.Replace(".mat", ".prefab"), cube);

				DestroyImmediate(cube);
			}
		}

		AssetDatabase.Refresh();
	}
}
