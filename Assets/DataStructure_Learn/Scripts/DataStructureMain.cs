using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStructureMain : MonoBehaviour {

	private string content1;
	private string content2;
	private string content3;
	private Vector2 scrollPos = Vector2.zero;

	void Start () {
	}
	
	void OnGUI()
	{
		if(GUILayout.Button("Code Time Test"))
		{
			content1 = codeTimeTest();
			content2 = "";
			content3 = "";
		}

		GUILayout.BeginHorizontal();
		if(GUILayout.Button("数组-添加字符串数组"))
		{
			ArrayTest tester = new ArrayTest();
			content1 = "Array : " + tester.addStrToStrArray();
			content2 = "ArrayList : " + tester.addStrToArrayList();
			content3 = "";
		}

		if(GUILayout.Button("数组-添加整型数组"))
		{
			ArrayTest tester = new ArrayTest();
			content1 = "Array : " + tester.addStrToStrArray();
			content2 = "ArrayList : " + tester.addStrToArrayList();
			content3 = "";
		}

		GUILayout.EndHorizontal();

		System.Action<ISorter> sort = (ISorter sorter) => 
		{
			CodeTimeTester timer = new CodeTimeTester();
			List<int> list = CodeTester.GetRandomIntList(1000);
			content1 = "";
			list.ForEach(i => {content1 += i + " ";});

			timer.StartTime();
			sorter.Sort(list);
			timer.StopTime();

			content2 = getIntListContent(list);
			content3 = "耗时: " + timer.duration;
		};

		if(GUILayout.Button("冒泡排序"))
		{
			BubbleSort sorter = new BubbleSort();
			sort(sorter);
		}

		if(GUILayout.Button("选择排序"))
		{
			SelectionSort sorter = new SelectionSort();
			sort(sorter);
		}

		if(GUILayout.Button("插入排序"))
		{
			InsertionSort sorter = new InsertionSort();
			sort(sorter);
		}

		if(GUILayout.Button("Stack - (5+6+3)"))
		{
			StackTest stack = new StackTest();
			content1 = stack.Test();
		}

		if(GUILayout.Button("Stack - MulBase"))
		{
			StackTest stack = new StackTest();
			content1 = stack.MulBaseTest();
		}

		scrollPos = GUILayout.BeginScrollView(scrollPos);
		GUILayout.Label(content1);
		GUILayout.Label(content2);
		GUILayout.Label(content3);
		GUILayout.EndScrollView();
	}

	string codeTimeTest()
	{
		List<int> testList = new List<int>();
		CodeTimeTester tester = new CodeTimeTester();
		
		tester.StartTime();
		for(int i = 0; i < 10000; ++i)
		{
			testList.Add(6);
		}
		tester.StopTime();

		return tester.duration.ToString();
	}

	string getIntListContent(List<int> list)
	{
		System.Text.StringBuilder sb = new System.Text.StringBuilder();
		foreach(int i in list)
		{
			sb.AppendFormat("{0} ", i);
		}

		return sb.ToString();
	}
}
