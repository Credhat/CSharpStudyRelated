// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.Linq;

namespace DestopOperation
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Hello, World!");
            // Console.OutputEncoding = "chcp 65001";
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            Console.OutputEncoding = System.Text.Encoding.GetEncoding("GB2312");
            // Console.OutputEncoding = System.Text.Encoding.GetEncoding("GBK");
            // System.system("chcp 65001");
            var customers = FakeData.Customers;
            var addresses = FakeData.Addresses;
            var result = customers                  // 1.外部数据源
                .Join(
                addresses                           // 2.内部数据源
                , c => c.AddressId                  // 3.外部键选择器
                , a => a.Id                         // 4.内部键选择器
                , (customer, address) => new        // 5.期望返回的结果集选择器
                {
                    name = customer.Name,
                    province = address.Province,
                    city = address.City,
                    district = address.District,
                    street = address.Street
                })
                .ToList();
            foreach (var item in result)
            {
                Console.WriteLine($"name:{item.name},province:{item.province},city:{item.city},district:{item.district},street:{item.street}");
            }

            var groupJoinResult = addresses         // 外部数据源(左表)
                 .GroupJoin(
                     customers,                      // 内部数据源(右表)
                     address => address.Id,          // 外部键选择器(左表数据源中的公共键)
                     customer => customer.AddressId, // 内部键选择器(右表数据源中的公共键)
                     (address, customer) => new      // 结果选择器(期望返回的结果集)
                     {
                         address,
                         customer
                     })
                 .ToList();
            foreach (var group in groupJoinResult)
            {
                var item = group.address;
                Console.WriteLine($"province:{item.Province},city:{item.City},district:{item.District},street:{item.Street}");
                // Colorizer.WriteLine($"province:{item.Province},city:{item.City},district:{item.District},street:{item.Street}", "Green");
                foreach (var c in group.customer)
                {
                    Console.WriteLine($" {c.Id},{c.Name}");
                }
            }

            var printCustomerId = customers.Select(custom => new customerList
            {
                Name = custom.Name,
                Id = custom.Id
            });

            printCustomerId.ToList().ForEach(i => String.Join($"{i.Name} ", printCustomerId));
            System.Console.WriteLine(String.Join(",", printCustomerId.ToList()[0]));

            var list = new List<string> { "Csharp", "JavaScript", "Golang" };
            var result1 = list.SelectMany(x => x);
            Console.WriteLine(string.Join(",", result1));


            PetOwner[] petOwners =
                        {
                new PetOwner { Name="Higa",
                    Pets = new List<string>{ "Scruffy", "Sam" } },
                new PetOwner { Name="Ashkenazi",
                    Pets = new List<string>{ "Walker", "Sugar" } },
                new PetOwner { Name="Price",
                    Pets = new List<string>{ "Scratches", "Diesel" } },
                new PetOwner { Name="Hines",
                    Pets = new List<string>{ "Dusty" } }
            };

            var query =
                petOwners
                    .SelectMany(petOwner => petOwner.Pets, (petOwner, petName) => new { petOwner, petName })
                    .Where(ownerAndPet => ownerAndPet.petName.StartsWith("S"))
                    .Select(ownerAndPet =>
                        new
                        {
                            Owner = ownerAndPet.petOwner.Name,
                            Pet = ownerAndPet.petName
                        }
                    );

            var query2 = petOwners.SelectMany(petOwner => petOwner.Pets, (petOwner, petName) => new { petOwner, petName });
            var query22 = query2.Where(ownerAndPet => ownerAndPet.petName.StartsWith("S"));
            var query222 = query22.Select(ownerAndPet =>
                        new
                        {
                            Owner = ownerAndPet.petOwner.Name,
                            Pet = ownerAndPet.petName
                        }
                    );


            foreach (var obj in query)
            {
                Console.WriteLine(obj);
            }

            int[] numbers1 = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            var summerNum = numbers1.Aggregate((num1, num2) => num1 += num2);
            System.Console.WriteLine(summerNum);
            string[] strs = new string[] { "1", "2", "3", "4", "5" };
            var operationStrs = strs.Aggregate("         ,",
                (str1, str2) => String.Join(',', str1, str2),
                str => str.TrimStart(new char[] { ',', ' ' })
                // secondStrs=> secondStrs+"hh"
                );
            System.Console.WriteLine(operationStrs);



            #region EnterToExitConsole

            Console.Write("Press <Enter> to exit... \r\n");
            while (Console.ReadKey().Key != ConsoleKey.Enter) { }

            #endregion

            void prnt(string name)
            {
                System.Console.WriteLine(name);
            }

        }
        class PetOwner
        {
            public string Name { get; set; }
            public List<string> Pets { get; set; }
        }
        private class customerList
        {
            public string? Name { get; set; }
            public int Id { get; set; }
        }
    }


    /// <summary>
    /// 订单实体
    /// </summary>
    public class Order
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string Customer { get; set; }
        /// <summary>
        /// 订单金额
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// 订单来源
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// 下单时间
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// 收货地址
        /// </summary>
        public ShippingAddress Address { get; set; }
    }

    public class Customer
    {
        public Customer()
        {
        }

        public Customer(int id, int age, string gender, string name, int addressId)
        {
            Id = id;
            Age = age;
            Gender = gender;
            Name = name;
            AddressId = addressId;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public int AddressId { get; set; }
    }

    /// <summary>
    /// 收货地址
    /// </summary>
    public class ShippingAddress
    {
        public int Id { get; set; }
        /// <summary>
        /// 省份
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 地区
        /// </summary>
        public string District { get; set; }
        /// <summary>
        /// 街道
        /// </summary>
        public string Street { get; set; }
    }


    /// <summary>
    /// 模拟数据
    /// </summary>
    public static class FakeData
    {
        /// <summary>
        /// 模拟订单集合数据
        /// </summary>
        public static List<Order> Orders => new()
    {
        new Order
        {
            Id = 1,
            Customer = "Rector",
            Price = 3699.00D,
            Source = "线上",
            CreatedAt = new DateTime(2021, 5, 1, 20, 30, 25),
            Address = Address1
        },
        new Order
        {
            Id = 2,
            Customer = "James",
            Price = 2699.00D,
            Source = "线上",
            CreatedAt = new DateTime(2021, 6, 15, 18, 06, 05),
            Address = Address2
        },
        new Order
        {
            Id = 3,
            Customer = "Chris",
            Price = 1999.00D,
            Source = "线下",
            CreatedAt = new DateTime(2021, 7, 21, 20, 10, 02),
            Address = Address3
        },
        new Order
        {
            Id = 4,
            Customer = "Steven",
            Price = 5699.00D,
            Source = "线上",
            CreatedAt = new DateTime(2021, 8, 30, 09, 30, 25),
            Address = Address4
        },
        new Order
        {
            Id = 5,
            Customer = "Jo",
            Price = 2569.00D,
            Source = "线下",
            CreatedAt = new DateTime(2021, 9, 11, 10, 28, 25),
            Address = Address1
        },
        new Order
        {
            Id = 6,
            Customer = "Rector",
            Price = 5699.00D,
            Source = "线上",
            CreatedAt = new DateTime(2021, 10, 16, 16, 30, 08),
            Address = Address1
        }
    };

        public static List<Customer> Customers => new()
    {
        new Customer(id: 1, age: 20, gender: "男", name: "Rector", addressId: 1),
        new Customer(id: 2, age: 24, gender: "女", name: "Anna", addressId: 2),
        new Customer(id: 3, age: 26, gender: "女", name: "Xi", addressId: 1),
        new Customer(id: 4, age: 30, gender: "男", name: "Curry", addressId: 5)
    };

        public static ShippingAddress Address1 => new()
        {
            Id = 1,
            Province = "重庆",
            City = "重庆",
            District = "渝北区",
            Street = "回兴街道"
        };
        public static ShippingAddress Address2 => new()
        {
            Id = 2,
            Province = "重庆",
            City = "重庆",
            District = "江北区",
            Street = "观音桥街道"
        };
        public static ShippingAddress Address3 => new()
        {
            Id = 3,
            Province = "四川",
            City = "成都",
            District = "金牛区",
            Street = "西华街道"
        };
        public static ShippingAddress Address4 => new()
        {
            Id = 4,
            Province = "四川",
            City = "成都",
            District = "青羊区",
            Street = "草市街街道"
        };
        public static ShippingAddress Address5 => new()
        {
            Id = 5,
            Province = "上海",
            City = "上海",
            District = "黄浦区",
            Street = "外滩街道"
        };

        public static List<ShippingAddress> Addresses =>
            new()
            {
            Address1,
            Address2,
            Address3,
            Address4,
            Address5
            };
    }
}