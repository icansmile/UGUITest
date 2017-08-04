using System.Collections.Generic;

/// <summary>
/// 冒泡排序 - 从后往前相邻的两个元素两两比较, 把小的放到前面去(小泡泡上浮)
/// 稳定排序算法
/// 时间复杂度 o(n^2)     n*n-1
/// </summary>
public class BubbleSort : ISorter
{
    public void Sort(List<int> oriList)
    {
        for(int i = oriList.Count - 1; i > 0; --i)
        {
            for(int j = i; j >= 0; --j)
            {
                if(oriList[i] < oriList[j])
                {
                    int temp = oriList[i];
                    oriList[i] = oriList[j];
                    oriList[j] = temp;
                }
            }
        }
    }
}