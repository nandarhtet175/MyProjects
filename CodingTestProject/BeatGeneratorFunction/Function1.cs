using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace BeatGeneratorFunction
{
    public class Function1
    {
        private readonly ILogger<Function1> _logger;
        private static Dictionary<int, string> _config = new Dictionary<int, string>
        {
            { 1, "Low Floor Tom" },
            { 3, "kick" },
            { 4, "snare" },
            { 12, "Hi-Hat" }
        };
        public Function1(ILogger<Function1> logger)
        {
            _logger = logger;
        }

        [Function("BeatGeneratorFunction")]
        public static IActionResult Run(
             [HttpTrigger(AuthorizationLevel.Function, "get", Route = "beatgenerator/{i:int}/{j:int}")] HttpRequest req,
             int i, int j)
        {


            if (i <= j)
            {
                return new BadRequestObjectResult("i must be greater than j.");
            }

            var result = new List<string>();
            for (int num = i; num >= j; num--)
            {
                if (num % 12 == 0)
                {
                    result.Add(_config[12]);
                }
                else if (num % 4 == 0)
                {
                    result.Add(_config[4]);
                }
                else if (num % 3 == 0)
                {
                    result.Add(_config[3]);
                }
                else
                {
                    result.Add(_config[1]);
                }
            }

            return new OkObjectResult(result);
        }
    }
}
