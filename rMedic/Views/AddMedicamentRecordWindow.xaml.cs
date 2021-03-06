﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using rMedic.Data;
using rMedic.Models;
using rMedic.ViewModels;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Globalization;

namespace rMedic.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class AddMedicamentRecordWindow : MetroWindow
    {
        public AddMedicamentRecordWindow(MainWindowViewModel model)
        {
            InitializeComponent();

            DataContext = new AddMedicamentRecordViewModel(model);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}