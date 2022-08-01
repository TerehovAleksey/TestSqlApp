using Microsoft.EntityFrameworkCore;
using TestApp.Data;
using TestApp.Dtos;

namespace TestApp.Services
{
    public class Db2Service : IDb2Service
    {
        private readonly IDbContextFactory<AppDb2Context> _factory;

        public Db2Service(IDbContextFactory<AppDb2Context> factory)
        {
            _factory = factory;
        }

        public async Task<IEnumerable<ProductDto>> GetNotInDb1Products()
        {
            await using var db2Context = await _factory.CreateDbContextAsync();
            var result = await db2Context.Products.FromSqlRaw("SELECT * FROM Tab2")
                .Select(p => new ProductDto(p.Article, p.Name, p.Quantity, p.Price))
                .AsNoTracking()
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<ProductDto>> GetOtherProducts()
        {
            await using var db2Context = await _factory.CreateDbContextAsync();
            var result = await db2Context.Products.FromSqlRaw("SELECT * FROM Tab1")
                .Select(p => new ProductDto(p.Article, p.Name, p.Quantity, p.Price))
                .AsNoTracking()
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            await using var db2Context = await _factory.CreateDbContextAsync();
            var result = await db2Context.Products
                .Select(p => new ProductDto(p.Article, p.Name, p.Quantity, p.Price))
                .AsNoTracking()
                .ToListAsync();
            return result;
        }
    }
}