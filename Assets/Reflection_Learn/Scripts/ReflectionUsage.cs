using System;
using System.Reflection;

/// <summary>
/// 反射: 运行时获取类型信息.
/// 
/// !!! - 程序集(Assembly)包含模块(Module)，而模块包含类型(Type)，类型又包含成员(Member)。
/// !!! - 反射则提供了封装程序集、模块和类型的对象。
/// !!! - 您可以使用反射动态地创建类型的实例，将类型绑定到现有对象，或从现有对象中获取类型。然后，可以调用类型的方法或访问其字段和属性。
/// 
/// 
/// 总结 http://www.cnblogs.com/wangshenhe/p/3256657.html
/// 插件的制作与应用 http://962410314.blog.51cto.com/7563109/1413393/
/// </summary>

// （1）使用Assembly定义和加载程序集，加载在程序集清单中列出模块，以及从此程序集中查找类型并创建该类型的实例。 
// （2）使用Module了解包含模块的程序集以及模块中的类等，还可以获取在模块上定义的所有全局方法或其他特定的非全局方法。 
// （3）使用ConstructorInfo了解构造函数的名称、参数、访问修饰符（如pulic 或private）和实现详细信息（如abstract或virtual）等。使用Type的GetConstructors或 GetConstructor方法来调用特定的构造函数。 
// （4）使用MethodInfo了解方法的名称、返回类型、参数、访问修饰符（如pulic 或private）和实现详细信息（如abstract或virtual）等。使用Type的GetMethods或GetMethod方法来调用特定的方法。 
// （5）使用FiedInfo了解字段的名称、访问修饰符（如public或private）和实现详细信息（如static）等，并获取或设置字段值。 
// （6）使用EventInfo了解事件的名称、事件处理程序数据类型、自定义属性、声明类型和反射类型等，添加或移除事件处理程序。 
// （7）使用PropertyInfo了解属性的名称、数据类型、声明类型、反射类型和只读或可写状态等，获取或设置属性值。 
// （8）使用ParameterInfo了解参数的名称、数据类型、是输入参数还是输出参数，以及参数在方法签名中的位置等。


// 创建实例
// 1、System.Activator 的CreateInstance方法。该方法返回新对象的引用。具体使用方法参见msdn
// 2、System.Activator 的createInstanceFrom 与上一个方法类似，不过需要指定类型及其程序集

// 3、System.Appdomain 的方法：createInstance,CreateInstanceAndUnwrap,CreateInstranceFrom和CreateInstraceFromAndUnwrap

// 4、System.type的InvokeMember实例方法：这个方法返回一个与传入参数相符的构造函数，并构造该类型。

// 5、System.reflection.constructinfo 的Invoke实例方法

public class ReflectionUsage
{
    private const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Static;

    public object CreateInstance(string assemblyName, string typeName)
    {
        object instance;

        Assembly ass = Assembly.Load(assemblyName);
        Type t = ass.GetType(typeName);
        instance = System.Activator.CreateInstance(t, "pri", "pub");

        return instance;
    }
    //获取成员
    public string GetMemberInfo()
    {
        System.Text.StringBuilder res = new System.Text.StringBuilder();
        Type t = typeof(ReflectionTestClass);
        MemberInfo[] memInfo = t.GetMembers(bindingFlags);
        foreach(MemberInfo m in memInfo)
        {
            res.AppendFormat("Name- {0}. Type- {1}\n", m.Name, m.MemberType);
        }

        return res.ToString();
    }

    /// <summary>
    /// 获取字段
    /// </summary>
    /// <param name="instance"></param>
    /// <returns></returns>
    public string GetFieldInfo(object instance)
    {
        System.Text.StringBuilder res = new System.Text.StringBuilder();

        Type t = instance.GetType();
        FieldInfo[] fieldInfo = t.GetFields(bindingFlags);
        foreach(FieldInfo f in fieldInfo)
        {
            res.AppendFormat("Name- {0}.\t\t\tType- {1}\t Value- {2}\n", f.Name, f.FieldType, f.GetValue(instance));
        }

        return res.ToString();
    }

    /// <summary>
    /// 设置实例字段值
    /// </summary>
    /// <param name="instance"></param>
    /// <param name="name"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public string SetFieldValue(object instance, string name, object value)
    {
        System.Text.StringBuilder res = new System.Text.StringBuilder();

        Type t = instance.GetType();
        
        FieldInfo f = t.GetField(name, bindingFlags);
        f.SetValue(instance, value);

        res.AppendFormat("Name- {0}.\t\t\tType- {1}\t Value- {2}\n", f.Name, f.FieldType, f.GetValue(instance));

        return res.ToString();
    }

    /// <summary>
    /// 获取属性
    /// 先通过GetProperty获取属性, 再通过 GetGetMethod, GetSetMethod分别获取 读取 和 设置 方法.
    /// </summary>
    /// <param name="instance"></param>
    /// <returns></returns>
    public string GetPropertyInfo(object instance)
    {
        System.Text.StringBuilder res = new System.Text.StringBuilder();

        Type t = instance.GetType();

        PropertyInfo[] propertyInfo = t.GetProperties(bindingFlags);
        foreach(PropertyInfo p in propertyInfo)
        {
            MethodInfo mthInfo = p.GetGetMethod(true);
            res.AppendFormat("Name- {0}.\t\t\tType- {1}\t \n", mthInfo.Name, mthInfo.ReturnType);

            mthInfo = p.GetSetMethod(true);
            res.AppendFormat("Name- {0}.\t\t\tType- {1}\t \n", mthInfo.Name, mthInfo.ReturnType);
        }

        return res.ToString();
    }

    public string GetPropertyValue(object instance, string name)
    {
        System.Text.StringBuilder res = new System.Text.StringBuilder();

        Type t = instance.GetType();

        PropertyInfo prptyInfo = t.GetProperty(name);
        MethodInfo mthInfo = prptyInfo.GetGetMethod();

        res.AppendFormat("Name- {0}.\t\t\tValue- {1}\t \n", mthInfo.Name, mthInfo.Invoke(instance, null));

        return res.ToString();
    }

    public string SetPropertyValue(object instance, string name, object value)
    {
        System.Text.StringBuilder res = new System.Text.StringBuilder();

        Type t = instance.GetType();

        PropertyInfo prptyInfo = t.GetProperty(name);
        MethodInfo mthInfo = prptyInfo.GetSetMethod();
        mthInfo.Invoke(instance, new object[]{value});

        mthInfo = prptyInfo.GetGetMethod(true);

        res.AppendFormat("Name- {0}.\t\t\tValue- {1}\t \n", mthInfo.Name, mthInfo.Invoke(instance, null));
        return res.ToString();
    }
}