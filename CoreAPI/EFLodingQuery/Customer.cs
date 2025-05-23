
namespace CoreAPI.EFLodingQuery
{
    public class Customer
    {
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
