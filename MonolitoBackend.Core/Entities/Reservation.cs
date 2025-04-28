using System;

namespace MonolitoBackend.Core.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public string ReservedBy { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        // Navegação
        public Room? Room { get; set; }
    }
}
