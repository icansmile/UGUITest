using System.Collections;
using System.Collections.Generic;

public class ArrayTest
{
    ArrayList arrayList;
    string[] strArray;
    int[] intArray;

    public ArrayTest()
    {
        arrayList = new ArrayList();
        strArray = new string[10000];
        intArray = new int[10000];
    }

    public string addStrToArrayList()
    {
        arrayList.Clear();

        CodeTimeTester tester = new CodeTimeTester();
        tester.StartTime();
        for(int i = 0; i < 10000; ++i)
        {
            //装箱
            arrayList.Add("test");
        }
        tester.StopTime();

        return tester.duration.ToString();
    }

    public string addStrToStrArray()
    {
        CodeTimeTester tester = new CodeTimeTester();
        tester.StartTime();
        for(int i = 0; i < 10000; ++i)
        {
            strArray[i] = "test";
        }
        tester.StopTime();
        return tester.duration.ToString();
    }

    public string addIntToArrayList()
    {
        arrayList.Clear();

        CodeTimeTester tester = new CodeTimeTester();
        tester.StartTime();
        for(int i = 0; i < 10000; ++i)
        {
            //装箱
            arrayList.Add(66);
        }
        tester.StopTime();

        return tester.duration.ToString();
    }

    public string addIntToStrArray()
    {
        CodeTimeTester tester = new CodeTimeTester();
        tester.StartTime();
        for(int i = 0; i < 10000; ++i)
        {
            intArray[i] = 66;
        }
        tester.StopTime();
        return tester.duration.ToString();
    }
}