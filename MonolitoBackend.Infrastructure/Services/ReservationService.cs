using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using MonolitoBackend.Core.Entities;
using MonolitoBackend.Core.Interfaces;
using MonolitoBackend.Infrastructure.Data;

namespace MonolitoBackend.Infrastructure.Services;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IRoomRepository _roomRepository;

    public ReservationService(IReservationRepository reservationRepository, IRoomRepository roomRepository)
    {
        _reservationRepository = reservationRepository;
        _roomRepository = roomRepository;
    }

    public async Task<Reservation> GetByIdAsync(int id)
    {
        var reservation = await _reservationRepository.GetByIdAsync(id);
        if (reservation == null)
            throw new KeyNotFoundException($"Reserva com ID {id} não encontrada");
        return reservation;
    }

    public async Task<IEnumerable<Reservation>> GetAllAsync()
    {
        return await _reservationRepository.GetAllAsync();
    }

    public async Task<IEnumerable<Reservation>> GetByRoomIdAsync(int roomId)
    {
        return await _reservationRepository.GetByRoomIdAsync(roomId);
    }

    public async Task<Reservation> CreateAsync(Reservation reservation)
    {
        // Verificar se a sala existe
        var room = await _roomRepository.GetByIdAsync(reservation.RoomId);
        if (room == null)
            throw new KeyNotFoundException($"Sala com ID {reservation.RoomId} não encontrada");

        // Verificar se já existe reserva para o mesmo horário
        var existingReservations = await _reservationRepository.GetByRoomIdAsync(reservation.RoomId);
        var hasConflict = existingReservations.Any(r =>
            r.StartTime < reservation.EndTime && r.EndTime > reservation.StartTime);

        if (hasConflict)
            throw new InvalidOperationException("Já existe uma reserva para este horário");

        return await _reservationRepository.AddAsync(reservation);
    }

    public async Task UpdateAsync(Reservation reservation)
    {
        await _reservationRepository.UpdateAsync(reservation);
    }

    public async Task DeleteAsync(int id)
    {
        await _reservationRepository.DeleteAsync(id);
    }
}
