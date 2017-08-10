using UnityEngine;  
using System.Collections;  
using System.Collections.Generic;  
using System;  
using System.Threading;  
using System.Linq;  

/// <summary>
/// 组件:
/// 1.线程计数: 当前线程数, 最大线程数. 用Interlocked.Increment 和 Decrement来操作多线程计数变量
/// 2.线程开启方法: RunAsync(Action) 从ThreadPool.QueueUserWorkItem(WorkCallBack, object arg)申请一个线程操作
/// 3.主线程委托列表:线程操作结束后, 由于没有通知, 并且子线程不能操作主线程模块, 所以主线程的后续操作用委托来实现 : _action, delayed,
/// 4.主线程委托列表加入方法:QueueOnMainThread, 对委托列表加锁!.防止和update对委托列表的操作冲突
/// 5.主线程委托执行: update 查看是否有主线程委托需要执行 ,同样要给委托列表加锁
/// </summary>
public class Loom : MonoBehaviour  
{  
    //最大线程数
    public static int maxThreads = 8;  
    //当前线程数
    static int numThreads;

    //修改后,只有一个Loom实例, current = Instance
    private static Loom _current;
    public static Loom Current  
    {  
        get  
        {  
            Initialize();  
            return _current;  
        }  
    }  
    //####去除Awake
//  void Awake()  
//  {  
//      _current = this;  
//      initialized = true;  
//  }  

    static bool initialized;  

    //####作为初始化方法自己调用，可在初始化场景调用一次即可
    public static void Initialize()  
    {  
        if (!initialized)  
        {  

            if(!Application.isPlaying)  
                return;  
            initialized = true;  
            GameObject g = new GameObject("Loom");  
            //####永不销毁
            DontDestroyOnLoad (g);
            _current = g.AddComponent<Loom>();  
        }  

    }  

    private List<Action> _actions = new List<Action>();  

    // 延迟队列(预约)
    public struct DelayedQueueItem  
    {  
        public float time;  
        public Action action;  
    }  
    private List<DelayedQueueItem> _delayed = new  List<DelayedQueueItem>();  

    List<DelayedQueueItem> _currentDelayed = new List<DelayedQueueItem>();  

    public static void QueueOnMainThread(Action action)  
    {  
        QueueOnMainThread( action, 0f);  
    }  

    //主线程操作添加进 _actions
    public static void QueueOnMainThread(Action action, float time)  
    {  
        if(time != 0)  
        {
            if (Current != null)
            {
                lock (Current._delayed)
                {
                    Current._delayed.Add(new DelayedQueueItem { time = Time.time + time, action = action });
                }
            }
        }  
        else  
        {
            if (Current != null)
            {
                lock (Current._actions)
                {
                    Current._actions.Add(action);
                }
            }
        }  
    }  

    //执行新线程
    public static Thread RunAsync(Action a)  
    {  
        Initialize();  
        //如果线程数满了,则持续等待
        while(numThreads >= maxThreads)  
        {  
            Thread.Sleep(1);  
        }  
        Interlocked.Increment(ref numThreads);  
        ThreadPool.QueueUserWorkItem(RunAction, a);  
        return null;  
    }  

    //将委托再封装一层try-catch-finally异常捕获
    private static void RunAction(object action)  
    {  
        try  
        {  
            ((Action)action)();  
        }  
        catch  
        {  
        }  
        finally  
        {  
            Interlocked.Decrement(ref numThreads);  
        }  

    }  


    //不应该null了吧?
    void OnDisable()  
    {  
        if (_current == this)  
        {  

            _current = null;  
        }  
    }  



    // Use this for initialization  
    void Start()  
    {  

    }  

    List<Action> _currentActions = new List<Action>();  

    // Update is called once per frame  
    // 轮询?
    // lock的对象有什么用? 比如lock A对象, 则A对象被加入lock列表, 其他线程不能操作A对象, 也不能执行与A绑定的代码块, 直至lock exit
    // Where, Select之类的用法
    // 这里的即时委托,延时委托都是针对Unity主线程的, 问题在于如何判断子线程是否完成 -> 直接把委托注册放在子线程方法中
    // 锁住_actions, _delayed列表, 在下一批委托加入前,先把前一批委托执行完
    // 并且保证同时只有一个线程在操作_action, 即子线程操作完_action后, 主线程才能执行这边lock的代码块
    void Update()  
    {  
        //_currentActions是当前要执行的委托, 每帧都把当前_actions中的委托执行完, 然后clear
        lock (_actions)  
        {  
            _currentActions.Clear();  
            _currentActions.AddRange(_actions);  
            _actions.Clear();  
        }  
        foreach(var a in _currentActions)  
        {  
            a();  
        }  

        //_currentDelayed是所有延迟委托列表, 每帧筛选出到达执行时间的委托, 并执行
        lock(_delayed)  
        {  
            _currentDelayed.Clear();  
            _currentDelayed.AddRange(_delayed.Where(d=>d.time <= Time.time));  
            foreach(var item in _currentDelayed)  
                _delayed.Remove(item);  
        }  
        foreach(var delayed in _currentDelayed)  
        {  
            delayed.action();  
        }  



    }  
}  