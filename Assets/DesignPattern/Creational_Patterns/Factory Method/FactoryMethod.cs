
/// <summary>
/// 工厂模式 : 定义一个用于创建对象的接口(CreateProduct), 让子类(ConcreteFactory)决定实例化哪个类(Product).比起简单工厂模式,工厂方法模式使一个类(Product)的实例化推迟到子类, 符合开闭原则
/// 
/// 优点 : 
/// 1.封装产品的具体实现,调用者只关心调用接口
/// 2.扩展性高
/// 缺点:
/// 1.类的数量增多, 同时依赖也就增多了
/// 
/// 组件:
/// 1.抽象工厂类,规定一个返回产品类的接口
/// 2.具体工厂类,实现接口返回具体产品实例
/// 3.抽象产品类
/// 4.具体产品类
/// </summary>
namespace FactoryMethod
{
    //抽象工厂类
    public abstract class AbstractFactory
    {
        public abstract AbstractProtuct CreateProduct();
    }

    //具体工厂类
    public class ConcreteFactoryA : AbstractFactory
    {
        public override AbstractProtuct CreateProduct()
        {
            return new ProductA();
        }
    }

    //具体工厂类
    public class ConcreteFactoryB : AbstractFactory
    {
        public override AbstractProtuct CreateProduct()
        {
            return new ProductB();
        }
    }

    //抽象产品类
    public abstract class AbstractProtuct
    {
    }

    //具体产品类
    public class ProductA : AbstractProtuct
    {

    }

    //具体产品类
    public class ProductB : AbstractProtuct
    {

    }
}
