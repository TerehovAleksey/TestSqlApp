namespace TestApp.Dtos;

public record ProductNameDto(int Article, string Name);
public record ProductPriceDto(int Article, decimal Price);
public record ProductCountDto(int Article, int Quantity);

public record ProductDto(int Article, string Name, int Quantity, decimal Price);