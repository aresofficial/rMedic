﻿using MahApps.Metro.Controls.Dialogs;
using rMedic.Data;
using rMedic.Helpers;
using rMedic.Models;
using rMedic.Models.Events;
using rMedic.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        private ObservableCollection<MedicamentRecord> _medicamentRecords;
        private object _medicamentRecordsLock = new object();
        private MedicamentRecord _selectedMedicamentRecord;

        private ICommand _addNewMedicamentRecordCommand;
        private ICommand _loadMedicamentRecordsCommand;
        private ICommand _editMedicamentRecordCommand;
        private ICommand _deleteMedicamentRecordCommand;

        private bool _isLoadedData = true;
        private bool _isDeleteMedicamentRecord = true;
        private bool _isEditMedicamentRecord = true;
        private bool _isAddedMedicamentRecord = true;

        private double _fullAmount = 0;
        private double _fullNumber = 0;

        private string _randomWatermark;
        private string _searchString;
        #endregion

        #region Events
        public event EventHandler<MedicamentRecordEventArgs> AddMedicamentRecordEvent;
        public event EventHandler<MedicamentRecordEventArgs> EditMedicamentRecordEvent;
        public event EventHandler<MedicamentRecordEventArgs> DeleteMedicamentRecordEvent;
        #endregion

        #region Public Properties
        public RMedicDbContext Context { get; set; }

        public ObservableCollection<MedicamentRecord> MedicamentRecords
        {
            get => _medicamentRecords;
            set { _medicamentRecords = value; OnPropertyChanged(); }
        }

        public ICollectionView FilteredMedicamentRecords { get; set; }

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

        public bool IsDeleteMedicamentRecord { get => _isDeleteMedicamentRecord; set { _isDeleteMedicamentRecord = value; OnPropertyChanged(); } }
        public bool IsEditMedicamentRecord { get => _isEditMedicamentRecord; set { _isEditMedicamentRecord = value; OnPropertyChanged(); } }
        public bool IsAddedMedicamentRecord { get => _isAddedMedicamentRecord; set { _isAddedMedicamentRecord = value; OnPropertyChanged(); } }

        public double FullAmount
        {
            get => Math.Round(_fullAmount, 2, MidpointRounding.AwayFromZero);
            set
            {
                _fullAmount = value; OnPropertyChanged();
            }
        }
        public double FullNumber
        {
            get => Math.Round(_fullNumber, 3, MidpointRounding.AwayFromZero);
            set
            {
                _fullNumber = value; OnPropertyChanged();
            }
        }

        public string RandomWatermark
        {
            get => _randomWatermark;
            set
            {
                _randomWatermark = value; OnPropertyChanged();
            }
        }

        public string SearchString
        {
            get => _searchString;
            set
            {
                _searchString = value; OnPropertyChanged();
                if (string.IsNullOrWhiteSpace(value))
                {
                    FilteredMedicamentRecords.Filter = null;
                }
                else
                {
                    Task.Run(() =>
                    {
                        Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            FilteredMedicamentRecords.Filter = new Predicate<object>(o => (o as MedicamentRecord).ToString().Contains(value.ToLower()));
                        }), DispatcherPriority.Background);
                        });
                    }
            }
        }

        #endregion

        #region Constructor
        public MainWindowViewModel()
        {
            Context = new RMedicDbContext();
            MedicamentRecords = new ObservableCollection<MedicamentRecord>();
            BindingOperations.EnableCollectionSynchronization(MedicamentRecords, _medicamentRecordsLock);

            FilteredMedicamentRecords = CollectionViewSource.GetDefaultView(MedicamentRecords);

            //AddNewMedicamentRecordCommand = new AsyncDelegateCommand(AddNewMedicamentRecord, can => IsAddedMedicamentRecord);
            AddNewMedicamentRecordCommand = new RelayCommand(AddTest);
            LoadMedicamentRecordsCommand = new AsyncDelegateCommand(LoadMedicamentRecords, can => IsLoadedData);
            EditMedicamentRecordCommand = new AsyncDelegateCommand(param => EditMedicamentRecord(param), can => IsLoadedData);
            DeleteMedicamentRecordCommand = new AsyncDelegateCommand(param => DeleteMedicamentRecord(param), can => IsLoadedData);

            AddMedicamentRecordEvent += MainWindowViewModel_AddMedicamentRecord;
            EditMedicamentRecordEvent += MainWindowViewModel_EditMedicamentRecordEvent;
            DeleteMedicamentRecordEvent += MainWindowViewModel_DeleteMedicamentRecordEvent;

            LoadMedicamentRecordsCommand.Execute(null);
        }
        #endregion

        #region Private Methods

        private void AddTest(object param)
        {
            AddMedicamentRecordWindow addWindow = new AddMedicamentRecordWindow(this);
            addWindow.Owner = Application.Current.MainWindow;
            addWindow.ShowDialog();
        }

        private async Task AddNewMedicamentRecord(object param)
        {
            //Example data for testing command
            await Task.Run(() =>
            {
                var medicRecord = new MedicamentRecord
                {
                    MedicamentId = 2,
                    Count = 2.5,
                    Received = DateTime.Now,
                    Expiration = DateTime.Now
                };

                MedicamentRecords.Add(medicRecord);
                AddMedicamentRecordEvent(this, new MedicamentRecordEventArgs() { Record = medicRecord });
            });
            await LoadFullAmountAndNumber();
        }

        private async Task LoadFullAmountAndNumber()
        {
            await Task.Run(() =>
            {
                FullAmount = MedicamentRecords.Select(x => x.Amount).Aggregate((x, y) => x + y);
                FullNumber = MedicamentRecords.Select(x => x.Count).Aggregate((x, y) => x + y);
            });
        }

        private async Task LoadRandomWatermark()
        {
            await Task.Run(() =>
            {
                Random r = new Random();
                RandomWatermark = MedicamentRecords[r.Next(0, MedicamentRecords.Count)].Medicament.Name;
            });
        }

        private async Task LoadMedicamentRecords(object param)
        {
            //Example data for testing command
            IsLoadedData = false;

            if (MedicamentRecords.Count > 0) MedicamentRecords.Clear();
            await Task.Factory.StartNew(() =>
            {
                foreach (var item in Context.MedicamentRecords)
                {
                    Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        MedicamentRecords.Add(item); //Adding to Collection without freezing UI
                    }), DispatcherPriority.Background).Wait(); //WaitForAdding
                }
            });
            IsLoadedData = true;

            await LoadFullAmountAndNumber(); //Then Show Total
            await LoadRandomWatermark();//And Watermark of Search
        }

        private async Task EditMedicamentRecord(object param)
        {
            //Example data for testing command
            if (param != null)
            {
                IsEditMedicamentRecord = false;
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
                    await LoadFullAmountAndNumber();
                }
                IsEditMedicamentRecord = true;
            }
        }

        private async Task DeleteMedicamentRecord(object param)
        {
            if (param != null)
            {
                IsDeleteMedicamentRecord = false;
                var med = param as MedicamentRecord;
                var res = await MetroDialogsHelper.ShowMessageAsync("Вы действительно хотите удалить данную запись?",
                    $"Название: {med.Medicament.Name}\n" +
                    $"Производитель: {med.Medicament.Manufacturer.Name}\n" +
                    $"Цена: {med.Medicament.Price}\n" +
                    $"Количество: {med.Count}",
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
                        BindingOperations.EnableCollectionSynchronization(MedicamentRecords, _medicamentRecordsLock);
                        MedicamentRecords.Remove(med);
                        DeleteMedicamentRecordEvent(this, new MedicamentRecordEventArgs() { Record = med });
                    });
                    await LoadFullAmountAndNumber();
                }
                IsDeleteMedicamentRecord = true;
            }
        }
        #endregion

        #region Event Handlers
        private void MainWindowViewModel_AddMedicamentRecord(object sender, MedicamentRecordEventArgs e)
        {
            Context.MedicamentRecords.Add(e.Record);
            Context.SaveChanges();
        }

        private void MainWindowViewModel_DeleteMedicamentRecordEvent(object sender, MedicamentRecordEventArgs e)
        {
            Context.MedicamentRecords.Remove(e.Record);
            Context.SaveChanges();
        }

        private void MainWindowViewModel_EditMedicamentRecordEvent(object sender, MedicamentRecordEventArgs e)
        {
            var record = Context.MedicamentRecords.Find(e.Record.Id);
            record = e.Record;
            Context.SaveChanges();
        }
        #endregion
    }
}