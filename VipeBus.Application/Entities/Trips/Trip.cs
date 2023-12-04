using VipeBus.Application.Entities.Buses;
using VipeBus.Application.Entities.Drivers;
using VipeBus.Application.Entities.Routes;
using VipeBus.Application.Entities.Users;

namespace VipeBus.Application.Entities.Trips
{
    public class Trip
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int BusId { get; set; }

        public virtual Bus Bus { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

        public int RouteId { get; set; }

        public virtual Route Route { get; set; }

    }
}
