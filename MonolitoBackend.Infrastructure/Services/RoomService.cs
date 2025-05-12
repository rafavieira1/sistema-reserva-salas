using System.Collections.Generic;
using System.Threading.Tasks;
using MonolitoBackend.Core.Entities;
using MonolitoBackend.Core.Interfaces;
using MonolitoBackend.Infrastructure.Data;

namespace MonolitoBackend.Infrastructure.Services;

public class RoomService : IRoomService
{
    private readonly IRoomRepository _roomRepository;

    public RoomService(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public async Task<Room> GetByIdAsync(int id)
    {
        var room = await _roomRepository.GetByIdAsync(id);
        if (room == null)
            throw new KeyNotFoundException($"Sala com ID {id} não encontrada");
        return room;
    }

    public async Task<IEnumerable<Room>> GetAllAsync()
    {
        return await _roomRepository.GetAllAsync();
    }

    public async Task<Room> CreateAsync(Room room)
    {
        return await _roomRepository.AddAsync(room);
    }

    public async Task UpdateAsync(Room room)
    {
        await _roomRepository.UpdateAsync(room);
    }

    public async Task DeleteAsync(int id)
    {
        await _roomRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<Reservation>> GetReservationsAsync(int roomId)
    {
        return await _roomRepository.GetReservationsAsync(roomId);
    }
}
