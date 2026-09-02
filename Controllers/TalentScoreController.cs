using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TalentScore.DTOs;
using TalentScore.Services.Interfaces;

namespace TalentScore.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TalentScoreController : ControllerBase
    {
        private readonly ITalentScanService _service;

        public TalentScoreController(ITalentScanService service)
        {
            _service = service;
        }

        [HttpPost]
        public  async Task<IActionResult> AnalyzeResume([FromForm] FileDTO file)
        {
           await _service.AnalyzeResume(file.File);
            return Ok();
        }
    }
}
