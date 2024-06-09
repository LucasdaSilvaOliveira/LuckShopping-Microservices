using LuckShopping.ProductAPI.Data.Context;
using LuckShopping.ProductAPI.Data.Entites;
using Microsoft.EntityFrameworkCore;

namespace LuckShopping.ProductAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly LocalContext _context;

        public ProductRepository(LocalContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> FindAll()
        {

            return await _context.Products.ToListAsync();
        }

        public async Task<Product> FindById(long id)
        {
            return await _context.Products.Where(p => p.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<Product> Create(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }
        public async Task<Product> Update(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> Delete(long id)
        {
            try
            {
                Product product =
                await _context.Products.Where(p => p.Id == id)
                    .FirstOrDefaultAsync();
                if (product == null) return false;
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
