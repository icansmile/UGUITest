

/// <summary>
/// 备忘录模式 : 在不破坏封装性的前提下， 捕获一个对象的内部状态，并在该对象之外保存这个状态。这样以后就可将该对象恢复到原先保存的状态。
/// 其实主要还是依赖原对象的内容，原对象确认如果要恢复状态，什么需要保存，根据这个，去定义个备忘录用来保存这些状态，然后可能会有多个备忘，再做个管理类来管理他们。
/// 
/// 优点：
/// 1.当然是可恢复了
/// 缺点：
/// 1.当然是消耗资源来记录这些备忘录了
/// 
/// 组件：
/// 原对象 ： 你自己要做备忘录，需要实现生成备忘录的方法， 以及使用备忘录的方法
/// 备忘录 ： 根据原对象来定义的类，主要用来保存和读取状态。
/// 管理者 ： 管理备忘录，保存生成的所有备忘录，当然还得允许读取备忘录。
/// </summary>
namespace Memento
{
    //原对象
    class Originator
    {
        //主要备忘的成员
        private string state;

        public string State
        {
            get{ return state; }
            set
            {
                state = value;
                UnityEngine.Debug.Log("State = " + state);
            }
        }

        //生成备忘录
        public Memento CreateMemento()
        {
            return new Memento(state);
        }

        //根据备忘录，重置状态
        public void SetMemento(Memento memento)
        {
            UnityEngine.Debug.Log("Restoring state...");
            State = memento.State;
        }
    }

    //备忘录
    class Memento
    {
        private string state;

        public Memento(string state)
        {
            this.state = state;
        }

        public string State
        {
            get { return state; }
        }
    }

    //看管者
    class Caretaker
    {
        //被看管的备忘录，也可以做成列表
        private Memento memento;

        public Memento Memento
        {
            get { return memento; }
            set { memento = value; }
        }
    }
}