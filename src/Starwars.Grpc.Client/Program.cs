using Grpc.Net.Client;
using Starwars.Grpc.Server;
using System;
using System.Threading.Tasks;

namespace Starwars.Grpc.Client
{
    class Program
    {
        private const string ENDPOINT = "https://localhost:5001";

        static async Task Main(string[] args)
        {

            using (var channel = GrpcChannel.ForAddress(ENDPOINT))
            {
                //option csharp_namespace = "Starwars.Grpc.Server";
                var client = new Greeter.GreeterClient(channel);

                var helloRequest = new HelloRequest()
                {
                    Name = "Yoda"
                };

                var helloReply = await client.SayHelloAsync(helloRequest);

                Console.WriteLine(helloReply.Message);

            }

            Console.WriteLine("Press any key to continue...");

            Console.ReadKey();
        }
    }
}
