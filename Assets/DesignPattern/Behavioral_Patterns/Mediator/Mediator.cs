

/// <summary>
/// 中介者模式 : 用一个中介对象封装一系列的对象交互， 中介者使各对象不需要显式的相互作用， 从而使其耦合松散， 而且可以独立的改变他们之间的交互。
/// 就是对象间原本是耦合的，我要和你交互，我必须得有你的实例。 使用了中介者以后， 对象间不再互相有用对方的实例，而是统一交由中介者保存和使用。
/// 当然，这样中介者会变得复杂了。
/// 
/// 
/// 优点：
/// 1.降低类的复杂度，将一对多转化成了一对一
/// 2.各个类之间的解耦
/// 3.符合迪米特法则(最少知道原则， 一个对象应当对其他对象有尽可能少的了解)
/// 缺点：
/// 1.中介者会庞大，变得难以维护
/// 
/// 组件
/// 中介者：包含用户对象的实例，以及有关对象间交互的接口（由对象间的互相调用，变成由中介者统一管理）
/// 互相交互的对象：包含中介者实例，调用中介者的方法， 以及被调用的方法（反正就是原本该交互的接口还是有，只是需要用到其他对象的地方，都幻城通过调用中介者来实现）
/// </summary>
namespace Mediator
{
    //中介者
    abstract class Mediator
    {
        public abstract void Send(string msg, Colleague colleague);
    }

    class ConcreteMediator : Mediator
    {
        public ConcreteColleague1 _colleague1
        {
            private get;
            set;
        }

        public ConcreteColleague1 _colleague2
        {
            private get;
            set;
        }

        public override void Send(string msg, Colleague colleague)
        {
            if(colleague == _colleague1)
            {
                _colleague2.Notify(msg);
            }
            else
            {
                _colleague1.Notify(msg);
            }
        }
    }

    //存在互相交互的对象
    abstract class Colleague
    {
        protected Mediator mediator;

        public Colleague(Mediator mediator)
        {
            this.mediator = mediator;
        }
    }

    class ConcreteColleague1 : Colleague
    {
        public ConcreteColleague1(Mediator mediator)
        :base(mediator)
        {

        }

        //input
        public void Send(string msg)
        {
            mediator.Send(msg, this);
        }

        //show ui
        public void Notify(string msg)
        {
            UnityEngine.Debug.Log("Colleague1 gets message: " + msg);
        }
    }

    class ConcreteColleague2 : Colleague
    {
        public ConcreteColleague2(Mediator mediator)
        :base(mediator)
        {

        }

        //input
        public void Send(string msg)
        {
            mediator.Send(msg, this);
        }

        //show ui
        public void Notify(string msg)
        {
            UnityEngine.Debug.Log("Colleague2 gets message: " + msg);
        }
    }
}