using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using rMedic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public ICommand AddMedicamentCommand { get => _returnTestCommand; set => _returnTestCommand = value; }

        public AddMedicamentRecordViewModel(MainWindowViewModel model)
        {
            Model = model;
            Medicaments = Model.Context.Medicaments.ToList();

            AddMedicamentCommand = new RelayCommand(param => AddMedicamentRecord(param));
        }

        private async void AddMedicamentRecord(object param)
        {
            //TODO: ValidationRule
                var record = new MedicamentRecord
                {
                    Medicament = SelectedMedicament,
                    Count = SelectedCount,
                    Received = SelectedReceived,
                    Expiration = SelectedExpiration
                };
                await Model.AddNewMedicamentRecordAsync(record);
                (param as MetroWindow).Close();
        }
    }
}
