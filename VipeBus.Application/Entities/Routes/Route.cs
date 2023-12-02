using System;
using VipeBus.Application.Entities.Cities;

namespace VipeBus.Application.Entities.Routes
{
    public class Route
    {
        public int Id { get; set; }

        public int DeparturePointId { get; set; }

        public virtual City DeparturePoint { get; set; }

        public int DestinationPointId { get; set; }

        public virtual City DestinationPoint { get; set; }

        public DateTime DepartureTime { get; set; }

        public DateTime DestinationTime { get; set; }
    }
}
