
/// <summary>
/// 桥接模式 : 将 抽象部分 和 实现部分 解耦,使得两者可以独立的变化
/// 就是将 可能会有多种变化的 实现方法 单独封装成一种对象. 然后抽象对象聚合这个方法对象. 实现的时候调用方法对象 的方法
/// 
/// 优点:
/// 1.抽象和实现分离
/// 2.容易扩展
/// 缺点:
/// 1.增加系统难度,不好理解
/// 
/// 组件:
/// 1.抽象 实现方法类 : 规定 实现方法类 需要实现的接口
/// 2.具体 实现方法类 : 实现具体的接口. 相当于API接口
/// 3.对用户的抽象类 : 包含一个实现方法类的实例, 并在接口中 使用 这个实现方法实例 来实现接口
/// 4.对用户的抽象类的具体类 : 继承自抽象类, 用于扩展(可有可无)
/// </summary>
namespace Bridge
{
    class Bridge
    {
        // 抽象实现者
        abstract class Implementor
        {
            public abstract void OperationImp();
        }

        // 具体实现者
        class ConcreteImplementorA : Implementor
        {
            public override void OperationImp()
            {

            }
        }

        class ConcreteImplementorB : Implementor
        {
            public override void OperationImp()
            {

            }
        }

        class Abstraction
        {
            //包含一个实现者实例(聚合,has-a), 使用时, 用具体的实现者对这个成员赋值
            public Implementor implementor;

            public virtual void Operation()
            {
                implementor.OperationImp();
            }
        }

        class RefinedAbstraction : Abstraction
        {
            public override void Operation()
            {
                implementor.OperationImp();
            }
        }
    }
}