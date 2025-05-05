using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MonolitoBackend.Core.Entities;
using MonolitoBackend.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MonolitoBackend.Api.Controllers
{
    [ApiController]
    [Route("reservations")]
    [Authorize]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationsController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet]
        public async Task<IEnumerable<Reservation>> Get() =>
            await _reservationService.GetAllReservationsAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Reservation>> GetById(int id)
        {
            var res = await _reservationService.GetReservationByIdAsync(id);
            if (res == null) return NotFound();
            return res;
        }

        [HttpGet("by-room/{roomId}")]
        public async Task<IEnumerable<Reservation>> GetByRoom(int roomId) =>
            await _reservationService.GetReservationsByRoomIdAsync(roomId);

        [HttpPost]
        public async Task<ActionResult<Reservation>> Post([FromBody] Reservation reservation)
        {
            var created = await _reservationService.CreateReservationAsync(reservation);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Reservation reservation)
        {
            if (id != reservation.Id) return BadRequest();
            await _reservationService.UpdateReservationAsync(reservation);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _reservationService.DeleteReservationAsync(id);
            return NoContent();
        }
    }
}
