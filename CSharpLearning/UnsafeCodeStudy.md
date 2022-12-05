### C# 不安全代码

当一个代码块使用 `unsafe` 修饰符标记时，C# 允许在函数中使用**指针变量**。*不安全代码或非托管代码*是指使用了**指针变量**的代码块。

#### 一、指针变量

`指针` 是值为另一个变量的**地址**的变量，即，内存位置的直接**地址**。就像其他变量或常量，您必须在使用*指针*存储其他变量**地址**之前声明*指针*。

指针变量声明的一般形式为：

```Csharp
   type *var-name;
//以下是有效的指针声明：
   int    *ip;    /* 指向一个整数 */
   double *dp;    /* 指向一个双精度数 */
   float  *fp;    /* 指向一个浮点数 */
   char   *ch     /* 指向一个字符 */
```

下面的实例说明了 C# 中使用了 unsafe 修饰符时指针的使用：

```Csharp
   using System;
   namespace UnsafeCodeApplication
   {
       class Program
       {
           static unsafe void Main(string[] args)
           {
               int var = 20;
               int* p = &var;
               Console.WriteLine("Data is: {0} ",  var);
               Console.WriteLine("Address is: {0}",  (int)p);
               Console.ReadKey();
           }
       }
   }
```

当上面的代码被编译和执行时，它会产生下列结果：

```Output
Output--->
            Data is: 20
            Address is: 99215364
```

您也可以不用声明整个方法作为不安全代码，只需要声明方法的一部分作为不安全代码。下面的实例说明了这点。

#### 二、使用指针检索数据值

您可以使用 `ToString()` 方法检索存储在指针变量所引用位置的数据。下面的实例演示了这点：

```Csharp
   using System;
   namespace UnsafeCodeApplication
   {
      class Program
      {
         public static void Main()
         {
            unsafe
            {
               int var = 20;
               int* p = &var;
               Console.WriteLine("Data is: {0} " , var);
               Console.WriteLine("Data is: {0} " , p->ToString());
               Console.WriteLine("Address is: {0} " , (int)p);
            }
            Console.ReadKey();
         }
      }
   }
```

当上面的代码被编译和执行时，它会产生下列结果：

```Output
Output--->
             Data is: 20
             Data is: 20
             Address is: 77128984
```

### 三、传递指针作为方法的参数

您可以向方法传递指针变量作为方法的参数。下面的实例说明了这点：

```Csharp
   using System;
   namespace UnsafeCodeApplication
   {
      class TestPointer
      {
         public unsafe void swap(int* p, int *q)
         {
            int temp = *p;
            *p = *q;
            *q = temp;
         }
         public unsafe static void Main()
         {
            TestPointer p = new TestPointer();
            int var1 = 10;
            int var2 = 20;
            int* x = &var1;
            int* y = &var2;
            Console.WriteLine("Before Swap: var1:{0}, var2: {1}", var1, var2);
            p.swap(x, y);
            Console.WriteLine("After Swap: var1:{0}, var2: {1}", var1, var2);
            Console.ReadKey();
         }
      }
   }
```

当上面的代码被编译和执行时，它会产生下列结果：

```Output
Output--->
            Before Swap: var1: 10, var2: 20
            After Swap: var1: 20, var2: 10
```

### 四、使用指针访问数组元素

在 **C#** 中，数组名称和一个指向与数组数据具有相同数据类型的指针是不同的变量类型。例如，`int *p` 和 `int[] p` 是不同的类型。您可以增加*指针变量 p*，因为它在内存中不是固定的，但是数组地址在内存中是固定的，所以您不能增加*数组 p*。

因此，如果您需要使用*指针变量*访问数组数据，可以像我们通常在 **C** 或 **C++** 中所做的那样，使用 `fixed` *关键字*来固定指针。下面的实例演示了这点：

```Csharp
   using System;
   namespace UnsafeCodeApplication
   {
      class TestPointer
      {
         public unsafe static void Main()
         {
            int[]  list = {10, 100, 200};
            fixed(int *ptr = list)
            /* 显示指针中数组地址 */
            for ( int i = 0; i < 3; i++)
            {
               Console.WriteLine("Address of list[{0}]={1}",i,(int)(ptr + i));
               Console.WriteLine("Value of list[{0}]={1}", i, *(ptr + i));
            }
            Console.ReadKey();
         }
      }
   }
```

当上面的代码被编译和执行时，它会产生下列结果：

```Output
Output--->
            Address of list[0] = 31627168
            Value of list[0] = 10
            Address of list[1] = 31627172
            Value of list[1] = 100
            Address of list[2] = 31627176
            Value of list[2] = 200
```

### 五、编译不安全代码

为了编译*不安全代码*，您必须切换到命令行编译器指定 `/unsafe` 命令行。

例如，为了编译包含*不安全代码*的名为 *prog1.cs* 的程序，需在命令行中输入命令：
> csc /unsafe prog1.cs

如果您使用的是 Visual Studio IDE，那么您需要在*项目属性*中启用*不安全代码*。步骤如下：

通过双击*资源管理器*（Solution Explorer）中的*属性*（properties）节点，打开*项目属性*（project properties）。

点击 *Build* 标签页。

选择选项"*Allow unsafe code*"。
