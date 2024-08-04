using System.Diagnostics.CodeAnalysis;

namespace Shared.Models;

public class ProductWrapper
{
  #region Public properties
  public List<Product> Products { get; set; }
  #endregion

  [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(ProductWrapper))]
  public ProductWrapper() => Products = new List<Product>();

  public ProductWrapper(List<Product> products) => Products = products;
}
