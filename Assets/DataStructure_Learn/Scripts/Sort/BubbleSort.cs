using System.Collections.Generic;

/// <summary>
/// 冒泡排序 - 两两相比, 大的放后
/// 优化: bool echanged, 如果某次遍历中没有发生交换,则说明排序完成
/// 稳定排序
/// 时间复杂度 o(n^2)     n*n-1
/// </summary>
public class BubbleSort <T> : ISorter<T>
where T : System.IComparable
{
    public void Sort(List<T> oriList)
    {
        bool exchanged = true;

        for(int i = 0; i < oriList.Count - 1; ++i)
        {
            for(int j = 0; j < oriList.Count - 1 - i; ++j)
            {
                if(oriList[j].CompareTo(oriList[j+1]) > 0)
                {
                    T temp = oriList[j];
                    oriList[j] = oriList[j+1];
                    oriList[j+1] = temp;
                    exchanged = true;
                }
            }

            if(!exchanged)
                break;
        }
    }
}