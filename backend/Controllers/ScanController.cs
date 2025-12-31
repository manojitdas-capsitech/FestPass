using backend.DTOs;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{

    [ApiController]
    [Route("api/scan")]
    public class ScanController : ControllerBase
    {
        private readonly ScanService _scanService;

        public ScanController(ScanService scanService)
        {
            _scanService = scanService;
        }

        // ENTRY SCAN
        [HttpPost("entry")]
        public async Task<IActionResult> EntryScan([FromBody] ScanRequestDto request)
        {
            if (string.IsNullOrWhiteSpace(request.TicketCode))
            {
                return BadRequest("TicketCode is required");
            }

            var result = await _scanService.HandleEntryScanAsync(request.TicketCode);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        // FOOD SCAN
        [HttpPost("food")]
        public async Task<IActionResult> FoodScan([FromBody] ScanRequestDto request)
        {
            if (string.IsNullOrWhiteSpace(request.TicketCode))
            {
                return BadRequest("TicketCode is required");
            }

            var result = await _scanService.HandleFoodScanAsync(request.TicketCode);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}

