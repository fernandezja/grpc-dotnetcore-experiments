using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace Starwars.GrpcWebNullProperties.Server
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;

        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            _logger.LogInformation($"[GreeterService] SayHello > {request.Name}");
            return Task.FromResult(
                    new HelloReply { 
                        Message = $"Hello {request.Name} (isDarkSide={request.IsDarkSide}, isLightSide={request.IsLightSide})",
                        IsActive = true,
                        IsDarkSide = request.IsDarkSide,
                        IsLightSide = request.IsLightSide
                    });
        }

        public override async Task SayHellos(HelloRequest request, IServerStreamWriter<HelloReply> responseStream, ServerCallContext context)
        {
            _logger.LogInformation($"[GreeterService] SayHellos > {request.Name}");

            var i = 0;
            while (!context.CancellationToken.IsCancellationRequested)
            {
                await responseStream.WriteAsync(
                                new HelloReply { 
                                    Message = $"Hello {request.Name} {i} (isDarkSide={request.IsDarkSide}, isLightSide={request.IsLightSide})",
                                    IsActive = true,
                                    IsDarkSide = request.IsDarkSide,
                                    IsLightSide = request.IsLightSide

                                });
                await Task.Delay(TimeSpan.FromSeconds(1));
                i++;
            }
        }
    }
}
