using System.ComponentModel.DataAnnotations.Schema;
using VipeBus.Application.Entities.Buses;

namespace VipeBus.Application.Entities.Drivers
{
    public class Driver
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int BusId { get; set; }

        public virtual Bus Bus { get; set; }

        [NotMapped]
        public string LastName => Name.Split(' ')[0];

        [NotMapped]
        public string FirstName => Name.Split(' ')[1];

        [NotMapped]
        public string MiddleName => Name.Split(' ')[2];
    }
}
