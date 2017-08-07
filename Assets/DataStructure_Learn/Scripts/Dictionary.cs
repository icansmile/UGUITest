using System.Collections;
using System.Collections.Generic;

public class IPAddresses : DictionaryBase
{
    public IPAddresses()
    {
    }

    public void Add(string name, string ip)
    {
        base.InnerHashtable.Add(name, ip);
    }

    public void Remove(string name)
    {
        base.InnerHashtable.Remove(name);
    }

    public string Item(string name)
    {
        return base.InnerHashtable[name].ToString();
    }
}