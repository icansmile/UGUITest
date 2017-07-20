using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// ScriptableObject 大致只是用来记录数据, 类似 ProjectSettings.asset 这种东西, 可以用assetbundle来更新
/// 但是通常游戏都是有自己的一套数据读取方案, json, xml, protobuf之类的, 所以暂时没啥用处
///	ScriptableObject一般用来存储u3d无法打包的对象（object），利用它可以将对象打包为assetbundle或者.assets文件以供后续使用。
///
/// 参考:
/// http://www.cnblogs.com/hammerc/p/4829934.html
/// http://blog.csdn.net/candycat1992/article/details/52181814
/// http://www.igiven.com/?p=879
/// </summary>
public class ScriptableObject_Learn : ScriptableObject{

	public int testInt;
	public float testFloat;
	public string testString;
	public List<int> testList;
	public Dictionary<int, int> testDictionary;

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		Debug.Log("ScriptableObject - Awake");
	}

	/// <summary>
	/// This function is called when the object becomes enabled and active.
	/// </summary>
	void OnEnable()
	{
		Debug.Log("ScriptableObject - OnEnable");
	}

	/// <summary>
	/// This function is called when the behaviour becomes disabled or inactive.
	/// </summary>
	void OnDisable()
	{
		Debug.Log("ScriptableObject - OnDisable");
		
	}

	/// <summary>
	/// This function is called when the MonoBehaviour will be destroyed.
	/// </summary>
	void OnDestroy()
	{
		Debug.Log("ScriptableObject - OnDestroy");
	}

	[MenuItem("Editor Tool/Create ScriptableObject")]
	private static void CreateScriptableObject()
	{
		ScriptableObject sobj = ScriptableObject.CreateInstance<ScriptableObject_Learn>();
		AssetDatabase.CreateAsset(sobj, "Assets/Editor_Learn/ScriptableObject_Learn.asset");
	}
}
