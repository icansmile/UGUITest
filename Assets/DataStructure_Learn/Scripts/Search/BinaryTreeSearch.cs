using System.Collections.Generic;

/// <summary>
/// 二叉查找法适用于有序队列， 迭代法比递归法效率高， 根据 (最大值+最小值)/2=中间值 ,每次取序号最小和序号最大来求得中间值，
/// 再与目标值比较，小于中间值，则最大值=中间值-1, 大于中间值，则最小值=中间值+1， 不断的二分比对，直到中间值等于目标值，
/// 或者最小值大于最大值表示查找不到，返回-1 
/// </summary>
public class BinaryTreeSerach : ISearch
{
    public int Search(List<int> oriList, int target)
    {
        int upperBound, lowerBound, mid;
        upperBound = oriList.Count - 1;
        lowerBound = 0;

        while(lowerBound <= upperBound)
        {
            mid = (lowerBound + upperBound) / 2;

            if(oriList[mid] == target)
                return mid;
            else
            {
                if(oriList[mid] < target)
                    lowerBound = mid + 1;
                else
                    upperBound = mid - 1;
            }
        }

        return -1;
    }

    public int RecursionBinaryTreeSearch(List<int> oriList, int target, int lowerBound, int upperBound)
    {
        int mid = (lowerBound + upperBound) / 2;
        if(lowerBound > upperBound)
            return -1;
        else
        {
            if(oriList[mid] == target) 
                return mid;
            else
            {
                if(oriList[mid] > target)
                    return RecursionBinaryTreeSearch(oriList, target, mid + 1, upperBound);
                else
                    return RecursionBinaryTreeSearch(oriList, target, lowerBound, mid - 1);

            }
        }    
    }
}