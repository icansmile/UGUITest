using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System;

/// <summary>
/// 
/// 计算量大的地方： 人工智能， 寻路， 网络通信， 文件操作
/// 如在场景中用A*算法进行大量的数据计算,变形网格中操作大量的顶点,持续的要运行上传数据到服务器,二维码识别等图像处理
/// 
/// 【风宇冲】Unity多线程 http://blog.sina.com.cn/s/blog_471132920101hh5d.html
/// Unity多线程（Thread）和主线程（MainThread）交互使用类——Loom工具分享 http://dsqiu.iteye.com/blog/2028503
/// unity3d之http多线程异步资源下载 http://www.cnblogs.com/U-tansuo/p/unity3d_Threading_AsyDown_HTTP.html
/// 多线程和网络 https://www.one-tab.com/page/TvMLyy31TzyJDz8E35pNLg
/// </summary>
public class ThreadTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Thread thread = new Thread(new ThreadStart(() => { print("Hello!"); }));
		thread.IsBackground = true;
		thread.Start();

		Thread thread2 = new Thread(new ParameterizedThreadStart( str => print(str)));
		thread.IsBackground = true;
		thread2.Start("param hi");

		ThreadPool.QueueUserWorkItem(str => print(str), "pool hi");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI()
	{
		if(GUILayout.Button("Loom多线程插件测试"))
		{
			Mesh mesh = GameObject.Find("Plane").GetComponent<MeshFilter>().mesh;
			scaleMesh(mesh, 2);
		}
	}

	void scaleMesh(Mesh mesh, float scale)
	{
		Vector3[] vertices = mesh.vertices;

		Loom.RunAsync(() => {
			for(int i = 0; i < vertices.Length; ++i)
			{
				vertices[i] *= scale;
			}

			Loom.QueueOnMainThread(() => {
				mesh.vertices = vertices;
				mesh.RecalculateBounds();
			});
		});
	}
}
