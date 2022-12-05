### C# 反射

反射指程序可以访问、检测和修改它本身状态或行为的一种能力。

程序集包含模块，而模块包含类型，类型又包含成员。反射则提供了封装程序集、模块和类型的对象。

您可以使用反射动态地创建类型的实例，将类型绑定到现有对象，或从现有对象中获取类型。然后，可以调用类型的方法或访问其字段和属性。

#### 优缺点

##### 优点

1. 反射提高了程序的灵活性和扩展性。  
2. 降低耦合性，提高自适应能力。  
3. 它允许程序创建和控制任何类的对象，无需提前硬编码目标类。

##### 缺点

1. 性能问题：使用反射基本上是一种解释操作，用于字段和方法接入时要远慢于直接代码。因此反射机制主要应用在对灵活性和拓展性要求很高的系统框架上，普通程序不建议使用。

2. 使用反射会模糊程序内部逻辑；程序员希望在源代码中看到程序的逻辑，反射却绕过了源代码的技术，因而会带来维护的问题，反射代码比相应的直接代码更复杂。

### 一. 反射（Reflection）的用途

*反射（Reflection）有下列用途：*

1. 它允许在运行时查看特性（attribute）信息。

2. 它允许审查集合中的各种类型，以及实例化这些类型。

3. 它允许延迟绑定的方法和属性（property）。

4. 它允许在运行时创建新类型，然后使用这些类型执行一些任务。

> 1. 使用Assembly定义和加载程序集，加载在程序集清单中列出模块，以及从此程序集中查找类型并创建该类型的实例。
> 2. 使用Module了解包含模块的程序集以及模块中的类等，还可以获取在模块上定义的所有全局方法或其他特定的非全局方法。
> 3. 使用ConstructorInfo了解构造函数的名称、参数、访问修饰符（如public 或private）和实现详细信息（如abstract或virtual）等。
> 4. 使用MethodInfo了解方法的名称、返回类型、参数、访问修饰符（如public 或private）和实现详细信息（如abstract或virtual）等。
> 5. 使用FieldInfo了解字段的名称、访问修饰符（如public或private）和实现详细信息（如static）等，并获取或设置字段值。
> 6. 使用EventInfo了解事件的名称、事件处理程序数据类型、自定义属性、声明类型和反射类型等，添加或移除事件处理程序。
> 7. 使用PropertyInfo了解属性的名称、数据类型、声明类型、反射类型和只读或可写状态等，获取或设置属性值。
> 8. 使用ParameterInfo了解参数的名称、数据类型、是输入参数还是输出参数，以及参数在方法签名中的位置等。
>
### 二. 查看元数据(MetaData)

我们已经在上面的章节中提到过，使用反射（Reflection）可以查看特性（attribute）信息。

*System.Reflection*类的 *MemberInfo* 对象需要被初始化，用于发现与类相关的特性（attribute）。为了做到这点，您可以定义目标类的一个对象，如下：

```Csharp
System.Reflection.MemberInfo info = typeof(MyClass);
```

下面的程序演示了这点：

```Csharp
      using System;
      [AttributeUsage(AttributeTargets.All)]
      public class HelpAttribute : System.Attribute
      {
         public readonly string Url;
         public string Topic  // Topic 是一个命名（named）参数
         {
            get
            {
               return topic;
            }
            set
            {
               topic = value;
            }
         }
         public HelpAttribute(string url)  // url 是一个定位（positional）参数
         {
            this.Url = url;
         }
         private string topic;
      }
      [HelpAttribute("Information on the class MyClass")]
      class MyClass
      {
      }
      namespace AttributeAppl
      {
         class Program
         {
            static void Main(string[] args)
            {
               System.Reflection.MemberInfo info = typeof(MyClass);
               object[] attributes = info.GetCustomAttributes(true);
               for (int i = 0; i < attributes.Length; i++)
               {
                  System.Console.WriteLine(attributes[i]);
               }
               Console.ReadKey();
            }
         }
      }
```

当上面的代码被编译和执行时，它会显示附加到类 MyClass 上的自定义特性：

