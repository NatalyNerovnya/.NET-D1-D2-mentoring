// Copyright © Microsoft Corporation.  All Rights Reserved.
// This code released under the terms of the 
// Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//Copyright (C) Microsoft Corporation.  All rights reserved.

using System.Collections;
using System.Linq;
using SampleSupport;
using Task.Data;
using System;

// Version Mad01

namespace SampleQueries
{
    [Title("LINQ Module")]
    [Prefix("Linq")]
    public class LinqSamples : SampleHarness
    {

        private DataSource dataSource = new DataSource();
        delegate IEnumerable del(int i);

<<<<<<< HEAD
        [Category("The only category")]
=======
        [Category("LINQ")]
>>>>>>> 22daee247d6331977858bac9f87a305602a95f4c
        [Title("Task 1")]
        [Description("All customers with sum of orders greater than X")]

        public void Linq1()
        {

            del vipCustomers = total => dataSource.Customers
                .Where(c => c.Orders.Sum(t => t.Total) > total)
                .Select(c => c.CustomerID);


            var x = 5000;
            var customers5000 = vipCustomers(x);

            ObjectDumper.Write("With total x = 5000");
            foreach (var c in customers5000)
            {
                ObjectDumper.Write(c);
            }

            x = 20000;
            var customers20000 = vipCustomers(x);
            ObjectDumper.Write("With total x = 20000");
            foreach (var c in customers20000)
            {
                ObjectDumper.Write(c);
            }

            x = 100000;
            var customers100000 = vipCustomers(x);
            ObjectDumper.Write("With total x = 100000");
            foreach (var c in customers100000)
            {
                ObjectDumper.Write(c);
            }
        }

<<<<<<< HEAD
        [Category("The only category")]
        [Title("Task 2")]
        [Description("List of supplier from the same country")]

