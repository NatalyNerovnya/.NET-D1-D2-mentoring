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

        [Category("LINQ")]
        [Title("Task 1")]
        [Description("All customers with sum of orders greater than X")]

        public void Linq1()
        {

            del myDelegate = total => dataSource.Customers
                .Where(c => c.Orders.Sum(t => t.Total) > total)
                .Select(c => c.CustomerID);


            var x = 5000;
            var customers5000 = myDelegate(x);

            ObjectDumper.Write("With total x = 5000");
            foreach (var c in customers5000)
            {
                ObjectDumper.Write(c);
            }

            x = 20000;
            var customers20000 = myDelegate(x);
            ObjectDumper.Write("With total x = 20000");
            foreach (var c in customers20000)
            {
                ObjectDumper.Write(c);
            }

            x = 100000;
            var customers100000 = myDelegate(x);
            ObjectDumper.Write("With total x = 100000");
            foreach (var c in customers100000)
            {
                ObjectDumper.Write(c);
            }
        }

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
            {
                ObjectDumper.Write(c);
            }
        }

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
                ObjectDumper.Write(c.Customer + ":  " + c.Date);
            }
            
        }

        [Category("LINQ")]
        [Title("Task 5")]
        [Description("Customers with the sorted date of the first order")]

        public void Linq5()
        {
//            Сделайте предыдущее задание, но выдайте список отсортированным по году, месяцу, 
//оборотам клиента (от максимального к минимальному) и имени клиента

        }
    }
}
