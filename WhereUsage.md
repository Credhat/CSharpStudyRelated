### Where Usage

###### 泛型约束: 基类/构造函数/接口/参数

1. [C# Where(泛型约束)](https://blog.csdn.net/qq_40302951/article/details/123308000)
2. [C#设计泛型类善用where关键字](https://cylycgs.blog.csdn.net/article/details/103095450?spm=1001.2101.3001.6650.17&utm_medium=distribute.pc_relevant.none-task-blog-2%7Edefault%7EBlogCommendFromBaidu%7ERate-17-103095450-blog-123308000.pc_relevant_3mothn_strategy_and_data_recovery&depth_1-utm_source=distribute.pc_relevant.none-task-blog-2%7Edefault%7EBlogCommendFromBaidu%7ERate-17-103095450-blog-123308000.pc_relevant_3mothn_strategy_and_data_recovery&utm_relevant_index=20) <font size=2> :star: </font>
3. [C# 泛型约束 xxx Where T：约束(二)](https://blog.csdn.net/WuLex/article/details/126801258?spm=1001.2101.3001.6661.1&utm_medium=distribute.pc_relevant_t0.none-task-blog-2%7Edefault%7ECTRLIST%7ERate-1-126801258-blog-103095450.pc_relevant_3mothn_strategy_and_data_recovery&depth_1-utm_source=distribute.pc_relevant_t0.none-task-blog-2%7Edefault%7ECTRLIST%7ERate-1-126801258-blog-103095450.pc_relevant_3mothn_strategy_and_data_recovery&utm_relevant_index=1) <font size=2> :star: :star: :star: </font>

### 设计泛型类时

**泛型是什么?**

1. <font size=2> 所谓泛型，即通过参数化类型来实现在同一份代码上操作多种数据类型，泛型编程是一种编程范式，它利用“参数化类型”将类型抽象化，从而实现更为灵活的复用。 </font>

2. <font size=2> 在定义泛型类时，可以对代码能够在实例化类时用于类型参数的类型种类施加限制。如果代码尝试使用某个约束所不允许的类型来实例化类，则会产生编译时错误。这些限制称为约束。约束是使用 where 上下文关键字指定的。</font>

- <font color=Pink size=2> 原则上，泛型类只有在类型参数只有一个时才能使用T，否则都要给类型参数清楚的命名，以提升程序的可读性 </font>

|       约束      | 说明                                                                                                |
|:---------------:|:---------------------------------------------------------------------------------------------------|
|    `T:struct`   | <font size=2>类型参数必须是值类型。可以指定除 Nullable 以外的任何值类型.</font>                        |
|    `T:class`    | <font size=2>类型参数必须是引用类型，包括任何类、接口、委托或数组类型.</font>                           |
|    `T:new ()`   | <font size=2>类型参数必须具有无参数的公共构造函数。当与其他约束一起使用时，new() 约束必须最后指定.</font> |
|  `T：<基类名>`  | <font size=2>类型参数必须是指定的基类或派生自指定的基类.</font>                                         |
| `T：<接口名称>` | <font size=2>类型参数必须是指定的接口或实现指定的接口。可以指定多个接口约束。约束接口也可以是泛型的.</font> |
|      `T：U`     | <font size=2>为 T 提供的类型参数必须是为 U 提供的参数或派生自为 U 提供的参数。这称为裸类型约束.</font>    |
