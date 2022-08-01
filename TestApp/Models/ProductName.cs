namespace TestApp.Models;

public class ProductName
{
    public int Id { get; set; }
    public Guid Article { get; set; }
    public string Name { get; set; } = default!;
}