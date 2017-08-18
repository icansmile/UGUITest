

/// <summary>
/// 状态模式 ： 当一个对象(Context)内在状态(State)改变时，允许其改变行为(Request)，这个对象(Context)看起来像改变了其类
/// 就是因为Context里面存在着 State 实例，而State实例又会不断的改变，导致Request方法是由不同的State执行的， 所以看起来就像是类变了吧
/// 
/// 优点：
/// 1.封装转换规则， 替换了复杂的条件分支语句
/// 2.方便增加状态
/// 缺点：
/// 1.增加类和对象个数（= =大都设计模式都会有这个问题）
/// 2.对开闭原则支持不好， 增加新状态， 需要对负责状态转换的代码进行修改
/// 
/// 组件：
/// 抽象状态类 ：规定接口， 有个接口用于该状态内的表现，和转换条件，以及进行状态转换（需要Context）
/// 具体状态类
/// 带有状态的类
/// </summary>
namespace State
{
    //抽象状态类
    abstract class State
    {
        public abstract void Handle(Context context);
    }

    class ConcreteStateA : State
    {
        //A状态时执行的操作 （切换为B状态）, 即转换规则 也是写在这里
        public override void Handle(Context context)
        {
            context.State = new ConcreteStateB();
        }
    }


    class ConcreteStateB : State
    {
        public override void Handle(Context context)
        {
            context.State = new ConcreteStateA();
        }
    }

    //带有状态的类
    class Context
    {
        //当前状态
        private State _state;

        public Context(State _state)
        {
            this._state = _state;
        }

        public State State
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
            }
        }

        //执行当前状态对应的方法
        public void Request()
        {
            State.Handle(this);
        }
    }
}