using MahApps.Metro.Controls;
using rMedic.Data;
using rMedic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace rMedic.ViewModels
{
    public class AddMedicamentRecordViewModel
    {
        private ICommand _returnTestCommand;

        public MainWindowViewModel Model { get; set; }

        public List<Medicament> Medicaments { get; set; }
        public Medicament SelectedMedicament { get; set; }
        public double SelectedCount { get; set; } = 1;
        public DateTime SelectedReceived { get; set; } = DateTime.Now;
        public DateTime SelectedExpiration { get; set; } = DateTime.Now;

        public ICommand ReturnTestCommand { get => _returnTestCommand; set => _returnTestCommand = value; }

        public AddMedicamentRecordViewModel(MainWindowViewModel model)
        {
            Model = model;
            Medicaments = Model.Context.Medicaments.ToList();

            ReturnTestCommand = new RelayCommand(param => ReturnTest(param));
        }

        private void ReturnTest(object param)
        {
            var record = new MedicamentRecord
            {
                Medicament = SelectedMedicament,
                Count = SelectedCount,
                Received = SelectedReceived,
                Expiration = SelectedExpiration
            };
            Model.MedicamentRecords.Add(record);
            (param as MetroWindow).Close();
        }
    }
}
