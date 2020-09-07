using Google.Protobuf.WellKnownTypes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rozetka.PriceTracker.EntityFramework.DbContext;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Rozetka.PriceTracker.Grpc.Features.TrackProduct
{
    public class SaveProductCommandHandler : IRequestHandler<SaveProductCommand, TrackProductResponse>
    {
        private readonly RozetkaDbContext _context;

        public SaveProductCommandHandler(RozetkaDbContext context)
        {
            _context = context;
        }

        public async Task<TrackProductResponse> Handle(SaveProductCommand request, CancellationToken cancellationToken)
        {
            var savedProduct = await _context.Products.FirstOrDefaultAsync(
                x => x.ExternalProductId == request.Product.ExternalProductId);

            if (savedProduct != null)
            {
                savedProduct.Href = request.Product.Href;
                savedProduct.ImageUrl = request.Product.ImageUrl;
                savedProduct.CreatedOn = request.Product.CreatedOn;
                savedProduct.LastUpdateOn = DateTime.Now;
                savedProduct.Description = request.Product.Description;
                savedProduct.Discount = request.Product.Discount;
                savedProduct.ExternalProductId = request.Product.ExternalProductId;
                savedProduct.Name = request.Product.Name;
                savedProduct.OldPrice = request.Product.OldPrice;
                savedProduct.Price = request.Product.Price;
                savedProduct.SellStatus = request.Product.SellStatus;
                savedProduct.Status = request.Product.Status;
                savedProduct.Title = request.Product.Title;

                _context.Update(savedProduct);

            }
            else
            {
                savedProduct = request.Product;
                _context.Add(savedProduct);
            }

            if (await _context.SaveChangesAsync() > 0)
            {
                var productResponse = new TrackProductResponse
                {
                    Description = savedProduct.Description,
                    Discount = savedProduct.Discount,
                    Id = (int)savedProduct.Id,
                    ImageUrl = savedProduct.ImageUrl,
                    Price = (float)savedProduct.Price,
                    Title = savedProduct.Title,
                    Url = savedProduct.Href,
                    SellStatus = savedProduct.SellStatus,
                    Status = savedProduct.Status,
                    PrevPrice = (float)(savedProduct.PriceHistory?.OrderByDescending(x => x.LastUpdated).FirstOrDefault()?.Price ?? savedProduct.Price)
                };

                if (savedProduct.AdditionalPrices != null)
                {
                    productResponse.AdditionalPrices.AddRange(savedProduct.AdditionalPrices.Select(x => new ProductAdditionalPricesResponse
                    {
                        Description = x.Description,
                        DiscountPrice = (float)x.DiscountPrice,
                        Id = x.Id,
                        LastUpdatedOn = Timestamp.FromDateTime(x.LastUpdated),
                        ProductId = x.ProductId,
                        Title = x.Title
                    }));
                }
                return productResponse;
            }

            return null;
        }

    }
    
}
