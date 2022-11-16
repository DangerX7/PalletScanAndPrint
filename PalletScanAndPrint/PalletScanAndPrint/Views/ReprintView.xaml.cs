using PalletScanAndPrint.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
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

    public partial class ReprintView : UserControl
    {

        ReprintViewModel viewModel;

        public ReprintView()
        {
            InitializeComponent();

            viewModel = new();

            this.DataContext = viewModel;

        }

        private void BackToMenu_Click(object sender, RoutedEventArgs e)
        {
            viewModel.DisplayMainView();
        }

        private void ButtonPrint1_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Print();
        }


    }
}
