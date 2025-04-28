namespace MonolitoBackend.Core.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public bool HasProjector { get; set; }

        // Navegação
        public List<Reservation>? Reservations { get; set; }
    }
}
