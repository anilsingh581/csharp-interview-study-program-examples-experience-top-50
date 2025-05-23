using Castle.Core.Resource;
using CoreAPI;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CoreAPI.EFLodingQuery
{
    public class Program
    {
        static void Main(string[] args)
        {
            using var context = new AppDbContext();

            if (!context.Orders.Any())
            {
                context.Orders.Add(new Order { OrderId = 1, CustomerID = "A", Amount = 100 });
                context.Orders.Add(new Order { OrderId = 2, CustomerID = "B", Amount = 150 });
                context.Orders.Add(new Order { OrderId = 3, CustomerID = "A", Amount = 200 });
                context.Orders.Add(new Order { OrderId = 4, CustomerID = "D", Amount = 300 });
                context.Orders.Add(new Order { OrderId = 5, CustomerID = "B", Amount = 120 });

                context.SaveChanges();
            }

            if (!context.Customers.Any())
            {              
                context.Customers.Add(new Customer { CustomerID = "A", CustomerName = "Anil" });
                context.Customers.Add(new Customer { CustomerID = "B", CustomerName = "Binod" });
                context.Customers.Add(new Customer { CustomerID = "A", CustomerName = "Anil" });
                context.Customers.Add(new Customer { CustomerID = "D", CustomerName = "Dinesh" });
                context.Customers.Add(new Customer { CustomerID = "B", CustomerName = "Binod" });

                context.SaveChanges();               
            }

          var OrderDetailOfCustomer = context.Orders
                .GroupBy(x => x.CustomerID)
                .Select(y => new
                {
                    CustomerName=y.Key,
                    TotalAmount= y.Sum(z=>z.Amount),
                    TotalOrders =y.Count()
                }).ToArray();

            //var groupedOrders = o
               
            //.GroupBy(o => o.CustomerID)
            //.Select(g => new
            //{
            //    Customer = g.Key,
            //    TotalOrders = g.Count(),
            //    TotalAmount = g.Sum(x => x.Amount),
            //    Orders = g.ToList()
            //});

            //foreach (var group in groupedOrders)
            //{
            //    Console.WriteLine($"Customer: {group.Customer}, Orders: {group.TotalOrders}, Total: {group.TotalAmount}");
                
            //    foreach (var order in group.Orders)
            //    {
            //        Console.WriteLine($" - OrderId: {order.OrderId}, Amount: {order.Amount}");
            //    }
            //}

        }
    }

    


}

