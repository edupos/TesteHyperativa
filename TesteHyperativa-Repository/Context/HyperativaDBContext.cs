using TesteHyperativa_Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace TesteHyperativa_Repository.Context
{
    public class HyperativaDBContext : DbContext
    {
        public HyperativaDBContext() { }
        public HyperativaDBContext(DbContextOptions<HyperativaDBContext> options)
            : base(options)
        { }
        public DbSet<User> Users { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<CreditCardsBatchFile> CreditCardsBatchFiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(o => new { o.Id });
            modelBuilder.Entity<User>()
                .Property(o => o.Name).HasMaxLength(250).IsRequired();
            modelBuilder.Entity<User>()
                .Property(o => o.UserName).HasMaxLength(150).IsRequired();
            modelBuilder.Entity<User>()
                .Property(o => o.Password).HasMaxLength(20).IsRequired();

            modelBuilder.Entity<CreditCard>()
                .HasKey(o => new { o.Id });
            modelBuilder.Entity<CreditCard>()
                .Property(o => o.Number).HasMaxLength(255).IsRequired();
            modelBuilder.Entity<CreditCard>()
                .Property(o => o.InputMode).IsRequired();
            modelBuilder.Entity<CreditCard>()
                .Property(o => o.InputUserId).IsRequired();
            modelBuilder.Entity<CreditCard>()
                .Property(o => o.InputDate).IsRequired();

            modelBuilder.Entity<CreditCardsBatchFile>()
                .HasKey(o => new { o.Id });
            modelBuilder.Entity<CreditCardsBatchFile>()
                .Property(o => o.Lote).HasMaxLength(255).IsRequired();
            modelBuilder.Entity<CreditCardsBatchFile>()
                .Property(o => o.CreditCardsText).HasMaxLength(1000000).IsRequired();
            modelBuilder.Entity<CreditCardsBatchFile>()
                .Property(o => o.InputDate).IsRequired();

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var absolutePath = Path.GetFullPath(@"..\TesteHyperativa-MinimalAPI\bin\Debug\net6.0");
            //IConfiguration configuration = new ConfigurationBuilder()
            //                .SetBasePath(absolutePath)
            //                .AddJsonFile("appsettings.json")
            //                .Build();
            //var connectionString = configuration
            //            .GetConnectionString("HyperativaConnectionString");
            //optionsBuilder.UseSqlServer(connectionString);


            optionsBuilder.UseSqlServer("Server=DESKTOP-004K27C\\SQLEXPRESS;Database=HyperativaDB;Trusted_Connection=SSPI;TrustServerCertificate=true");

        }
    }
}
