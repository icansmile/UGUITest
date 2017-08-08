using System.Collections;
using System.Collections.Generic;

public interface ISorter<T> where T : System.IComparable
{
    void Sort(List<T> oriList);
}