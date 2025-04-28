using System.Collections.Generic;
using System.Threading.Tasks;
using MonolitoBackend.Core.Entities;
using MonolitoBackend.Core.Interfaces;
using MonolitoBackend.Core.Services;

namespace MonolitoBackend.Infrastructure.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;

        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public Task<IEnumerable<Room>> GetAllRoomsAsync() => _roomRepository.GetAllAsync();

        public Task<Room> GetRoomByIdAsync(int id) => _roomRepository.GetByIdAsync(id);

        public Task<Room> CreateRoomAsync(Room room) => _roomRepository.AddAsync(room);

        public Task UpdateRoomAsync(Room room) => _roomRepository.UpdateAsync(room);

        public Task DeleteRoomAsync(int id) => _roomRepository.DeleteAsync(id);

        public Task<IEnumerable<Reservation>> GetRoomReservationsAsync(int roomId)
            => _roomRepository.GetReservationsAsync(roomId);
    }
}
