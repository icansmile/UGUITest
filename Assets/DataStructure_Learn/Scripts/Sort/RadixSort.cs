using System.Collections.Generic;

/// <summary>
/// 基数排序
/// http://www.cnblogs.com/skywang12345/p/3603669.html
/// </summary>
public class RadixSort <T> : ISorter<T>
where T : System.IComparable
{
    public void Sort(List<T> oriList)
    {
        radixSort(oriList as List<int>);
    }

    int getMax(List<int> oriList)
    {
        int max = oriList[0];

        for(int i = 1; i < oriList.Count; ++i)
        {
            if(oriList[i] > max)
                max = oriList[i];
        }

        return max;
    }

    void countSort(List<int> oriList, int exp)
    {
        int[] output = new int[oriList.Count];
        int[] buckets = new int[10];

        for(int i = 0; i < oriList.Count; ++i)
            ++buckets[(oriList[i]/exp)%10];

        for(int i = 1; i < 10; ++i)
            buckets[i] += buckets[i-1];

        for(int i = oriList.Count - 1; i >= 0; --i)
        {
            int index = buckets[ oriList[i]/exp%10 ] - 1;
            output[index] = oriList[i];
            buckets[ oriList[i]/exp%10 ]--;
        }

        for(int i = 0; i < oriList.Count; ++i)
        {
            oriList[i] = output[i];
        }
    }

    void radixSort(List<int> oriList)
    {
        int max = getMax(oriList);

        for(int exp = 1; max/exp > 0; exp *= 10)
        {
            countSort(oriList, exp);
        }
    }
}