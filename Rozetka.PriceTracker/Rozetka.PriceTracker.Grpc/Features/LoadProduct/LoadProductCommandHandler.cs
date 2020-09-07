using MediatR;
using Rozetka.PriceTracker.Models.Products;
using Rozetka.PriceTracker.Services.ProductLoader;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Rozetka.PriceTracker.Grpc.Features.LoadProduct
{
    public class LoadProductCommandHandler : IRequestHandler<LoadProductCommand, Product>
    {
        private readonly IProductLoaderService _productLoader;
        
        public LoadProductCommandHandler(IProductLoaderService productLoader)
        {
            _productLoader = productLoader;
        }

        public async Task<Product> Handle(LoadProductCommand request, CancellationToken cancellationToken)
        {
            return await _productLoader.LoadProductAsync(request.ProductUrl);
        }
    }
}
