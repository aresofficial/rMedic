using rMedic.Data;
using rMedic.Models;
using rMedic.Models.Events;
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
        #region Private Fields
        private ICommand _addNewMedicamentCommand;
        #endregion

        #region Events
        public event EventHandler<AddMedicamentRecordEventArgs> AddMedicamentRecord;
        #endregion

        #region Public Properties
        public RMedicDbContext Context { get; set; }

        public ObservableCollection<MedicamentRecord> MedicamentRecords { get; private set; }

        public ICommand AddNewMedicamentCommand
        {
            get => _addNewMedicamentCommand = new RelayCommand(AddNewMedicament);
        }
        #endregion

        #region Constructor
        public MainWindowViewModel()
        {
            Context = new RMedicDbContext();
            MedicamentRecords = new ObservableCollection<MedicamentRecord>(Context.MedicamentRecords.ToList());

            AddMedicamentRecord += MainWindowViewModel_AddMedicamentRecord;
        }

        private void MainWindowViewModel_AddMedicamentRecord(object sender, AddMedicamentRecordEventArgs e)
        {
            Context.MedicamentRecords.Add(e.Record);
            Context.SaveChanges();
        }
        #endregion

        #region Private Methods
        private void AddNewMedicament(object param)
        {
            //Example data for testing command
            var medicRecord = new MedicamentRecord { Medicament = Context.Medicaments.FirstOrDefault(), Count = 111, Received = DateTime.Now, Expiration = DateTime.Now };
            MedicamentRecords.Add(medicRecord);
            AddMedicamentRecord(this, new AddMedicamentRecordEventArgs() { Record = medicRecord });
        }
        #endregion
    }
}