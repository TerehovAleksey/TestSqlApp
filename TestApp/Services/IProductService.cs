using TestApp.Dtos;

namespace TestApp.Services;

public interface IProductService
{
    public Task<bool> GenerateFakeData();
    public Task ClearTables();

    #region DB1

    public Task<IEnumerable<ProductNameDto>> GetProductNames();
    public Task<IEnumerable<ProductCountDto>> GetProductQuantity();
    public Task<IEnumerable<ProductPriceDto>> GetProductPrices();

    #endregion

    #region DB2

    public Task<IEnumerable<ProductDto>> GetProducts();
    public Task<IEnumerable<ProductDto>> GetNotInDb1Products();
    public Task<IEnumerable<ProductDto>> GetOtherProducts();

    #endregion
}