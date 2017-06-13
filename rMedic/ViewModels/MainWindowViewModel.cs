using rMedic.Data;
using rMedic.Models;
using rMedic.Models.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace rMedic.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Private Fields
        private object _medicamenRecordsLock = new object();
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
            get => _addNewMedicamentRecordCommand;
            set => _addNewMedicamentRecordCommand = value;
        }
        #endregion

        #region Constructor
        public MainWindowViewModel()
        {
            Context = new RMedicDbContext();
            MedicamentRecords = new ObservableCollection<MedicamentRecord>(Context.MedicamentRecords);
            BindingOperations.EnableCollectionSynchronization(MedicamentRecords, _medicamenRecordsLock);

            AddNewMedicamentRecordCommand = new AsyncDelegateCommand(AddNewMedicamentRecord);
            AddMedicamentRecord += MainWindowViewModel_AddMedicamentRecord;
        }
        #endregion

        #region Private Methods
        private async Task AddNewMedicamentRecord(object param)
        {
            //Example data for testing command
            await Task.Run(/*async */() =>
            {
                try
                {
                    //await Task.Delay(1000);
                    var medicRecord = new MedicamentRecord { MedicamentId = 1, Count = 111, Received = DateTime.Now, Expiration = DateTime.Now };
                    MedicamentRecords.Add(medicRecord);
                    AddMedicamentRecord(this, new AddMedicamentRecordEventArgs() { Record = medicRecord });
                }
                catch (Exception)
                {
                    System.Windows.MessageBox.Show("Не удалось добавить новую запись!");
                }
            }
            );           
        }

        private void MainWindowViewModel_AddMedicamentRecord(object sender, AddMedicamentRecordEventArgs e)
        {
            Context.MedicamentRecords.Add(e.Record);
            Context.SaveChangesAsync();
        }
        #endregion
    }
}