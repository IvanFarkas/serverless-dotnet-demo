using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Amazon.Lambda.RuntimeSupport;
using Amazon.Lambda.Serialization.SystemTextJson;

using Shared;
using Shared.DataAccess;

namespace GetProducts;

public class Function
{
  private static readonly ProductsDAO dataAccess;

  [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Function))]
  [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(APIGatewayHttpApiV2ProxyRequest))]
  [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(APIGatewayHttpApiV2ProxyResponse))]
  static Function() => dataAccess = new DynamoDbProducts();

  public static async Task<APIGatewayHttpApiV2ProxyResponse> FunctionHandler(APIGatewayHttpApiV2ProxyRequest apigProxyEvent, ILambdaContext context)
  {
    if (!apigProxyEvent.RequestContext.Http.Method.Equals(HttpMethod.Get.Method))
    {
      return new APIGatewayHttpApiV2ProxyResponse
             {
               Body = "Only GET allowed",
               StatusCode = (int)HttpStatusCode.MethodNotAllowed
             };
    }

    context.Logger.LogInformation($"Received {apigProxyEvent}");

    var products = await dataAccess.GetAllProducts();

    context.Logger.LogInformation($"Found {products.Products.Count} product(s)");

    return new APIGatewayHttpApiV2ProxyResponse
           {
             Body = JsonSerializer.Serialize(products, CustomJsonSerializerContext.Default.ProductWrapper),
             StatusCode = 200,
             Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
           };
  }

  /// <summary>
  /// The main entry point for the custom runtime.
  /// </summary>
  /// <param name="args"></param>
  private static async Task Main()
  {
    var handler = FunctionHandler;
    await LambdaBootstrapBuilder.Create(handler, new SourceGeneratorLambdaJsonSerializer<CustomJsonSerializerContext>(options => { options.PropertyNameCaseInsensitive = true; }))
      .Build()
      .RunAsync();
  }
}
