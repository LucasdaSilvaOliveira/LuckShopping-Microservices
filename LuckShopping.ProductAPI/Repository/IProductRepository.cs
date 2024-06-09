using LuckShopping.ProductAPI.Data.Entites;

namespace LuckShopping.ProductAPI.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> FindAll();
        Task<Product> FindById(long id);
        Task<Product> Create(Product vo);
        Task<Product> Update(Product vo);
        Task<bool> Delete(long id);
    }
}