```Output
Output--> HelpAttribute
```

#### 二.一. 实例

*在本实例中，我们将使用在上一章中创建的 DeBugInfo 特性，并使用反射（Reflection）来读取 Rectangle 类中的元数据。*

```Csharp  
      using System;
      using System.Reflection;
      namespace BugFixApplication
      {
         // 一个自定义特性 BugFix 被赋给类及其成员
         [AttributeUsage(AttributeTargets.Class |
         AttributeTargets.Constructor |
         AttributeTargets.Field |
         AttributeTargets.Method |
         AttributeTargets.Property,
         AllowMultiple = true)]
         public class DeBugInfo : System.Attribute
         {
            private int bugNo;
            private string developer;
            private string lastReview;
            public string message;
            public DeBugInfo(int bg, string dev, string d)
            {
               this.bugNo = bg;
               this.developer = dev;
               this.lastReview = d;
            }
            public int BugNo
            {
               get
               {
                  return bugNo;
               }
            }
            public string Developer
            {
               get
               {
                  return developer;
               }
            }
            public string LastReview
            {
               get
               {
                  return lastReview;
               }
            }
            public string Message
            {
               get
               {
                  return message;
               }
               set
               {
                  message = value;
               }
            }
         }
         [DeBugInfo(45, "Zara Ali", "12/8/2012",
          Message = "Return type mismatch")]
         [DeBugInfo(49, "Nuha Ali", "10/10/2012",
          Message = "Unused variable")]
         class Rectangle
         {
            // 成员变量
            protected double length;
            protected double width;
            public Rectangle(double l, double w)
            {
               length = l;
               width = w;
            }
            [DeBugInfo(55, "Zara Ali", "19/10/2012",
             Message = "Return type mismatch")]
            public double GetArea()
            {
               return length * width;
            }
            [DeBugInfo(56, "Zara Ali", "19/10/2012")]
            public void Display()
            {
               Console.WriteLine("Length: {0}", length);
               Console.WriteLine("Width: {0}", width);
               Console.WriteLine("Area: {0}", GetArea());
            }
         }//end class Rectangle  
         class ExecuteRectangle
         {
            static void Main(string[] args)
            {
               Rectangle r = new Rectangle(4.5, 7.5);
               r.Display();
               Type type = typeof(Rectangle);
               // 遍历 Rectangle 类的特性
               foreach (Object attributes in type.GetCustomAttributes(false))
               {
                  DeBugInfo dbi = (DeBugInfo)attributes;
                  if (null != dbi)
                  {
                     Console.WriteLine("Bug no: {0}", dbi.BugNo);
                     Console.WriteLine("Developer: {0}", dbi.Developer);
                     Console.WriteLine("Last Reviewed: {0}",
                          dbi.LastReview);
                     Console.WriteLine("Remarks: {0}", dbi.Message);
                  }
               }
               // 遍历方法特性
               foreach (MethodInfo m in type.GetMethods())
               {
                  foreach (Attribute a in m.GetCustomAttributes(true))
                  {
                     DeBugInfo dbi = (DeBugInfo)a;
                     if (null != dbi)
                     {
                        Console.WriteLine("Bug no: {0}, for Method: {1}",
                              dbi.BugNo, m.Name);
                        Console.WriteLine("Developer: {0}", dbi.Developer);
                        Console.WriteLine("Last Reviewed: {0}",
                              dbi.LastReview);
                        Console.WriteLine("Remarks: {0}", dbi.Message);
                     }
                  }
               }
               Console.ReadLine();
            }
         }
      }
```

当上面的代码被编译和执行时，它会产生下列结果：

```Output
Output--->
           Length: 4.5
           Width: 7.5
           Area: 33.75
           Bug No: 49
           Developer: Nuha Ali
           Last Reviewed: 10/10/2012
           Remarks: Unused variable
           Bug No: 45
           Developer: Zara Ali
           Last Reviewed: 12/8/2012
           Remarks: Return type mismatch
           Bug No: 55, for Method: GetArea
           Developer: Zara Ali
           Last Reviewed: 19/10/2012
           Remarks: Return type mismatch
           Bug No: 56, for Method: Display
           Developer: Zara Ali
           Last Reviewed: 19/10/2012
           Remarks:
```

