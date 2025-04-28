using System.Collections.Generic;
using System.Threading.Tasks;
using MonolitoBackend.Core.Entities;

namespace MonolitoBackend.Core.Interfaces
{
    public interface IReservationRepository
    {
        Task<IEnumerable<Reservation>> GetAllAsync();
        Task<Reservation> GetByIdAsync(int id);
        Task<IEnumerable<Reservation>> GetByRoomIdAsync(int roomId);
        Task<Reservation> AddAsync(Reservation reservation);
        Task UpdateAsync(Reservation reservation);
        Task DeleteAsync(int id);
    }
}
