using System.Collections.Generic;
using System.Threading.Tasks;
using MonolitoBackend.Core.Entities;

namespace MonolitoBackend.Core.Services
{
    public interface IReservationService
    {
        Task<IEnumerable<Reservation>> GetAllReservationsAsync();
        Task<Reservation> GetReservationByIdAsync(int id);
        Task<IEnumerable<Reservation>> GetReservationsByRoomIdAsync(int roomId);
        Task<Reservation> CreateReservationAsync(Reservation reservation);
        Task UpdateReservationAsync(Reservation reservation);
        Task DeleteReservationAsync(int id);
    }
}
