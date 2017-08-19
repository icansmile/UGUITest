

/// <summary>
/// 模板方法模式 : 定义一个操作中的算法的框架，而将一些步骤延迟到子类中。使得子类可以不改变一个算法的结构即可重定义该算法的某些特定步骤。
/// 
/// 优点：
/// 1.封装不变部分，扩展可变部分！
/// 2.提取公共代码，便于维护！
/// 3.行为由父类控制，子类实现。
/// 缺点：
/// 1.每一个不同的实现都需要一个子类来实现，导致类增多（不可避免）
/// 
/// 组件：
/// 抽象模板类：规定具体类要实现的方法接口， 设定好执行方法的步骤（方法的实现交由子类去做）
/// 具体方法类：实现具体方法（当然是可变的部分）
/// </summary>
namespace Template
{
    //模板
    abstract class AbstractClass
    {
        public abstract void PrimitiveOperation1();
        public abstract void PrimitiveOperation2();

        //模板方法
        public void TemplateMethod()
        {
            PrimitiveOperation1();
            PrimitiveOperation2();
        }
    }

    class ConcreteClassA : AbstractClass
    {
        public override void PrimitiveOperation1()
        {
            UnityEngine.Debug.Log("ConcreteClassA.PrimitiveOperation1");
        }

        public override void PrimitiveOperation2()
        {
            UnityEngine.Debug.Log("ConcreteClassA.PrimitiveOperation2");
        }
    }

    class ConcreteClassB : AbstractClass
    {
        public override void PrimitiveOperation1()
        {
            UnityEngine.Debug.Log("ConcreteClassB.PrimitiveOperation1");
        }

        public override void PrimitiveOperation2()
        {
            UnityEngine.Debug.Log("ConcreteClassB.PrimitiveOperation2");
        }
    }
}