using System.Collections.Generic;
using System.Threading.Tasks;
using MonolitoBackend.Core.Entities;

namespace MonolitoBackend.Core.Interfaces;

public interface IRoomService
{
    Task<Room> GetByIdAsync(int id);
    Task<IEnumerable<Room>> GetAllAsync();
    Task<Room> CreateAsync(Room room);
    Task UpdateAsync(Room room);
    Task DeleteAsync(int id);
    Task<IEnumerable<Reservation>> GetReservationsAsync(int roomId);
} 