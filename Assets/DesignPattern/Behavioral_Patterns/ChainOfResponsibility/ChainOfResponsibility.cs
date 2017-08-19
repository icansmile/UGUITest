

/// <summary>
/// 责任链模式 ： 使多个对象都有机会处理请求，从而避免了请求的发送者和接受者之间的耦合关系。 将这些对象连成一条链，并沿着这条链传递该请求，直到有对象处理它为止。
/// 就是接受者一环套一环， 上一环处理不了， 就传递给下一环去处理。
/// 
/// 优点：
/// 1.降低耦合度。将请求的发送者和接受者解耦。
/// 2.增强给对象指派职责的灵活性。通过改变链内成员或者调动他们的次序，允许动态的新增或者删除责任。
/// 3.增加新的请求处理类很方便。
/// 缺点：
/// 1.不能保证请求一定被接收。
/// 2.系统性能有影响。
/// 3.不易调试。
/// 
/// 组件：
/// 抽象接收者： 包含一个抽象接受者实例， 设置实例的接口， 处理请求的接口。
/// 具体接收者： 处理请求的时候， 先判断自己能否处理这个请求， 能的话处理完就结束， 否则传递给下个接受者
/// </summary>
namespace ChainOfResponsibility
{
    //抽象接收者
    abstract class Handler
    {
        protected Handler successor;

        public void SetSuccesser(Handler successor)
        {
            this.successor = successor;
        }

        public abstract void HandleRequest(int request);
    }

    //具体接收者
    class ConcreteHandler1 : Handler
    {
        public override void HandleRequest(int request)
        {
            if(request >= 0 && request < 10)
            {
                UnityEngine.Debug.Log(this.GetType().Name + " handler request " + request);
            }
            else
            {
                if(successor != null)
                {
                    successor.HandleRequest(request);
                }
            }
        }
    }

    class ConcreteHandler2 : Handler
    {
        public override void HandleRequest(int request)
        {
            if(request >= 10 && request < 20)
            {
                UnityEngine.Debug.Log(this.GetType().Name + " handler request " + request);
            }
            else
            {
                if(successor != null)
                {
                    successor.HandleRequest(request);
                }
            }
        }
    }

    class ConcreteHandler3 : Handler
    {
        public override void HandleRequest(int request)
        {
            if(request >= 20 && request < 30)
            {
                UnityEngine.Debug.Log(this.GetType().Name + " handler request " + request);
            }
            else
            {
                if(successor != null)
                {
                    successor.HandleRequest(request);
                }
            }
        }
    }
}