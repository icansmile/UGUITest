using System;
using System.Collections;
using System.Text.RegularExpressions;

/// <summary>
/// 堆栈： FILO 先进后出
/// 主要方法：进栈，出栈，取值，数量获得，清空栈，把堆栈数值作为数组返回
/// 可以用来检查是否是回文
/// </summary>
public class CStack
{
    private int curIndex;
    private ArrayList list;

    public CStack()
    {
        list = new ArrayList();
        curIndex = -1;
    }

    public int count
    {
        get
        {
            return list.Count;
        }
    }

    public void Push(object item)
    {
        list.Add(item);
        ++curIndex;
    }

    public object Pop()
    {
        object item = list[curIndex];
        list.RemoveAt(curIndex);
        --curIndex;

        return item;
    }

    public void Clear()
    {
        list.Clear();
        curIndex = -1;
    }

    public object Peek()
    {
        return list[curIndex];
    }
}

public class StackTest
{
    public string Test()
    {
        Stack nums = new Stack();
        Stack ops = new Stack();
        string expression = "5+6 + 3";
        calculate(nums, ops, expression);
        return nums.Pop().ToString();
    }

    public string MulBaseTest()
    {
        return mulBase(6, 8);
    }

    private string mulBase(int n, int b)
    {
        Stack digits = new Stack();
        do
        {
            digits.Push(n % b);
            n /= b;
        }while(n != 0);

        System.Text.StringBuilder result = new System.Text.StringBuilder();

        while(digits.Count > 0)
            result.Append(digits.Pop());

        return result.ToString();
    }
    
    private bool isNumeric(string input)
    {
        string patter = @"^\d+$";
        Regex validate = new Regex(patter);

        if(!validate.IsMatch(input))
            return false;

        return true;
    }

    private bool isOperator(string input)
    {
        return input == "+" || input == "-" || input == "*" || input == "/" ;
    }

    private void calculate(Stack n, Stack o, string expression)
    {
        string ch = "";
        string token = "";

        expression = expression.Replace(" ", "");

        for(int i = 0; i < expression.Length; ++i)
        {
            ch = expression.Substring(i, 1);

            if(isNumeric(ch))
            {
                token += ch;

                if(i == expression.Length - 1)
                {
                    n.Push(token);
                }
            }

            if(isOperator(ch))
            {
                o.Push(ch);

                n.Push(token);
                token = "";
            }

            if(n.Count == 2)
                compute(n, o);
        }
    }

    private void compute(Stack n, Stack o)
    {
        int oper1 = Convert.ToInt32(n.Pop());
        int oper2 = Convert.ToInt32(n.Pop());
        string oper = Convert.ToString(o.Pop());

        switch(oper)
        {
            case "+":
                n.Push(oper1 + oper2);
                break;
            case "-":
                n.Push(oper1 - oper2);
                break;
            case "*":
                n.Push(oper1 * oper2);
                break;
            case "/":
                n.Push(oper1 / oper2);
                break;
        }
    }

}