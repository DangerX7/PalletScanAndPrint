using PalletScanAndPrint.Services;
using PalletScanAndPrint.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
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
using System.Windows.Threading;
using System.IO;

namespace PalletScanAndPrint.Views
{

    public partial class MainView : UserControl
    {

        MainViewModel viewModel;
        public string settingsPassword = "";


        public MainView()
        {

            LoadSettings();

            InitializeComponent();

            viewModel = new();

            this.DataContext = viewModel;

            viewModel.UpdateTimer();

            //Automatically focus on the scan field
            txtName.Focus();
        }


        private void LoadSettings()
        {
            if (!SettingsClass.isSettingFileLoaded)
            {
                SettingsClass.LoadSettings();
                SettingsClass.isSettingFileLoaded = true;
            }
        }
  

        private void scanField_KeyDown(object sender, KeyEventArgs e)
        {
            //Clean scan field
            if (e.Key == Key.RightShift)
            {
                viewModel.InputPrintString = "";
            }
        }

        private void settingsPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                settingsPassword = SettingsPasswordField.Text;

                if (settingsPassword == "Makita?2023")
                {
                    SettingsPasswordField.Background = Brushes.LightGreen;
                }
                else
                {
                    SettingsPasswordField.Background = Brushes.Red;
                }
            }
        }
        

        private void ButtonPrint_Click(object sender, RoutedEventArgs e)
        {
            viewModel.PrintInitialize();
        }

        private void CancelTimer_Click(object sender, RoutedEventArgs e)
        {
            viewModel.CancelPrint();
        }


        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            if (settingsPassword == "Makita?2023")
            {
                viewModel.DisplaySettingView();
            }
            else
            {
                MessageBox.Show("Acces interzis!");
            }
        }

        private void ButtonReprint_Click(object sender, RoutedEventArgs e)
        {
            viewModel.DisplayReprintView();
        }
    }
}
