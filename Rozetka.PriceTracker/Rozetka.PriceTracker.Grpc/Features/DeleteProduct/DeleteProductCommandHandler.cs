using MediatR;
using Microsoft.EntityFrameworkCore;
using Rozetka.PriceTracker.EntityFramework.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Rozetka.PriceTracker.Grpc.Features.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, DeleteTrackingProductResponse>
    {
        private readonly RozetkaDbContext _context;
        public DeleteProductCommandHandler(RozetkaDbContext context)
        {
            _context = context;
        }
        public async Task<DeleteTrackingProductResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == request.ProductId);

            if (product != null)
            {
                _context.Remove(product);
                
                return new DeleteTrackingProductResponse
                {
                    IsSuccess = await _context.SaveChangesAsync() > 0
                };
            }

            return new DeleteTrackingProductResponse();

        }
    }
}
