using PalletScanAndPrint.Models;
using PalletScanAndPrint.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Media.Imaging;
using System.IO;
using System.Threading;
using System.Windows.Controls;
using Lextm.SharpSnmpLib.Security;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;
using System.Security.Principal;
using System.Windows.Media;

namespace PalletScanAndPrint.ViewModels
{
    public class MainViewModel:BaseViewModel
    {
        public MainViewModel()
        {
            //Check if printing is set to automatic or manual
            ReadPrintMode();
            PalletsScannedToday = DataAccessClass.GetTodayPalletsCount();
            Status = "Scanati Eticheta";
        }

        private string _inputPrintString = "";
		public int inputStringLimit = 55;

        public string InputPrintString
		{
			get { return _inputPrintString; }
			set
            { 
                if(_inputPrintString != value)
                {
                    _inputPrintString = value;
                    RaisePropertyChanged("InputPrintString");

					TempScanTrigger();

                }            
            }
		}


        private string _inputTimerString;

        public string InputTimerString
        {
            get { return _inputTimerString; }
            set
            {
                if (_inputTimerString != value)
                {
                    _inputTimerString = value;

                    if (!UpdateTimer())
					{
						_inputPrintString = SettingsClass.CountdownTimer.ToString();
                        
                    }

                    RaisePropertyChanged("InputTimerString");

                }
            }
        }

        private int _counterTimeSpan;

        public int CounterTimeSpan
        {
            get { return _counterTimeSpan; }
            set
			{
				if (_counterTimeSpan != value)
				{
                    _counterTimeSpan = value;
					RaisePropertyChanged("CounterTimeSpan");
				}
			}
        }

        private int _palletsScannedToday;

        public int PalletsScannedToday
        {
            get { return _palletsScannedToday; }
            set
            {
                if (_palletsScannedToday != value)
                {
                    _palletsScannedToday = value;
                    RaisePropertyChanged("PalletsScannedToday");
                }
            }
        }


        private LabelPrint _foundLabel;

		public LabelPrint FoundLabel
		{
			get { return _foundLabel; }
			set 
			{
				if (_foundLabel != value)
				{
					_foundLabel = value;
					RaisePropertyChanged("FoundLabel");
				} 
			}
        }


		public DispatcherTimer Timer { get; set; }

		private bool _isCounterActive;

		public bool IsCounterActive
		{
			get { return _isCounterActive; }
			set
			{
				if (_isCounterActive != value)
				{
					_isCounterActive = value;
					RaisePropertyChanged("IsCounterActive");
				}
			}
		}


		private string _displayTimer;

		public string DisplayTimer
		{
			get { return _displayTimer; }
			set
			{
				if (_displayTimer != value)
				{
					_displayTimer = value;
					RaisePropertyChanged("DisplayTimer");
				}
			}
		}


		private string _status;

		public string Status
		{
			get { return _status; }
			set
			{
				if(_status != value)
				{
					_status = value;
					RaisePropertyChanged("Status");
				}
			}
		}


        private bool _isManualEnable = true;

        public bool IsManualEnable
        {
            get { return _isManualEnable; }
            set
            {
                if (_isManualEnable != value)
                {
                    _isManualEnable = value;
                    RaisePropertyChanged("IsManualEnable");
                }
            }
        }

        public void StartTimer()
        {
            Status = "Initiam printarea...";
            IsCounterActive = true;
			IsManualEnable = false;

            TimeSpan _time = TimeSpan.FromSeconds(CounterTimeSpan);


            Timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                DisplayTimer = Convert.ToString(_time.Seconds);

                _time = _time.Add(TimeSpan.FromSeconds(-1));
                CounterTimeSpan -= 1;
                if (_time <= TimeSpan.Zero)
                {
                    StopTimer();
                    Print();
                }
            }, Application.Current.Dispatcher);

