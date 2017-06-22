using System;
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
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            App.LanguageChanged += App_LanguageChanged;
            CultureInfo currentLanguage = App.SelectedLanguage;
            App.SelectedLanguage = new CultureInfo("en-US");
            App.SelectedLanguage = new CultureInfo("ru-RU");
            //App.SelectedLanguage = new CultureInfo("uk-UA");
            DataContext = new MainWindowViewModel();

        }

        private void App_LanguageChanged(object sender, EventArgs e)
        {
            //MessageBox.Show("Сменили язык.");
        }

        private void medicamentRecordsList_MouseDown(object sender, MouseButtonEventArgs e)
        {
            HitTestResult r = VisualTreeHelper.HitTest(this, e.GetPosition(this));
            if (r.VisualHit.GetType() != typeof(ListBoxItem))
            {
                medicamentRecordsList.UnselectAll();
            }
        }

    }
}