

/// <summary>
/// 迭代器模式 : 提供一种方法访问一个容器对象中的各个元素，而又不暴露该对象的内部细节。
/// 其实就是对容器再进行一层封装啦，用户不能对容器直接操作了，得先经过迭代器（用户没有容器实例）,容器提供获取迭代器的接口。
/// 
/// 优点：
/// 1.支持以不同的方式遍历一个聚合对象
/// 2.迭代器简化了聚合类
/// 3.在同一个聚合上可以有多个遍历（= =？？？）
/// 
/// 缺点：
/// 1.由于迭代器模式将 存储数据 和 遍历数据的职责 分离，所以增加新的聚合类需要对应的增加新的迭代器类，所以类会增多。
/// 
/// 组件：
/// 抽象聚合类 ： 规定必须有生成迭代器的接口啊
/// 抽象迭代器 ： 规定迭代器该有的功能接口。 当前，下一个 之类的
/// 具体聚合类 ： 包含一个 存储数据的集合， 可以做个索引器 供迭代器使用
/// 具体迭代器 ： 保存一个对应的具体聚合类的实例， 然后就是对这个实例的一顿有关容器的封装并提供外部访问接口。因此自己也要保存一个计数变量。
/// </summary>
namespace Iterator
{

    //抽象集合类
    abstract class Aggregate
    {
        //生成迭代器
        public abstract Iterator CreateIterator();
    }

    //抽象迭代器
    abstract class Iterator
    {
        public abstract object First();
        public abstract object Next();
        public abstract bool IsDone();
        public abstract object Current();
    }

    //具体集合类
    class ConcreteAggregate : Aggregate
    {
        //存储集合列表
        private System.Collections.ArrayList _items = new System.Collections.ArrayList();

        //生成对应的具体迭代器
        public override Iterator CreateIterator()
        {
            return new ConcreteIterator(this);
        }

        public int Count
        {
            get { return _items.Count; }
        }

        //索引器
        public object this[int index]
        {
            get
            {
                return _items[index];
            }

            set
            {
                _items.Insert(index, value);
            }
        }
    }

    //具体迭代器
    class ConcreteIterator : Iterator
    {
        //具体集合类实例
        private ConcreteAggregate aggregate;
        //当前索引
        private int current;

        public ConcreteIterator(ConcreteAggregate aggregate)
        {
            this.aggregate = aggregate;
        }

        //第一个
        public override object First()
        {
            return aggregate[0];
        }

        //下一个
        public override object Next()
        {
            object ret = null;
            if(current < aggregate.Count - 1)
            {
                ret = aggregate[++current];
            }
            return ret;
        }

        public override bool IsDone()
        {
            return current >= aggregate.Count;
        }

        public override object Current()
        {
            return aggregate[current];
        }
    }
}