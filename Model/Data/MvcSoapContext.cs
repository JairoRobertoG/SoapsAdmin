using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Soaps.Model.Data
{
    public class MvcSoapContext : IdentityDbContext
    {
        private readonly DbContextOptions _options;

        public MvcSoapContext(DbContextOptions<MvcSoapContext> options) : base(options)
        {
            _options = options;
        }

        public DbSet<Soap> Soaps { get; set; }

        public DbSet<SoapType> SoapTypes { get; set; }

        public DbSet<SoapDetail> SoapDetails { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<UserRegister> UserRegisters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRegister>()
                        .HasOne<ApplicationUser>(ad => ad.ApplicationUser)
                        .WithOne(s => s.UserRegister)
                        .HasForeignKey<UserRegister>(ad => ad.UserRegisterOfApplicationUser);

            base.OnModelCreating(modelBuilder);
        }
    }
}
