using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace rMedic.Models
{
    public class MedicamentRecord
    {
        #region Private Fields
        private int _id;
        private Medicament _medicament;
        private double _count;
        private DateTime _received;
        private DateTime _expiration;
        #endregion

        #region Public Properties
        public int Id { get => _id; set => _id = value; }
        public double Count { get => Math.Round(_count, 3); set => _count = value; }
        public DateTime Received { get => _received; set => _received = value; }
        public DateTime Expiration { get => _expiration; set => _expiration = value; }
        [NotMapped]
        public decimal Amount { get => decimal.Round(Medicament.Price * (decimal)Count, 2, MidpointRounding.AwayFromZero); }
        #endregion

        #region Virtual Properties
        public virtual Medicament Medicament { get => _medicament; set => _medicament = value; }
        #endregion
    }
}
