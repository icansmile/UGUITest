

/// <summary>
/// 解释器模式 : 给定一门语言，定义它的文法的一种表示，并定义一个解释器，该解释器使用该表示来解释语言中的句子。
/// 就是有个奇怪的对象， 咱做个解释器，用来解释这个对象是个啥。 转换成可读的对象（比如蝙蝠， 是哺乳动物吗？是鸟类吗？）
/// 
/// 优点：
/// 1.可扩展性好，灵活
/// 2.增加了新的解释表达式的方式
/// 3.易于实现简单的问法
/// 缺点:
/// 1.可利用场景少
/// 
/// 组件：
/// 1.解释器：包含一个用来解释奇怪对象的接口， 接收奇怪的对象， 返回可读的对象（不一定是返回，反正让这个对象能间接的正常使用）
/// 2.奇怪的对象
/// </summary>
namespace Interpreter
{
    //求解释类
    class Context
    {

    }

    //解释器
    abstract class AbstractExpression
    {
        public abstract void Interpret(Context context);
    }

    class TerminalExpression : AbstractExpression
    {
        public override void Interpret(Context context)
        {
            UnityEngine.Debug.Log("Called Terminal.Interpret- context is >.<");
        }
    }


    class NonTerminalExpression : AbstractExpression
    {
        public override void Interpret(Context context)
        {
            UnityEngine.Debug.Log("Called NonTerminal.Interpret- context is = =!");
        }
    }
}