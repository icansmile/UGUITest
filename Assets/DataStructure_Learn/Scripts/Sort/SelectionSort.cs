using System.Collections.Generic;

/// <summary>
/// 直接选择排序: 将数组分成有序和无序区, 主要操作无序区, 每次从无序区选择一个最小的, 放到有序区后面
/// 稳定排序
/// </summary>
public class SelectionSort <T> : ISorter<T>
where T : System.IComparable
{
    public void Sort(List<T> oriList)
    {
        for(int i = 0; i < oriList.Count - 1; ++i)
        {
            int minIndex = i;
            for(int j = i + 1; j < oriList.Count; ++j)
                if(oriList[minIndex].CompareTo(oriList[j]) > 0) minIndex = j;

            T temp = oriList[minIndex];
            oriList[minIndex] = oriList[i];
            oriList[i] = temp;
        }
    }
}