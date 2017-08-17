
/// <summary>
/// 原型模式 : 用原型实例指定创建对象的种类,并通过拷贝这些原型 从而 创建新的对象
/// 主要是通过clone来避免构造函数可能会做的不必要的操作, 优化新对象的创建性能
/// 
/// 优点:
/// 1.提高性能, 不用走构造函数
/// 缺点:
/// 1.要考虑是否符合需求, 比如浅拷贝或者深拷贝
/// 
/// 组件:
/// 原型抽象类 : 规定必须实现 Clone 接口
/// 具体原型类
/// </summary>
namespace Prototype
{
    //原型类抽象
    abstract class Prototype
    {
        private string _id;
        public string Id
        {
            get
            {
                return _id;
            }
        }

        public Prototype(string id)
        {
            _id = id;
        }

        public abstract Prototype Clone();
    }

    class ConcretePrototypeA : Prototype
    {
        public ConcretePrototypeA(string id)
        :base(id)
        {

        }

        public override Prototype Clone()
        {
            return (ConcretePrototypeA)this.MemberwiseClone();
        }
    }
}