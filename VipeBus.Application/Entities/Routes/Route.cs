using System;
using System.ComponentModel.DataAnnotations.Schema;
using VipeBus.Application.Entities.Cities;
using VipeBus.Application.Entities.Drivers;

namespace VipeBus.Application.Entities.Routes
{
    public class Route
    {
        public int Id { get; set; }

        public int DeparturePointId { get; set; }

        public virtual City DeparturePoint { get; set; }

        public int DestinationPointId { get; set; }

        public virtual City DestinationPoint { get; set; }

        public int DriverId { get; set; }

        public virtual Driver Driver { get; set; }

        public DateTime DepartureTime { get; set; }

        public DateTime DestinationTime { get; set; }

        [NotMapped]
        public string Name => $"{DeparturePoint.Name} - {DestinationPoint.Name}";
    }
}
