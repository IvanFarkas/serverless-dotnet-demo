using System.Diagnostics.CodeAnalysis;

namespace Shared.Models;

public class Product
{
  #region Public properties
  public string Id { get; set; }

  public string Name { get; set; }

  public decimal Price { get; private set; }
  #endregion

  [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Product))]
  public Product()
  {
    Id = string.Empty;
    Name = string.Empty;
  }

  public Product(string id, string name, decimal price)
  {
    Id = id;
    Name = name;
    Price = price;
  }

  public void SetPrice(decimal newPrice) { Price = Math.Round(newPrice, 2); }

  public override string ToString() =>
    "Product{" +
    "id='" + Id + '\'' +
    ", name='" + Name + '\'' +
    ", price=" + Price +
    '}';
}
