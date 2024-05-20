using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodingTestProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneratorController : ControllerBase
    {
        private static Dictionary<int, string> _config = new Dictionary<int, string>
        {
            { 1, "Low Floor Tom" },
            { 3, "kick" },
            { 4, "snare" },
            { 12, "Hi-Hat" }
        };
        private static bool _isReady = false;
        [HttpGet("/beatgenerator/{i:int}/{j:int}")]
        public IActionResult GetBeatPattern(int i, int j)
        {
            if (i <= j)
            {
                return BadRequest("i must be greater than j.");
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

            return Ok(result);
        }
        [HttpGet("/livez")]
        public IActionResult Livez()
        {
            return Ok();
        }

        [HttpGet("/readyz")]
        public IActionResult Readyz()
        {
            if (_isReady)
            {
                return Ok();
            }
            return StatusCode(503);
        }
        [HttpPost("/configure/{i:int}/{text}")]
        public IActionResult Configure(int i, string text)
        {
            var validTexts = new HashSet<string> { "snare", "kick", "Hi-Hat", "Low Floor Tom", "cymbal", "Low-Mid Tom", "Bass Drum" };

            if (!_config.ContainsKey(i) || !validTexts.Contains(text))
            {
                return BadRequest("Invalid configuration.");
            }

            _config[i] = text;
            return Ok();
        }

        [HttpPost("/reset")]
        public IActionResult Reset()
        {
            _config = new Dictionary<int, string>
            {
                { 1, "Low Floor Tom" },
                { 3, "kick" },
                { 4, "snare" },
                { 12, "Hi-Hat" }
            };

            return Ok();
        }
        // A method to set the readiness of the server.
        [HttpPost("/setready")]
        public IActionResult SetReady([FromBody] bool ready)
        {
            _isReady = ready;
            return Ok();
        }


    }
}
