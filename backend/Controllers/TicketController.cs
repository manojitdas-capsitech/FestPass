using backend.DTOs;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin, SuperAdmin")]
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
        [Authorize(Roles = "Admin, SuperAdmin")]
        [HttpGet]
        public async Task<IActionResult> GetAllTickets()
        {
            var tickets = await _ticketService.GetAllTicketsAsync();
            return Ok(tickets);
        }

        // Attendee: View all his tickets
        [AllowAnonymous]
        [HttpGet("email")]
        public async Task<IActionResult> GetAttendeeTickets()
        {
            var userEmail = User.FindFirst("email")?.Value;

            if (string.IsNullOrEmpty(userEmail))
                return Unauthorized();

            var tickets = await _ticketService.GetTicketsByUserEmailAsync(userEmail);
            return Ok(tickets);
        }

        // ADMIN: Block ticket
        [Authorize(Roles = "Admin, SuperAdmin")]
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