
/// <summary>
/// 外观模式 : 要求一个子系统的外部与其内部的通信必须通过一个统一的对象进行. 外观模式提供一个高层次的接口, 使得子系统更易于使用.
/// 
/// 优点:
/// 1.减少系统间的相互依赖
/// 2.提高灵活性
/// 3.提高安全性
/// 缺点:
/// 1.违反开闭原则,如果要改东西,继承和重写都不合适
/// 
/// 组件:
/// 统一对象 : 持有多个子系统的实例, 提供调用子系统功能的接口
/// 子系统
/// </summary>
namespace Facade
{
    //子系统1
    class SubSystem1
    {
        public void Method1()
        {

        }
    }


    //子系统2
    class SubSystem2
    {
        public void Method2()
        {
            
        }
    }


    //子系统3
    class SubSystem3
    {
        public void Method3()
        {
            
        }
    }

    //统一对象
    class Facade
    {
        private SubSystem1 subSystem1;
        private SubSystem2 subSystem2;
        private SubSystem3 subSystem3;

        public Facade()
        {
            subSystem1 = new SubSystem1();
            subSystem2 = new SubSystem2();
            subSystem3 = new SubSystem3();
        }

        public void MethodA()
        {
            subSystem1.Method1();
            subSystem2.Method2();
        }

        public void MethodB()
        {
            subSystem2.Method2();
            subSystem3.Method3();
        }
    }
}