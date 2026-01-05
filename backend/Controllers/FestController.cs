using backend.DTOs;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/fest")]
    public class FestController : ControllerBase
    {
        private readonly FestService _festService;

        public FestController(FestService festService)
        {
            _festService = festService;
        }

        // SUPER ADMIN: Create Fest (Single API)
        [HttpPost]
        public async Task<IActionResult> CreateFest([FromBody] CreateFestDto dto)
        {
            var fest = await _festService.CreateFestAsync(dto);
            return Ok(fest);
        }
    }
}
