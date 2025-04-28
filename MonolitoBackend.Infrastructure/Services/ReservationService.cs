using System.Collections.Generic;
using System.Threading.Tasks;
using MonolitoBackend.Core.Entities;
using MonolitoBackend.Core.Interfaces;
using MonolitoBackend.Core.Services;

namespace MonolitoBackend.Infrastructure.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationService(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public Task<IEnumerable<Reservation>> GetAllReservationsAsync() => _reservationRepository.GetAllAsync();

        public Task<Reservation> GetReservationByIdAsync(int id) => _reservationRepository.GetByIdAsync(id);

        public Task<IEnumerable<Reservation>> GetReservationsByRoomIdAsync(int roomId)
            => _reservationRepository.GetByRoomIdAsync(roomId);

        public Task<Reservation> CreateReservationAsync(Reservation reservation)
            => _reservationRepository.AddAsync(reservation);

        public Task UpdateReservationAsync(Reservation reservation)
            => _reservationRepository.UpdateAsync(reservation);

        public Task DeleteReservationAsync(int id) => _reservationRepository.DeleteAsync(id);
    }
}
