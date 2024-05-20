using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace TimeTriggeredFunction
{
    public class Function1
    {
        private readonly ILogger _logger;

        public Function1(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<Function1>();
        }
        private static Dictionary<int, string> _config = new Dictionary<int, string>
        {
            { 1, "Low Floor Tom" },
            { 3, "kick" },
            { 4, "snare" },
            { 12, "Hi-Hat" }
        };
        [Function("TimeTriggeredFunction")]
        public static void Run([TimerTrigger("0 0 8,12,17 * * *")] TimerInfo myTimer, ILogger log)
        {
            var currentTime = DateTime.UtcNow;
            log.LogInformation($"C# Timer trigger function executed at: {currentTime}");

            if (currentTime.Hour == 8 && currentTime.Minute == 0)
            {
                // Morning configuration
                _config[1] = "Bass Drum";
                _config[3] = "kick";
                _config[4] = "snare";
                _config[12] = "cymbal";
                log.LogInformation("Configuration updated for 8:00 AM UTC.");
            }
            else if (currentTime.Hour == 12 && currentTime.Minute == 30)
            {
                // Lunch configuration
                _config[1] = "Low-Mid Tom";
                _config[3] = "snare";
                _config[4] = "Hi-Hat";
                _config[12] = "cymbal";
                log.LogInformation("Configuration updated for 12:30 PM UTC.");
            }
            else if (currentTime.Hour == 17 && currentTime.Minute == 0)
            {
                // End of day reset
                _config[1] = "Low Floor Tom";
                _config[3] = "kick";
                _config[4] = "snare";
                _config[12] = "Hi-Hat";
                log.LogInformation("Configuration reset for 5:00 PM UTC.");
            }
        }
    }
}
