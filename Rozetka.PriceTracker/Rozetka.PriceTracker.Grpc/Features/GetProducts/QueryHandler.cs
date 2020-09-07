using Google.Protobuf.WellKnownTypes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rozetka.PriceTracker.EntityFramework.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Rozetka.PriceTracker.Grpc.Features.GetProducts
{
    public partial class GetProducts
    {
        public class QueryHandler : IRequestHandler<Query, TrackProductPriceResponse>
        {
            private readonly RozetkaDbContext _context;
            
            public QueryHandler(RozetkaDbContext context)
            {
                _context = context;
            }

            public async Task<TrackProductPriceResponse> Handle(Query request, CancellationToken cancellationToken)
            {
                var productsList = await _context.Products
                        .AsNoTracking()
                        .Include(x => x.PriceHistory)
                        .Include(x => x.AdditionalPrices)
                        .OrderBy(x => x.SellStatus)
                        .ThenByDescending(x => x.CreatedOn)
                        .ToListAsync();

                var response = new TrackProductPriceResponse();

                foreach (var product in productsList)
                {
                    var responseProduct = new TrackProductResponse
                    {
                        Description = product.Description,
                        Discount = product.Discount,
                        Id = (int)product.Id,
                        ImageUrl = product.ImageUrl,
                        Price = (float)product.Price,
                        Title = product.Title,
                        Url = product.Href,
                        SellStatus = product.SellStatus,
                        Status = product.Status,
                        PrevPrice = (float)(product.PriceHistory?.OrderByDescending(x => x.LastUpdated).FirstOrDefault()?.Price ?? product.Price)
                    };

                    responseProduct.AdditionalPrices.AddRange(product.AdditionalPrices.Select(x => new ProductAdditionalPricesResponse
                    {
                        Description = x.Description,
                        DiscountPrice = (float)x.DiscountPrice,
                        Id = x.Id,
                        LastUpdatedOn = Timestamp.FromDateTime(x.LastUpdated.ToUniversalTime()),
                        ProductId = x.ProductId,
                        Title = x.Title
                    }));

                    response.Products.Add(responseProduct);
                }


                return response;
            }
        }
    }
}
