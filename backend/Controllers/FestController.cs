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

        [HttpPost]
        public async Task<IActionResult> CreateFest(CreateFestDto dto)
        {
            var fest = await _festService.CreateFestAsync(dto);
            return Ok(fest);
        }

        [HttpPost("{festId}/day")]
        public async Task<IActionResult> AddFestDay(string festId, CreateFestDayDto dto)
        {
            var success = await _festService.AddFestDayAsync(festId, dto);
            return success ? Ok("Fest day added") : BadRequest();
        }

        [HttpPost("{festId}/session")]
        public async Task<IActionResult> AddSession(string festId, CreateSessionDto dto)
        {
            var success = await _festService.AddSessionAsync(festId, dto);
            return success ? Ok("Session added") : BadRequest();
        }

        [HttpPost("meal-slot")]
        public async Task<IActionResult> AddMealSlot(CreateMealSlotDto dto)
        {
            await _festService.AddMealSlotAsync(dto);
            return Ok("Meal slot added");
        }
    }
}

