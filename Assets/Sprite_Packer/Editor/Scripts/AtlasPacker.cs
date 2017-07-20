using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;


//[Sprite Packer] https://docs.unity3d.com/Manual/SpritePacker.html
//还可以批量设置图集属性，往后在实践中了解
public class AtlasPacker {

	[MenuItem("MyTool/MakePrefabsWithAtlas")]
	private static void MakePrefabsWithAtlas()
	{
		string prefabDir = Application.dataPath + "/Resources/Sprite";

		if(!Directory.Exists(prefabDir))
		{
			Directory.CreateDirectory(prefabDir);
		}

		//获取Atlas目录下所有的png, 创建对应的sprite prefab， 存进 prefabDir
		string atlasPicDir = Application.dataPath + "/Sprite_Packer/Atlas";

		//获取DirectionInfo才能使用pattern搜索文件名
		DirectoryInfo atlasPicDirInfo = new DirectoryInfo(atlasPicDir);

		foreach(DirectoryInfo dirInfo in atlasPicDirInfo.GetDirectories())
		{
			// Debug.Log(dirInfo.FullName);
			string resourcesDir = prefabDir + "/" + dirInfo.Name;

			if(!Directory.Exists(resourcesDir))
			{
				Directory.CreateDirectory(resourcesDir);
			}

			foreach(FileInfo fileInfo in dirInfo.GetFiles("*.png"))
			{
				string picPath = fileInfo.FullName;
				string assetPath = picPath.Substring(picPath.IndexOf("Assets"));
				// Debug.Log(assetPath);

				Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(assetPath);
				GameObject spriteGo = new GameObject(sprite.name);
				SpriteRenderer spriteRenderer = spriteGo.AddComponent<SpriteRenderer>();
				spriteRenderer.sprite = sprite;

				string prefabPath = resourcesDir + "/" + sprite.name + ".prefab";
				PrefabUtility.CreatePrefab(prefabPath.Substring(prefabPath.IndexOf("Assets")), spriteGo);

				//Destroy may not be called from edit mode! Use DestroyImmediate instead.
				GameObject.DestroyImmediate(spriteGo);
			}
		}

		AssetDatabase.Refresh();

	}
}
