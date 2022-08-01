using TestApp.Dtos;
using TestApp.Models;

namespace TestApp.Services
{
    public interface IDb2Service
    {
        public Task<IEnumerable<ProductDto>> GetProducts();
        public Task<IEnumerable<ProductDto>> GetNotInDb1Products();
        public Task<IEnumerable<ProductDto>> GetOtherProducts();
    }
}
