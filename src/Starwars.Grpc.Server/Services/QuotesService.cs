using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace Starwars.Grpc.Server
{
    public class QuotesService : Quotes.QuotesBase
    {
        private readonly ILogger<QuotesService> _logger;

        /// <summary>
        /// Quotes from https://parade.com/393857/lharris-2/20-of-the-most-epic-star-wars-quotes-of-all-time/
        /// </summary>
        private readonly List<(string Quote, string Where)> _quotes = 
                new List<(string Quote, string Where)> { 
                    ("A long time ago in a galaxy far, far away…", "Title card"),
                    ("The Force is strong with this one.", "A New Hope"),
                    ("No. I am your father.", "The Empire Strikes Back"),
                    ("I find your lack of faith disturbing", "A New Hope"),
                    ("If you only knew the power of the dark side.", "A New Hope"),
                    ("Be careful not to choke on your aspirations.", "Rogue One"),
                    ("You have failed me for the last time.", "The Empire Strikes Back"),
                    ("Remember…the Force will be with you, always.", "A New Hope"),
                    ("Use the Force, Luke.", "A New Hope"),
                    ("I have a bad feeling about this.", "The Phantom Menace")
                };

        public QuotesService(ILogger<QuotesService> logger)
        {
            _logger = logger;
        }

        public override Task<QuoteResponse> GetQuote(QuoteRequest request, ServerCallContext context)
        {

            var  random = new Random();
            var index = random.Next(0, _quotes.Count);
            var quote = _quotes[index];

            return Task.FromResult(new QuoteResponse
            {
               Quote = quote.Quote,
               Where = quote.Where,
            });
        }
    }
}
