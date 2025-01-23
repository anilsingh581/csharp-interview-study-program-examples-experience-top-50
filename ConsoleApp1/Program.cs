using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Reflection;
using static System.Collections.Specialized.BitVector32;
using System.Threading;
using System.Reflection.Emit;
using System.Resources;
using System.Runtime.Intrinsics.X86;
using System.Diagnostics;
using System.IO;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace CSharpConceptsConsoleApp
{

    #region Overloading methods Usage
    /*
        Yes, method overloading in C# allows you to define multiple methods with the same name but different parameter types, number of parameters, or both.

        In your case, the two Test methods:

        public int Test(int? a)
        public string Test(string b)
        Are valid overloads, because they differ in the types of parameters (one takes a nullable int and the other takes a string).

        Explanation:
        The first method takes a nullable int (int?), which allows it to accept both int values and null.
        The second method takes a string.
        Since these methods have different parameter types, C# can distinguish between them based on the argument passed, so overloading works correctly in this case.
     

        Things to Keep in Mind:
        Return Type: In method overloading, the return type does not contribute to differentiating the methods. The method signatures must differ in the parameters.
        Nullable Types: int? is a nullable type, which means it can hold both an integer value and null. This makes it different from a non-nullable int, allowing overloading based on this difference.
     */
    /*
    public class OverloadingExample
    {
        // Method accepting nullable int
        public int Test(int? a)
        {
            if (a.HasValue)
            {
                // Do something with 'a'
                return a.Value;
            }
            // Return a default value if 'a' is null
            return 0;  
        }

        // Method accepting string
        public string Test(string b)
        {
            if (string.IsNullOrEmpty(b))
            {
                return "Input is null or empty";
            }
            return $"You entered: {b}";
        }
    }
    
    public class Program
    {
        public static void Main()
        {
            OverloadingExample example = new OverloadingExample();

            // Calling Test with an int? (nullable int)
            int result1 = example.Test(10);      // Output: 10
            Console.WriteLine("Test(10) : " + result1);

            int result2 = example.Test((int?)null);    // Output: 0
            Console.WriteLine("Test((int?)null) : " + result2);

            // Calling Test with a string
            string result3 = example.Test("Hello");  // Output: "You entered: Hello"
            Console.WriteLine("Test(\"Hello\") : " + result3);

            string result4 = example.Test("");      // Output: "Input is null or empty"
            Console.WriteLine("Test(\"\") : " + result4);

            Console.ReadLine();
        }
    }
    */
    #endregion

    #region To find the duplicate records in an integer array in C# - int[] abc = { 1, 2, 3, 4, 5, 2, 1 };

    /*
    class duplicateRecords
    {
        static void Main()
        {
            int[] abc = { 1, 2, 3, 4, 5, 2, 1 };

            // Find duplicates
            var duplicates = abc.GroupBy(x => x)
                                .Where(g => g.Count() > 1)
                                .Select(g => g.Key)
                                .ToList();

            // Display duplicates
            Console.WriteLine("Duplicate records are: " + string.Join(", ", duplicates));
        }
    }
    */
    /*
    class duplicateRecords1
    {
        static void Main()
        {
            int[] abc = { 1, 2, 3, 4, 5, 2, 1 };

            List<int> duplicates = new List<int>();
            List<int> seen = new List<int>();

            // Loop through the array to find duplicates
            for (int i = 0; i < abc.Length; i++)
            {
                // If the element has been seen before and is not already in the duplicates list, add it to the duplicates
                if (seen.Contains(abc[i]) && !duplicates.Contains(abc[i]))
                {
                    duplicates.Add(abc[i]);
                }

                // Add the element to the seen set
                seen.Add(abc[i]);
            }

            // Display duplicates
            Console.WriteLine("Duplicate records are: " + string.Join(", ", duplicates));
        }
    }
    */
    #endregion

    #region Example of a Left Join and Right Join in LINQ with Two Tables ( Customer and Order)
    /*
     Suppose you have two tables (or collections) Customers and Orders, where not every customer has an order. 
        A left join would retrieve all customers, and if they don’t have an order, the result for the order will be null.
     */

    /*
    //[PrimaryKey("Id")]
    public class Customer
    {
        //This will help us auto generated Identity column 'Id' 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Name { get; set; }
    }

    //[PrimaryKey("Id")]
    public class Order
    {
        //This will help us auto generated Identity column 'Id' 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int OrderId { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        //This is virtual Customer and Entity framwork will use to perform CRUD operation.
        public virtual Customer Customer { get; set; }

        public string Product { get; set; }
    }


  
    class Program
    {
        static void Main()
        {           

            List<Customer> customers = new List<Customer>
                        {
                        new Customer { CustomerId = 1, Name = "John" },
                        new Customer { CustomerId = 2, Name = "Jane" },
                        new Customer { CustomerId = 3, Name = "Tom" }
                        };

            List<Order> orders = new List<Order>
                        {
                        new Order { OrderId = 101, CustomerId = 1, Product = "Laptop" },
                        new Order { OrderId = 102, CustomerId = 1, Product = "Tablet" },
                        new Order { OrderId = 103, CustomerId = 2, Product = "Smartphone" }
                        };


            //LINQ Query for Left Join
            var leftJoinQuery = from cust in customers
                                join order in orders
                                on cust.CustomerId equals order.CustomerId into customerOrders
                                from co in customerOrders.DefaultIfEmpty()  // Simulates a left join
                                select new
                                {
                                    CustomerName = cust.Name,
                                    Product = co?.Product ?? "No Order"  // Handle null when no matching order
                                };

            foreach (var item in leftJoinQuery)
            {
                Console.WriteLine($"Customer: {item.CustomerName}, Product: {item.Product}");
            }


            //LINQ Query for Right Join
            //Here, we will swap the Customers and Orders collections and perform a left join:
            var rightJoinQuery = from order in orders
                                 join cust in customers
                                 on order.CustomerId equals cust.CustomerId into orderCustomers
                                 from oc in orderCustomers.DefaultIfEmpty()  // Simulates a right join
                                 select new
                                 {
                                     OrderId = order.OrderId,
                                     Product = order.Product,
                                     CustomerName = oc?.Name ?? "Unknown Customer"  // Handle null when no matching customer
                                 };

            foreach (var item in rightJoinQuery)
            {
                Console.WriteLine($"Order: {item.OrderId}, Product: {item.Product}, Customer: {item.CustomerName}");
            }

        }
    }
    */
    #endregion

    #region To sort half of the array in ascending order and the other half in descending order in C#
    /*  
        To sort half of the array in ascending order and the other half in descending order in C#, you can follow this approach:

        Sort the first half of the array in ascending order.
        Sort the second half of the array in descending order.
     */
    /*
    class Program
    {
        static void Main()
        {
            int[] abc = { 1, 2, 3, 5, 6, 7, 10, 12, 30 };

            // Find the midpoint
            int mid = abc.Length / 2;

            // Sort first half in ascending order
            int[] firstHalf = abc.Take(mid).OrderBy(x => x).ToArray();

            // Sort second half in descending order
            int[] secondHalf = abc.Skip(mid).OrderByDescending(x => x).ToArray();

            // Combine the two halves
            int[] sortedArray = firstHalf.Concat(secondHalf).ToArray();

            // Print the result
            Console.WriteLine("Sorted array:");
            Console.WriteLine(string.Join(", ", sortedArray));     

            //Result > Sorted array: 1, 2, 3, 5,   30, 12, 10, 7, 6
        }
    }
    */


    #endregion

    #region Tuples in c#
    /*
     * In C#, a Tuple is a data structure that allows you to store a fixed number of elements of different types. 
     * It is often used when you want to return multiple values from a method without needing to create a separate class or struct.
     * 
     Summary:
        Tuples allow grouping multiple values of different types.
        They are commonly used for returning multiple values from methods.
        Named tuples enhance readability by allowing meaningful element names.
        They can be easily deconstructed into individual variables.

    Benefits of Using Tuples: Return Multiple Values, Readability
    Drawbacks of Tuples: If not using named tuples, Item1, Item2, etc., might not provide enough clarity for the elements.

     */
    /*
    //Example 1: Basic Tuple Creation and Access
    class Tuple1
    {
        static void Main(string[] args)
        {
            // Creating a tuple with three values
            var tuple = (1, "John Doe", 3.5);

            // Access tuple elements using Item1, Item2, Item3...
            Console.WriteLine($"ID: {tuple.Item1}, Name: {tuple.Item2}, GPA: {tuple.Item3}");

            // Output: ID: 1, Name: John Doe, GPA: 3.5
        }
    }

    //Example 2: Named Tuples
    class Tuple2
    {
        static void Main(string[] args)
        {
            // Creating a tuple with named elements
            var student = (Id: 1, Name: "John Doe", GPA: 3.5);

            // Accessing tuple elements by name
            Console.WriteLine($"ID: {student.Id}, Name: {student.Name}, GPA: {student.GPA}");

            // Output: ID: 1, Name: John Doe, GPA: 3.5
        }
    }

    //Example 3: Returning Multiple Values Using Tuples
    //Tuples are especially useful for returning multiple values from a method without defining a class or struct.
    public class Tuple3
    {
        // Method that returns a tuple with two values
        public static (int, string) GetStudentInfo()
        {
            int id = 101;
            string name = "Alice";
            return (id, name);  // Returning tuple
        }

        static void Main(string[] args)
        {
            // Get tuple values
            var student = GetStudentInfo();

            // Access tuple elements
            Console.WriteLine($"ID: {student.Item1}, Name: {student.Item2}");

            // Output: ID: 101, Name: Alice
        }
    }

    //Example 4: Deconstructing Tuples
    //Deconstruction allows you to unpack tuple values directly into individual variables.
    public class Tuple4
    {
        // Method returning a tuple
        public static (int Id, string Name) GetStudentInfo()
        {
            return (101, "Alice");
        }

        static void Main(string[] args)
        {
            // Deconstructing tuple into individual variables
            (int id, string name) = GetStudentInfo();

            Console.WriteLine($"ID: {id}, Name: {name}");

            // Output: ID: 101, Name: Alice
        }
    }

    */

    #endregion

    #region Country Master Cache using the Singleton pattern in C#

    /*
     Explanation:
        Singleton Pattern:
            The CountryMasterCache class is a Singleton. This means only one instance of the class will be created, and it is accessed using the static Instance property.
            The private constructor prevents external instantiation.
            The Instance property ensures thread-safe access by using double-check locking.
        
        Country Cache:
            The _countryCache field holds the list of countries.
            The LoadCountries() method is responsible for loading the country data (from a database, API, etc.). In this example, it is hardcoded for simplicity.
            The GetCountries() method allows retrieval of the cached countries.
            The RefreshCache() method reloads the country data, allowing you to refresh the cache when needed (e.g., when the country data changes).

        Key Benefits:
            Performance: Since the country list is cached, the system avoids unnecessary repeated calls to the database or API.
            Thread-Safe Singleton: The lock ensures thread safety so that the instance is only created once, even in a multi-threaded environment.
            Flexibility: You can call RefreshCache() to reload the cache when country data changes.
        This design ensures that the country data is cached in memory and only fetched once, improving performance while keeping the cache synchronized with the latest data when required.

     */
    /// <summary>
    /// Singleton Example 1
    /// </summary>
    

    // Define the Country model
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    // Singleton class to manage and cache the list of countries
    public sealed class CountryMaster
    {
        // Cached list of countries
        private static List<Country> _countryCache;

        // Private static instance of the class
        private static readonly Lazy<CountryMaster> _instance = new Lazy<CountryMaster>(() => new CountryMaster());

        // Private constructor to prevent direct instantiation
        private CountryMaster()
        {
            // Initialize the cache
            LoadCountries();
        }

        // Public static property to access the singleton instance
        public static CountryMaster Instance => _instance.Value;

        // Method to load countries into the cache
        private void LoadCountries()
        {
            // Simulate loading data from a data source
            _countryCache = new List<Country>
            {
                new Country { Id = 1, Name = "United States" },
                new Country { Id = 2, Name = "Canada" },
                new Country { Id = 3, Name = "Mexico" },
                // Add more countries as needed
            };
            Console.WriteLine("Countries loaded into cache.");
        }

        // Public method to get all countries
        public List<Country> GetCountries()
        {
            return _countryCache;
        }

        // Public method to refresh the cache
        public void RefreshCountries()
        {
            LoadCountries();
            Console.WriteLine("Country cache refreshed.");
        }
    }

    // Usage example of the CountryMaster Singleton
    class SingletonPattern
    {
        static void Main(string[] args)
        {
            // Access the Singleton instance and retrieve the cached countries
            var countryCache = CountryMaster.Instance;
            var countries = countryCache.GetCountries();

            // Display the countries
            Console.WriteLine("Cached Countries:");
            foreach (var country in countries)
            {
                Console.WriteLine($"Country ID: {country.Id}, Name: {country.Name}");
            }

            // Refresh the cache
            countryCache.RefreshCountries();

            // Display the countries again after refreshing
            Console.WriteLine("\nAfter Refresh:");
            foreach (var country in countryCache.GetCountries())
            {
                Console.WriteLine($"Country ID: {country.Id}, Name: {country.Name}");
            }
        }
    }


    /*
     //* Example 2
    //Define the Country model
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    //Create the CountryMaster Singleton class
    //This class will load and cache the list of countries.
    //We will use a private constructor to prevent direct instantiation and a public static method to access the single instance.
    public sealed class CountryMaster
    {
        // Holds the cached list of countries
        private static List<Country> _countryCache;

        // Private static instance of the class (Singleton)
        private static CountryMaster _instance;

        // Private constructor to prevent direct instantiation
        private CountryMaster()
        {
            // Initialize the cache
            LoadCountries();
        }

        // Public static method to get the singleton instance
        public static CountryMaster Instance
        {
            get
            {                
                // Lock for thread-safety
                //lock (_instance)
                //{
                    if (_instance == null)
                    {
                        _instance = new CountryMaster();
                    }
               // }
                
                return _instance;
            }
        }

        // Method to load countries into the cache (for example, from a database)
        private void LoadCountries()
        {
            // This could be a database call or an API request. 
            // For simplicity, we will hardcode the countries here.
            _countryCache = new List<Country>
            {
                new Country { Id = 1, Name = "United States" },
                new Country { Id = 2, Name = "Canada" },
                new Country { Id = 3, Name = "Mexico" },
                // Add more countries as needed
            };
            Console.WriteLine("Countries loaded into cache.");
        }

        // Public method to get all countries
        public List<Country> GetCountries()
        {
            return _countryCache;
        }

        // Public method to refresh the cache if needed (e.g., if data changes)
        public void RefreshCountries()
        {
            LoadCountries();
            Console.WriteLine("Country cache refreshed.");
        }
    }

    //Use the CountryMasterCache Singleton in your application
    //Here’s how you can access the cached list of countries and interact with the Singleton.
    class SingletonPattern
    {
        static void Main(string[] args)
        {
            // Access the Singleton instance and get the countries
            var countryCache = CountryMaster.Instance;
            var countries = countryCache.GetCountries();

            // Display the countries
            Console.WriteLine("Cached Countries:");
            foreach (var country in countries)
            {
                Console.WriteLine($"Country ID: {country.Id}, Name: {country.Name}");
            }

            // Refresh the cache (e.g., if you want to reload the data)
            countryCache.RefreshCountries();

            // Access the countries again after refreshing the cache
            var refreshedCountries = countryCache.GetCountries();
            Console.WriteLine("\nAfter Refresh:");
            foreach (var country in refreshedCountries)
            {
                Console.WriteLine($"Country ID: {country.Id}, Name: {country.Name}");
            }
        }
    }
    
    */

    #endregion

    #region difference between IEnumerable and IQueryable in C#
    /*
     Key Differences:
     Execution Type:
        IEnumerable: Executes queries in-memory (client-side). Once data is loaded into memory, any further processing (like filtering, projection) is done in-memory.
        IQueryable: Allows deferred execution and enables remote querying (typically to a database). Queries are translated into the query language of the underlying data source (like SQL) and executed on the server.

    When to Use:
        IEnumerable: Best for in-memory collections (like arrays, lists, etc.) and performing operations on data that's already loaded in memory.
        IQueryable: Useful for querying data from external data sources, like databases, because it allows the query to be translated and executed on the database server, optimizing performance by reducing data load and memory usage.

    Query Capability:
        IEnumerable: Supports LINQ-to-Objects, meaning it operates on in-memory collections.
        IQueryable: Supports LINQ-to-SQL, LINQ-to-Entities, etc., meaning it can execute queries in the database or other external systems.

    Deferred Execution:
        IEnumerable: Supports deferred execution, but all data is loaded into memory first before any operation is performed.
        IQueryable: Supports deferred execution and queries are constructed in the form of expression trees, which are translated and executed on the server side.

    Performance:
        IEnumerable: Retrieves all data from the source into memory before filtering. This may lead to inefficiencies when dealing with large datasets.
        IQueryable: Only fetches data when necessary, and filters, ordering, and other operations are performed on the database, which results in more efficient queries for large datasets.

    Key Points in the Example:
    IEnumerable (In-Memory):
        Works with in-memory collections (like List<T>).
        Query execution happens in-memory after all data is loaded.
        Good for small collections that are already in memory.


   IQueryable (Database Querying):
        Works with databases or external data sources (like DbSet<T> in Entity Framework).
        Query is deferred and translated to SQL (or another query language).
        Better for large datasets and optimizing performance by pushing filtering to the database.

   When to Use IEnumerable:
        When working with in-memory collections like arrays, lists, or any data that’s already in memory.
        When dealing with small data sets where performance is not a major concern.

   When to Use IQueryable:
        When working with external data sources (like databases, remote APIs) and you want to avoid loading all data into memory.
        When you want to defer the execution of the query and optimize performance by executing filters, projections, and aggregations directly on the data source.

   Conclusion:
        Use IEnumerable for in-memory data manipulation.
        Use IQueryable when working with external data sources to avoid loading unnecessary data into memory and to allow server-side filtering and querying.

     */

    //Example 1: Using IEnumerable
    //Explanation: The query using IEnumerable is applied to the names list, which is an in-memory collection.
    //The Where method filters the names, but the actual filtering happens in memory.
    /*
    class IEnumerableClass
    {
        static void Main()
        {
            List<string> names = new List<string> { "Alice", "Bob", "Charlie", "David", "Eve" };

            // IEnumerable example (in-memory)
            IEnumerable<string> result = names.Where(name => name.StartsWith("A"));

            // All data is loaded into memory first and filtering is done in-memory
            foreach (var name in result)
            {
                Console.WriteLine(name); // Output: Alice
            }
        }
    }
    */

    //Example 2: Using IQueryable
    //Explanation: The IQueryable query is constructed but not executed until it's enumerated (when the foreach loop runs).
    //The query is sent to the database, and only customers whose names start with "A" are retrieved.
    /*
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    class IQueryableClass
    {
        static void Main()
        {
            using (var context = new MyDbContext())
            {
                // IQueryable example (query executed on database)
                IQueryable<Customer> query = context.Customers.Where(c => c.Name.StartsWith("A"));

                // SQL query is executed here, fetching only the relevant data from the database
                foreach (var customer in query)
                {
                    Console.WriteLine(customer.Name);
                }
            }
        }
    }

    */


    #endregion

    #region What is a delegate in C#?
    //A delegate is a type-safe function pointer in C#.
    //It holds references to the methods
    //It can be used to pass methods as parameters.
    //It can be used Invoke the method via delegate

    /*
    public delegate void PrintDelegate(string message);
    public class Printer
    {
        public void Print(string message)
        {
             Console.WriteLine(message);
        }
    }

    public class PrintDelegateClass
    {
        public static void Main()
        {
            Printer printer = new Printer();
            PrintDelegate printDelegate = printer.Print; // Delegate holds reference to the method
            printDelegate("Hello, Delegate!"); // Invoke the method via delegate
        }
    }

    */

    //What are Func, Action, and Predicate in C#?
    /*
     * In C#, Func, Action, and Predicate are generic delegates used for representing methods or functions.

    Func: A delegate that represents a method that returns a value. It can have 0 to 16 input parameters and one output parameter.
    Func: A delegate that returns a value.
    Func is useful when you need to pass a method that returns a value.

    Signature: Func<T1, T2, ..., TResult>
    T1, T2, ... are the input parameter types.
    TResult is the return type.

    Example: Func<int, int, int> add = (x, y) => x + y; // Func that takes two integers and returns an integer (sum)


    Action: A delegate that represents a method that returns void. It can have 0 to 16 input parameters but no return value.
    Action: A delegate that returns void.
    Action is used when you need to perform an operation without returning a value.

    Signature: Action<T1, T2, ...>
    T1, T2, ... are the input parameter types.
    No return type (always void).

    Example: Action<string> print = message => Console.WriteLine(message);// Action that takes a string and prints it


    Predicate: A delegate that represents a method that takes a single input parameter and returns a boolean value (bool).
    Predicate: A delegate that returns a bool.
    Predicate is used when you need to check conditions or perform boolean tests.

    Signature: Predicate<T>
    T is the input parameter type.
    Always returns bool.

    Example: Predicate<int> isEven = x => x % 2 == 0; // Predicate that checks if a number is even

     */


    #endregion

    #region Unmanaged code in C# Examples
    /*
         * Unmanaged resources are those which are not controlled by .NET CLR runtime like File handle, Connection objects,COM objects and so on.
         * Managed resources are those which are pure .NET objects and these objects are controlled by .NET CLR.
     */

    /// <summary>
    /// Allocate memory from the unmanaged memory of the process by using the specified number of bytes.
    /// Free memory previously allocated from the unmanaged memory of the process. 
    /// </summary>
    /*
    class UnmanagedResourcesClass
    {
        public static void Main(string[] args)
        {
            IntPtr hglobal = Marshal.AllocHGlobal(100);//allocate memory from the unmanaged memory of the process by using the specified number of bytes.
            Marshal.FreeHGlobal(hglobal);//free memory previously allocated from the unmanaged memory of the process. 

            Console.WriteLine("Both tasks completed.");
        }      
    }
    */

    #region SqlConnection objects - unmanaged code (not hanlde by .Net CLR)
    /*
       Best Practices for Connection Objects:
            1. Use using blocks: This ensures that the connection is properly disposed of, freeing up resources.
            2. Keep connections open for the shortest time necessary: Open the connection only when needed and close it as soon as you're done.
            3. Handle exceptions: Always handle potential connection errors to avoid crashes.
            4. Use connection pooling: By default, ADO.NET uses connection pooling, which reuses connections for better performance. Keep connection strings consistent to leverage this feature.

        Other Connection Objects in ADO.NET:
            1. OleDbConnection: Used for connecting to databases via OLE DB providers, like MS Access or Excel.
            2. MySqlConnection: Used to connect to MySQL databases (requires a separate package like MySql.Data from NuGet).
            3. NpgsqlConnection: Used to connect to PostgreSQL databases.

        Opening and Closing the Connection:
            1. connection.Open(): Opens the connection to the database.
            2. connection.Close(): Closes the connection when it's no longer needed.
            3. Connections are expensive resources, so it's important to close them as soon as you're done using them. Using the using statement ensures that the connection is automatically closed and disposed of, even if an exception occurs.
     */

    /// <summary>
    /// Example: Executing a SQL Query: You can use the SqlConnection object along with a SqlCommand to run SQL queries.
    /// </summary>
    /*
    class ConnectionObjectsClass
    {
        static void Main()
        {
            string connectionString = "Data Source=ServerName;Initial Catalog=DatabaseName;User ID=Username;Password=Password";
            string query = "SELECT COUNT(*) FROM Users";

            //start connection scope
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Create a SqlCommand object
                    SqlCommand command = new SqlCommand(query, connection);

                    // Execute the query and get the result
                    int userCount = (int)command.ExecuteScalar();
                    Console.WriteLine("Number of users: " + userCount);
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("SQL error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }//end connection scope
        }
    }
    */
    #endregion

    #region file handle code in c# - FileStream, StreamReader, and StreamWriter - unmanaged code (not hanlde by .Net CLR)
    /*   
     Key Takeaways:
        Use StreamWriter/StreamReader for writing and reading text files.
        Use File class for simple file operations like WriteAllText, ReadAllText, and AppendAllText.
        Always handle exceptions like FileNotFoundException and IOException to manage runtime errors.
        For binary files, use FileStream.
     */

    /// <summary>
    /// Example 1-  Writing to a File: You can write text to a file using the StreamWriter or File.WriteAllText method.
    /// </summary>
    /*
    class StreamWriterClass
    {
        static void Main(string[] args)
        {
            string filePath = "C:\\Users\\anils\\examples\\C#Examples\\ArrayArrayList\\ConsoleApp1\\Files\\example.txt";
            string content = "Hello, this is a test file.";

            // Method 1: Using StreamWriter
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(content);
            }

            // Method 2: Using File.WriteAllText
            File.WriteAllText(filePath, content);

            Console.WriteLine("File written successfully.");
        }
    }
    */

    /// <summary>
    /// Example 2-  Reading from a File: You can read from a file using StreamReader or File.ReadAllText.
    /// </summary>
    /*
    class StreamReaderClass
    {
        static void Main(string[] args)
        {
            string filePath = "C:\\Users\\anils\\examples\\C#Examples\\ArrayArrayList\\ConsoleApp1\\Files\\example.txt";

            // Method 1: Using StreamReader
            using (StreamReader reader = new StreamReader(filePath))
            {
                string content = reader.ReadToEnd();
                Console.WriteLine("File content using StreamReader:");
                Console.WriteLine(content);
            }

            // Method 2: Using File.ReadAllText
            string fileContent = File.ReadAllText(filePath);
            Console.WriteLine("File content using File.ReadAllText:");
            Console.WriteLine(fileContent);
        }
    }
    */

    #endregion

    #endregion

    #region  What is the difference between Task and Thread in C#?
    /// <summary>
    /// Both Threads and Task are used for Concurrent and Parallel programming. 
    /// Concurrent means - executing multiple task in the same core/machine through time-sharing. Parallel means - executing the multiple task in the diffent core/machine simultaneously.
    /// Concurrent means - making program usable. Parallel means - making program performance.
    /// Thread are scheduled by the Operating system while the Task is scheduled by the TPL (Task parallel Liabrary).
    /// Threads offer low-level control but require manual management of synchronization while Tasks provide a higher-level abstraction for asynchronous operations and management are autometically.
    /// Threads is synchronous programing while Task is asynchronous programming using async and await keywords.
    /// Threads is a lightweight unit of execution that operates independently of other threads.
    /// Threads require manual exception handling (try-catch block) within the thread and no exception aggregation while Task handle exceptions automatically to the calling code and aggregate multiple exceptions if necessary using AggregateException for multiple errors.
    /// In general, Tasks provide a more modern, structured way to handle errors, especially when dealing with parallelism or asynchronous programming 
    /// while Thread's code must be inside a try-catch block to handle exceptions and Unhandled exceptions in threads are not caught by the parent thread by default.
    /// Task can return a result. There is no direct mechanism to return the result from Thread.
    /// We can chain Tasks together to execute one after the other but not in the thread.
    /// </summary>

    //Example of Thread (a synchronous programing)
    /*
    public class ThreadExample
    {
        public static void Main(string[] args)
        {
            //create a new thread and specify a method to execute.
            Thread thread = new Thread(ExecuteNumber);

            //thread execute
            thread.Start();

            //Main thread execute
            for (int i = 0; i < 5; i++) {
                Console.WriteLine("Main thread : " +  i);
                Thread.Sleep(500);
            }

            //wait for the thread to complete
            thread.Join();

            Console.WriteLine("Thread execution completed.");
            Console.ReadLine();
        }

        public static void ExecuteNumber() {
            for (int i = 0; i < 5; i++) {
                Console.WriteLine("Second thread : "+ i);
                Thread.Sleep(1000);
            }
        }
    }
    */

    //Error-handling added
    /*
    public class ThreadErrorHandling
    {
        public static void Main(string[] args)
        {
            // Create a new thread and specify a method to execute.
            Thread thread = new Thread(ExecuteNumber);

            // Thread execution wrapped in try-catch to handle any exceptions from the thread.
            try
            {
                // Start the thread.
                thread.Start();

                // Main thread execution
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        Console.WriteLine("Main thread : " + i);
                        Thread.Sleep(500);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Exception in main thread: " + ex.Message);
                    }
                }

                // Wait for the secondary thread to complete.
                thread.Join();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occurred in the secondary thread: " + ex.Message);
            }

            Console.WriteLine("Thread execution completed.");
            Console.ReadLine();
        }

        public static void ExecuteNumber()
        {
            try
            {
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine("Second thread : " + i);
                    Thread.Sleep(1000);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in second thread: " + ex.Message);
            }
        }
    }
    */


    //Example of Task (a asynchronous programing using async and await)
    /*
    public class TaskExample
    {
        public static async Task Main(string[] args)
        {
            //Create a Task and execute a method - ExecuteNumber
            Task task = ExecuteNumber();

            //Continue a main thread
            for (int i = 0; i < 5; i++) {
                Console.WriteLine("Main Thread : " + i);
               await Task.Delay(500);
            }

            await task;

            Console.WriteLine("Task execution is completed.");
            Console.ReadLine();
        }

        public static async Task ExecuteNumber()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Task : " + i);
                //Delay 1 second
                await Task.Delay(1000);
            }
        }
    }
    */

    //Error - Handling
    /*
    public class TaskErrorHandling
    {
        public static async Task Main(string[] args)
        {
            try
            {
                // Create a Task and execute a method - ExecuteNumber
                Task task = ExecuteNumber();

                // Continue the main thread execution
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        Console.WriteLine("Main Thread : " + i);
                        await Task.Delay(500);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Exception in Main Thread: " + ex.Message);
                    }
                }

                // Await the task to complete
                await task;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in Task: " + ex.Message);
            }

            Console.WriteLine("Task execution is completed.");
            Console.ReadLine();
        }

        public static async Task ExecuteNumber()
        {
            try
            {
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine("Task : " + i);
                    // Delay for 1 second
                    await Task.Delay(1000);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in ExecuteNumber: " + ex.Message);
            }
        }
    }
    */

    #endregion

    #region Multithreading vs Multitasking
    /*
     * Multithreading can be a powerful tool for improving the performance and responsiveness of your applications.
     * Multitasking in C# typically involves using asynchronous programming (e.g., async and await with Task objects) to run multiple tasks concurrently without necessarily using multiple threads. This allows your program to perform multiple operations "at the same time," but without blocking a thread while waiting for something like I/O to complete.

    In addition to above points, it would be good to know that:
    1. Task can return a result. There is no direct mechanism to return the result from thread.
    2. We can chain Tasks together to execute one after the other but not in the thread.
    3. Establish a parent/child relationship when one task is started from another task.
    4. Tasks support cancellation through the use of cancellation tokens.
    5. Asynchronous implementation is easy in Task, using async and await keywords.
    6. Task is a lightweight option as Threading can lead to complex code management.
    7. A task is by default a background task. You cannot have a foreground task. On the other hand a thread can be background or foreground (Use IsBackground property to change the behavior).
    8. Tasks created in thread pool recycle the threads which helps save resources. So in most cases tasks should be your default choice.
    9. If the operations are quick, it is much better to use a task instead of thread. For long running operations, tasks do not provide much advantages over threads.

    When to use Multithreading: This can dramatically speed up operations like:
    1. Complex mathematical calculations
    2. Image or video processing
    3. Real-Time Data Processing and Large-scale data analysis (e.g., working with huge datasets)
    4. Games and Real-Time Simulations
    5. Background Operations in UI Applications
    6. Tasks That Can Be Parallelized

    When to Avoid Multithreading:
    1. Too much complexity: Debugging, testing, and managing thread safety can add significant complexity to your code.
    2. Lightweight tasks: If the tasks are very small or simple, the overhead of creating and managing threads can outweigh the benefits of parallelism.
    3. Limited hardware: On systems with only one or two CPU cores, multithreading might not provide any performance gain and could even slow things down due to context switching.
    4. Thread Safety Issues: If the code frequently accesses shared resources, implementing thread safety (e.g., locks) can negate the performance benefits and introduce bugs like deadlocks.


    Multitasking in Microservices Architecture:  
    =========================================
    In a microservices architecture, services often need to make multiple asynchronous API calls to other services or databases. Multitasking helps handle these calls concurrently, reducing latency and improving the overall responsiveness of the system. i.e. 

    Example:
    1. A service that calls multiple downstream services asynchronously to aggregate data.
    2. Handling multiple HTTP requests in parallel in a microservice without blocking the thread.

    Asynchronous Workflow in Real-Time Systems: 
    =========================================
    In systems where real-time responsiveness is essential (e.g., games, live data feeds, real-time collaboration tools), multitasking can be used to handle non-blocking operations like fetching data from external services or calculating real-time statistics.

    Example:
    1. Fetching game updates from a server without blocking the main game loop.
    2. Synchronizing real-time data across clients in collaboration tools.

    Running Tasks in the Background: 
    =========================================
    Sometimes you may need to perform background tasks that don't require immediate attention but still need to be done. For example, background data synchronization, periodic checks, or maintenance tasks can be executed using multitasking, which allows these operations to run asynchronously without affecting the primary workflow.

    Example:
    1. Syncing data with a remote server in the background.
    2. Performing scheduled maintenance tasks like cleaning up log files.


    Parallel Execution of Tasks in Batch Processing: 
    ===============================================
    When you have a large number of small, independent tasks (e.g., processing records in a batch), multitasking allows you to run many of these tasks in parallel. Since each task is short-lived and doesn't require its own thread, using multitasking is more efficient than manually managing threads.

    Example:
    1. Sending batch emails.
    2. Processing multiple records from a file concurrently

    Avoiding Blocking or Deadlock in Web Applications: 
    ==================================================
    In web applications (such as ASP.NET or ASP.NET Core), blocking a thread while waiting for an operation (like a database query or API call) can reduce the scalability of the application. By using multitasking, you allow the system to handle other incoming requests while waiting for the operation to complete.

    Example:
    1. Making asynchronous database calls to avoid blocking the thread pool.
    2. Running multiple background tasks in a web API while responding to client requests.


    Concurrent Processing of Independent Tasks: 
    =========================================    
    If you have multiple independent tasks that need to run concurrently, and they don't require complex inter-thread communication, multitasking is ideal. It enables these tasks to run in parallel (using the thread pool under the hood) without the complexity of manually managing threads.

    Example:
    1. Processing multiple web requests concurrently in a web application.
    2. Executing multiple independent API calls or services in parallel.


    When to Avoid Multitasking:
    While multitasking can provide significant benefits, there are cases where it is not suitable:

    1. Highly CPU-bound tasks: If the tasks are CPU-intensive, parallelism (multithreading) may offer better performance.
    2. Complex Threading Requirements: Multitasking is not suited for cases where precise control over thread synchronization and state is required.
    3. Frequent Task Switching: Too many concurrent tasks may lead to overhead from task switching, reducing performance.
    4. Multitasking is most beneficial when dealing with asynchronous I/O, independent tasks, and non-blocking operations, making applications more responsive and scalable.
    */


    #endregion

    #region Multitasking Examples

    ///Basic Task Parallelism with Task.Run()
    ///The most common way to start tasks in C# is using Task.Run(). This allows you to run multiple tasks concurrently.
    /*
    class MultitaskingExample1
    {
        static async Task Main(string[] args)
        {
            Task task1 = Task.Run(() => PerformTask("Task 1"));
            Task task2 = Task.Run(() => PerformTask("Task 2"));

            await Task.WhenAll(task1, task2);
            Console.WriteLine("Both tasks completed.");
        }

        static void PerformTask(string taskName)
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"{taskName} is running: {i}");
                Task.Delay(1000).Wait(); // Simulate work
            }
        }
    }
    */

    ///Using Task.WhenAll() to Wait for Multiple Tasks
    /// Here we create multiple tasks and use Task.WhenAll() to wait for all of them to finish
    /*
    class MultitaskingExample2
    {
        static async Task Main()
        {
            Task<int> task1 = Task.Run(() => CalculateSum(100));
            Task<int> task2 = Task.Run(() => CalculateSum(200));

            int[] results = await Task.WhenAll(task1, task2);
            Console.WriteLine($"Sum 1: {results[0]}, Sum 2: {results[1]}");
        }

        static int CalculateSum(int max)
        {
            int sum = 0;
            for (int i = 0; i <= max; i++)
            {
                sum += i;
            }
            return sum;
        }
    }
    */

    ///Using Task.WhenAny() for First Completed Task
    ///You can use Task.WhenAny() to perform an action when the first of several tasks completes.
    /*
    class MultitaskingExample3
    {
        static async Task Main()
        {
            Task task1 = Task.Delay(3000);
            Task task2 = Task.Delay(5000);

            Task firstCompletedTask = await Task.WhenAny(task1, task2);
            Console.WriteLine("First task completed!");
        }
    }
    */

    /// Multitasking with async and await
    /// In this example, we use async and await for running multiple asynchronous tasks.
    /// 
    /*
    class MultitaskingExample4
    {
        static async Task Main(string[] args)
        {
            Task task1 = PerformAsyncTask("Task 1", 3000);
            Task task2 = PerformAsyncTask("Task 2", 5000);

            await Task.WhenAll(task1, task2);
            Console.WriteLine("All tasks completed.");
        }

        static async Task PerformAsyncTask(string taskName, int delay)
        {
            Console.WriteLine($"{taskName} started.");
            await Task.Delay(delay); // Simulate work
            Console.WriteLine($"{taskName} completed.");
        }
    }
    */

    ///Handling Return Values with Tasks
    ///Tasks can also return values, which can be awaited using async and await.
    /*
    class MultitaskingExample5
    {
        static async Task Main(string[] args)
        {
            Task<int> task1 = PerformCalculationAsync(10);
            Task<int> task2 = PerformCalculationAsync(20);

            int[] results = await Task.WhenAll(task1, task2);
            Console.WriteLine($"Results: {results[0]} and {results[1]}");
        }

        static async Task<int> PerformCalculationAsync(int number)
        {
            await Task.Delay(2000); // Simulate delay
            return number * 2;
        }
    }
    */

    ///Using Parallel.ForEach() for Concurrent Task Processing
    ///C# also supports Parallel.ForEach() for concurrently processing multiple elements in a collection
    /*
    class MultitaskingExample6
    {
        static void Main(string[] args)
        {
            List<int> numbers = Enumerable.Range(1, 10).ToList();

            Parallel.ForEach(numbers, number =>
            {
                Console.WriteLine($"Processing number: {number}");
                Task.Delay(1000).Wait(); // Simulate work
            });

            Console.WriteLine("All numbers processed.");
        }
    }
    */

    ///Multitasking with I/O-Bound Tasks
    ///Here's an example where multiple tasks are used to simulate fetching data from different web services asynchronously.
    /*
    class MultitaskingExample7
    {
        static async Task Main()
        {
            Task<string> task1 = FetchDataAsync("https://example.com/api1");
            Task<string> task2 = FetchDataAsync("https://example.com/api2");

            string[] results = await Task.WhenAll(task1, task2);
            Console.WriteLine($"Result from API 1: {results[0]}");
            Console.WriteLine($"Result from API 2: {results[1]}");
        }

        static async Task<string> FetchDataAsync(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                string result = await client.GetStringAsync(url);
                return result;
            }
        }
    }
    */


    #endregion

    #region Multithreading Examples
    /*
    class MultithreadingExample1
    {
        static void Main()
        {
            Thread thread1 = new Thread(DoWork);
            thread1.Start();

            Thread thread2 = new Thread(DoWork);
            thread2.Start();
        }

        static void DoWork()
        {
            Console.WriteLine("Work done by thread: " + Thread.CurrentThread.ManagedThreadId);
        }
    }
    */

    /// <summary>
    /// Basic Multithreading with Thread Class
    /// In this example, we manually create and start threads using the Thread class.
    /// </summary>
    /// 
    /*
    class MultithreadingExample2
    {
        static void Main()
        {
            Thread thread1 = new Thread(PerformTask);
            Thread thread2 = new Thread(PerformTask);

            thread1.Start();
            thread2.Start();

            thread1.Join(); // Wait for thread1 to complete
            thread2.Join(); // Wait for thread2 to complete

            Console.WriteLine("Both threads completed.");
        }

        static void PerformTask()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} is running: {i}");
                Thread.Sleep(1000); // Simulate some work
            }
        }
    }
    */

    /// <summary>
    ///  Threading with Parameters Using ParameterizedThreadStart
    /// This example shows how to pass parameters to a thread using ParameterizedThreadStart.
    /// </summary>
    /// 
    /*
    class MultithreadingExample3
    {
        static void Main()
        {
            Thread thread1 = new Thread(new ParameterizedThreadStart(PerformTask));
            Thread thread2 = new Thread(new ParameterizedThreadStart(PerformTask));

            thread1.Start("Thread 1");
            thread2.Start("Thread 2");

            thread1.Join();
            thread2.Join();
        }

        static void PerformTask(object obj)
        {
            string threadName = obj as string;
            for (int i = 0; i < 15; i++)
            {
                Console.WriteLine($"{threadName} is running: {i}");
                Thread.Sleep(1000);
            }
        }
    }
    */

    /// <summary>
    /// Multithreading with Locking for Shared Resources
    ///In this example, we use a lock to prevent multiple threads from accessing a shared resource simultaneously.
    /// </summary>

    /*
    class MultithreadingExample4
    {
        private static int counter = 0;
        private static readonly object lockObject = new object();

        static void Main()
        {
            Thread thread1 = new Thread(IncrementCounter);
            Thread thread2 = new Thread(IncrementCounter);

            thread1.Start();
            thread2.Start();

            thread1.Join();
            thread2.Join();

            Console.WriteLine($"Final Counter Value: {counter}");
        }

        static void IncrementCounter()
        {
            for (int i = 0; i < 1000; i++)
            {
                lock (lockObject) // Ensure only one thread can access this code at a time
                {
                    Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} is running: {i}");
                    counter++;
                }
            }
        }
    }
    */
    /// <summary>
    /// Using Thread Pool
    /// The ThreadPool allows you to execute tasks without creating explicit threads.The thread pool manages worker threads and runs tasks as they become available.
    /// </summary>

    /*
        class MultithreadingExample5
        {
            static void Main()
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(PerformTask), "Task 1");
                ThreadPool.QueueUserWorkItem(new WaitCallback(PerformTask), "Task 2");

                // Wait for user input to keep the application running
                Console.ReadLine();
            }

            static void PerformTask(object state)
            {
                string taskName = state as string;
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine($"{taskName} is running: {i}");
                    Thread.Sleep(1000);
                }
            }
        }
    */

    /// <summary>
    /// Using Monitor for Thread Synchronization
    ///Monitor is a low-level synchronization primitive that can be used instead of lock to control access to shared resources.
    /// </summary>
    /// 
    /*
    class MultithreadingExample6
    {
        private static int counter = 0;
        private static readonly object lockObject = new object();

        static void Main()
        {
            Thread thread1 = new Thread(IncrementCounter);
            Thread thread2 = new Thread(IncrementCounter);

            thread1.Start();
            thread2.Start();

            thread1.Join();
            thread2.Join();

            Console.WriteLine($"Final Counter Value: {counter}");
        }

        static void IncrementCounter()
        {
            for (int i = 0; i < 1000; i++)
            {
                Monitor.Enter(lockObject); // Equivalent to "lock"
                try
                {
                    Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} is running: {i}");
                    counter++;
                }
                finally
                {
                    Monitor.Exit(lockObject);
                    //Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} is running: {i}");
                }
            }
        }
    }

    */

    ///Threading with AutoResetEvent for Signaling
    ///AutoResetEvent is used to signal between threads.In this example, we simulate a producer-consumer scenario.
    /*
    class MultithreadingExample7
    {
        static AutoResetEvent autoResetEvent = new AutoResetEvent(false);

        static void Main()
        {
            Thread producer = new Thread(Produce);
            Thread consumer = new Thread(Consume);

            consumer.Start();
            producer.Start();

            producer.Join();
            consumer.Join();
        }

        static void Produce()
        {
            Console.WriteLine("Producing data...");
            Thread.Sleep(2000); // Simulate work
            autoResetEvent.Set(); // Signal the consumer that data is ready
        }

        static void Consume()
        {
            Console.WriteLine("Waiting for data...");
            autoResetEvent.WaitOne(); // Wait for the signal
            Console.WriteLine("Data received!");
        }
    }

    /// <summary>
    /// Multithreading with ThreadLocal<T>
    ///This example shows how to use ThreadLocal<T> to maintain thread-specific data.
    /// </summary>

    class MultithreadingExample9
    {
        static ThreadLocal<int> threadLocalData = new ThreadLocal<int>(() => Thread.CurrentThread.ManagedThreadId);

        static void Main()
        {
            Thread thread1 = new Thread(() => Console.WriteLine($"Thread 1: {threadLocalData.Value}"));
            Thread thread2 = new Thread(() => Console.WriteLine($"Thread 2: {threadLocalData.Value}"));

            thread1.Start();
            thread2.Start();

            thread1.Join();
            thread2.Join();
        }
    }

    */

    #endregion

    #region ReverseStringWord
    /// <summary>
    /// Reverse each word by converting to array and using LINQ c#
    /// </summary>
    /// 
    /*
    class ReverseStringWord
    {
        static void Main(string[] args)
        {
            string[] word = "Hello Word".Split(" ");
            string result = "";

            for(int i=0; i < word.Length; i++)
            {
                 // Reverse each word by converting to array and using LINQ
                result += new string(word[i].Reverse().ToArray()) + " ";
            }        
              Console.WriteLine(result);
              Console.ReadLine();
        }
    }
    */

    #endregion

    #region Liskov Substitution Principle

    /// <summary>
    /// This example voliting the Liskov Substitution Principle because changes the Vihaviour of the class. Apple color is RED but overide color with ORANGE. 
    /// As per lisko principle - behaviour of the class shoud not change. as per below example behaviour are chaging RED to YELLOW.
    /// </summary>

    /*
    class LISKOExample1
    {
        static void Main(string[] args)
        {
            Apple apple = new Orange();
            Console.WriteLine(apple.GetColor());
        }
    }
    public class Apple
    {
        public virtual string GetColor()
        {
            return "Red";
        }
    }
    public class Orange : Apple
    {
        public override string GetColor()
        {
            return "Orange";
        }
    }
    */

    //Example 2



    /// <summary> 
    ///Here is behaviour of the class are not changing that meanse it follow the Liskov Substitution Principle.
    ///Here is Apple color is RED and Orange color is ORANGE
    /// </summary>
    /*
    class LISKOExample2
    {
        static void Main(string[] args)
        {
            IFruit fruit = new Orange();
            Console.WriteLine($"Color of Orange: {fruit.GetColor()}");
            fruit = new Apple();
            Console.WriteLine($"Color of Apple: {fruit.GetColor()}");
            Console.ReadKey();
        }
    }

    public interface IFruit
    {
        string GetColor();
    }
    public class Apple : IFruit
    {
        public string GetColor()
        {
            return "Red";
        }
    }
    public class Orange : IFruit
    {
        public string GetColor()
        {
            return "Orange";
        }
    }
    */

    #endregion

    #region Extension Method - utility class methods
    /*
     Syntax of Extension Methods
        The class that contains the extension method must be static.
        The method itself must be static.
        The first parameter of the method defines the type that you want to extend, and it must be preceded by the this keyword.
     */

    /// <summary>
    /// Example 1
    /// In this example, the WordCount method is an extension method for the string type.
    /// </summary>
    #region StringExtensions
    /*
    public static class StringExtensions
    {
        // Extension method to count words in a string
        public static int WordsLenthCount(this string str)
        {
            return str.Split(new char[] { ' ', '.', '?' }, StringSplitOptions.RemoveEmptyEntries).Length;
        }
    }
    */
    /*
     Usage of Extension Method
        Once you define the extension method, you can use it just like any other method that belongs to the class being extended. 
        In this case, the WordCount() method can be called on any string object as if it were a built-in method of the string class.
     */

    /*
    class ExtensionMethod
    {
        public static void Main(string[] args)
        {
            string sentence = "Hello, how are you doing today?";
            int count = sentence.WordsLenthCount();

            Console.WriteLine($"Word Count: {count}");
            Console.Read();

        }
    }
    */
    #endregion

    /// <summary>
    /// Example 2
    /// This example demonstrates an extension method for the int type that checks whether a number is even.
    /// </summary>
    /// 
    #region checks whether a number is even or not
    /* 
    public static class IntExtensions
    {
        public static bool IsEven(this int number)
        {
            return number % 2 == 0;
        }
    }

    class ToCheckEvenNumber
    {
        static void Main(string[] args)
        {
            int number = 4;
            bool isEven = number.IsEven();  // Calls the extension method

            Console.WriteLine($"{number} is even: {isEven}");
        }
    }
   */
    #endregion


    /// <summary>
    /// Example 3
    /// Here, the ToPower extension method allows a double to be raised to a specified exponent.
    /// </summary>

    #region allows a double to be raised to a specified exponent
    /*
    public static class DoubleExtensions
    {
        public static double ToPower(this double baseValue, double exponent)
        {
            return Math.Pow(baseValue, exponent);
        }
    }

    // Usage
    class Program
    {
        static void Main(string[] args)
        {
            double number = 2.0;
            double result = number.ToPower(3);  // Calls the extension method
            Console.WriteLine($"{number} raised to the power of 3 is: {result}");
        }
    }
    */
    #endregion

    #region Common Use Cases: LINQ: The most well-known use of extension methods in C# is in LINQ (Where, Select, OrderBy, etc.)

    /*
     Common Use Cases:
            LINQ: The most well-known use of extension methods in C# is in LINQ (Language-Integrated Query), 
            where methods like Where, Select, OrderBy, etc., are added to IEnumerable<T> and IQueryable<T> through extension methods.
     */
    //For example:
    /*
    using System.Linq;

    class LINQextension
    {
        static void Main(string[] args)
        {
            var numbers = new List<int> { 1, 2, 3, 4, 5 };
            var evenNumbers = numbers.Where(n => n % 2 == 0);
        }
    }
    */

    #endregion

    #endregion

    #region Use of 'async' and 'await' in C#

    /// <summary>
    /* 
     * Asynchronous programming allows programs to perform these operations without blocking the calling thread. 
     * When an asynchronous operation is started, the program continues to execute other code while it waits for the operation to complete. 
     * The program is notified when the operation is complete and can continue with the following line of code.

     * Asynchronous programming can be implemented using various techniques, such as callbacks, events, and promises.
       In C#, the "async" and "await" keywords provide a convenient way to write asynchronous code that looks similar to synchronous code, making it easier to read and maintain.

       We will get all the benefits of traditional Asynchronous programming with much less effort with the help of async and await keywords.
    */
    ///  Example 1 - Use of 'async' and 'await' in C#
    /// </summary>

    /*
    class Example1
    {
        static void Main(string[] args)
        {
            Method1();
            Method2();
            Console.ReadKey();
        }
        public static async Task Method1()
        {
            await Task.Run(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine(" Method 1");
                    // Do something
                    Task.Delay(100).Wait();
                }
            });
        }

        public static void Method2()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(" Method 2");
                // Do something
                Task.Delay(100).Wait();
            }
        }
    }
    */

    ///Esample 2 -Use of 'async' and 'await' in C#
    ///
    /*
    class Example2
    {
        static void Main(string[] args)
        {
            callMethod();
            Console.ReadKey();
        }

        public static async void callMethod()
        {
            Task<int> task = Method1();
            Method2();
            int count = await task;
            Method3(count);
        }

        public static async Task<int> Method1()
        {
            int count = 0;
            await Task.Run(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine(" Method 1");
                    count += 1;
                }
            });
            return count;
        }

        public static void Method2()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(" Method 2");
            }
        }

        public static void Method3(int count)
        {
            Console.WriteLine("Total count is " + count);
        }
    }
    */

    ///Example 3 -Use of 'async' and 'await' in C# 
    /*
    class Example3
    {
        static async Task Main(string[] args)
        {
            await callMethod();
            Console.ReadKey();
        }

        public static async Task callMethod()
        {
            Method2();
            var count = await Method1();
            Method3(count);
        }

        public static async Task<int> Method1()
        {
            int count = 0;
            await Task.Run(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine(" Method 1");
                    count += 1;
                }


            });
            return count;
        }

        public static void Method2()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(" Method 2");

            }
        }

        public static void Method3(int count)
        {
            Console.WriteLine("Total count is " + count);
        }
    }
    */
    #endregion

    #region Bitwise Operators in C# (&, |, ^, ~, <<,>>)
    /*
    class BitwiseOperators
    {
        static void Main(string[] args)
        {
            int a = 5, b = 6;
            a = a & b;//=4 

            b = b | a;// 6

            a = a ^ b;//2

            b = b << 1; //letf shift - 6<<1 = (1st no) * (2 to power of 2nd no) = 6 * 2 =12;
            a = a << 1; //2<<1 = 2*2 =4
            int ans = a & b; //4&12 =4
            Console.WriteLine(ans); //Now Ans will be 4

            int c = 10 >> 2; //rigt shift - 10>>2 (1st no)/ (2 to power of 2nd no) = 10/2*2 =10/4 =2
            Console.WriteLine(c); //Now Ans will be 2

            decimal d = 10 >> 2; //rigt shift - 10>>2 (1st no)/ (2 to power of 2nd no) = 10/2*2 =10/4 =2
            Console.WriteLine(d); //Now Ans will be 2
        }
    }
    */
    #endregion

    #region What is the output of this program
    /*
        Because hack and hacker are initialized before h gets its value, both hack and hacker will end up with their default values, which are 0 for integer types in C#.

        Here’s what’s happening step-by-step in your code:

        1. hack is initialized to 0 because hacker is not yet initialized.
        2. hacker is initialized to 0 because h is not yet initialized.
        3. h is initialized to 100.

        When you call Console.WriteLine(hacker);, it prints the value of hacker, which is 0.    
     */
    /*
    class Hackerearth
    {
        static int hack = hacker;
        static int hacker = h;
        static int h = 100;

        static void Main(string[] args)
        {
            Console.WriteLine(hacker);
            Console.WriteLine(h);
            Console.WriteLine(hack);
        }
    }
    */

    #endregion

    #region Reflection Example
    /*    
    1. Reflection used for: Type Inspection
    2. Reflection used for: Assembly Inspection
    3. Reflection used for: Member Inspection
    4. Dynamic Loading: Reflection allows dynamic loading of assemblies at runtime
    5. Reflection used for: Creating Instances
    6. Reflection used for: Invoking Methods
    7. Reflection used for: Accessing Fields and Properties

    8. Performance Considerations:  
    Slower Execution: Reflection is generally slower than direct code execution due to the overhead of metadata inspection and dynamic invocation.
    Use Judiciously: It should be used sparingly in performance-critical applications.

    9. Security Considerations:
    Bypassing Encapsulation: Reflection can access private members, potentially breaking encapsulation and security boundaries.
    Restricted Usage: In secure environments, such as partial trust scenarios, reflection usage might be restricted.

    10. Use Cases:
    Frameworks and Tools: Reflection is widely used in frameworks and tools that need to inspect and manipulate assemblies and types, such as ORMs, serialization libraries, dependency 	injection containers, and testing frameworks.
    Dynamic Programming: It enables dynamic programming techniques, such as creating flexible and reusable components that can operate on unknown types.
  */
    /*
      * Basic Operations with Reflection
      * Getting Type Information:
      * 
            Type type = typeof(MyClass); // Get type from a class
            // or
            MyClass obj = new MyClass();
            Type type = obj.GetType(); // Get type from an instance

      * Getting Assembly Information:
      * 
        Assembly assembly = Assembly.GetExecutingAssembly(); // Current assembly
        // or
        Assembly assembly = typeof(MyClass).Assembly; // Assembly of a specific type

      * Creating Instances:
      * 
        Type type = typeof(MyClass);
        object instance = Activator.CreateInstance(type);

      * Invoking Methods:
      * 
        MethodInfo method = type.GetMethod("MyMethod");
        method.Invoke(instance, new object[] { parameter1, parameter2 });

      * Accessing Fields and Properties:
      * 
        FieldInfo field = type.GetField("MyField");
        object fieldValue = field.GetValue(instance);
        field.SetValue(instance, newValue);

        PropertyInfo property = type.GetProperty("MyProperty");
        object propertyValue = property.GetValue(instance);
        property.SetValue(instance, newValue);

      * Getting Members
        * You can get all members (fields, properties, methods, etc.) of a type:

        MemberInfo[] members = type.GetMembers();
        foreach (MemberInfo member in members)
        {
            Console.WriteLine($"{member.MemberType} {member.Name}");
        }


     */
    /*
    public class AssemblyUseCase
    {
        public static void Main(string[] args)
        {
            //Dynamic Loading Assembly: Reflection allows dynamic loading of assemblies at runtime
            var assembly = Assembly.LoadFile(@"C:\Users\anils\source\repos\AssemblyPOC\AssemblyPOC\bin\Debug\net6.0\AssemblyPOC.dll");

            //Getting Assembly's Class Type
            var MyAssemblyType = assembly.GetType("AssemblyPOC.Customer");

            //Creating Instances
            object assemblyObject = Activator.CreateInstance(MyAssemblyType);

            //Getting Assembly Type 
            Type assemblyType = assemblyObject.GetType();

            //Getting Members using PropertyInfo
            PropertyInfo[] array = assemblyType.GetProperties();
            for (int i = 0; i < array.Length; i++)
            {
                PropertyInfo? property = array[i];
                if (property != null)
                {
                    Console.WriteLine(property.Name);
                }
            }

            Console.ReadLine();
        }
    } */

    #endregion

    #region Read only property - How to write
    /* 
        Characteristics of Read-Only Properties
        1. Immutable after Initialization: Once the property value is set (usually in the constructor), it cannot be changed.
        2. Encapsulation: It helps in encapsulating data and preventing unauthorized modifications, thereby maintaining the integrity of the object's state.
        3. Thread-Safety: Read-only properties can help make your code more thread-safe since they cannot be modified by multiple threads simultaneously.
     */

    /*
    public class ReadOnlyProperty
    {
        public static void Main(string[] args)
        {
            Employee emp = new Employee(1, "abc", "sss");

            Console.WriteLine(emp.Name);
            Console.ReadLine();
        }
    }
    public class Employee
    {
        private int id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public Employee(int _id, string name, string address)
        {
            id = _id;
            Name = name;
            Address = address;
        }
    }
    */
    #endregion

    #region Collections

    #region Generic Collections - List, Dictionry, Shorted List, Stack, Queue

    #region List Collection
    /*
     *  In modern C#, it's recommended to use List<T>, a generic collection that provides type safety, better performance, and avoids boxing/unboxing issues.
     *  
     *  Type Safety: List<T> is type-safe, meaning you can only store elements of the specified type (e.g., List<int>), whereas ArrayList stores everything as object.
        Performance: List<T> avoids boxing and unboxing for value types, making it faster.
        Generic: List<T> provides compile-time type checking, reducing runtime errors and making the code easier to maintain.

    /// <summary>
    /// A List in C# is a dynamic collection that belongs to the System.Collections.Generic namespace.
    /// Key Features of List<T>: Generic type collection, Dynamic Size, Generic Type List<T>, Index-Based Access, Rich Set of Methods, Zero-Based Indexing 
    /// </summary>
    public class ListCollection
    {
        public static void Main(string[] args)
        {
            ///Example 1

            List<int> list = new List<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);

            Console.WriteLine(list.Count);
            foreach (int arg in list) { 
                Console.WriteLine("List - " + arg);
            }


            ///Example 2

            List<User> users = new List<User>();
            users.Add(new User() { id = 1, Name = "Anil", Address = "14th Avanue" });
            users.Add(new User() { id = 2, Name = "Alok", Address = "4th Avanue" });
            users.Add(new User()
            {
                id = 3,
                Name="Harsh",
                Address="12/22 Noida"
            });

            Console.WriteLine("length :" + users.Count);

            foreach (var user in users) { 
                Console.WriteLine($"Id: {user.id}, Name: {user.Name}, Address: {user.Address}");
            }   


            ///Example 3
            List<Employee1> emplist = new List<Employee1>();
            emplist.Add(new Employee1(1,"Anil", "14th Avanue"));
            emplist.Add(new Employee1(2, "Alok", "4th Avanue"));
            emplist.Add(new Employee1(3,"Harsh", "12/22 Noida"));

            Console.WriteLine("length :" + emplist.Count);
            emplist.Reverse();

            foreach (Employee1 emp in emplist) {
                Console.WriteLine($"Id: " + emp.id +" Name:" + emp.Name + " Address: "+ emp.Address);
            }


            Console.ReadLine();
        }
    }

    public class User
    {
        public int id;
        public string Name { get; set; }
        public string Address { get; set; }

    }

    public class Employee1
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public Employee1(int _id, string name, string address) {
            id = _id;
            Name = name;
            Address = address;
        }
    }
    */
    #endregion

    #region Dictionry Collection
    /*
    /// <summary>
    /// A Dictionary in C# is a collection of key-value pairs, where each key is unique and is used to access its corresponding value. It is part of the System.Collections.Generic namespace and is commonly used when you need to associate keys with values and perform lookups efficiently.
    /// Key Features of Dictionary<TKey, TValue>: Generic type collection, Key-Value Pairs, Unique Keys, Fast Lookups, 
    /// </summary>
    public class DictionryCollection
    {
        public static void Main(string[] args)
        {
            Dictionary<int, string> dicObj = new Dictionary<int, string>();
            dicObj.Add(1, "Anil");
            dicObj.Add(2, "Alok");

            foreach (var item in dicObj)
            {
                Console.WriteLine($"Key: " + item.Key + " Value:" + item.Value);
            }

            dicObj.Remove(3);//remove index 2 row

            dicObj.Add(3, "Harsh"); //Added new one row

            foreach(KeyValuePair<int , string> obj in dicObj)
            {
               Console.WriteLine($"Key" + obj.Key + " Val: " + obj.Value);
            }

            foreach (var item in dicObj)
            {
                Console.WriteLine($"Key: " + item.Key + " Value:" + item.Value);
            }

            Console.ReadLine();
        }
    }
    */
    #endregion

    #region SortedList Collection
    /*
    /// <summary>
    /// Key Points: 
    /// Sorting: The SortedList automatically sorts the elements by their keys.
    /// Duplicates: The keys in a SortedList must be unique.If you try to add a key that already exists, it will throw an ArgumentException.
    /// The SortedList is useful when you need a collection that maintains order by keys and provides fast lookup for values based on keys. 
    /// For other types of collections, such as unsorted collections or those that allow duplicate keys,consider using Dictionary or List.
    /// </summary>

    public class SortedListCollection
    {
        static void Main()
        {
            // Create a sorted list with string keys and int values
            SortedList<string, int> sortedList = new SortedList<string, int>();

            // Add key-value pairs to the sorted list
            sortedList.Add("Anil", 39);
            sortedList.Add("Alok", 40);
            sortedList.Add("Harsh", 25);

            // Access values by their keys
            Console.WriteLine("Anil's age: " + sortedList["Anil"]);
            Console.WriteLine("Alok's age: " + sortedList["Alok"]);

            // Iterate through the sorted list
            Console.WriteLine("All entries in sorted order:");

            //foreach (var kvp in sortedList)
            //{
            //    Console.WriteLine(kvp.Key + ": " + kvp.Value);
            //}

            foreach (KeyValuePair<string, int> kvp in sortedList)
            {
                Console.WriteLine($"Key: " + kvp.Key + " Value: " + kvp.Value);
            }

            // Check if a key exists
            if (sortedList.ContainsKey("Harsh"))
            {
                Console.WriteLine("Harsh's age: " + sortedList["Harsh"]);
            }

            // Check if a value exists
            if (sortedList.ContainsValue(25))
            {
                Console.WriteLine("The list contains an age of 25.");
            }

            // Remove a key-value pair
            sortedList.Remove("Alok");

            // Print all entries after removal
            Console.WriteLine("All entries after removing Alok:");
            foreach (KeyValuePair<string, int> kvp in sortedList)
            {
                Console.WriteLine(kvp.Key + ": " + kvp.Value);
            }

            // Get the count of elements
            Console.WriteLine("Number of entries in the sorted list: " + sortedList.Count);

            // Clear the sorted list
            sortedList.Clear();
            Console.WriteLine("Number of entries after clearing: " + sortedList.Count);

            Console.ReadLine();
        }
    }
    */
    #endregion

    #region Stack Collection
    /*
    /// <summary>
    /// A Stack in C# is a collection that follows the Last-In-First-Out (LIFO) principle, meaning that the last element added to the stack is the first one to be removed.
    /// Key Features of Stack<T>: LIFO Structure, Dynamic Size, Generic: The Stack<T> class is generic
    /// Push(T item): Adds an item to the top of the stack.
    /// Pop() : Removes and returns the item at the top of the stack.
    /// Peek(): Returns the item at the top of the stack without removing it.
    /// Clear(): Removes all elements from the stack.
    /// Contains(T item): Determines whether an element is in the stack.
    /// ToArray(): Copies the stack to a new array.
    /// </summary>
    public class StackCollection
    {
        public static void Main(string[] args)
        {
            //Generic - Declaration of stack
            //Stack <string> stack = new Stack<string>();

            //No-generic - Declaration of stack
            Stack stack = new Stack();
            stack.Push("Anil");
            stack.Push(10);
            stack.Push("Alok");

            //List<int> objList =new List<int>();
            //objList.Add(1);
            //objList.Add(2);
            //objList.Add(3);
            //stack.Push(objList);

            //if (stack.Contains("Alok"))
            //{
            //    stack.Push(objList); //Insert an object on top of the stack
            //}            

            // Check if the stack contains an element
            bool containsTen = stack.Contains("Alok");
            if (containsTen)
            {
                stack.Push("Harsh");
            }

            //stack.Pop();//Remove the top value of the stack
            object item = stack.Pop(); // item = Harsh
            Console.WriteLine("stack.Pop() : " + item);

            //stack.Peek();//RETURN the object at the top of stack withouting removing it.
            Console.WriteLine("stack.Peek() : " + stack.Peek());

            foreach(var i in stack)
            {
                Console.WriteLine("Stack: " + i);
            }

            // Clear the stack
            stack.Clear();

            foreach (var item1 in stack)
            {
                Console.WriteLine("item1 : " + item1);

                //List<int> list1 = (List<int>) item;

                //foreach (var it in list1)
                //{
                //    Console.WriteLine(it);
                //}               
            }            
            Console.ReadLine();
        }
    }
    */
    #endregion

    #endregion


    #region Non - Generic Collections - HashTable, Array List, Shorted List, Stack, Queue

    #region HashTable
    /*
     * Key Features of Hashtable:
        Key-Value pairs: Each key is unique and maps to one value.
        Non-generic: Both keys and values are stored as objects (object type).
        Fast lookup: Lookups are done in constant time on average, thanks to hashing.
        Hash codes: Keys are hashed to determine where the value should be placed in the table.
        Thread safety: By default, Hashtable is not thread-safe. However, you can use Hashtable.Synchronized to create a thread-safe wrapper.
     * 
     * Generic Alternative of HashTable : In modern C# applications, prefer Dictionary<TKey, TValue> for better type safety and performance.

    public class hashTableExample
    {
        public static void Main(string[] args)
        {
            Hashtable hashtable = new Hashtable();
            hashtable.Add(1, "Anil");
            hashtable.Add("2", "Alok");
            hashtable.Add(2, null);
            hashtable.Add(3, null);

            Console.WriteLine("hashtable index : " + hashtable[6]);// will not throw error even 6 is not in this collection

            foreach (DictionaryEntry item in hashtable)
            {
                Console.WriteLine("Hashtable Key: " + item.Key +", Value : " + item.Value);
            }
            Console.ReadLine();
        }
    }
    */
    #endregion

    #region ArrayList 
    /*
     * Key Features of ArrayList:
            Dynamic resizing: Automatically resizes as elements are added or removed.
            Non-generic: All elements are stored as object, so you may need to cast them when retrieving.
            Indexed access: You can access elements by their index, similar to arrays.
            Zero-based indexing: Like arrays, indexing starts at 0.
     * 
     * 
         Performance: Since ArrayList is dynamically sized, adding elements can result in resizing, which may have a performance cost.
         Type Safety: As ArrayList stores elements as object, there is no compile-time type checking, and you must cast elements when retrieving them (leading to possible runtime errors).
         Boxing/Unboxing: Since value types (like integers and floats) are stored as object, there is a cost associated with boxing and unboxing these values when adding or retrieving them.
         Better Alternative of ArrayList is List<T>
     */
    /*
    public class ArrryListExample
    {              
        public static void Main(string[] args)
        {
            ArrayList list = new ArrayList();
            list.Add("Anil");
            list.Add("Alok");
            list.Add("Harsh");
            list.Add(1);


            foreach (var lst in list)
            {
                Console.WriteLine("Result is : " + lst);

            }
        }
    }
    */

    #endregion

    #region Array - not a Collections part. Added for example only
    /*
     * Key Features of Arrays in C#:
            Fixed Size: Once an array is created, its size cannot be changed.
            Zero-based Indexing: The first element is at index 0, the second at index 1, and so on.
            Type-Safe: All elements in an array must be of the same type (e.g., int[], string[]).
            Efficient Access: Accessing an element by its index is a constant-time operation (O(1)).
            Multi-dimensional Arrays: C# supports single-dimensional, multi-dimensional (rectangular), and jagged arrays (arrays of arrays).
     * 
     * 
     * 
    class Program
    {

        static int[] arr = new int[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        static int[] arr1 = new int[2];

        static ArrayList arrList = new ArrayList();


        static void Main(string[] args)
        {
            //Arry
            for (int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine(arr[i]);
            }

            //Arr1
            arr1[0] = 20;
            arr1[1] = 30;
            Array.Resize(ref arr1, 3);
            arr1[2] = 40;

            for (int i = 0; i < arr1.Length; i++)
            {
                Console.WriteLine(arr1[i]);
            }

            //ArrayList
            arrList.Add(100);
            arrList.Add("Anil Singh");
            arrList.Add(200);
            arrList.Add(300.5);
            //arrList.Add("Capacity");

            //Console.WriteLine(arrList.Capacity);
            //arrList.Insert(1, "Insert value");
            //arrList.Remove(3.5);

            foreach (var item in arrList)
            {
                Console.WriteLine(Convert.ToString(item +" "));
            }           

            //for (int i = 0; i < arrList.Count; i++)
            //{               
            //    Console.WriteLine(Convert.ToString(arrList[i]));
            //}

            Console.ReadLine();
        }
    }
    */

    #endregion

    #endregion

    #endregion

    #region Read-Only Property with an Expression Body
    /*
    public class ReadOnlyPropertyExpression
    {
        // Read-only property with an expression body
        public string Name { get; }

        // Constructor to initialize the property
        public ReadOnlyPropertyExpression(string name)
        {
            Name = name;
        }

        // Read-only property with an expression body
        public int Age { get; } = 39;

        // Read-only property with an expression body method
        public string FullName => $"{Name} Singh";
    }

    class Program3
    {
        static void Main()
        {
            ReadOnlyPropertyExpression person = new ReadOnlyPropertyExpression("Anil");
            Console.WriteLine(person.Name);     // Output: Anil
            Console.WriteLine(person.Age);      // Output: 39
            Console.WriteLine(person.FullName); // Output: Anil Singh
        }
    }
    */
    #endregion

    #region Read-Only Auto-Implemented Property
    /*
    public class ReadOnlyPropertyAuto
    {
        // Read-only auto-implemented property
        public string Name { get; }

        // Constructor to initialize the property
        public ReadOnlyPropertyAuto(string name)
        {
            Name = name;
        }
    }

    public class Program2
    {
        static void Main()
        {
            ReadOnlyPropertyAuto person = new ReadOnlyPropertyAuto("Anil Singh");
            Console.WriteLine(person.Name); // Output: Anil Singh

            // The following line would cause a compile-time error
            // person.Name = "Anil Singh";
        }
    }
    */
    #endregion

    #region Read-Only Property with a Backing Field
    /*
    public class ReadOnlyProperty
    {
        // Private backing field
        private string _name;

        // Constructor to initialize the property
        public ReadOnlyProperty(string name)
        {
            _name = name;
        }

        // Read-only property
        public string Name
        {
            get { return _name; }
        }
    }

    public class Program1
    {
        static void Main()
        {
            ReadOnlyProperty person = new ReadOnlyProperty("Anil Singh");
            Console.WriteLine(person.Name); // Output: Anil Singh

            // The following line would cause a compile-time error
            // person.Name = "Anil Singh";
        }
    }
    */
    #endregion

    #region JSON manipulation in c#

    /* Out Put will be -
     *  {
          "name": {
            "first": "Robert",
            "last": "Smith"
          },
          "age": 25,
          "hobbies": [
            "running",
            "coding"
          ],
          "education": {
            "college": "Yale"
          }
        }
     */
    /*
     * INPUT will be - Remove (-, N/A, and blank) from Input and output will be above
        string jsonString = @"
            {
                'name': {
                    'first': 'Robert',
                    'middle': '',
                    'last': 'Smith'
                },
                'age': 25,
                'DOB': '-',
                'hobbies': [
                    'running',
                    'coding',
                    '-'
                ],
                'education': {
                    'highschool': 'N/A',
                    'college': 'Yale'
                }
            }";
     */
    /*
    public class JSONManipulationProgram
    {
        public static async Task Main()
        {
            // Original JSON string - INPUT
            //string jsonString = @"
            //    {
            //        'name': {
            //            'first': 'Robert',
            //            'middle': '',
            //            'last': 'Smith'
            //        },
            //        'age': 25,
            //        'DOB': '-',
            //        'hobbies': [
            //            'running',
            //            'coding',
            //            '-'
            //        ],
            //        'education': {
            //            'highschool': 'N/A',
            //            'college': 'Yale'
            //        }
            //    }";

            //OR 
            HttpClient client = new HttpClient();
            string jsonString = await client.GetStringAsync("https://coderbyte.com/api/challenges/json/json-cleaning");

            // Parse the JSON string into a JObject
            JObject parseJObject = JObject.Parse(jsonString);

            // Create a new JObject to hold the filtered data
            JObject jsonObjectReturn = new JObject();

            // Iterate over each property in the original JObject
            foreach (var property in parseJObject.Properties())
            {
                string propertyName = property.Name;
                JToken val = property.Value;

                if (val.Type == JTokenType.Object)
                {
                    JObject objectData = new JObject();

                    // Iterate over each sub-property in the JObject
                    foreach (var subProperty in ((JObject)val).Properties())
                    {
                        string subPropertyName = subProperty.Name;
                        JToken data = subProperty.Value;

                        if (!string.IsNullOrEmpty(data.ToString()) && data.ToString() != "-" && data.ToString() != "N/A")
                        {
                            objectData[subPropertyName] = data;
                        }
                    }

                    jsonObjectReturn[propertyName] = objectData;
                }
                else if (val.Type == JTokenType.Array)
                {
                    JArray arrayData = new JArray();

                    // Iterate over each item in the JArray
                    foreach (var item in (JArray)val)
                    {
                        if (!string.IsNullOrEmpty(item.ToString()) && item.ToString() != "-" && item.ToString() != "N/A")
                        {
                            arrayData.Add(item);
                        }
                    }

                    jsonObjectReturn[propertyName] = arrayData;
                }
                else
                {
                    if (!string.IsNullOrEmpty(val.ToString()) && val.ToString() != "-" && val.ToString() != "N/A")
                    {
                        jsonObjectReturn[propertyName] = val;
                    }
                }
            }

            // Convert the filtered JObject to a JSON string
            string resultJsonString =  Convert.ToString(jsonObjectReturn);

            // Output the result
            Console.WriteLine(resultJsonString);
        }
    }
    */
    #endregion

    #region Check records is exists in Dictionary in c# 
    /*
    public class DictionaryExample
    {
        static void Main()
        {
            // Create a dictionary with some sample data
            Dictionary<string, int> myDictionary = new Dictionary<string, int>();
            //Set Value will be -
            //{
            //    { "apple", 1 },
            //    { "banana", 2 },
            //    { "orange", 3 }
            //};

            //Set Value will be -
            myDictionary.Add("apple", 1);
            myDictionary.Add("banana", 2);
            myDictionary.Add("orange", 3);

            // Get key to check from the user
            Console.WriteLine("Enter the key to check:");
            string keyToCheck = Console.ReadLine();

            // Check if the key exists in the dictionary
            if (myDictionary.ContainsKey(keyToCheck))
            {
                Console.WriteLine("The key '" + keyToCheck + "' exists in the dictionary with value: " + myDictionary[keyToCheck]);
            }
            else
            {
                Console.WriteLine("The key '" + keyToCheck + "' does not exist in the dictionary.");
            }
        }
    }
    */
    #endregion

    #region What will be the output
    /*
      public class Animal
      {
          static Animal()
          {
              Console.WriteLine("I am animal constructor.");
          }

          protected int getAnimalId()
          {
              return 1;
          }
      }
      public class Dog : Animal
      {
          static Dog()
          {
              Console.WriteLine("I am dog constructor.");
          }

          protected int getDogId()
          {
              return 1;
          }

      }
      class Program
      {
          static void Main(string[] args)
          {
              //Dog obj = new Dog();
              //obj.
              Console.Read();
          }
      }
   */
    #endregion

    #region Parameters in C# - Value Parameter, ref Parameter, out Parameter, and Parameter Array
    /*
    public class ParametersInCSharp
    {
        public static void Main()
        {
            // Value Parameter - Passed by value
            //Any changes to the parameter within the method do not affect the original argument.
            int valueParam = 10;
            ValueParamExample(valueParam);
            Console.WriteLine("Value Parameter after method call: " + valueParam); // Output: 10

            // Reference Parameter - Passed by reference, meaning the method can modify the original argument.
            //The argument must be initialized before being passed.
            int refParam = 10;
            RefParamExample(ref refParam);
            Console.WriteLine("Reference Parameter after method call: " + refParam); // Output: 100

            // Output Parameter - Passed by reference and must be assigned a value before the method returns.
            //The argument does not need to be initialized before being passed.
            int outParam;
            OutParamExample(out outParam);
            Console.WriteLine("Output Parameter after method call: " + outParam); // Output: 100

            // Parameter Array - Allows passing a variable number of arguments to a method.
            //Must be the last parameter in the method signature.
            ParamArrayExample(1, 2, 3, 4, 5); // Output: 1 2 3 4 5
        }

        public static int ValueParamExample(int param)
        {
           return param = 100;
        }

        public static void RefParamExample(ref int param)
        {
            param = 100;
        }

        public static void OutParamExample(out int param)
        {
            param = 100;
        }

        public static void ParamArrayExample(params int[] numbers)
        {
            foreach (int number in numbers)
            {
                Console.WriteLine(number);
            }
        }
    }
    */

    #endregion

    #region Abstraction Vs Encapsulation
    /*
        * Abstraction deals with providing only necessary functionality and hiding the complex details.
        * Encapsulation deals with bundling the data and methods into a class and controlling access to the data to protect it.

      Abstraction:
        1. Focuses on hiding implementation details from the user.
        2. Achieved using abstract classes and interfaces.	
        3. Helps in focusing on the essential functionalities.
        4. Purpose: Focuses on what an object does, rather than how it does it.


      Encapsulation: 
        1. Focuses on hiding the data and controlling access.
        2. Achieved using access modifiers, properties, and getters/setters.
        3. Helps in ensuring data integrity and restricting unauthorized access.        
        4. Purpose: Focuses on controlling access to data using access modifiers like private, protected, and public.
     */

    /*
    public class Abstraction
    {
        public static void Main(string[] args)
        {
            //Encapsulation and Abstraction working together
            //Abstraction
            //Showing only what is necessary - Abstraction
            //Abstraction happen in the design phase

            //Encapsulation
            //Hide the complexcity (ValidateName and ValidateAddress) of this class - Encapsulation
            //Encapsulation happed in the coding phase
            Employee employee = new Employee();
            employee.Name = "Anil";
            employee.Address = "Gaur City 2";
            employee.Validate(employee);

            Console.WriteLine();
        }
    }

    public class Employee
    {
        //Show only what is necessary - That is called Abstraction
        public string Name { get; set; }

        //Show only what is necessary - That is called Abstraction
        public string Address { get; set; }


        //Show only what is necessary - That is called Abstraction
        //Show only method not internal/developer logic
        public bool Validate(Employee e)
        {
            bool flag = false;
            if (ValidateName(e))
            {
                flag = true;
            }

            if (ValidateAddress(e))
            {
                flag = true;
            }
            return flag;
        }

        //Hide complexcity of this class - That is called Encapsulation
        //That is the reason we made is private - because this is internal/develper logic. Do not need to show at end user.
        private bool ValidateName(Employee e)
        {
            if (e.Name == null)
            {
                return false;
            }
            return true;
        }

        //Hide complexcity of this class - That is called Encapsulation
        //That is the reason we made is private - because this is internal/develper logic. Do not need to show at end user.
        private bool ValidateAddress(Employee e)
        {
            if (e.Address == null)
            {
                return false;
            }
            return true;
        }

    }
    */
    #endregion

    #region SwapTwoNumber
    /*
    public class SwapTwoNumber
    {       
        public static void Main( string[] args)
        {
            int a = 3;
            int b = 5;

            Console.WriteLine($"Before swap: a = {a}, b = {b}");

            // Swap the values with using a third variable
            //int temp = a;
            //a = b;
            //b = temp;

            // Swap the values without using a third variable
            a = a + b;
            b = a - b;
            a = a - b;

            Console.WriteLine($"After swap: a = {a}, b = {b}");
        }
    }
    */
    #endregion

    #region Odd & Even Number Tables
    /*
    public class OddEvenNumberTables
    {
        static void Main()
        {

            //EXAMPLE 1
            for (int i = 1; i <= 100; i++)
            {
                int a = i % 2;
                //if a==0 the even Number and if a==1 the odd number
                if (a == 0)
                {
                    Console.WriteLine(i);
                }
            }
            Console.ReadLine();


            #region Tables of 7
            //Tables of 7

            //int tableForWhat = 7;

            //for (int i = 1; i <= 10; i++) {
            //    Console.WriteLine(i* tableForWhat);
            //}

        }
    }
   */
    #endregion

    #region  Even odd number finding program
    /*
        1.Print all even numbers from 1 to 10, 
        2.Print the sum of all odd numbers from 1 to 10, 
        3.Print all numbers from 1 to 10 that are divisible by 3
     */
    /*
    class EvenOddNumber
    {
        static void Main()
        {
            // 1. Print all even numbers from 1 to 10
            Console.WriteLine("Even numbers from 1 to 10:");
            for (int i = 1; i <= 10; i++)
            {
                if (i % 2 == 0)
                {
                    Console.WriteLine(i);
                }
            }

            // 2. Print the sum of all odd numbers from 1 to 10
            int sumOfOdds = 0;
            for (int i = 1; i <= 10; i++)
            {
                if (i % 2 != 0)
                {
                    sumOfOdds += i;
                }
            }
            Console.WriteLine($"\nSum of all odd numbers from 1 to 10: " + sumOfOdds);

            // 3. Print all numbers from 1 to 10 that are divisible by 3
            Console.WriteLine("\nNumbers from 1 to 10 divisible by 3:");
            for (int i = 1; i <= 10; i++)
            {
                if (i % 3 == 0)
                {
                    Console.WriteLine(i);
                }
            }
        }
    }*/

    #endregion

    #region Var vs Dynamic
    /*
    public class VarVSDynamic
    {
        public static void Main(string[] args)
        {
            ///Var keyword is a statically typed and determine the things at the compile time.
            ///It must be initialized at the point of declaration.
            ///Once value is assinged in variable the type can not not change  
            var x = "Anil Singh";
            //x.
            // once type the (x.) all the string operations are avaliable at the time of codeing (i mean compile time)
            // x = 1;//It will get error >> cannot impleciltly convert type int to string. 

            ///Dynamic keyword is a dynamic typed and determine the things at the run-time.
            ///The dynamic value can be initialized or it cannot at the point of declaration. Its tottaly depend on you.
            dynamic dy = "Anil singh";
            //dy.
            // once type the (dy.) not any string operation are avaliable at the time of codeing (i mean compile time). All string operations will avaliable on run-tine only.
            dy = 1;// we can impleciltly convert type int to string. Not getting error.

            Console.WriteLine("Hello");
        }
    }
    */
    #endregion

    #region abstract vs interface
    // Abstract class is a half defied base class. Interface is a contract
    ///A class can implement any number of interfaces but a subclass can implement only one abstract class because c# not support multiple inheritance.
    ///An abstract class can have non-abstract Methods(concrete methods) while in the case of Interface, all the methods have to be abstract.
    ///An abstract class can declare or use any variables while an interface is not allowed to do so.
    ///An abstract class can have a constructor declaration while an interface can not do so.
    ///An abstract Class is allowed to have all access modifiers for all of its member declarations while in the interface we can not declare any access modifier(including public) as all the members of the interface are implicitly public.


    //abstract
    #region abstract - multiple inheritence not support
    /*
    public class absClass : abstractEat
    {
        public static void Main(string[] args)
        {
        }

        public override void Eat()
        {
            throw new NotImplementedException();
        }
    }
    public abstract class abstractSleep
    {
        public abstract void Sleep();
    }
    public abstract class abstractEat
    {
        protected abstractEat()
        {

        }
        public abstract void Eat();
        public virtual void abc()
        {

        }

    }
    */
    #endregion

    //interface
    #region interface - multiple inheritence support - explicity and implecitly
    /*
    public class interfaceCalss: IEat,ISleep
       {
           public  static void Main(string[] args)
           {

           }

        void IEat.eact()
        {
            throw new NotImplementedException();
        }

        void IEat.sleep()
        {
            throw new NotImplementedException();
        }

        void ISleep.sleep()
        {
            throw new NotImplementedException();
        }
    }

       public interface IEat{
            void eact();
            void sleep();
       }

       public interface ISleep { 
          void sleep();
       }
    */
    #endregion

    #endregion

    #region Singleton - Examle with Create instance and County master data cache

    /// <summary>
    /// private constructor can have n number of private constructor
    /// Static constructor can have only one Static constructor
    /// Only private constructor not allow to create intance out side of this class
    /// If we create a private constructor and a public constructor in the same class, it will allow to create intance out side of this class 
    /// </summary>
    /// 
    /*
    class SimpleConsuctors
    {
        private static int counter;
        //private constructor 
        private SimpleConsuctors() { 
            counter = 50;
        }

        //public constructor
        public SimpleConsuctors(int count)
        {
            counter += count;
        }

        public static int getCounter()
        {
            return counter;
        }
    }

    public class ConstructorTest
    {
        public static void Main(string[] args)
        {
            ///Create the objects of SimpleConsuctors class
            ///And Assign the inputs value of public constructor 
            SimpleConsuctors simpleConsuctors = new SimpleConsuctors(10);

            Console.WriteLine("SimpleConsuctors.getCounter() :" + SimpleConsuctors.getCounter());
            Console.ReadLine();
        }
    }
    */
    /// <summary>
    /// difference between static and private constructor in c#
    /// </summary>
    /// 
    /*
    class SimpleClass
    {
        // Static variable that must be initialized at run time.
       public static long datetime;

        // Static constructor is called at most one time, before any
        // instance constructor is invoked or member is accessed.
        ///Static constructor can have only one Static constructor
        ///Static constructor use to Initialize static field or data
        ///Access modifier not allowed in the static constructor
        static SimpleClass()
        {
            datetime = DateTime.Now.Ticks;
        }

        public void test()
        {
            Console.WriteLine("static CONSUCTOR");
        }
    }
    */
    #region Create Singleton instance
    /// <summary>
    /// Private constructors Prevent object creation from outside the class, enforce the singleton design pattern
    /// </summary>
    /// 



    /*
    public class Singleton
    {
        static Singleton s_myInstance = null;
        private Singleton()
        {
        }

        public Singleton New_Instance
        {
            get
            {
                lock (s_myInstance)
                {
                    if (s_myInstance == null)
                        s_myInstance = new Singleton();
                    return s_myInstance;
                }
            }
        } 
    }*/

    #endregion


    //Singleton
    /*
    public class Employee
    {
        public static void Main(string[] args)
        {
            var country = singletoneWithStatic.getCounty();
             country = singletoneWithStatic.refresh();

            var country1 = singletoneWithSealed.getCounty();
            country1 = singletoneWithSealed.refresh();
        }
    }

    /// <summary>
    /// singletone Example 1 With Static keyword
    /// </summary>
    public static class singletoneWithStatic
    {
        private static List<county> county_ = null;

        public static List<county> getCounty()
        {
            if (county_ == null)
            {
                county_ = new List<county>();
                county_.Add(new county()
                {
                    id = 1,
                    Name = "India"
                });

            }
            return county_;
        }

        public static List<county> refresh()
        {
            lock (county_)
            {
                county_ = new List<county>();
                county_.Add(new county()
                {
                    id = 2,
                    Name = "USA"
                });
            }

            return county_;
        }
    }

    /// <summary>
    /// Singletone Example 1 With Sealed keyword
    /// </summary>
    public sealed class singletoneWithSealed
    {
        private static List<county> county_ = null;

        public static IEnumerable<county> getCounty()
        {
            if (county_ == null)
            {
                county_ = new List<county>();
                county_.Add(new county()
                {
                    id = 1,
                    Name = "India"
                });
            }
            return county_;
        }

        public static IEnumerable<county> refresh()
        {
            lock (county_)
            {
                county_ = new List<county>();
                county_.Add(new county()
                {
                    id = 2,
                    Name = "USA"
                });
            }

            return county_;
        }
    }

    public class county
    {
        public int id { get; set; }
        public string Name { get; set; }

    }

    */
    #endregion

    #region  lamda Functionand With Delegates
    /*

    public class lamdaFunctionandWithDelegatesExamples
    {
        public static void Main(string[] args)
        {
            //With List example
            List<string> list = new List<string>() { "Anil", "Alok", "Harsh", "Chetan", "Anil"};

            //Expression Lambdas
            //left side is input and right side is expression
            var count = list.Count(x => x == "Anil");

            Console.WriteLine("Count of List is : " + count);

            //Statement Lambdas
            //left side is input and right side is expression
            var count1 = list.Count(x => {
                return x == "Anil";
            });

            Console.WriteLine("Count 1 of List is : " + count);

            //With Delegate example 1
            Func<int, int> squareDelegate = x => x * x;
            Console.WriteLine("Square of Delegate is : " + squareDelegate(5));

            //With Delegate example 2
            Func<int, int> addDelegate = x => x + x;
            Console.WriteLine("Add of Delegate is : " + addDelegate(5));

        }
    }
    */

    #endregion


}
