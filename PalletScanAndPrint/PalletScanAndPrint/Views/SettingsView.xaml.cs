using PalletScanAndPrint.Services;
using PalletScanAndPrint.ViewModels;
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

namespace PalletScanAndPrint.Views
{

    public partial class SettingsView : UserControl
    {

        SettingsViewModel viewModel;

        public SettingsView()
        {
            InitializeComponent();

            viewModel = new();

            this.DataContext = viewModel;

        }


        private void BacktoMenu_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.CountdownTimer < 1)
            {
                MessageBox.Show("Contor prea mic. Valoarea minima este 1!");
                return;
            }
            if (!viewModel.PrintLabel1 && !viewModel.PrintLabel2)
            {
                MessageBox.Show("Alege cel putin o eticheta pentru printare! (bifeaza o casuta)");
                return;
            }
            if (viewModel.LabelsCopies < 1)
            {
                MessageBox.Show("Alege cel putin o eticheta pentru printare! (in casuta cu copii)");
                return;
            }
            viewModel.CheckSettings();
            viewModel.DisplayMainView();
        }

        private void DefaultButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.CountdownTimer = 5;
            viewModel.ConnectionString = @"Server=MKDEV01\MMR_DEV;Database=PS_ProdSimulator;USER ID=ps_fixedasset;Password=makita?2021;MultipleActiveResultSets=True";
            viewModel.PrinterIP = "192.168.34.249";
            viewModel.PrinterPort = 6101;
            viewModel.PrintLabel1 = true;
            viewModel.PrintLabel2 = true;
            viewModel.SettingsFolderPath = @"C:\AppSettings";
            viewModel.LabelsCopies = 2;
        }

        //Connection string backup
        private void Dev_Str_Click(object sender, RoutedEventArgs e)
        {
            viewModel.ConnectionString = @"Data Source=MKDEV01\MMR_DEV;Initial Catalog=test7000;Integrated Security=True";
        }

        private void User_Str_Click(object sender, RoutedEventArgs e)
        {
            viewModel.ConnectionString = @"Server=MKDEV01\MMR_DEV;Database=PS_ProdSimulator;USER ID=ps_fixedasset;Password=makita?2021;MultipleActiveResultSets=True";
        }
    }
}
