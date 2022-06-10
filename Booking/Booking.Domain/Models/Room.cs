namespace Booking.Domain.Models
{
    public class Room
    {
        public int RoomId { get; set; }
        public int RooNumber { get; set; }
        public double Surface { get; set; }
        public bool NeedsRepair { get; set; }
    }
}
