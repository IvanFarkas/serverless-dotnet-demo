using Shared.Models;

namespace Shared.DataAccess;

public interface ProductsDAO
{
  Task DeleteProduct(string id);

  Task<ProductWrapper> GetAllProducts();
  Task<Product?> GetProduct(string id);

  Task PutProduct(Product product);
}
