using MediatR;
using Rozetka.PriceTracker.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rozetka.PriceTracker.Grpc.Features.TrackProduct
{
    public class SaveProductCommand : IRequest<TrackProductResponse>
    {
        public Product Product { get; set; }
    }
}
