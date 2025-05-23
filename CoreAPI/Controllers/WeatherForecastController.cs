using CoreAPI.EFLodingQuery;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;

            
        }


        //public void bac()
        //{
        //    using var context = new AppDbContext();

        //    // Seed data
        //    if (!context.Customers.Any())
        //    {
        //        var customer = new Customer
        //        {
        //            Name = "John Doe",
        //            Orders = new List<Order>
        //            {
        //                new Order { OrderId =1, OrderDate = DateTime.Now.AddDays(-1) },
        //                new Order { OrderId =2, OrderDate = DateTime.Now.AddDays(-2) }
        //            }
        //        };
        //        context.Customers.Add(customer);

        //        var customer1 = new Customer
        //        {
        //            Name = "John Doe",
        //            Orders = new List<Order>
        //            {
        //                new Order { OrderId =1, OrderDate = DateTime.Now.AddDays(-1) },
        //                new Order { OrderId =2, OrderDate = DateTime.Now.AddDays(-2) }
        //            }
        //        };
        //        context.Customers.Add(customer1);
        //        context.SaveChanges();
        //    }

        //    // Eager Loading
        //    Console.WriteLine("Eager Loading:");
        //    var eagerCustomers = context.Customers.Include(c => c.Orders).GroupBy(x => x.Name).ToList();
        //    var eagerCustomers1 = context.Customers.Include(x=>x.Name).ToList();
            
        //    //foreach (var customer in eagerCustomers)
        //    //{
        //    //    Console.WriteLine($"Customer: {customer.Name}");
        //    //    foreach (var order in customer.Orders)
        //    //        Console.WriteLine($"  Order Date: {order.OrderDate}");
        //    //}

        //    // Lazy Loading
        //    Console.WriteLine("\nLazy Loading:");
        //    var lazyCustomer = context.Customers.First();

        //    foreach (var order in lazyCustomer.Orders)
        //        Console.WriteLine($"  Order Date: {order.OrderDate}");

        //    // Explicit Loading
        //    Console.WriteLine("\nExplicit Loading:");
        //    var explicitCustomer = context.Customers.First();
        //    context.Entry(explicitCustomer).Collection(c => c.Orders).Load();

        //    foreach (var order in explicitCustomer.Orders)
        //        Console.WriteLine($"  Order Date: {order.OrderDate}");
        //}

        //[HttpGet(Name = "GetWeatherForecast")]
        //public int Get()
        //{
        //    bac();

        //    return 1;
        //}
    }
}
