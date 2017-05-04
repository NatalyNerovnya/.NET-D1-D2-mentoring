// Copyright © Microsoft Corporation.  All Rights Reserved.
// This code released under the terms of the 
// Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//Copyright (C) Microsoft Corporation.  All rights reserved.

using System.Collections;
using System.Linq;
using SampleSupport;
using Task.Data;

// Version Mad01

namespace SampleQueries
{
    [Title("LINQ Module")]
    [Prefix("Linq")]
    public class LinqSamples : SampleHarness
    {

        private DataSource dataSource = new DataSource();
        delegate IEnumerable del(int i);

        [Category("MAX")]
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
    }
}
