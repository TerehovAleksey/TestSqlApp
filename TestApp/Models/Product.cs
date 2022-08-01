namespace TestApp.Models
{
    public class Product
    {
        public int Id { get; set; }
        public Guid Article { get; set; }
        public string Name { get; set; } = default!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
