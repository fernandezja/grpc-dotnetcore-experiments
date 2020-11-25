using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using Starwars.GrpcWebNullProperties;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Starwars.GrpcWebNullProperties.Client
{
    class Program
    {
       private const string ENDPOINT = "https://localhost:5001";

        static async Task Main(string[] args)
        {
            Console.WriteLine("Greeter!");

            await GreeterAsync();

            Console.WriteLine("Press any key to continue...");

            Console.ReadKey();

        }

        private static async Task GreeterAsync() {
            var channel = GrpcChannel.ForAddress(ENDPOINT, new GrpcChannelOptions
            {
                HttpHandler = new GrpcWebHandler(new HttpClientHandler())
            });

            var client = new Starwars.GrpcWebNullProperties.Greeter.GreeterClient(channel);

            var response = await client.SayHelloAsync(
                                new HelloRequest { 
                                    Name = "Yoda",
                                    IsDarkSide = false,
                                    IsLightSide = true
                                });

            Console.WriteLine($"{response.Message} (isActive: {response.IsActive}, isDarkSide: {response.IsDarkSide}, isLightSide: {response.IsLightSide})");
        }
    }
}
