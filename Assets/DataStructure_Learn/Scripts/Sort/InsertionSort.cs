using System.Collections.Generic;

/// <summary>
/// 插入排序, 分有序区和无序区, 主要对有序区进行操作, 依次从无序区提取一个元素, 然后插入有序区, 从有序区后面开始, 将比自己大的元素后移
/// 稳定排序
/// </summary>
public class InsertionSort <T> : ISorter<T>
where T : System.IComparable
{
    public void Sort(List<T> oriList)
    {
       for(int i = 1; i < oriList.Count; ++i) 
       {
           int j = i;
           T temp = oriList[i];

           while(j > 0 && oriList[j-1].CompareTo(temp) > 0)
           {
               oriList[j] = oriList[j-1];
                --j;
           }
           oriList[j] = temp;
       }
    }
}