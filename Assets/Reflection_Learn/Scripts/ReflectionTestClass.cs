
using System;

public interface ReflectionTestInterface
{

}

public class ReflectionTestClass : ReflectionTestInterface
{
    public string PubVal
    {
        get;set;
    }

    private string priVal;

    public ReflectionTestClass(string _priVal, string _pubVal)
    {
        priVal = _priVal;
        PubVal = _pubVal;
    }

    private string priMethod()
    {
        return "priMethod";
    }

    public string PubMethod(string suffix)
    {
        return "PubMethod." + suffix;
    }

    public static void staticMethod()
    {
        
    }

    public delegate void VoidHandle();
    public event VoidHandle VoidEvent;
}