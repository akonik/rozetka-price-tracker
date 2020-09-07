using MediatR;
using Rozetka.PriceTracker.Models.Products;

namespace Rozetka.PriceTracker.Grpc.Features.LoadProduct
{
    public class LoadProductCommand : IRequest<Product>
    {
        public string ProductUrl { get; set; }
    }
}
