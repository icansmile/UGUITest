

/// <summary>
/// 代理模式 : 为其他对象提供一种代理(Proxy),以控制对这个对象(ConcreteSubject)的访问
/// 大致就是创建一个新类, 将原类型重新封装
/// 
/// 优点:
/// 1.职责清晰
/// 2.高扩展化
/// 缺点:
/// 1.增加了中间层,可能影响效率
/// 
/// 组件:
/// 抽象对象 : 规定要实现的接口
/// 具体对象 : 实现接口
/// 代理对象 : 继承抽象类, 并包含一个具体类的实例, 实现抽象类的接口, 对具体类的使用重新封装
/// </summary>
namespace Proxy
{

    //抽象对象
    abstract class Subject
    {
        public abstract void Request();
    }

    //具体对象
    class ConcreteSubject : Subject
    {
        public override void Request()
        {

        }
    }

    //代理
    class Proxy : Subject
    {
        private ConcreteSubject concreteSubject;
        public override void Request()
        {
            if(concreteSubject == null)
                concreteSubject = new ConcreteSubject();

            concreteSubject.Request();
        }
    }
}