### 三.反射的用法

*反射用到的命名空间*

```Csharp
System.Reflection
System.Type
System.Reflection.Assembly
```

*反射用到的主要类：*

- System.Type 类－－通过这个类可以访问任何给定数据类型的信息。  
- System.Reflection.Assembly类－－它可以用于访问给定程序集的信息，或者把这个程序集加载到程序中。

##### System.Type类

 `System.Type` 类对于反射起着核心的作用。但它是一个抽象的基类，`Type`有与每种数据类型对应的派生类，我们使用这个派生类的对象的方法、字段、属性来查找有关该类型的所有信息。  获取给定类型的`Type`引用有3种常用方式：

```Csharp
    //使用 C# typeof 运算符。
        Type t = typeof(string);
    //使用对象GetType()方法。
        string s = "grayworm";
        Type t = s.GetType(); 
    //还可以调用Type类的静态方法GetType()。
        Type t = Type.GetType("System.String");
```

上面这三类代码都是获取`string`类型的`Type`，在取出`string`类型的`Type`引用`t`后，我们就可以通过t来探测`string`类型的结构了。

```Csharp
    string n = "grayworm";
    Type t = n.GetType();
    foreach (MemberInfo mi in t.GetMembers())
    {
        Console.WriteLine("{0}\t{1}", mi.MemberType, mi.Name);
    }
```

###### Type类的属性

>   1. Name 数据类型名
>   2. FullName 数据类型的完全限定名(包括命名空间名)
>   3. Namespace 定义数据类型的命名空间名
>   4. IsAbstract 指示该类型是否是抽象类型
>   5. IsArray   指示该类型是否是数组
>   6. IsClass   指示该类型是否是类
>   7. IsEnum   指示该类型是否是枚举
>   8. IsInterface    指示该类型是否是接口
>   9. IsPublic 指示该类型是否是公有的
>   10. IsSealed 指示该类型是否是密封类
>   11. IsValueType 指示该类型是否是值类型

###### Type类的方法

1. GetConstructor(), GetConstructors()：返回ConstructorInfo类型，用于取得该类的构造函数的信息
2. GetEvent(), GetEvents()：返回EventInfo类型，用于取得该类的事件的信息
3. GetField(), GetFields()：返回FieldInfo类型，用于取得该类的字段（成员变量）的信息
4. GetInterface(), GetInterfaces()：返回InterfaceInfo类型，用于取得该类实现的接口的信息
5. GetMember(), GetMembers()：返回MemberInfo类型，用于取得该类的所有成员的信息
6. GetMethod(), GetMethods()：返回MethodInfo类型，用于取得该类的方法的信息
7. GetProperty(), GetProperties()：返回PropertyInfo类型，用于取得该类的属性的信息
*可以调用这些成员，其方式是调用`Type`的`InvokeMember()`方法，或者调用`MethodInfo`, `PropertyInfo`和其他类的`Invoke()`方法。*

