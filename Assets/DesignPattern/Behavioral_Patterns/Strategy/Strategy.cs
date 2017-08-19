

/// <summary>
/// 策略模式 ： 定义一组算法，将每个算法都封装起来，并且使他们之间可以互换
/// 首先是利用多态，再来主题是封装算法
/// 
/// 优点：
/// 1.算法可以自由切换
/// 2.避免使用多重条件判断（ = =？？）
/// 3.扩展性良好（多态）
/// 缺点：
/// 1.所有策略类都要对外暴露（= =不然怎么替换）
/// 2.策略类会增多（废话）
/// </summary>
namespace Strategy
{

    //策略抽象类
    abstract class Strategy
    {
        public abstract void AlgorithmInterface();
    }

    class ConcreteStrategyA : Strategy
    {
        public override void AlgorithmInterface()
        {
            UnityEngine.Debug.Log("COncreteStrategyA.AlgorithmInterface()");
        }
    }

    class ConcreteStrategyB : Strategy
    {
        public override void AlgorithmInterface()
        {
            UnityEngine.Debug.Log("COncreteStrategyB.AlgorithmInterface()");
        }
    }

    class ConcreteStrategyC : Strategy
    {
        public override void AlgorithmInterface()
        {
            UnityEngine.Debug.Log("COncreteStrategyC.AlgorithmInterface()");
        }
    }

    //策略使用类
    class Context
    {
        private Strategy strategy;

        public Context(Strategy strategy)
        {
            this.strategy = strategy;
        }

        public void ContextInterface()
        {
            strategy.AlgorithmInterface();
        }
    }
}