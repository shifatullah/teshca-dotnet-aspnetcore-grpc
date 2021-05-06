using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace Teshca.DotNet.AspNetCore.gRPC
{
    public class ServerStreamService : ServerStreaming.ServerStreamingBase
    {
        private readonly ILogger<ServerStreamService> _logger;
        public ServerStreamService(ILogger<ServerStreamService> logger)
        {
            _logger = logger;
        }

        public override async Task StreamingFromServer(Empty _,
            IServerStreamWriter<SampleServerResponse> responseStream, ServerCallContext context)
        {
            for (var i = 0; i < 5; i++)
            {
                await responseStream.WriteAsync(new SampleServerResponse() { Message = i.ToString() });
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }        
    }
}