        public void Linq2()
        {
            var suppliers = dataSource.
            
            foreach (var c in customers100000)
=======
        [Category("LINQ")]
        [Title("Task 2")]
        [Description("Suppliers from the same country")]

        public void Linq2()
        {
            //            Для каждого клиента составьте список поставщиков, находящихся в той же стране и том же городе.

            var suppliersCountries = from suppliers in dataSource.Suppliers
                                     join customers in dataSource.Customers on suppliers.Country equals customers.Country
                                     select new
                                     {
                                         Country = suppliers.Country,
                                         Supplier = suppliers.SupplierName,
                                         Customer = customers.CompanyName
                                     };


            foreach (var c in suppliersCountries)
>>>>>>> 22daee247d6331977858bac9f87a305602a95f4c
            {
                ObjectDumper.Write(c);
            }
        }
<<<<<<< HEAD
=======

        [Category("LINQ")]
        [Title("Task 3")]
        [Description("Customers with order greater than X")]

        public void Linq3()
        {
            //Найдите всех клиентов, у которых были заказы, превосходящие по сумме величину X
            var x = 10000;

            var customers = dataSource.Customers
                .Where(c => c.Orders
                    .Where(o => o.Total > x).ToArray().Length > 0)
                .Select(c => c.CompanyName);
            foreach (var c in customers)
            {
                ObjectDumper.Write(c);
            }
        }


        [Category("LINQ")]
        [Title("Task 4")]
        [Description("Customers with the date of the first order")]

        public void Linq4()
        {
            //            Выдайте список клиентов с указанием, начиная с какого месяца какого года они стали 
            //клиентами (принять за таковые месяц и год самого первого заказа)
            var customers = dataSource.Customers.Select(c => new
            {
                Customer = c.CompanyName,
                Date = c.Orders.Any() ?
                    c.Orders.DefaultIfEmpty().OrderBy(o => o.OrderDate).FirstOrDefault().OrderDate :
                    new DateTime()
            });

            foreach (var c in customers)
            {
                ObjectDumper.Write(c.Customer + ":  " + c.Date.ToShortDateString());
            }

        }

        [Category("LINQ")]
        [Title("Task 5")]
        [Description("Customers with the sorted date of the first order")]

        public void Linq5()
        {
            //            Сделайте предыдущее задание, но выдайте список отсортированным по году, месяцу и имени клиента
            var customers = dataSource.Customers.Select(c => new
            {
                Customer = c.CompanyName,
                Date = c.Orders.Any() ?
                    c.Orders.DefaultIfEmpty().OrderBy(o => o.OrderDate).FirstOrDefault().OrderDate :
                    new DateTime()
            }).OrderBy(c => c.Date.Year).ThenBy(c => c.Date.Month).ThenBy(c => c.Customer);

            foreach (var c in customers)
            {
                ObjectDumper.Write(c.Customer + ":  " + c.Date.ToShortDateString());
            }
        }

        [Category("LINQ")]
        [Title("Task 6")]
        [Description("Укажите всех клиентов, у которых указан нецифровой почтовый код или не заполнен " +
            "регион или в телефоне не указан код оператора (для простоты считаем, что это " +
            "равнозначно &laquo;нет круглых скобочек в начале&raquo;).")]

        public void Linq6()
        {
            var letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            var operatorCodeSymbols = new char[] { '(', ')' };
            var customers = dataSource.Customers
                .Where(c => (!string.IsNullOrEmpty(c.PostalCode) && c.PostalCode.IndexOfAny(letters) > -1)
                    || string.IsNullOrEmpty(c.Region)
                    || c.Phone.IndexOfAny(operatorCodeSymbols) == -1).Select(c => c.CompanyName);

            foreach (var c in customers)
            {
                ObjectDumper.Write(c);
            }
        }

        [Category("LINQ")]
        [Title("Task 7")]
        [Description("Сгруппируйте все продукты по категориям, внутри &ndash; по наличию на складе, внутри последней группы отсортируйте по стоимости")]

        public void Linq7()
        {
            var products = dataSource.Products.OrderBy(p => p.Category).ThenBy(p => p.UnitsInStock).ThenBy(p => p.UnitPrice);

            foreach (var c in products)
            {
                ObjectDumper.Write(c.Category + "  " + c.UnitsInStock + "  " + c.UnitPrice);
            }
        }

        [Category("LINQ")]
        [Title("Task 8")]
        [Description("Сгруппируйте товары по группам &laquo;дешевые&raquo;, &laquo;средняя цена&raquo;, &laquo;дорогие&raquo;. Границы каждой группы задайте сами")]

        public void Linq8()
        {
            var smaillPrice = 5;
            var mediumPrice = 15;

            var products1 = dataSource.Products.Where(p => p.UnitPrice <= smaillPrice);
            var products2 = dataSource.Products.Where(p => p.UnitPrice <= mediumPrice && p.UnitPrice >= smaillPrice);
            var products3 = dataSource.Products.Where(p => p.UnitPrice >= mediumPrice);

            var products = products1.Concat(products2).Concat(products3);

            foreach (var c in products)
            {
                ObjectDumper.Write(c.ProductName + "  " + c.UnitPrice);
            }
        }

        [Category("LINQ")]
        [Title("Task 9")]
        [Description("Рассчитайте среднюю прибыльность каждого города (среднюю сумму заказа " +
                  "по всем клиентам из данного города) и среднюю интенсивность (среднее количество" +
                  "заказов, приходящееся на клиента из каждого города)")]
        public void Linq9()
        {
            var citiesInfo = dataSource.Customers
                .GroupBy(x => x.City,
                    (city, customers) =>
                    {
                        var totalOrders = customers.Sum(y => y.Orders.Length);

                        return new
                        {
                            City = city,
                            Profitability = customers.Sum(y => y.Orders.Sum(z => z.Total)) / totalOrders,
                            Intensity = totalOrders / customers.Count()
                        };
                    });

            foreach (var cityInfo in citiesInfo)
            {
                Console.WriteLine(cityInfo.City + "    " + cityInfo.Profitability + "    " + cityInfo.Intensity);
            }
        }

        [Category("LINQ")]
        [Title("Task 10")]
        [Description(" Сделайте среднегодовую статистику активности клиентов по месяцам (без учета года)," +
                     "статистику по годам, по годам и месяцам (т.е. когда один месяц в разные годы имеет своё значение).")]
        public void Linq10()
        {
            var statistics = dataSource.Customers.SelectMany(x => x.Orders, (customer, order) => new
                                    {
                                        Month = order.OrderDate.Month,
                                        Order = order
                                    }).GroupBy(x => x.Month, (month, orders) => new
                                    {
                                        Month = month,
                                        Amount = orders.Count()
                                    }).OrderBy(x => x.Month);

            foreach (var info in statistics)
            {
                Console.WriteLine(info.Month + "   " + info.Amount);
            }

        }
>>>>>>> 22daee247d6331977858bac9f87a305602a95f4c
    }
}
