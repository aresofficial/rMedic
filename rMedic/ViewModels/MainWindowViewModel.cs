using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using rMedic.Data;
using rMedic.Helpers;
using rMedic.Models;
using rMedic.Models.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;

namespace rMedic.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Private Fields
        private object _medicamenRecordsLock = new object();
        private ICommand _addNewMedicamentRecordCommand;
        private ICommand _loadMedicamentRecordsCommand;
        private ICommand _editMedicamentRecordCommand;
        private ICommand _deleteMedicamentRecordCommand;
        private bool _isLoadedData = false;
        private MedicamentRecord _selectedMedicamentRecord;
        #endregion

        #region Events
        public event EventHandler<MedicamentRecordEventArgs> AddMedicamentRecordEvent;
        public event EventHandler<MedicamentRecordEventArgs> EditMedicamentRecordEvent;
        public event EventHandler<MedicamentRecordEventArgs> DeleteMedicamentRecordEvent;
        #endregion

        #region Public Properties
        public RMedicDbContext Context { get; set; }

        public ObservableCollection<MedicamentRecord> MedicamentRecords { get; set; }

        public ICommand AddNewMedicamentRecordCommand
        {
            get => _addNewMedicamentRecordCommand;
            set => _addNewMedicamentRecordCommand = value;
        }
        public ICommand LoadMedicamentRecordsCommand
        {
            get => _loadMedicamentRecordsCommand;
            set => _loadMedicamentRecordsCommand = value;
        }
        public ICommand EditMedicamentRecordCommand
        {
            get => _editMedicamentRecordCommand;
            set => _editMedicamentRecordCommand = value;
        }
        public ICommand DeleteMedicamentRecordCommand
        {
            get => _deleteMedicamentRecordCommand;
            set => _deleteMedicamentRecordCommand = value;
        }

        public bool IsLoadedData { get => _isLoadedData; set { _isLoadedData = value; OnPropertyChanged(); } }

        public MedicamentRecord SelectedMedicamentRecord
        {
            get => _selectedMedicamentRecord;
            set { _selectedMedicamentRecord = value; OnPropertyChanged(); }
        }

        #endregion

        #region Constructor
        public MainWindowViewModel()
        {
            Context = new RMedicDbContext();
            MedicamentRecords = new ObservableCollection<MedicamentRecord>();
            BindingOperations.EnableCollectionSynchronization(MedicamentRecords, _medicamenRecordsLock);

            AddNewMedicamentRecordCommand = new AsyncDelegateCommand(AddNewMedicamentRecord);
            LoadMedicamentRecordsCommand = new AsyncDelegateCommand(LoadMedicamentRecords);
            EditMedicamentRecordCommand = new AsyncDelegateCommand(param => EditMedicamentRecord(param));
            DeleteMedicamentRecordCommand = new AsyncDelegateCommand(param => DeleteMedicamentRecord(param));

            AddMedicamentRecordEvent += MainWindowViewModel_AddMedicamentRecord;
            EditMedicamentRecordEvent += MainWindowViewModel_EditMedicamentRecordEvent;
            DeleteMedicamentRecordEvent += MainWindowViewModel_DeleteMedicamentRecordEvent;

            LoadMedicamentRecordsCommand.Execute(null);
        }
        #endregion

        #region Private Methods
        private async Task AddNewMedicamentRecord(object param)
        {
            //Example data for testing command
            await Task.Run(() =>
            {
                var medicRecord = new MedicamentRecord
                {
                    Medicament = new Medicament
                    {
                        Name = "test",
                        Price = 54.82m,
                        ManufacturerId = 1,
                        Description = "desc",
                        Unit = Unit.Bottle
                    },
                    Count = 2.5,
                    Received = DateTime.Now,
                    Expiration = DateTime.Now
                };

                MedicamentRecords.Add(medicRecord);
                AddMedicamentRecordEvent(this, new MedicamentRecordEventArgs() { Record = medicRecord });
            });
        }

        private async Task LoadMedicamentRecords(object param)
        {
            //Example data for testing command
            await Task.Run(() =>
            {
                MedicamentRecords.Clear();
                IsLoadedData = false;
                foreach (var item in Context.MedicamentRecords)
                {
                    MedicamentRecords.Add(item);
                }
                IsLoadedData = true;
            });
        }

        private async Task EditMedicamentRecord(object param)
        {
            //Example data for testing command
            if (param != null)
            {
                var med = param as MedicamentRecord;
                var input = await MetroDialogsHelper.ShowInputAsync(
                    "Введите количество:",
                    "Введите новое количество:",
                    new MetroDialogSettings
                    {
                        AffirmativeButtonText = "Сохранить",
                        NegativeButtonText = "Отмена"
                    });

                if (input != null)
                {
                    await Task.Run(() =>
                    {
                        var record = MedicamentRecords.Where(x => x == med).FirstOrDefault();
                        record.Count = double.Parse(input);
                        EditMedicamentRecordEvent(this, new MedicamentRecordEventArgs() { Record = med });
                    });
                }
            }
        }

        private async Task DeleteMedicamentRecord(object param)
        {
            if (param != null)
            {
                var med = param as MedicamentRecord;
                var res = await MetroDialogsHelper.ShowMessageAsync("Удаление записи",
                    string.Format($"Вы действительно хотите удалить данную запись?\n" +
                    $"Название: {med.Medicament.Name}\n" +
                    $"Производитель: {med.Medicament.Manufacturer.Name}\n" +
                    $"Цена: {med.Medicament.Price}\n" +
                    $"Количество: {med.Count}"),
                    MessageDialogStyle.AffirmativeAndNegative,
                    new MetroDialogSettings
                    {
                        AffirmativeButtonText = "Удалить",
                        NegativeButtonText = "Отмена"
                    });

                if (res == MessageDialogResult.Affirmative)
                {
                    await Task.Run(() =>
                    {
                        MedicamentRecords.Remove(med);
                        DeleteMedicamentRecordEvent(this, new MedicamentRecordEventArgs() { Record = med });
                    });
                }
            }
        }

        private void MainWindowViewModel_AddMedicamentRecord(object sender, MedicamentRecordEventArgs e)
        {
            Context.MedicamentRecords.Add(e.Record);
            Context.SaveChangesAsync();
        }

        private void MainWindowViewModel_DeleteMedicamentRecordEvent(object sender, MedicamentRecordEventArgs e)
        {
            Context.MedicamentRecords.Remove(e.Record);
            Context.SaveChangesAsync();
        }

        private void MainWindowViewModel_EditMedicamentRecordEvent(object sender, MedicamentRecordEventArgs e)
        {
            var record = Context.MedicamentRecords.Find(e.Record.Id);
            record = e.Record;
            Context.SaveChangesAsync();
        }
        #endregion
    }
}