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
        private ICommand _addNewMedicamentRecordCommand;
        #endregion

        #region Events
        public event EventHandler<AddMedicamentRecordEventArgs> AddMedicamentRecord;
        #endregion

        #region Public Properties
        public RMedicDbContext Context { get; set; }

        public ObservableCollection<MedicamentRecord> MedicamentRecords { get; private set; }

        public ICommand AddNewMedicamentRecordCommand
        {
            get => _addNewMedicamentRecordCommand = new RelayCommand(AddNewMedicamentRecord);
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
            Context.SaveChangesAsync();
        }
        #endregion

        #region Private Methods
        private void AddNewMedicamentRecord(object param)
        {
            //Example data for testing command
            try
            {
                var medicRecord = new MedicamentRecord { MedicamentId = 1, Count = 111, Received = DateTime.Now, Expiration = DateTime.Now };
                MedicamentRecords.Add(medicRecord);
                AddMedicamentRecord(this, new AddMedicamentRecordEventArgs() { Record = medicRecord });
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Не удалось добавить новую запись!");
            }
        }
        #endregion
    }
}