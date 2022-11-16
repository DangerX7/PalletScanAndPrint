using PalletScanAndPrint.Services;
using PalletScanAndPrint.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PalletScanAndPrint.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        internal void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }


        public void DisplayMainView()
        {
            Application.Current.Dispatcher.Invoke( (Action) delegate 
            {
               ProcessingClass.MainWindowFrame.Content = new MainView();
            });
        }

        public void DisplaySettingView()
        {
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                ProcessingClass.MainWindowFrame.Content = new SettingsView();
            });
        }


        public void DisplayReprintView()
        {
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                ProcessingClass.MainWindowFrame.Content = new ReprintView();
            });
        }

    }
}
