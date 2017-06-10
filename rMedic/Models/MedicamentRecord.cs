using System;

namespace rMedic.Models
{
    public class MedicamentRecord
    {
        #region Private fields
        private int _id;
        private Medicament _medicament;
        private double _count;      
        private DateTime _received;
        private DateTime _expiration;
        #endregion

        #region Public properties
        public int Id { get => _id; set => _id = value; }
        public Medicament Medicament { get => _medicament; set => _medicament = value; }
        public double Count { get => Math.Round(_count, 2); set => _count = value; }
        public DateTime Received { get => _received; set => _received = value; }
        public DateTime Expiration { get => _expiration; set => _expiration = value; }
        #endregion
    }
}
