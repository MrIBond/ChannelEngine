using System.Net.Http.Headers;
using ChannelEngine.Application.Exceptions;
using ChannelEngine.Application.Interfaces;
using ChannelEngine.Application.Models;
using ChannelEngine.Infrastructure.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace ChannelEngine.Infrastructure.Services;

public class ProductApiService : ApiServiceBase, IProductApiService
{
    //can be done more elegant. skipped for test project.
    private readonly string _stockFieldPath = "Stock";

    public ProductApiService(
        HttpClient httpClient,
        IOptions<ApiSettings> options,
        ILogger<OrderApiService> logger
    ) : base(httpClient, options, logger)
    {
    }

    public async Task UpdateProductStockAsync(ProductDto product, CancellationToken cancellationToken = default)
    {
        try
        {
            var uri = $"products/{product.MerchantProductNo}?apikey={ApiSettings.ApiKey}";
            var patchDocument = GetPatchDocument(_stockFieldPath, product.Stock);

            var response = await HttpClient.PatchAsync(uri, patchDocument, cancellationToken).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
            var result = JsonConvert.DeserializeObject<ProductUpdateResponse>(responseString);
            result.EnsureSuccessResponse();
        }
        catch (HttpRequestException httpRequestException)
        {
            Logger.LogError(httpRequestException, httpRequestException.Message);
            throw new ChannelEngineApiException($"Can not update stock for product MerchantProductNo = {product.MerchantProductNo}");
        }
    }

    public static StringContent GetPatchDocument(string path, object value)
    {
        var patchDocument = new JsonPatchDocument();
        patchDocument.Replace(path, value);

        var serializedItemToUpdate = JsonConvert.SerializeObject(patchDocument);
        serializedItemToUpdate = serializedItemToUpdate.Replace("/", "");

        var content = new StringContent(serializedItemToUpdate);
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        return content;
    }
}