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

    }
}
