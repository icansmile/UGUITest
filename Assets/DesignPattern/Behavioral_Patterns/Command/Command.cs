

/// <summary>
/// 命令模式 : 将 请求 封装成对象, 以便使用不同的请求,队列或者日志来参数化其他对象, 同时支持撤销操作
/// 
/// 优点:
/// 1.降低系统耦合度
/// 2.新的命令容易加入系统中
/// 缺点:
/// 1.可能有过多的命令= =???
/// 
/// 组件:
/// 抽象命令类 : 规定命令接口. Excute!   保存命令接收者(劳动者)的实例,用来Excute
/// 具体命令类
/// 命名接收类 : 实现具体操作
/// 调用者类 : 保存命令实例, 提供命令的设置接口, 还有命令的执行接口
/// </summary>
namespace Command
{

    //命令接受者
    class Receiver
    {
        public void Action()
        {

        }
    }

    //抽象命令类
    abstract class Command
    {
        //命令接受者
        protected Receiver receiver;

        public Command(Receiver _receiver)
        {
            receiver = _receiver;
        }

        public abstract void Excute();
    }

    //具体命名类
    class ConcreteCommand : Command
    {
        public ConcreteCommand(Receiver _receiver)
        :base(_receiver)
        {

        }

        public override void Excute()
        {
            receiver.Action();
        }
    }

    //调用者
    class Invoker
    {
        private Command command;

        public void SetCommand(Command _command)
        {
            this.command = _command;
        }

        public void ExcuteCommand()
        {
            command.Excute();
        }
    }
}