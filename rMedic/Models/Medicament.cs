using rMedic.ViewModels;
using System;
using System.Collections.Generic;

namespace rMedic.Models
{
    public class Medicament : ViewModelBase
    {
        #region Private Fields
        private int _id;
        private string _name;
        private string _description;
        private int? _manufacturerId;
        private Manufacturer _manufacturer;
        private decimal _price;
        private Unit _unit;
        #endregion

        #region Public Properties
        public int Id { get => _id; set => _id = value; }
        public string Name
        {
            get => _name;
            set => _name = (string.IsNullOrWhiteSpace(value)) ? throw new ArgumentNullException() : value;
        }
        public string Description
        {
            get => _description;
            set => _description = (string.IsNullOrWhiteSpace(value)) ? throw new ArgumentNullException() : value;
        }
        public decimal Price
        {
            get => decimal.Round(_price, 2, MidpointRounding.AwayFromZero);
            set { _price = value; OnPropertyChanged(); }
        }
        public Unit Unit { get => _unit; set => _unit = value; }
        #endregion

        #region Foreign Keys
        public int? ManufacturerId { get => _manufacturerId; set => _manufacturerId = value; }
        #endregion

        #region Navigation Properties
        public virtual Manufacturer Manufacturer { get => _manufacturer; set => _manufacturer = value; }
        public virtual ICollection<MedicamentRecord> MedicamentRecords { get; set; }
        #endregion
    }
}
