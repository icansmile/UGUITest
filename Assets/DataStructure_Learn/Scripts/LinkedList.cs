using System.Collections;
using System.Collections.Generic;

public class CNode
{
    public object Element;
    public CNode Link;

    public CNode()
    {
        Element = null;
        Link = null;
    }

    public CNode(object element)
    {
        Element = element;
        Link = null;
    }
}

public class CLinkedList
{
    protected CNode header;

    public CLinkedList()
    {
        header = new CNode("header");
    }

    private CNode Find(object item)
    {
        return null;
    }

    public void Insert(object newItem, object afterItem)
    {
    }

    private CNode FindPrevious(object item)
    {
        return null;
    }

    public void Remove(object item)
    {

    }

    public string PrintList()
    {
        return null;
    }
}