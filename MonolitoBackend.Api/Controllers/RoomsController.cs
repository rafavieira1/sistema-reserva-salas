using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MonolitoBackend.Core.Entities;
using MonolitoBackend.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MonolitoBackend.Api.Controllers
{
    [ApiController]
    [Route("rooms")]
    [Authorize]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomsController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet]
        public async Task<IEnumerable<Room>> Get() =>
            await _roomService.GetAllRoomsAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> GetById(int id)
        {
            var room = await _roomService.GetRoomByIdAsync(id);
            if (room == null) return NotFound();
            return room;
        }

        [HttpPost]
        public async Task<ActionResult<Room>> Post([FromBody] Room room)
        {
            var created = await _roomService.CreateRoomAsync(room);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Room room)
        {
            if (id != room.Id) return BadRequest();
            await _roomService.UpdateRoomAsync(room);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _roomService.DeleteRoomAsync(id);
            return NoContent();
        }

        [HttpGet("{id}/reservations")]
        public async Task<IEnumerable<Reservation>> GetReservations(int id) =>
            await _roomService.GetRoomReservationsAsync(id);
    }
}
