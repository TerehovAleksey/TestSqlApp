using TestApp.Dtos;

namespace TestApp.Services
{
    public interface IDb1Service
    {
        public Task<IEnumerable<ProductNameDto>> GetProductNames();
        public Task<IEnumerable<ProductCountDto>> GetProductQuantity();
        public Task<IEnumerable<ProductPriceDto>> GetProductPrices();
    }
}
