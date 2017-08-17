
/// <summary>
/// 适配器模式 : 将一个类(Target)的接口(Request)转换(Adapter.Request)成客户希望的另一个接口(NewRequest)。适配器模式使得原本由于接口不兼容而不能一起工作的类可以一起工作。
/// 就是把新方法封装进（调用）旧方法的接口
/// 
/// 优点：
/// 1.可以让任何两个没有关联的类一起运行
/// 2.提高了类的复用
/// 缺点：
/// 1.过多使用，会使系统变得不清晰， 一个接口内可能调用了其他类的接口
/// 
/// 组件
/// 原类型：已经正常运行的类型
/// 新类型：新开发的类型，包含了新方法，并且接口和原类型接口不一致
/// 适配器：一个中间层，和原类型是同一个类型（继承原类型，或者共同父类），在内部存有新类型的实例，并用这个实例来实现接口
/// </summary>
namespace Adapter
{
	//原类型
	class Target
	{
		//原方法
		public virtual void Request()
		{
		}
	}

	//适配器
	class Adapter : Target
	{
		private Adaptee _adaptee = new Adaptee();

		//用原方法的接口 调用 新方法的 相同功能 的接口
		public override void Request()
		{
			_adaptee.NewRequest();
		}
	}

	//新类型
	class Adaptee
	{
		public void NewRequest()
		{

		}
	}
}
