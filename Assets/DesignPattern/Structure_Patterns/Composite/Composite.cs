
/// <summary>
/// 组合模式 : 将对象组合成树形结构(Composite-_children)以表示"部分(component)-整体(_children)"的层次结构, 使得用户对单个对象(Component)和组合对象(Composite._children)的使用具有一致性
/// 某个类型的互相嵌套
/// 
/// 优点:
/// 1.高层模块调用简单
/// 2.节点增删灵活
/// 缺点:
/// 1.违反依赖倒置原则, 叶子和树枝的声明都是依赖具体实现类, 而不是抽象接口
/// 
/// 组件:
/// 1.组件抽象类 : 规定组件接口(也可不要,直接继承具体组件类)
/// 2.具体组件类 : 实现组件接口, 并且持有一个组件列表, 用来保存子节点
/// </summary>
namespace Composite
{
    // 组件抽象类
    abstract class Component
    {
        protected string name;

        public Component(string _name)
        {
            name = _name;
        }

        public abstract void Add(Component c);
        public abstract void Remove(Component c);
        public abstract void Display(int depth);
    }

    //树枝
    class Composite : Component
    {
        //关键, 保存子节点的列表
        private System.Collections.Generic.List<Component> _children = new System.Collections.Generic.List<Component>();

        public Composite(string _name)
        :base(_name)
        {

        }

        public override void Add(Component c)
        {
            _children.Add(c);
        }

        public override void Remove(Component c)
        {
            _children.Remove(c);
        }

        public override void Display(int depth)
        {
            UnityEngine.Debug.Log(new System.String('-', depth) + name);

            foreach(var c in _children)
            {
                c.Display(depth + 2);
            }
        }
    }

    //叶子
    class Leaf : Component
    {
        public Leaf(string _name)
        :base(_name)
        {

        }

        public override void Add(Component c)
        {
            UnityEngine.Debug.Log("叶节点无法添加子节点");
        }

        public override void Remove(Component c)
        {
            UnityEngine.Debug.Log("叶节点没有子节点");
        }
        
        public override void Display(int depth)
        {
            UnityEngine.Debug.Log(new System.String('-', depth) + name);
        }
    }
}