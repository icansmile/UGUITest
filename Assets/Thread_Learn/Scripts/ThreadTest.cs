﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

/// <summary>
/// 
/// 计算量大的地方： 人工智能， 寻路， 网络通信， 文件操作
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
		thread.Start();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
