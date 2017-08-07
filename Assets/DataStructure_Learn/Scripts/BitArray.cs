using System.Collections;
using System.Collections.Generic;

/// <summary>
/// BitArray: 用来处理位集合, 位集合可以用来表示一组Bool值
/// 
/// 质数又称素数。一个大于1的自然数，除了1和它自身外，不能被其他自然数整除的数叫做质数；否则称为合数
/// </summary>
public class BitArrayTest
{
    public string GetPrimes(int[] array)
    {
        for(int i = 2; i < array.Length; ++i)
        {
            for(int j = i + 1; j < array.Length; ++j)
            {
                if(array[j] == 1)
                {
                    if(j % i == 0)
                    {
                        array[j] = 0;
                    }
                }
            }
        }

        System.Text.StringBuilder result = new System.Text.StringBuilder();
        for(int i = 0; i < array.Length; ++i)
        {
            if(array[i] == 1)
                result.Append(i).Append(" ");
        }

        return result.ToString(); 
    }

    public void BuildSieve(BitArray bits)
    {
        
    }
}