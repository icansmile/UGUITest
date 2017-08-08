using System.Collections.Generic;

/// <summary>
/// 快速排序 : 核心是以一个基数的值为标准, 分成大小两边, 直到左边界等于右边界
/// 不稳定排序
/// </summary>
public class QuickSort <T> : ISorter<T>
where T : System.IComparable
{
    public void Sort(List<T> oriList)
    {
        sort(oriList, 0, oriList.Count - 1);
    }

    private void sort(List<T> oriList, int leftBound, int rightBound)
    {
        //递归终止条件,左边界和右边界重合
        if(leftBound >= rightBound)
            return;

        int left = leftBound;
        int right = rightBound;
        T baseVal = oriList[left];

        //保证所有元素都比对一遍, 以基数为中心,分成左右大小两部分
        while(left < right)
        {
            //左指标小于右指标, 且基数小于等于右指标的值
            while(left < right && baseVal.CompareTo(oriList[right]) <= 0)
            {
                --right;
            }

            //把小的丢到左边去
            oriList[left] = oriList[right];

            //左指标小雨右指标, 且基数大于左指标的值
            while(left < right && baseVal.CompareTo(oriList[left]) > 0)
            {
                ++left;
            }

            //把大的丢右边去
            oriList[right] = oriList[left];

            //基数第一次复位到左边
            oriList[left] = baseVal;
        }
        
        //对基数左半部递归排序
        sort(oriList, leftBound, left - 1);

        //对基数右半部递归排序
        sort(oriList, left + 1, rightBound);
    }
}