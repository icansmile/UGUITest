

/// <summary>
/// 享元模式 : 使用共享对象, 可有效支持大量的细粒度的对象
/// 优点:
/// 1.减少对象的创建,优化内存
/// 缺点:
/// 1.
/// 
/// 组件:
/// 共享对象
/// 享元工厂 : 包含一个hashtable用来存储生成过的共享对象的变量, 一个用来获取共享对象的接口,如果没有找到可用对象则生成一个
/// 
/// </summary>
namespace Flyweight
{
    //享元工厂
    class FlyweightFactory
    {
        private System.Collections.Hashtable flyweights = new System.Collections.Hashtable();

        public FlyweightFactory()
        {
            flyweights.Add("X", new ConcreteFlyweight());
            flyweights.Add("Y", new ConcreteFlyweight());
        }

        public Flyweight GetFlyweight(string key)
        {
            if(flyweights.ContainsKey(key))
                return (Flyweight)flyweights[key];
            else
            {
                Flyweight flyweight = new ConcreteFlyweight();
                flyweights.Add(key, flyweight);
                return flyweight;
            }
        }
    }

    //共享对象
    abstract class Flyweight
    {
    }

    class ConcreteFlyweight : Flyweight
    {

    }

    class UnsharedConcreteFlyweight : Flyweight
    {

    }
}