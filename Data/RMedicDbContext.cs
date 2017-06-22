using rMedic.Models;
using System.Data.Entity;

namespace rMedic.Data
{
    public class RMedicDbContext : DbContext
    {
        #region Constructor
        public RMedicDbContext() : base("rMedicDataBase")
        {
            Configure();
        }
        #endregion

        #region Configuration
        private void Configure()
        {
            Configuration.ProxyCreationEnabled = true;
            Configuration.LazyLoadingEnabled = true;
        }
        #endregion

        #region Initialization
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var initializer = new RMedicDbInitializer(modelBuilder);
            Database.SetInitializer(initializer);
        }
        #endregion

        #region Public Properties
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<MedicamentRecord> MedicamentRecords { get; set; }
        #endregion
    }
}
