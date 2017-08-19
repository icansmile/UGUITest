
/// <summary>
/// 观察者模式 ：定义对象间一种一对多的依赖关系，使得每当一个对象改变状态，则所有依赖于它的对象都会得到通知并被自动更新。
/// 重点在于通知者， 拥有观察者的列表， 以及观察者有一个对外的通知接口。类似Events拥有delegate的列表，并且delegate本身就是作为一个方法可以被调用。
/// 
/// 优点：
/// 1.观察者和通知者是抽象耦合的
/// 2.建立了一套触发机制
/// 缺点：
/// 1.观察者过多，性能消耗
/// 
/// 组件：
/// 抽象通知者 ： 包含 观察者列表，添加和移除观察者的接口， 通知观察者更新的接口（调用观察者的接口）
/// 具体通知类 ： 具体实现呗
/// 抽象观察者 ： 提供一个响应通知的接口， 给通知者调用。是否存有通知者引用，应该根据实际需求来就行了。
/// 具体观察者 ： 具体实现呗
/// </summary>
namespace Observer
{
    //观察者
    abstract class Observer
    {
        protected Subject subject;
        public abstract void Update();
    }

    class ConcreteObserver : Observer
    {
        public ConcreteObserver(Subject _subject)
        {
            this.subject = _subject;
            _subject.Attach(this);
        }

        public override void Update()
        {
        }
    }

    //通知者
    abstract class Subject
    {
        private System.Collections.Generic.List<Observer> _observers = new System.Collections.Generic.List<Observer>();

        public void Attach(Observer _observer)
        {
            _observers.Add(_observer);
        }

        public void Detach(Observer _observer)
        {
            _observers.Remove(_observer);
        }

        public void Notify()
        {
            foreach(var o in _observers)
            {
                o.Update();
            }
        }
    }

    class ConcreteSubject : Subject
    {
        private string _subjectState;

        public string SubjectState
        {
            get
            {
                return _subjectState;
            }
            set
            {
                _subjectState = value;
            }
        }
    }
}