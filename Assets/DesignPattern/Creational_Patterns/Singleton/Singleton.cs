using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 单例模式 : 保证一个类只有一个实例, 并提供一个访问它的全局访问点
/// 优点 : 
/// 1.内存只有一个实例, 当类需要频繁创建销毁时, 可以减少内存开销
/// 2.避免资源的多重占用
/// 3.统一管理资源
/// 缺点 : 
/// 1.与单一职责原则冲突, 一个类应该只关心内部逻辑,而不关心外部如何实例化
/// 
/// 
/// 组件:
/// 1.私有构造函数, 类实例只能由自身创建
/// 2.静态的类实例
/// 3.静态的实例获取属性
/// </summary>
public class Singleton
{
	//静态的类实例
	private static Singleton _instance;

	//私有的构造函数
	protected Singleton()
	{
	}

	//静态的实例获取属性
	public static Singleton Instance
	{
		get
		{
			if(_instance == null)
			{
				_instance = new Singleton();
			}

			return _instance;
		}
	}
}
