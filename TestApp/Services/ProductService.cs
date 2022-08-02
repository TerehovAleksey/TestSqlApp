using Bogus;
using Microsoft.EntityFrameworkCore;
using TestApp.Data;
using TestApp.Dtos;
using TestApp.Models;

namespace TestApp.Services;

public class ProductService : IProductService
{
    private readonly IDbContextFactory<AppDb1Context> _db1Factory;
    private readonly IDbContextFactory<AppDb2Context> _db2Factory;

    public ProductService(IDbContextFactory<AppDb1Context> db1Factory, IDbContextFactory<AppDb2Context> db2Factory)
    {
        _db1Factory = db1Factory;
        _db2Factory = db2Factory;
    }

    public async Task<bool> GenerateFakeData()
    {
        var faker = new Faker<Product>()
            .RuleFor(x => x.Article, f => f.Random.Int(250000, 500000).ToString())
            .RuleFor(x => x.Name, f => f.Commerce.Product())
            .RuleFor(x => x.Price, f => decimal.Parse(f.Commerce.Price()))
            .RuleFor(x => x.Quantity, f => f.Random.Int(1,1000));

        var list = faker.Generate(30);
        
        await using var db1Context = await _db1Factory.CreateDbContextAsync();
        await db1Context.Amounts.AddRangeAsync(list.Take(20).Select(x=>new ProductCount {Id = x.Id, Article = x.Article, Quantity = x.Quantity}));
        await db1Context.Names.AddRangeAsync(list.Take(20).Select(x=>new ProductName {Id = x.Id, Article = x.Article, Name = x.Name}));
        await db1Context.Prices.AddRangeAsync(list.Take(20).Select(x=>new ProductPrice {Id = x.Id, Article = x.Article, Price = x.Price}));
        var result = await db1Context.SaveChangesAsync();
        
        await using var db2Context = await _db2Factory.CreateDbContextAsync();
        await db2Context.Products.AddRangeAsync(list.Skip(10));
        result += await db2Context.SaveChangesAsync();

        return result > 0;
    }

    public async Task ClearTables()
    {
        await using var db1Context = await _db1Factory.CreateDbContextAsync();
        await db1Context.Database.ExecuteSqlRawAsync($"DELETE FROM {nameof(db1Context.Names)}");
        await db1Context.Database.ExecuteSqlRawAsync($"DELETE FROM {nameof(db1Context.Amounts)}");
        await db1Context.Database.ExecuteSqlRawAsync($"DELETE FROM {nameof(db1Context.Prices)}");
        
        await using var db2Context = await _db2Factory.CreateDbContextAsync();
        await db2Context.Database.ExecuteSqlRawAsync($"DELETE FROM {nameof(db2Context.Products)}");
    }

    #region DB1

    public async Task<IEnumerable<ProductNameDto>> GetProductNames()
    {
        await using var dbContext = await _db1Factory.CreateDbContextAsync();
        var result = await dbContext.Names
            .Select(x=> new ProductNameDto(x.Article, x.Name))
            .AsNoTracking()
            .ToListAsync();
        return result;
    }

    public async Task<IEnumerable<ProductCountDto>> GetProductQuantity()
    {
        await using var dbContext = await _db1Factory.CreateDbContextAsync();
        var result = await dbContext.Amounts
            .Select(x=> new ProductCountDto(x.Article, x.Quantity))
            .AsNoTracking()
            .ToListAsync();
        return result;
    }

    public async Task<IEnumerable<ProductPriceDto>> GetProductPrices()
    {
        await using var dbContext = await _db1Factory.CreateDbContextAsync();
        var result = await dbContext.Prices
            .Select(x=> new ProductPriceDto(x.Article, x.Price))
            .AsNoTracking()
            .ToListAsync();
        return result;
    }

    #endregion

    #region DB2

    public async Task<IEnumerable<ProductDto>> GetNotInDb1Products()
    {
        await using var db2Context = await _db2Factory.CreateDbContextAsync();
        var result = await db2Context.Products.FromSqlRaw("SELECT * FROM Tab2")
            .Select(p => new ProductDto(p.Article, p.Name, p.Quantity, p.Price))
            .AsNoTracking()
            .ToListAsync();
        return result;
    }

    public async Task<IEnumerable<ProductDto>> GetOtherProducts()
    {
        await using var db2Context = await _db2Factory.CreateDbContextAsync();
        var result = await db2Context.Products.FromSqlRaw("SELECT * FROM Tab1")
            .Select(p => new ProductDto(p.Article, p.Name, p.Quantity, p.Price))
            .AsNoTracking()
            .ToListAsync();
        return result;
    }

    public async Task<IEnumerable<ProductDto>> GetProducts()
    {
        await using var db2Context = await _db2Factory.CreateDbContextAsync();
        var result = await db2Context.Products
            .Select(p => new ProductDto(p.Article, p.Name, p.Quantity, p.Price))
            .AsNoTracking()
            .ToListAsync();
        return result;
    }

    #endregion
}