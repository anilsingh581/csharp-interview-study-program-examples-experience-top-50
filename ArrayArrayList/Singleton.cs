using System;
using System.Collections.Generic;
using System.Text;

namespace ArrayArrayList
{      

    /*
     * Singleton class for managing the Country list
     Key Points:
        Singleton Pattern: The CountryMaster class ensures only one instance is created using Lazy<T>.
        Caching: The _countyCache stores the list of countries in memory to avoid frequent database calls.
        Lazy Initialization: The Lazy<T> object ensures the instance is created only when accessed.
        Refresh Mechanism: RefreshCountry simulates refreshing data from a database.
        Encapsulation: The constructor is private to restrict direct instantiation of CountryMaster.
     */

    
    public sealed class CountryMaster
    {
        // Static cache to store the list of countries
        private static List<Country> _countyCache;

        // Lazy initialization to ensure the singleton instance is created only when needed
        private static readonly Lazy<CountryMaster> _instnace = new Lazy<CountryMaster>(() => new CountryMaster());

        // Public static property to access the singleton instance
        public static readonly CountryMaster Instance = _instnace.Value;

        // Private constructor to prevent instantiation from outside the class
        private CountryMaster()
        {
            // Initialize the country list when the singleton instance is created
            LoadCountries();
        }

        // Method to load the initial set of countries into the cache
        private void LoadCountries()
        {
            _countyCache = new List<Country>()
            {
                new Country { ID = 0, Name = "India" },
                new Country { ID = 1, Name = "United States" },
                new Country { ID = 2, Name = "UK" },
                new Country { ID = 3, Name = "Mexico" }
            };
        }

        // Method to get the list of countries from the cache
        public List<Country> GetCountries()
        {
            // If the cache is null, reload the initial set of countries
            return _countyCache;
        }

        // Method to refresh the list of countries, simulating a database call
        public List<Country> RefreshCountry()
        {
            // Replace the cache with an updated list of countries
            _countyCache = new List<Country>()
            {
                new Country { ID = 0, Name = "India" },
                new Country { ID = 1, Name = "United States" },
                new Country { ID = 2, Name = "UK" },
                new Country { ID = 3, Name = "Mexico" },
                // Add more countries as needed
            };

            return _countyCache; // Return the refreshed country list
        }
    }

    // Class to demonstrate the usage of the Singleton CountryMaster
    public class Singlton
    {
        public static void Main(string[] args)
        {
            // Get the initial list of countries from the singleton instance
            var listOfCounty = CountryMaster.Instance.GetCountries();
            Console.WriteLine($"GetCountries: ");
            foreach (var country in listOfCounty)
            {
                // Print each country's ID and Name
                Console.WriteLine($"Country ID:  {country.ID}");
                Console.WriteLine($"Country Name:  {country.Name}");
            }

            // Refresh the country list and fetch the updated list
            var refreshListOfCounty = CountryMaster.Instance.RefreshCountry();
            Console.WriteLine($"RefreshCountry: ");
            foreach (var country in refreshListOfCounty)
            {
                // Print each country's ID and Name from the refreshed list
                Console.WriteLine($"Country ID:  {country.ID}");
                Console.WriteLine($"Country Name:  {country.Name}");
            }
        }
    }

    public class Country
    {
        public int ID { get; set; } // Unique identifier for the country
        public string Name { get; set; } // Name of the country
        public string Description { get; set; } // Description of the country
    }

}