            Timer.Start();
        }


        public void StopTimer()
        {
            Timer.Stop();
            DisplayTimer= "0";
            IsManualEnable = true;
            IsCounterActive = false;
        }



		public void CancelPrint()
        {
			if (IsCounterActive)
            {
                StopTimer();

                CounterTimeSpan = 0;
                if (_inputPrintString.Length == inputStringLimit)
                {
                    Status = "Printat oprit";
                    InputPrintString = "";
                }
            }

        }


		public bool UpdateTimer()
		{
			bool Result = false;

			if (int.TryParse(_inputTimerString, out int ResultInt))
			{
				if (ResultInt < 1)
				{
					ResultInt = 1;
				}

                CounterTimeSpan = ResultInt;
				Result = true;
			}
			else
			{
                CounterTimeSpan = SettingsClass.CountdownTimer;
            }

			return Result;
		}



        private bool _autoPrint;

        public bool AutoPrint
        {
            get { return _autoPrint; }
            set
            {
                if (_autoPrint != value)
                {
                    _autoPrint = value;
                    SettingsClass.AutoPrint = AutoPrint;
                    SettingsClass.SaveSettings();
                    _inputPrintString = "";
                    RaisePropertyChanged("AutoPrint");
                    RaisePropertyChanged("IsEnabled");
                    RaisePropertyChanged("InputPrintString");
                }
            }
        }


        public bool IsEnabled
        {
            get
            {
                return !AutoPrint;
            }

        }

        public void ReadPrintMode()
        {
            if (SettingsClass.AutoPrint)
            {
                AutoPrint = true;
            }
        }


        //Access and display the label information from the database on scan
        internal void TempScanTrigger()
        {
            if (_inputPrintString.Length == inputStringLimit)
            {
                ScanStringReaderClass ScannedInfo = ProcessingClass.ProcessInputString(InputPrintString);
                if (ScannedInfo != null)
                {
                    Status = "Ne conectam la server...";
                    FoundLabel = DataAccessClass.GetData(ScannedInfo, InputPrintString);

                    if (FoundLabel != null)
                    {
                        Status = "Data a fost gasita";
                    }
                    else
                    {
                        Status = "Data nu a fost gasita";
                    }
                    if (AutoPrint)
                    {
                        PrintInitialize();
                    }
                }
            }
        }


        public void PrintInitialize()
        {
            if (FoundLabel == null)
            {
                if (_inputPrintString.Length < inputStringLimit)
                {
                    Status = "Data Incompleta";
                }
                else
                {
                    Status = "Data nu a fost gasita";
                    _inputPrintString = "";
                }
                return;
            }
            if (_inputPrintString.Length < inputStringLimit)
            {
                Status = "Data Incompleta";
            }
            else
            {
                if (!IsCounterActive && AutoPrint)
                {
                    UpdateTimer();
                    StartTimer();
                }
                else
                {
                    Print();
                }
            }
        }


        public void Print()
        {
            Status = "Ne conectam la printer...";

            bool didPrintHappened = PrintClass.Print(SettingsClass.PrinterIP, SettingsClass.PrinterPort, FoundLabel.Specification,
                FoundLabel.Customer, FoundLabel.PalletNumber, FoundLabel.SeriesStart, FoundLabel.SeriesEnd, _inputPrintString, FoundLabel.Series,
                SettingsClass.PrintLabel1, SettingsClass.PrintLabel2, SettingsClass.LabelsCopies);
            if (!didPrintHappened)
            {
                Status = "Printare Nereusita. Verificati Setarile!";
            }
            else
            {
                if (FoundLabel == null)
                {
                    Status = "Data nu a fost gasita";
                    return;
                }
                Status = "Printat terminat";
            }

            PalletsScannedToday = DataAccessClass.GetTodayPalletsCount();
            DataAccessClass.StoreData(FoundLabel);
            DataAccessClass.ClearHistory();

            _inputPrintString = "";
            RaisePropertyChanged("InputPrintString");
        }

	}
}
