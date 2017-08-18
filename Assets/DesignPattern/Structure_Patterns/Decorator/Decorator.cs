
/// <summary>
/// 装饰模式 : 动态的给一个对象添加一些额外的职责.就增加功能来说, 装饰模式比直接继承更加灵活.
/// 装饰类继承组件类, 实际上还是个组件, 但是拥有原组件的实例, 并借此来扩展, 重写接口. 和继承功能差不多, 但是装饰模式可以叠加多个装饰类, 变相的动态继承
/// 
/// 优点:
/// 1.装饰类 和 被装饰类 可以独立发展, 不会互相耦合. 装饰模式是继承的一个替代模式, 用来动态扩展一个实现类的功能
/// 缺点:
/// 1.多层装饰会变得复杂
/// 
/// 组件:
/// 抽象被装饰类 : 规定被装饰类和装饰类要实现的接口
/// 具体被装饰类 : 实现原生接口
/// 抽象装饰类   : 规定装饰类内容, 继承自 抽象被装饰类! 并增加 被修饰类 的实例变量, 以及设置 被修饰类实例 的接口
/// 具体装饰类   : 具体实现要扩展的接口
/// 
/// </summary>
namespace Decorator
{
    //抽象类
    abstract class Component
    {
        public abstract void Operation();
    }

    //具体类
    class ConcreteComponent : Component
    {
        public override void Operation()
        {

        }
    }

    //抽象装饰类
    abstract class Decorator : Component
    {
        protected Component component;

        public void SetComponent(Component c)
        {
            component = c;
        }

        public override void Operation()
        {
            if(component != null)
                component.Operation();
        }
    }

    //具体装饰类A  ConcreteDecoratorA.SetComponent(ConcreteComponent);
    class ConcreteDecoratorA : Decorator
    {
        public override void Operation()
        {
            base.Operation();
            UnityEngine.Debug.Log("A Operation");
        }
    }

    //具体装饰类B  ConcreteDecoratorB.SetComponent(ConcreteDecoratorA);
    class ConcreteDecoratorB : Decorator
    {
        public override void Operation()
        {
            base.Operation();
            UnityEngine.Debug.Log("A Operation");
        }
    }
}