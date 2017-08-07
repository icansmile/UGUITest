using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 队列: FIFO 先进先出
/// 主要操作: 入列, 出列, 取值, 取数量, 清空
/// </summary>
public class CQueue
{
    private ArrayList queue;

    public int count
    {
        get
        {
            return queue.Count;
        }
    }

    public CQueue()
    {
        queue = new ArrayList();
    }

    public void EnQueue(object item)
    {
        queue.Add(item);
    }

    public object DeQueue()
    {
        object item = queue[0];
        queue.RemoveAt(0);

        return item;
    }

    public object Peek()
    {
        return queue[0];
    }

    public void Clear()
    {
        queue.Clear();
    }
}