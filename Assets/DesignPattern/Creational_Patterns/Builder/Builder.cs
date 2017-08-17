using System.Collections.Generic;
/// <summary>
/// 建造者模式 : 将一个复杂对象(Product)的 构造(Builder) 与 表示(Product) 分离, 使同样的 构建过程(Director) 可以创建 不一样的表示(Product)
/// 封装实现过程 
///
/// 优点:
/// 1.控制细节
/// 缺点:
/// 1.如果内部变化复杂,则有很多建造类
/// 2.产品必须有共同点
///  
/// 组件:
/// 导演类 : 用具体建造类的接口, 建造产品. 控制一个抽象的建造步骤
/// 抽象建造类 : 规定具体建造类需要实现的建造步骤 , 和一个获取产品实例的接口
/// 具体建造类 : 实现具体建造步骤, 保存一个专属的产品实例, 并提供外部获取接口
/// 产品类 : 一个建造过程分步, 且复杂, 变种多的产品
/// </summary>
namespace Builder
{

    //过程导演类
    class Director
    {
        public void Construct(Builder builder)
        {
            builder.BuildPartA();
            builder.BuildPartB();
        }
    }

    //抽象构造类
    abstract class Builder
    {
        public abstract void BuildPartA();
        public abstract void BuildPartB();
        public abstract Product GetResult();
    }

    //具体构造
    class ConcreteBuilderA : Builder
    {
        private Product _product = new Product();

        public override void BuildPartA()
        {
            _product.Add("BuilderA-PartA");
        }

        public override void BuildPartB()
        {
            _product.Add("BuilderA-PartB");
        }

        public override Product GetResult()
        {
            return _product;
        }
    }

    //具体构造类
    class ConcreteBuilderB : Builder
    {
        private Product _product = new Product();

        public override void BuildPartA()
        {
            _product.Add("BuilderB-PartA");
        }

        public override void BuildPartB()
        {
            _product.Add("BuilderB-PartB");
        }

        public override Product GetResult()
        {
            return _product;
        }
    }

    //最终产品类
    class Product
    {
        private List<string> _parts = new List<string>();

        public void Add(string part)
        {
            _parts.Add(part);
        }

        public void Show()
        {
            UnityEngine.Debug.Log("Product======");
            foreach(var part in _parts)
            {
                UnityEngine.Debug.Log("Part " + part);
            }
            UnityEngine.Debug.Log("Product======");
        }
    }
}