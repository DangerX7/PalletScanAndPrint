using Accessibility;
using PalletScanAndPrint.Models;
using PalletScanAndPrint.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace PalletScanAndPrint.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        public SettingsViewModel()
        {
            LoadSettingsForEdit();
        }

        private int _countdownTimer;

        public int CountdownTimer
        {
            get { return _countdownTimer; }
            set
            {
                if (_countdownTimer != value)
                {
                    _countdownTimer = value;
                    RaisePropertyChanged("CountdownTimer");
                }
            }
        }


        private string _connectionString;

        public string ConnectionString
        {
            get { return _connectionString; }
            set
            {
                if (_connectionString != value)
                {
                    _connectionString = value;
                    RaisePropertyChanged("ConnectionString");
                }
            }
        }


        private string _printerIP;

        public string PrinterIP
        {
            get { return _printerIP; }
            set
            {
                if (_printerIP != value)
                {
                    _printerIP = value;
                    RaisePropertyChanged("PrinterIP");
                }
            }
        }


        private int _printerPort;

        public int PrinterPort
        {
            get { return _printerPort; }
            set
            {
                if (_printerPort != value)
                {
                    _printerPort = value;
                    RaisePropertyChanged("PrinterPort");
                }
            }
        }


        private bool _autoprint;

        public bool AutoPrint
        {
            get { return _autoprint; }
            set
            {
                if (_autoprint != value)
                {
                    _autoprint = value;
                    RaisePropertyChanged("AutoPrint");
                }
            }
        }

        private bool _printLabel1;

        public bool PrintLabel1
        {
            get { return _printLabel1; }
            set
            {
                if (_printLabel1 != value)
                {
                    _printLabel1 = value;
                    RaisePropertyChanged("PrintLabel1");
                }
            }
        }

        private bool _printLabel2;

        public bool PrintLabel2
        {
            get { return _printLabel2; }
            set
            {
                if (_printLabel2 != value)
                {
                    _printLabel2 = value;
                    RaisePropertyChanged("PrintLabel2");
                }
            }
        }

        private string _settingsFolderPath;

        public string SettingsFolderPath
        {
            get { return _settingsFolderPath; }
            set
            {
                if (_settingsFolderPath != value)
                {
                    _settingsFolderPath = value;
                    RaisePropertyChanged("SettingsFolderPath");
                }
            }
        }

        private int _labelsCopies;

        public int LabelsCopies
        {
            get { return _labelsCopies; }
            set
            {
                if (_labelsCopies != value)
                {
                    _labelsCopies = value;
                    RaisePropertyChanged("LabelsCopies");
                }
            }
        }



        public void LoadSettingsForEdit()
        {
            CountdownTimer = SettingsClass.CountdownTimer;
            ConnectionString = SettingsClass.ConnectionString;
            PrinterIP = SettingsClass.PrinterIP;
            PrinterPort = SettingsClass.PrinterPort;
            PrintLabel1 = SettingsClass.PrintLabel1;
            PrintLabel2 = SettingsClass.PrintLabel2;
            SettingsFolderPath = SettingsClass.SettingsFolderPath;
            LabelsCopies = SettingsClass.LabelsCopies;
        }

        public void CheckSettings()
        {
            if (CountdownTimer != SettingsClass.CountdownTimer || ConnectionString != SettingsClass.ConnectionString || PrinterIP != SettingsClass.PrinterIP ||
                PrinterPort != SettingsClass.PrinterPort || PrintLabel1 != SettingsClass.PrintLabel1 || PrintLabel2 != SettingsClass.PrintLabel2 ||
                SettingsFolderPath != SettingsClass.SettingsFolderPath || LabelsCopies != SettingsClass.LabelsCopies)
            {
                MessageBox.Show("" + ShowDialog("Vrei sa salvezi schimbarile?"));
            }
        }

        public string? ShowDialog(string message)
        {
            MessageBoxResult result = MessageBox.Show(message, "", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:

                    SaveSettings();
                    return "Schimbari salvate";
                case MessageBoxResult.No:
                    return "Schimbarile nu au fost salvate";
                default:
                    return null;
            }
        }


        public void SaveSettings()
        {
            UpdateSettings();
            SettingsClass.SaveSettings();
        }

        void UpdateSettings()
        {
            SettingsClass.CountdownTimer = CountdownTimer;
            SettingsClass.ConnectionString = ConnectionString;
            SettingsClass.PrinterIP = PrinterIP;
            SettingsClass.PrinterPort = PrinterPort;
            SettingsClass.PrintLabel1 = PrintLabel1;
            SettingsClass.PrintLabel2 = PrintLabel2;
            SettingsClass.SettingsFolderPath = SettingsFolderPath;
            SettingsClass.LabelsCopies = LabelsCopies;
        }

    }
}

