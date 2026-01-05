using backend.DTOs;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{

    [ApiController]
    [Route("api/tickets")]
    public class TicketController : ControllerBase
    {
        private readonly TicketService _ticketService;

        public TicketController(TicketService ticketService)
        {
            _ticketService = ticketService;
        }

        // ADMIN: Assign ticket
        [HttpPost]
        public async Task<IActionResult> AssignTicket([FromBody] AssignTicketDto request)
        {
            if (request == null)
            {
                return BadRequest("Invalid request");
            }

            var ticket = await _ticketService.AssignTicketAsync(request);

            return Ok(new
            {
                ticket.Id,
                ticket.FestId,
                ticket.TicketCode,
                ticket.TicketType,
                ticket.UserEmail,
                ticket.SeatNumber
            });
        }

        // ADMIN: View all tickets
        [HttpGet]
        public async Task<IActionResult> GetAllTickets()
        {
            var tickets = await _ticketService.GetAllTicketsAsync();
            return Ok(tickets);
        }

        // ADMIN: Block ticket
        [HttpPost("{ticketCode}/block")]
        public async Task<IActionResult> BlockTicket(string ticketCode)
        {
            var success = await _ticketService.BlockTicketAsync(ticketCode);

            if (!success)
            {
                return NotFound("Ticket not found");
            }

            return Ok("Ticket blocked successfully");
        }
    }

}