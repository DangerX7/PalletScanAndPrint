using Accessibility;
using Microsoft.AspNet.SignalR.Messaging;
using Newtonsoft.Json.Linq;
using PalletScanAndPrint.Models;
using PalletScanAndPrint.Services;
using PalletScanAndPrint.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Threading;
using static iTextSharp.text.pdf.AcroFields;

namespace PalletScanAndPrint.ViewModels
{
    public class ReprintViewModel : BaseViewModel
    {
        public ReprintViewModel()
        {
            ReadData();
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

        private string _status;

        public string Status
        {
            get { return _status; }
            set
            {
                if (_status != value)
                {
                    _status = value;
                    RaisePropertyChanged("Status");
                }
            }
        }

        private ObservableCollection<LabelPrint> _labelsInfo;

        public ObservableCollection<LabelPrint> LabelsInfo
        {
            get { return _labelsInfo; }
            set
            {
                if (_labelsInfo != value)
                {
                    _labelsInfo = value;
                    RaisePropertyChanged("LabelsInfo");
                }
            }
        }

        private LabelPrint _selectedRow;

        public LabelPrint SelectedRow
        {
            get { return _selectedRow; }
            set
            {
                if (_selectedRow != value)
                {
                    _selectedRow = value;
                    RaisePropertyChanged("SelectedRow");
                }
            }
        }


        internal void ReadData()
        {
            Status = "Ne conectam la server...";

            DataTable DT = DataAccessClass.LoadReprintData();

            LabelsInfo = new();

            foreach (DataRow dataRow in DT.Rows)
            {
                LabelPrint labelToAdd = new();

                labelToAdd.Customer = Convert.ToString(dataRow["Client"]);
                labelToAdd.PalletNumber = Convert.ToString(dataRow["Numar Palet"]);
                labelToAdd.Specification = Convert.ToString(dataRow["Specificatii"]);
                labelToAdd.Series = Convert.ToString(dataRow["Model"]);
                labelToAdd.SeriesStart = Convert.ToInt32(dataRow["Prima Serie"]);
                labelToAdd.SeriesEnd = Convert.ToInt32(dataRow["Ultima Serie"]);
                labelToAdd.Quantity = Convert.ToInt32(dataRow["Cantitate"]);
                labelToAdd.SearchString = Convert.ToString(dataRow["String Scanare"]);

                LabelsInfo.Add(labelToAdd);

            }
            
            //MessageBox.Show(Convert.ToString(LabelsInfo.Rows[2]["palletNumber"]));
            Status = "Selectati un rand";
        }


        public void Print()
        {
            if (SelectedRow != null)
            {
                Status = "Ne conectam la printer...";
                FoundLabel = SelectedRow;

                bool didPrintHappened = PrintClass.Print(SettingsClass.PrinterIP, SettingsClass.PrinterPort, FoundLabel.Specification,
                    FoundLabel.Customer, FoundLabel.PalletNumber, FoundLabel.SeriesStart, FoundLabel.SeriesEnd, FoundLabel.SearchString,
                    FoundLabel.Series, SettingsClass.PrintLabel1, SettingsClass.PrintLabel2, 1);
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
            }
            else
            {
                Status = "Va rog selectati un rand!";
            }

        }


    }
}

