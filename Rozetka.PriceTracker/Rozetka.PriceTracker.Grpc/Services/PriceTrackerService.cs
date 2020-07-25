using System;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Logging;
using Rozetka.PriceTracker.Services.ProductLoader;
using Rozetka.PriceTracker.Services.Products;

namespace Rozetka.PriceTracker.Grpc.Services
{
    public class PriceTrackerService : PriceTracker.PriceTrackerBase
    {
        private readonly ILogger<PriceTrackerService> _logger;
        private readonly IProductLoaderService _productLoaderService;
        private readonly IProductsService _productsService;

        public PriceTrackerService(ILogger<PriceTrackerService> logger, 
            IProductLoaderService productLoaderService,
            IProductsService productsService)
        {
            _logger = logger;
            _productLoaderService = productLoaderService;
            _productsService = productsService;
        }

        public override async Task<TrackProductResponse> TrackProduct(TrackProductRequest request, ServerCallContext context)
        {
            var product = await _productLoaderService.LoadProductAsync(request.ProductUrl);

            if (product != null)
            {
                var savedProduct = await _productsService.AddOrUpdateProductAsync(product);
                
                if (savedProduct != null)
                {
                    return new TrackProductResponse
                    {
                        Description = savedProduct.Description,
                        Discount = savedProduct.Discount,
                        Id = (int)savedProduct.Id,
                        ImageUrl = savedProduct.ImageUrl,
                        Price = (float)savedProduct.Price,
                        Title = savedProduct.Title,
                        Url = savedProduct.Href,
                        SellStatus = savedProduct.SellStatus,
                        Status = savedProduct.Status
                    };
                }
            }

            return null;

        }

        public override async Task TrackProductStream(TrackProductRequest request, IServerStreamWriter<TrackProductResponse> responseStream, ServerCallContext context)
        {
            while (!context.CancellationToken.IsCancellationRequested)
            {
                var product = await _productLoaderService.LoadProductAsync(request.ProductUrl);

                if (product != null)
                {
                    await responseStream.WriteAsync(
                            new TrackProductResponse
                            {
                                Description = product.Description,
                                Discount = product.Discount,
                                Id = (int)product.Id,
                                ImageUrl = product.ImageUrl,
                                Price = (float)product.Price,
                                Title = product.Title,
                                Url = product.Href,
                                SellStatus = product.SellStatus,
                                Status = product.Status
                            });

             
                }

                await Task.Delay(TimeSpan.FromSeconds(15));
            }

            if (context.CancellationToken.IsCancellationRequested)
            {
                Console.WriteLine("Stream canceled");
            }
        }

        public override async Task<TrackProductPriceResponse> TrackPrices(Empty request, ServerCallContext context)
        {
            var productsList = await _productsService.GetProductsAsync();

            var response = new TrackProductPriceResponse();
            
            response.Products.AddRange(productsList.Select(product => new TrackProductResponse
            {
                Description = product.Description,
                Discount = product.Discount,
                Id = (int)product.Id,
                ImageUrl = product.ImageUrl,
                Price = (float)product.Price,
                Title = product.Title,
                Url = product.Href,
                SellStatus = product.SellStatus,
                Status = product.Status
            }));

            return response;
            
        }

    }
}
