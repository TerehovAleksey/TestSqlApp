namespace TestApp.Dtos;

public record ProductNameDto(string Article, string Name);
public record ProductPriceDto(string Article, decimal Price);
public record ProductCountDto(string Article, int Quantity);

public record ProductDto(string Article, string Name, int Quantity, decimal Price);