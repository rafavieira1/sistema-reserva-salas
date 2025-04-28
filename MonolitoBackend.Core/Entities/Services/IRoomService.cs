using System.Collections.Generic;
using System.Threading.Tasks;
using MonolitoBackend.Core.Entities;

namespace MonolitoBackend.Core.Services
{
    public interface IRoomService
    {
        Task<IEnumerable<Room>> GetAllRoomsAsync();
        Task<Room> GetRoomByIdAsync(int id);
        Task<Room> CreateRoomAsync(Room room);
        Task UpdateRoomAsync(Room room);
        Task DeleteRoomAsync(int id);
        Task<IEnumerable<Reservation>> GetRoomReservationsAsync(int roomId);
    }
}
