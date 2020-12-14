using Microsoft.EntityFrameworkCore;

namespace Soaps.Model.Data
{
    public class MvcSoapContext : DbContext
    {
        public MvcSoapContext(DbContextOptions<MvcSoapContext> options)
            : base(options)
        {
        }

        public DbSet<Soap> Soaps { get; set; }

        public DbSet<SoapType> SoapTypes { get; set; }

        public DbSet<SoapDetail> SoapDetails { get; set; }
    }
}
