using System.Diagnostics.CodeAnalysis;

using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

using Shared.Models;

namespace Shared.DataAccess;

public class DynamoDbProducts : ProductsDAO
{
  private static readonly string PRODUCT_TABLE_NAME = Environment.GetEnvironmentVariable("PRODUCT_TABLE_NAME") ?? string.Empty;
  private readonly AmazonDynamoDBClient _dynamoDbClient;

  [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(DynamoDbProducts))]
  public DynamoDbProducts()
  {
    _dynamoDbClient = new AmazonDynamoDBClient();
    _dynamoDbClient.DescribeTableAsync(PRODUCT_TABLE_NAME).GetAwaiter().GetResult();
  }

  public async Task DeleteProduct(string id)
  {
    await _dynamoDbClient.DeleteItemAsync(PRODUCT_TABLE_NAME, new Dictionary<string, AttributeValue>(1)
                                                              {
                                                                { ProductMapper.PK, new AttributeValue(id) }
                                                              });
  }

  public async Task<ProductWrapper> GetAllProducts()
  {
    var data = await _dynamoDbClient.ScanAsync(new ScanRequest
                                               {
                                                 TableName = PRODUCT_TABLE_NAME,
                                                 Limit = 20
                                               });

    var products = new List<Product>();

    foreach (var item in data.Items)
    {
      products.Add(ProductMapper.ProductFromDynamoDB(item));
    }

    return new ProductWrapper(products);
  }

  public async Task<Product?> GetProduct(string id)
  {
    var getItemResponse = await _dynamoDbClient.GetItemAsync(new GetItemRequest(PRODUCT_TABLE_NAME,
      new Dictionary<string, AttributeValue>(1)
      {
        { ProductMapper.PK, new AttributeValue(id) }
      }));

    return getItemResponse.IsItemSet ? ProductMapper.ProductFromDynamoDB(getItemResponse.Item) : null;
  }

  public async Task PutProduct(Product product) { await _dynamoDbClient.PutItemAsync(PRODUCT_TABLE_NAME, ProductMapper.ProductToDynamoDb(product)); }
}
