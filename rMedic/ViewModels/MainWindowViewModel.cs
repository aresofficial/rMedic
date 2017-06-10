using rMedic.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace rMedic.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Private fields
        private ICommand _addNewMedicamentCommand;
        #endregion

        #region Public properties
        public ObservableCollection<MedicamentRecord> MedicamentRecords { get; set; }

        public ICommand AddNewMedicamentCommand
        {
            get => _addNewMedicamentCommand = new RelayCommand(AddNewMedicament);
        }
        #endregion

        #region Constructor
        public MainWindowViewModel()
        {
            //Example data
            MedicamentRecords = new ObservableCollection<MedicamentRecord>()
            {
                new MedicamentRecord {
                    Id = 1,
                    Medicament = new Medicament{
                        Id = 1,
                        Name = "Цитрамон",
                        Description = "Описание товара",
                        Price = 6523.1251231232m,
                    Manufacturer = new Manufacturer {
                        Id = 1,
                        Name = "Дарница",
                        Phone = "0508383555",
                        Address = "г. Дарница" },
                        Unit = Unit.Pills },
                    Count = 3.666,
                    Expiration = DateTime.Now,
                    Received = DateTime.Now
                }
            };
        }
        #endregion

        #region Private methods
        private void AddNewMedicament(object param)
        {
            //Example data for testing command
            try
            {
                MedicamentRecords.Add(new MedicamentRecord
                {
                    Id = 1,
                    Medicament = new Medicament
                    {
                        Id = 1,
                        Name = "Цитрамон",
                        Description = "Описание товара",
                        Price = 6523.1252m,
                        Manufacturer = new Manufacturer { Id = 1, Name = "Дарница", Phone = "0508383555", Address = "г. Дарница" },
                        Unit = Unit.Pills
                    },
                    Count = 1,
                    Expiration = DateTime.Now,
                    Received = DateTime.Now
                });
            }
            catch (ArgumentException)
            {
                System.Windows.MessageBox.Show("Error!");
            }
        }
        #endregion
    }
}