using Microsoft.EntityFrameworkCore;
using MonolitoBackend.Core.Entities;
using MonolitoBackend.Core.Interfaces;
using MonolitoBackend.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonolitoBackend.Infrastructure.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly ApplicationDbContext _context;
        public RoomRepository(ApplicationDbContext context) => _context = context;

        public async Task<IEnumerable<Room>> GetAllAsync()
            => await _context.Rooms.Include(r => r.Reservations).ToListAsync();

        public async Task<Room?> GetByIdAsync(int id)
            => await _context.Rooms.Include(r => r.Reservations).FirstOrDefaultAsync(r => r.Id == id);

        public async Task<Room> AddAsync(Room room)
        {
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();
            return room;
        }

        public async Task UpdateAsync(Room room)
        {
            _context.Rooms.Update(room);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var room = await GetByIdAsync(id);
            if (room != null)
            {
                _context.Rooms.Remove(room);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Reservation>> GetReservationsAsync(int roomId)
            => await _context.Reservations.Where(r => r.RoomId == roomId).ToListAsync();
    }
}
