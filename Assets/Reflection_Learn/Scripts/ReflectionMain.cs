using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using dllTest;

/// <summary>
/// 用编译器生成dll动态链接库,然后放进asset中即可调用
/// Assembly-CSharp.dll 包含所有脚本文件
/// libmono.so用来在运行是解析dll
/// </summary>
public class ReflectionMain : MonoBehaviour {
	private string content1;
	private ReflectionUsage refUsage = new ReflectionUsage();

	private const string assemblyName = "Assembly-CSharp";
	private const string className = "ReflectionTestClass";

	private ReflectionTestClass _instance;
	private ReflectionTestClass instance
	{
		get
		{
			if(_instance == null)
			{
				_instance = refUsage.CreateInstance(assemblyName, className) as ReflectionTestClass;
				content1 = "生成实例";
			}
			return _instance;
		}
	}

	// Use this for initialization
	void Start () {
		//调用动态链接库dll
		var c = new dllTest.MyClass();
		Debug.Log(c.testA());

		Debug.Log(instance);
	}

	void OnGUI()
	{
		if(GUILayout.Button("生成实例"))
		{
			content1 = refUsage.CreateInstance(assemblyName, className).ToString();
		}

		if(GUILayout.Button("获取成员"))
		{
			content1 = refUsage.GetMemberInfo();
		}

		GUILayout.BeginHorizontal();
		if(GUILayout.Button("获取字段"))
		{
			content1 = refUsage.GetFieldInfo(instance);
		}

		if(GUILayout.Button("修改字段 priVal"))
		{
			content1 = refUsage.SetFieldValue(instance, "priVal", "ref pri val");
		}
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		if(GUILayout.Button("获取属性器"))
		{
			content1 = refUsage.GetPropertyInfo(instance);
		}

		if(GUILayout.Button("属性器-get"))
		{
			content1 = refUsage.GetPropertyValue(instance, "PubVal");
		}

		if(GUILayout.Button("属性器-set"))
		{
			content1 = refUsage.SetPropertyValue(instance, "PubVal", "ref set pubval");
		}
		GUILayout.EndHorizontal();

		GUILayout.Label(content1);
	}
}
