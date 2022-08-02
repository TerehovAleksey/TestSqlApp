namespace TestApp.Models
{
    public class Product : ProductBase
    {
        public string Name { get; set; } = default!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
