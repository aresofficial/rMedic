using rMedic.ViewModels;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace rMedic.Models
{
    public class MedicamentRecord : ViewModelBase
    {
        #region Private Fields
        private int _id;
        private int? _medicamentId;
        private Medicament _medicament;
        private double _count;
        private DateTime _received;
        private DateTime _expiration;
        #endregion

        #region Public Properties
        public int Id { get => _id; set { _id = value; OnPropertyChanged(); } }
        public double Count { get => Math.Round(_count, 3); set { _count = value; OnPropertyChanged(); OnPropertyChanged("Amount"); } }
        public DateTime Received { get => _received; set { _received = value; OnPropertyChanged(); } }
        public DateTime Expiration { get => _expiration; set { _expiration = value; OnPropertyChanged(); } }
        [NotMapped]
        public double Amount { get => Math.Round((double)Medicament.Price * Count, 2, MidpointRounding.AwayFromZero); }
        #endregion

        #region Foreign Keys
        public int? MedicamentId { get => _medicamentId; set { _medicamentId = value; OnPropertyChanged(); } }
        #endregion

        #region Navigation Properties
        public virtual Medicament Medicament
        {
            get => _medicament;
            set
            {
               if(_medicament!=null)
                {
                    _medicament.PropertyChanged -= _medicament_PropertyChanged;
                }
                _medicament = value;
                if (_medicament != null)
                {
                    _medicament.PropertyChanged += _medicament_PropertyChanged;
                }
            }
        }

        private void _medicament_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Medicament.Price))
            {
                OnPropertyChanged(nameof(Amount));
            }
        }
        #endregion
    }
}
