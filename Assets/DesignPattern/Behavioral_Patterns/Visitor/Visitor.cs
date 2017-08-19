

/// <summary>
/// 访问者模式：封装一些作用于某种数据结构中的 各元素 的操作， 它可以在不改变数据结构的前提下定义作用于这些元素的新的操作。
/// 就是访问者定义一个操作某对象的方法， 被访问者定义一个接受访问者的方法，并且调用访问者的方法，表示允许访问者操作自己的实例了。
/// 其实就是把 对某个对象的一系列操作 封装起来(visit)，作为访问者。 被操作对象作为被访问者，有权决定允许不允许操作（accept).
/// 
/// 优点：
/// 1.符合单一职责原则
/// 2.优秀的扩展性
/// 3.灵活性
/// 缺点：
/// 1.具体元素对访问者公布细节，违反了迪米特法则。(不违反怎么操作？ 被访问者定义一堆操作接口？)
/// 2.具体元素变更比较困难（很多访问者使用？）
/// 3.违反了依赖倒置原则，依赖了具体类，没有依赖抽象（抽象访问者的锅吧）
/// </summary>
namespace Visitor
{
    //抽象访问者
    abstract class Visitor
    {
        public abstract void VisitConcreteElementA(ConcreteElementA concreteElementA);
        public abstract void VisitConcreteElementB(ConcreteElementB concreteElementB);
    }

    class ConcreteVisitor1 : Visitor
    {
        public override void VisitConcreteElementA(ConcreteElementA concreteElementA)
        {
            UnityEngine.Debug.Log(concreteElementA.GetType().Name + " visited by " + this.GetType().Name);
        }

        public override void VisitConcreteElementB(ConcreteElementB concreteElementB)
        {
            UnityEngine.Debug.Log(concreteElementB.GetType().Name + " visited by " + this.GetType().Name);
        }
    }

    class ConcreteVisitor2 : Visitor
    {
        public override void VisitConcreteElementA(ConcreteElementA concreteElementA)
        {
            UnityEngine.Debug.Log(concreteElementA.GetType().Name + " visited by " + this.GetType().Name);
        }

        public override void VisitConcreteElementB(ConcreteElementB concreteElementB)
        {
            UnityEngine.Debug.Log(concreteElementB.GetType().Name + " visited by " + this.GetType().Name);
        }
    }

    //被访问者
    abstract class Element
    {
        public abstract void Accept(Visitor visitor);
    }

    class ConcreteElementA : Element
    {
        public override void Accept(Visitor visitor)
        {
            visitor.VisitConcreteElementA(this);
        }
    }

    class ConcreteElementB : Element
    {
        public override void Accept(Visitor visitor)
        {
            visitor.VisitConcreteElementB(this);
        }
    }

    class ObjectStructure
    {
        private System.Collections.Generic.List<Element> elements = new System.Collections.Generic.List<Element>();

        public void Attach(Element element)
        {
            elements.Add(element);
        }

        public void Detach(Element element)
        {
            elements.Remove(element);
        }

        public void Accept(Visitor visitor)
        {
            foreach(Element e in elements)
            {
                e.Accept(visitor);
            }
        }
    }
}