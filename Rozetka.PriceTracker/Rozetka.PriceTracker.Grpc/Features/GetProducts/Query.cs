using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rozetka.PriceTracker.Grpc.Features.GetProducts
{
    public partial class GetProducts
    {
        public class Query : IRequest<TrackProductPriceResponse>
        {
        }
    }
}