```Csharp
    //查看类中的构造方法：
        NewClassw nc = new NewClassw();
        Type t = nc.GetType();
        ConstructorInfo[] ci = t.GetConstructors();    //获取类的所有构造函数
        foreach (ConstructorInfo c in ci) //遍历每一个构造函数
        {
            ParameterInfo[] ps = c.GetParameters();    //取出每个构造函数的所有参数
            foreach (ParameterInfo pi in ps)   //遍历并打印所该构造函数的所有参数
            {
                Console.Write(pi.ParameterType.ToString()+" "+pi.Name+",");
            }
            Console.WriteLine();
        }
    //用构造函数动态生成对象：
        Type t = typeof(NewClassw);
        Type[] pt = new Type[2];
        pt[0] = typeof(string);
        pt[1] = typeof(string);
        //根据参数类型获取构造函数 
        ConstructorInfo ci = t.GetConstructor(pt); 
        //构造Object数组，作为构造函数的输入参数 
        object[] obj = new object[2]{"grayworm","hi.baidu.com/grayworm"};   
        //调用构造函数生成对象 
        object o = ci.Invoke(obj);    
        //调用生成的对象的方法测试是否对象生成成功 
        //((NewClassw)o).show();    
    //用Activator生成对象：
        Type t = typeof(NewClassw);
        //构造函数的参数 
        object[] obj = new object[2] { "grayworm", "hi.baidu.com/grayworm" };   
        //用Activator的CreateInstance静态方法，生成新对象 
        object o = Activator.CreateInstance(t,"grayworm","hi.baidu.com/grayworm"); 
        //((NewClassw)o).show();
    //查看类中的属性：
        NewClassw nc = new NewClassw();
        Type t = nc.GetType();
        PropertyInfo[] pis = t.GetProperties();
        foreach(PropertyInfo pi in pis)
        {
            Console.WriteLine(pi.Name);
        }
    //查看类中的public方法：
        NewClassw nc = new NewClassw();
        Type t = nc.GetType();
        MethodInfo[] mis = t.GetMethods();
        foreach (MethodInfo mi in mis)
        {
            Console.WriteLine(mi.ReturnType+" "+mi.Name);
        }
    //查看类中的public字段
        NewClassw nc = new NewClassw();
        Type t = nc.GetType();
        FieldInfo[] fis = t.GetFields();
        foreach (FieldInfo fi in fis)
        {
            Console.WriteLine(fi.Name);
        } (http://hi.baidu.com/grayworm)
    //用反射生成对象，并调用属性、方法和字段进行操作 
        NewClassw nc = new NewClassw();
        Type t = nc.GetType();
        object obj = Activator.CreateInstance(t);
        //取得私有字段ID 
        FieldInfo fi = t.GetField("ID", BindingFlags.NonPublic | BindingFlags.Instance);
        //给ID字段赋值 
        fi.SetValue(obj, "k001");
        //取得MyName属性 
        PropertyInfo pi1 = t.GetProperty("MyName");
        //给MyName属性赋值 
        pi1.SetValue(obj, "grayworm", null);
        PropertyInfo pi2 = t.GetProperty("MyInfo");
        pi2.SetValue(obj, "hi.baidu.com/grayworm", null);
        //取得show方法 
        MethodInfo mi = t.GetMethod("show");
        //调用show方法 
        mi.Invoke(obj, null);
```

##### System.Reflection.Assembly类

`Assembly`类可以获得程序集的信息，也可以动态的加载程序集，以及在程序集中查找类型信息，并创建该类型的实例。使用`Assembly`类可以降低程序集之间的耦合，有利于软件结构的合理化。

```Csharp

//通过程序集名称返回Assembly对象
        Assembly ass = Assembly.Load("ClassLibrary831");
//通过DLL文件名称返回Assembly对象
        Assembly ass = Assembly.LoadFrom("ClassLibrary831.dll");
//通过类型返回Assembly对象
        Assembly editorAssembly = Assembly.GetAssembly(typeof(NewClassw));
//通过Assembly获取程序集中类 
        Type t = ass.GetType("ClassLibrary831.NewClass");   //参数必须是类的全名
//通过Assembly获取程序集中所有的类
        Type[] t = ass.GetTypes();
//通过程序集的名称反射
        Assembly ass = Assembly.Load("ClassLibrary831");
        Type t = ass.GetType("ClassLibrary831.NewClass");
        object o = Activator.CreateInstance(t, "grayworm", "http://hi.baidu.com/grayworm");
        MethodInfo mi = t.GetMethod("show");
        mi.Invoke(o, null);
//通过DLL文件全名反射其中的所有类型
        Assembly assembly = Assembly.LoadFrom("xxx.dll的路径");
        Type[] aa = a.GetTypes();
        foreach(Type t in aa)
        {
            if(t.FullName == "a.b.c")
            {
                object o = Activator.CreateInstance(t);
            }
        }
```
