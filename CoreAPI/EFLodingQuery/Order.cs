namespace CoreAPI.EFLodingQuery
{
    public class Order
    {

        public int OrderId { get; set; }       
        public decimal Amount { get; set; }

        public string CustomerID { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
