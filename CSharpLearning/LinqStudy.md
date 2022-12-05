### Linq

##### Language Intergrated Query(Linq) 集成查询语言

```Csharp
using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
namespace LinqTest
{
    class MainClass
    {
        public static void Main(string[] args)
        {
         /*
          * where        筛选操作符定义了返回元素的条件
          * 
          * Select       投射操作符用于把对象转换为另一个类型的新对象。
          * 
          * OrderBy      排序操作符返回的元素的顺序
          * ThenBy
          * OerderByDescending
          * ThenByDescending
          * Reverse
          * 
          * Join         连接操作符用于合并不直接相关的集合。
          * GroupJoin
          * 
          * GroupBy      组合操作符把数组放在组中。
          * ToLookup
          * 
          * Any          如果元素序列满足指定的条件，限定符操作就返回布尔值
          * All
          * Contains     检查某个元素是否在集合中
          * 
          * Take         分区操作符返回集合的一个子集。指定要从集合中提取的元素个数
          * Skip         跳过指定的元素个数
          * TakeWhile
          * SkipWhile
          * 
          * Distinct     Set操作符返回一个集合。从集合中删除重复的元素。
          * Union        需要两个集合，返回出现在其中一个集合中的唯一元素。
          * Intersect    需要两个集合，返回两个集合都有的元素。
          * Except       需要两个集合，返回只出现在一个集合中的元素。
          * Zip          需要两个集合，把两个集合合并为一个
          * 
          * First        这些元素操作符仅返回一个元素。返回第一个满足条件的元素。
          * FirstOrDefault   返回第一个满足条件的元素。如果没有找到，就返回默认值。
          * Last
          * LastOrDefault
          * ElementAt    指定了要返回的元素的位置
          * ElementAtOrDefault
          * Single       只返回一个满足条件的元素。如果有多个元素都满足条件，就抛出一个异常。
          * SingleOrDefault
          * 
          * Count        聚合操作符计算集合的一个值。利用这些聚合操作符，可以计算所有值的总和、所有元素的个数
          * Sum          总和
          * Min          值最小的元素
          * Max          值最大的元素
          * Average      平均值
          * Aggregate    聚合     可以传递一个lambda表达式，该表达式对有所的值进行聚合
          * 
          * ToArray      转换为数组
          * AsEnumerable 转换为IEnumerable
          * ToList       转换为IList
          * ToDictionary 转换为IDictionary
          * Cast<TResult>
          * 
          * Empty        这些生成操作符返回一个新集合。使用Empty时集合是空的
          * Range        返回一系列数字
          * Repeat       返回一个始终重复一个值的集合；返回一个迭代器，该迭代器把同一个值重复特定的次数。
          * 
          */
            // 1. 查询出 Unity1609 的所有冠军 并按照总分排序
            /*
             * 查询表达式必须以 from 子句开头，以 select 或 group 子句结束。
             * 在这两个子句之间，可以使用 where、oderby、join、let 和其他 from 子句。
             */
            var query = from n in Formula.GetChampions()
                        where n.ClassesName == "Unity1609"
                        orderby n.Sum descending
                        select n;
            // 使用扩展方法实现
            var champions = new List<Student>(Formula.GetChampions());
            IEnumerable<Student> result = champions.Where(o => o.ClassesName == "Unity1609").
            OrderByDescending(o => o.Sum).
            Select(n => n);
            foreach (var item in result)
            {
                Console.WriteLine("{0:N}", item);
            }
            // 2. 筛选 
            // 使用where子句，可以合并多个表达式。
            // 3. 用索引筛选 
            // 不能使用LINQ查询的一个例子是Where()方法的重载。在Where()方法的重载时，可以传递第2个参数-索引。
            // 索引是筛选器返回的每个结果的计数器。
            // 4. 类型筛选 
            // 为了进行基于类型的筛选，可以使用OfType<>()扩展方法。
            object[] data1 = { "one", 2, 3, "four" };
            var query1 = data1.OfType<string>();    // 从集合中仅返回字符串
            // 5. 复合的from子句
            // 如果需要根据对象的一个成员进行筛选，而该成员本身是一个系列，就可以使用复合的from子句。
            // 查询元素是集合的元素  扩展方法 SelectMany() 用于迭代序列的序列
            //var query2 = from r in Formula.GetChampions()
            //from c in r.Scores
            //where c > 90
            //orderby r.Sum
            //select r.Name + " " + r.Sum;
            var query2 = Formula.GetChampions().
            SelectMany(source => source.Scores, (source, collection) => new { Stu = source, Scores = collection }).
            Where(newObj => newObj.Stu.Sum > 90).
            OrderBy(newObj => newObj.Stu.Sum).
            Select(newObj => newObj.Stu.Name + " " + newObj.Stu.Sum);
            // 6. 排序
            // 要对序列排序，前面使用了orderby子句。
            // 7. 分组 
            // 要根据一个关键字值对查询结果分组，可以使用group子句。
            //var query3 = from n in Formula.GetChampions()
            //group n by n.ClassesName into g
            //orderby g.Count(), g.Key
            //where g.Count() >= 2
            //select new
            //{
            //    ClassName = g.Key,
            //    Count = g.Count()
            //};
            // GroupBy()  IGrouping  Key
            var query3 = Formula.GetChampions().
            GroupBy(n => n.ClassesName).
            OrderByDescending(group => group.Count()).
            ThenBy(group => group.Key).
            Where(g => g.Count() >= 2).
            Select(g => new { ClassName = g.Key, Count = g.Count() });
            // 8. 对嵌套的对象分组
            // 如果分组的对象应包含嵌套的序列，就可以改变select子句创建的匿名类型
            var query4 = from r in Formula.GetChampions()
                         group r by r.ClassesName into g
                         orderby g.Count() descending, g.Key
                         where g.Count() >= 2
                         select new
                         {
                             ClassName = g.Key,
                             Count = g.Count(),
                             Scores = from r1 in g
                                      orderby r1.Sum descending, r1.Name
                                      select r1.Scores
                         };
            foreach (var item in query4)
            {
                Console.WriteLine(item.ClassName + " " + item.Count);
            }
            // 9. 内连接 
            // 使用join子句可以根据特定的条件合并两个数据源
            var query5 = from n in Formula.GetChampions()
                         from score in n.Scores
                         select new
                         {
                             Name = n.Name,
                             Score = score,
                             ClassName = n.ClassesName,
                             Years = n.Years
                         };
            var query6 = from n in Formula.GetContructorChampions()
                         from y in n.Years
                         select new
                         {
                             ClassName = n.ClassesName,
                             Year = y
                         };
            var query7 = (from n in query5
                          join t in query6 on n.ClassName equals t.ClassName
                          select new
                          {
                              n.ClassName,
                              n.Name,
                              n.Score,
                              t.Year
                          }).Take(10);
            foreach (var item in query7)
            {
                Console.WriteLine("{0},{1},{2},{3}", item.ClassName, item.Name, item.Score, item.Year);
            }
            // 10. 左外连接  
            // 返回左边序列的全部元素，即时它们在右边的序列中没有匹配的元素
            // 使用 join into、 DefaultIfEmpty 定义 
            var query8 = from n in query5
                         join t in query6 on n.ClassName equals t.ClassName into rt
                         from q in rt.DefaultIfEmpty()
                         orderby n.Score
                         select new
                         {
                             Name = n.Name,
                             ClassName = q == null ? "NoClassName" : n.ClassName,
                             Score = n.Score,
                             Year = q == null ? -1 : q.Year
                         };
            foreach (var item in query8)
            {
                Console.WriteLine($"{item.Name},{item.ClassName},{item.Score},{item.Year}");
            }
            // 11. 组连接 
            // 左外连接使用了组连接和into子句。它有一部分语法与组连接相同。只不过组连接不使用DefaultIfEmpty方法。
            // 使用组连接时，可以连接两个独立的序列，对于其中一个序列中的某个元素，另一个序列中存在对应的一个项列表。
            var query9 = from r in query5
                         from y in r.Years
                         join r2 in query6 on
                         new { ClassName = r.ClassName, Year = y }
                         equals
                         new { ClassName = r2.ClassName, Year = r2.Year }
                         into g
                         select new { ClassName = r.ClassName, Name = r.Name, Year = y, Score = r.Score };
            foreach (var item in query9)
            {
                Console.WriteLine($"{item.ClassName},{item.Name},{item.Score},{item.Year}");
            }
            // 12. 集合操作
            // 扩展方法 Distinct()、Union()、Intersect()和Except() 都是集合操作。
            // 13. 合并
            // Zip()方法允许用一个谓词函数把两个相关的序列合并为一个。
            var query10 = query5.Zip(query6, (first, second) => first.ClassName + "," + first.Name + "," + second.Year);
            // 14. 分区
            // 扩展方法 Take() 和 Skip() 等的分区操作可用于分页。
            // 使用 TakeWhile() 和 SkipWhile() 扩展方法，还可以传递一个谓词，根据谓词的结果提取或跳过某些项。
            // 15. 聚合操作符
            // 聚合操作符（如Count()、Sum()、Min()、Max()、Average()和Aggregate()）不返回一个序列，而返回一个值。
            var query11 = from r in Formula.GetChampions()
                          let scoreCount = r.Scores.Count()        // let子句定义了一个变量
                          where scoreCount >= 2
                          orderby scoreCount descending
                          select new
                          {
                              Name = r.Name,
                              ClassName = r.ClassesName
                          };
            // 16. 转换操作符
            // ToList()
            // Lookup<TKey, TElement>   一个键可以对应多个值
            var query12 = (from r in Formula.GetChampions()
                           from s in r.Scores
                           select new
                           {
                               ClassName = r.ClassesName,
                               Name = r.Name,
                               Score = s
                           }).ToLookup(cr => cr.ClassName, cr => cr.Name);
            foreach (var item in query12["Unity1605"])
            {
                Console.WriteLine($"{item}");
            }
            // 如果需要在非类型化的集合上（如ArrayList）使用LINQ查询，就可以使用Cast()方法。
            var list = new System.Collections.ArrayList(Formula.GetChampions() as System.Collections.ICollection);
            var query13 = from r in list.Cast<Student>()
                          where r.ClassesName == "Unity1605"
                          select r;
            foreach (var item in query13)
            {
                Console.WriteLine($"{item.ClassesName}, {item.Name}");
            }
            // 17. 生成操作符
            // 生成操作符Range()、Empty()和Repeat()不是扩展方法，而是返回序列的正常静态方法。
            //var values = Enumerable.Range(1, 20);   // 第一个参数作为起始值，第二个参数作为要填充的项数
            //values = values.Select(n => n * 3);
            //foreach (var item in values)
            //{
            //    Console.WriteLine(item);
            //}
            // Range()方法不返回填充了所定义值的集合，这个方法与其他方法一样，也推迟执行查询，
            // 并返回一个RangeEnumerator，其中只有一条yield return 语句，来递增值。
            // 18. 并行LINQ
            // System.Linq名称空间中包含的类ParalleEnumerable可以分解查询的工作，使其分布在多个线程上。
            //var res = (from x in SampleData().AsParallel()
            //where Math.Log(x) < 4
            //select x).Average();
            var res = SampleData().AsParallel().Where(x => Math.Log(x) < 4).Select(x => x).Average();
            Console.WriteLine(res);
            // 19. 分区器 
            // AsParalle() 方法不仅扩展了IEnuerable<T> 接口，还扩展了 Partitioner 类。通过它，可以影响要创建的分区。
            // Partitioner 类用 System.Collection.ConCurrent 名称空间定义，并且有不同的变体。
            // Create() 方法接受实现了 IList<T> 类的数组或对象。
            var data = SampleData();
            var res1 = (from x in Partitioner.Create(data).AsParallel()
                        where Math.Log(x) < 4
                        select x).Average();
            Console.WriteLine(res1);
            // 也可以调用 WithExecutionMode() 和 WithDegreeOfParallelism() 方法，来影响并行机制。
            // 20. 取消
            // .NET 提供了一种标准方式，来取消长时间运行的任务，这也适用于并行LINQ
            // 要取消长时间运行的查询，可以给查询添加WithCancellation()方法，并传递一个CancellationToken令牌作为参数。
            // CancellationToken令牌从CancellationTokenSource类中创建。
            // 该查询在单独的线程中运行，在该线程中，捕获一个OperationCanceledException类型的异常。
            // 如果取消了查询，就触发这个异常。在主线程中，调用CancellationTokenSource类的Cancel()方法可以取消任务。
            // 21. 表达式树 Expression<T>
        }
        static IEnumerable<int> SampleData()
        {
            const int arraySize = 100000000;
            var r = new Random();
            return Enumerable.Range(0, arraySize).Select(x => r.Next(140)).ToList();
        }
    }
    #region 扩展方法
    /*
     * 扩展方法 ： 定义为静态方法，其第一个参数定义了它扩展的类型，扩展方法在一个静态类中声明。
     *           为了区分扩展方法和一般的静态方法，扩展方法还需要对第一个参数使用 this 关键字。
     * 
     *           扩展方法不能访问它扩展的类型的私有成员。调用扩展方法只是调用静态方法的一种新语法。
     *           string s = "hello";
     *           s.Foo();
     *           StringExtension.Foo(s);
     */
    public static class StringExtension
    {
        public static void Foo(this string s)
        {
            Console.WriteLine("Foo invoked for {0}", s);
        }
    }
    #endregion
    // 2016~2018年间，所有获得第一名的名单
    static class Formula
    {
        public static List<Student> nameList;
        public static IList<Student> GetChampions()
        {
            if (nameList == null)
            {
                nameList = new List<Student>()
                {
                    new Student("张三", 18, "Unity1605", new int[]{99, 98}, new int[]{2016}),
                    new Student("李四", 20, "Unity1605", new int[]{98, 97}, new int[]{2016}),
                    new Student("李四", 20, "Unity1607", new int[]{98, 97}, new int[]{2016}),
                    new Student("刘五", 21, "Unity1609", new int[]{98, 96}, new int[]{2016}),
                    new Student("王二", 21, "Unity1609", new int[]{99, 96}, new int[]{2016}),
                    new Student("王六", 22, "Unity1609", new int[]{94, 95}, new int[]{2017}),
                    new Student("赵六", 23, "Unity1701", new int[]{96, 96}, new int[]{2017}),
                    new Student("田七", 23, "Unity1711", new int[]{95, 97}, new int[]{2017}),
                    new Student("孙二", 22, "Unity1711", new int[]{94, 98}, new int[]{2018}),
                    new Student("王八", 21, "Unity1801", new int[]{99, 99}, new int[]{2018}),
                    new Student("王八", 21, "Unity1805", new int[]{99, 99}, new int[]{2018}),
                };
            }
            return nameList;
        }
        private static List<ClassesChampion> teams;
        public static IList<ClassesChampion> GetContructorChampions()
        {
            if (teams == null)
            {
                teams = new List<ClassesChampion>()
                {
                    new ClassesChampion("Unity1605", 2016),
                    new ClassesChampion("Unity1607", 2016),
                    new ClassesChampion("Unity1609", 2016, 2017),
                    new ClassesChampion("Unity1701", 2017),
                    new ClassesChampion("Unity1711", 2017, 2018),
                    new ClassesChampion("Unity1801", 2018),
                    new ClassesChampion("Unity1803", 2019)
                };
            }
            return teams;
        }
    }
    // 排行榜 获得第一名学员的班级名字和年份
    class ClassesChampion
    {
        public ClassesChampion(string name, params int[] years)
        {
            this.ClassesName = name;
            this.Years = new List<int>(years);
        }
        public string ClassesName { get; private set; }
        public IEnumerable<int> Years { get; private set; }
    }
    class Student : IComparable<Student>, IFormattable
    {
        public string Name { get; private set; }
        public string ClassesName { get; private set; }
        public int Age { get; set; }
        public int Sum { get; private set; }
        public IEnumerable<int> Scores { get; private set; }
        public IEnumerable<int> Years { get; private set; }        // 获得第一名的年份
        public Student(string name, int age, string classesName) : this(name, age, classesName, null, null) { }
        public Student(string name, int age, string classesName, IEnumerable<int> scores, IEnumerable<int> years)
        {
            this.Name = name;
            this.ClassesName = classesName;
            this.Age = age;
            this.Scores = new List<int>(scores);
            this.Years = new List<int>(years);
            foreach (var item in Scores)
            {
                Sum += item;
            }
        }
        public override string ToString()
        {
            return string.Format("[Student: Name={0}, Age={1}]", Name, Age);
        }
        public string ToString(string format)
        {
            return ToString(format, null);
        }
        public string ToString(string format, IFormatProvider formatProvider)
        {
            switch (format)
            {
                case "N":
                    return this.Name;
                default:
                    return ToString();
            }
        }
        public int CompareTo(Student other)
        {
            if (other == null) return 1;
            return this.Sum - other.Sum;
        }
    }
}
```

```Csharp
            // Aggregate 用法
            // ---------------------------- 一、对第1个和第2个元素执行操作，将执行的结果继续携带进行操作 ----------------
            // 求1～100的和
            var list = Enumerable.Range(1, 100);
            var sum = list.Aggregate((a, b) => a + b);
            Console.WriteLine(sum);
            // ---------------------------- 二、种子重载 --------------------------------------------------------
            // 先从种子开始作为第一个元素执行操作。
            // 求1～5的阶乘
            var nums = Enumerable.Range(2, 4);  // 2 3 4 5
            var sum1 = nums.Aggregate(1, (a, b) => a * b);
            Console.WriteLine(sum1);
            // 翻转单词
            string content = "i am hxsd";
            content = content.Split(' ').Aggregate((a, b)=> b + " " + a );
            Console.WriteLine(content);
            // --------------------------- 三、结果选择器 --------------------------------------------------------
            // 最长的字符串 输出大写
            string[] fruits = { "apple", "mango", "orange", "passionfruit" };
            string longestName = fruits.Aggregate(fruits[0], (longest, next) => longest.Length < next.Length ? next : longest, n => n.ToUpper());
            Console.WriteLine(longestName);
```
