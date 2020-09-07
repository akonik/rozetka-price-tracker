using Google.Protobuf.WellKnownTypes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rozetka.PriceTracker.EntityFramework.DbContext;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Rozetka.PriceTracker.Grpc.Features.GetProductInfo
{
    public partial class GetProductInfo
    {
        public class QueryHandler : IRequestHandler<Query, ProductInfoResponse>
        {
            private readonly RozetkaDbContext _context;
            public QueryHandler(RozetkaDbContext context)
            {
                _context = context;
            }
            
            public async Task<ProductInfoResponse> Handle(Query request, CancellationToken cancellationToken)
            {
                var product  = await _context.Products
                 .AsNoTracking()
                 .Include(x => x.PriceHistory)
                 .Include(x => x.AdditionalPrices)
                 .FirstOrDefaultAsync(x => x.Id == request.ProductId);

                if (product == null)
                    return null;

                var response = new ProductInfoResponse();

                response.Prices.AddRange(product.PriceHistory.Select(x => new PorductPriceHistoryResponse
                {
                    ProductId = product.Id,
                    Id = x.Id,
                    Date = Timestamp.FromDateTime(x.LastUpdated.ToUniversalTime()),
                    Price = (float)x.Price
                }));

                response.ProductInfo = new TrackProductResponse
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
                };

                response.ProductInfo.AdditionalPrices.AddRange(product.AdditionalPrices.Select(x => new ProductAdditionalPricesResponse
                {
                    Description = x.Description,
                    DiscountPrice = (float)x.DiscountPrice,
                    Id = x.Id,
                    LastUpdatedOn = Timestamp.FromDateTime(x.LastUpdated.ToUniversalTime()),
                    ProductId = x.ProductId,
                    Title = x.Title
                }));

                return response;
            }
        }
    }
}
