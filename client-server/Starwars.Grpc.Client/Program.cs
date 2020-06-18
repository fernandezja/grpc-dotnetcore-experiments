using Grpc.Net.Client;
using Starwars.Grpc;
using System;
using System.Threading.Tasks;

namespace Starwars.Grpc.Client
{
    class Program
    {
        private const string ENDPOINT = "https://localhost:5001";

        static async Task Main(string[] args)
        {
            await SayHello();

            await GetQuote();

            await GetQuote();

            await GetQuote();

            Console.WriteLine("Press any key to continue...");

            Console.ReadKey();
        }

        private static async Task SayHello()
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
        }

        private static async Task GetQuote()
        {
            using (var channel = GrpcChannel.ForAddress(ENDPOINT))
            {

                var client = new Quotes.QuotesClient(channel);

                var quoteRequest = new QuoteRequest();

                var quoteResponse = await client.GetQuoteAsync(quoteRequest);

                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.Yellow;

                Console.WriteLine($"\"{quoteResponse.Quote}\" - ({quoteResponse.Where})");

                Console.ResetColor();

            }
        }
    }
}
