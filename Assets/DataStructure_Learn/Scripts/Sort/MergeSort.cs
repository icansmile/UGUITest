using System.Collections.Generic;

public class MergeSort <T> : ISorter<T>
where T : System.IComparable
{
    public void Sort(List<T> oriList)
    {
        mergeSort(oriList, 0, oriList.Count - 1);
    }

    private void mergeSort(List<T> oriList, int first, int last)
    {
        if(first < last)
        {
            int mid = (first + last) / 2;
            mergeSort(oriList, first, mid);
            mergeSort(oriList, mid + 1, last);
            mergeArray(oriList, first, mid, last);
        }
    }

    private void mergeArray(List<T> oriList, int first, int mid, int last)
    {
        List<T> tempList = new List<T>();

        int i = first;
        int j = mid + 1;

        while(i <= mid && j <= last)
        {
            if(oriList[i].CompareTo(oriList[j]) <= 0)
                tempList.Add(oriList[i++]);
            else
                tempList.Add(oriList[j++]);
        }

        while(i <= mid)
            tempList.Add(oriList[i++]);


        while(j <= last)
            tempList.Add(oriList[j++]);

        for(int k = 0; k < tempList.Count; ++k)
        {
            oriList[first + k] = tempList[k];
        }
    }
}