namespace TestApp.Dtos;

public record ProductNameDto(Guid Article, string Name);
public record ProductPriceDto(Guid Article, decimal Price);
public record ProductCountDto(Guid Article, int Quantity);

public record ProductDto(Guid Article, string Name, int Quantity, decimal Price);