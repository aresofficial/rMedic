using System;

namespace rMedic.Models
{
    public class Medicament
    {
        #region Private fields
        private int _id;
        private string _name;
        private string _description;
        private Manufacturer _manufacturer;
        private decimal _price;
        #endregion

        #region Public properties
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
        public Manufacturer Manufacturer { get => _manufacturer; set => _manufacturer = value; }
        public decimal Price { get => decimal.Round(_price, 2, MidpointRounding.AwayFromZero); set => _price = value; }
        #endregion
    }
}
