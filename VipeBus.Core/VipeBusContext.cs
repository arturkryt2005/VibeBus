using System.Data.Entity;
using VipeBus.Application.Entities.Users;

namespace VipeBus.Core
{
    public class VipeBusContext : DbContext
    {
        public VipeBusContext() : base("DbConnection")
        {

        }

        DbSet<User> Users { get; set; }
    }
}
