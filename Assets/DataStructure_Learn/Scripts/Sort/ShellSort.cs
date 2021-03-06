using System.Collections.Generic;

/// <summary>
/// https://zh.wikipedia.org/wiki/%E6%8E%92%E5%BA%8F%E7%AE%97%E6%B3%95#.E7.AE.80.E8.A6.81.E6.AF.94.E8.BE.83
/// 希尔排序: 预订步长, 分组, 然后对组内元素进行直接插入排序, 直到步长为0
/// </summary>
public class ShellSort<T> : ISorter<T>
where T : System.IComparable
{
    public void Sort(List<T> oriList)
    {
        int gap;
        for(gap = oriList.Count / 2; gap > 0; gap /= 2) //步长逐渐缩小
        {
            for(int i = 0; i < gap; ++i)    //分组
            {
                for(int j = i + gap; j < oriList.Count; j += gap)   //一组,组内进行 直接插入排序
                {
                    // if(oriList[j].CompareTo(oriList[j - gap]) < 0)
                    {
                        T temp = oriList[j];
                        int k = j - gap;
                        while(k >= 0 && temp.CompareTo(oriList[k]) < 0)
                        {
                            oriList[k + gap] = oriList[k];
                            k -= gap;
                        }

                        oriList[k + gap] = temp;
                    }
                }
            }
        }
    }
}