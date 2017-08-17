
/// <summary>
/// 简单工厂模式 -> 工厂模式 -> 抽象工厂模式
/// 简单工厂模式通过传参数,由工厂类直接判断返回具体产品; 工厂模式由工厂类的子工厂类来返回具体产品,想比简单工厂模式, 符合了开闭原则; 抽象工厂模式中, 在工厂模式的基础上, 工厂可以生产多种产品, 并且多种产品中存在联系(一个系列或者品牌)
/// 抽象工厂模式 : 提供一个接口(client中不同类型的的AbstractFactory), 用于创建相关或者依赖对象(Product)的家族(ConcreteFactory), 而不需要指定具体类(如ProductA1)
/// 
/// 
/// 优点 : 当一个产品族的多个对象被设计成一起工作时, 能保证系统中用的都是同一个产品租
/// 缺点 : 扩展麻烦, 要增加一个系列的多种产品
/// 
/// 组件:
/// 抽象工厂类 : 定义具体工厂类需要提供的产品生成接口
/// 具体工厂类 : 实现具体产品的生成接口, 返回抽象产品类
/// 抽象产品类 : 定义某种产品内容
/// 具体产品类 : 实现具体产品内容
/// </summary>
namespace AbstractFactory
{

	public abstract class AbstractFactory
	{
		public abstract AbstractProductA CreateProductA();
		public abstract AbstractProductB CreateProductB();
	}

	public class ConcreteFacory1 : AbstractFactory
	{
		public override AbstractProductA CreateProductA()
		{
			return new ProductA1();
		}

		public override AbstractProductB CreateProductB()
		{
			return new ProductB1();
		}
	}

	public class ConcreteFacory2 : AbstractFactory
	{
		public override AbstractProductA CreateProductA()
		{
			return new ProductA2();
		}

		public override AbstractProductB CreateProductB()
		{
			return new ProductB2();
		}
	}

	//抽象产品类A
	public abstract class AbstractProductA
	{

	}

	//抽象产品B
	public abstract class AbstractProductB
	{

	}

	//具体产品A-1
	public class ProductA1 : AbstractProductA
	{

	}

	//具体产品A-2
	public class ProductA2 : AbstractProductA
	{

	}

	//具体产品B-1
	public class ProductB1 : AbstractProductB
	{

	}

	//具体产品B-2
	public class ProductB2 : AbstractProductB
	{

	}

	public class Client
	{
		public AbstractProductA productA;
		public AbstractProductB productB;

		public Client(AbstractFactory factory)
		{
			productA = factory.CreateProductA();
			productB = factory.CreateProductB();
		}
	}

}
