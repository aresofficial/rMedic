using rMedic.Models;
using SQLite.CodeFirst;
using System;
using System.Data.Entity;

namespace rMedic.Data
{
    public class RMedicDbInitializer : SqliteDropCreateDatabaseWhenModelChanges<RMedicDbContext>
    {
        #region Constructor
        public RMedicDbInitializer(DbModelBuilder modelBuilder) : base(modelBuilder)
        {
        }
        #endregion

        #region Initialization
        protected override void Seed(RMedicDbContext context)
        {
            //for testing
            var manuf = new Manufacturer { Name = "Дарница", Address = "г. Дарница", Phone = "0503567890" };
            var medic = new Medicament { Name = "Цитрамон №14", Description = "Обезбаливающие", Manufacturer = manuf, Price = 14.24m, Unit = Unit.Pills };
            var medicRecord = new MedicamentRecord { Medicament = medic, Count = 1.666, Received = DateTime.Now, Expiration = DateTime.Now };
            context.MedicamentRecords.Add(medicRecord);
            context.SaveChanges();
            base.Seed(context);
        }
        #endregion
    }
}