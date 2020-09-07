using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rozetka.PriceTracker.Grpc.Features.DeleteProduct
{
    public class DeleteProductCommand : IRequest<DeleteTrackingProductResponse>
    {
        public long ProductId { get; set; }
    }
}
