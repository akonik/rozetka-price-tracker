using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rozetka.PriceTracker.Grpc.Features.GetProductInfo
{
    public partial class GetProductInfo
    {
        public class Query : IRequest<ProductInfoResponse>
        {
            public long ProductId { get; set; }
        }
    }
}
