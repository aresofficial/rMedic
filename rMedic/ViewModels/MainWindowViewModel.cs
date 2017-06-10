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
        public ObservableCollection<Medicament> Medicaments { get; set; }

        public ICommand AddNewMedicamentCommand
        {
            get => _addNewMedicamentCommand = new RelayCommand(AddNewMedicament);
        }
        #endregion

        #region Constructor
        public MainWindowViewModel()
        {
            //Example data
            Medicaments = new ObservableCollection<Medicament>()
            {
                new Medicament {
                    Id = 1, Name = "Цитрамон", Description = "Описание товара",
                    Manufacturer = new Manufacturer { Id = 1, Name = "Дарница", Phone = "0508383555", Address = "г. Дарница" }
                }
            };
        }
        #endregion

        #region Private methods
        private void AddNewMedicament(object param)
        {
            //Example data for testing command
            Medicaments.Add(new Medicament { Id = 2, Name = "тест", Description = "Описание товара", Manufacturer = new Manufacturer { Id = 1, Name = "Дарница", Phone = "0508383555", Address = "г. Дарница" } });
        }
        #endregion
    }
}