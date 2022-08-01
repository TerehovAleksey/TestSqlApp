using Microsoft.EntityFrameworkCore;
using TestApp.Data;
using TestApp.Dtos;

namespace TestApp.Services
{
    public class Db1Service : IDb1Service
    {
        private readonly IDbContextFactory<AppDb1Context> _factory;

        public Db1Service(IDbContextFactory<AppDb1Context> factory)
        {
            _factory = factory;
        }


        public async Task<IEnumerable<ProductNameDto>> GetProductNames()
        {
            await using var dbContext = await _factory.CreateDbContextAsync();
            var result = await dbContext.Names
                .Select(x=> new ProductNameDto(x.Article, x.Name))
                .AsNoTracking()
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<ProductCountDto>> GetProductQuantity()
        {
            await using var dbContext = await _factory.CreateDbContextAsync();
            var result = await dbContext.Amounts
                .Select(x=> new ProductCountDto(x.Article, x.Quantity))
                .AsNoTracking()
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<ProductPriceDto>> GetProductPrices()
        {
            await using var dbContext = await _factory.CreateDbContextAsync();
            var result = await dbContext.Prices
                .Select(x=> new ProductPriceDto(x.Article, x.Price))
                .AsNoTracking()
                .ToListAsync();
            return result;
        }
    }
}