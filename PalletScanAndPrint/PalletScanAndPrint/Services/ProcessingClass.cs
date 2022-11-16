using PalletScanAndPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PalletScanAndPrint.Services
{
    public static class ProcessingClass
    {

        public static ScanStringReaderClass ProcessInputString(string input)
        {


            ScanStringReaderClass ScannedItem = new ScanStringReaderClass();

            bool pass=true;


            if (input.Length == 55)
            {

                var Arr = input.Split(" ");

                int last = Arr.Length - 1;

                string firstHalf = Arr[0];

                string secondHalf = Arr[last];

                if (secondHalf.Length > 30)
                {
                    ScannedItem.ScannedCustomer = secondHalf.Substring(secondHalf.Length - 3, 3);
                }

                ScannedItem.ScannedModel = firstHalf;


                if(int.TryParse(secondHalf.Substring(0, 9), out int ReadSeriesInt))
                {
                    ScannedItem.ScannedSeries = ReadSeriesInt;
                }
                else
                {
                    pass = false;
                }




                pass = true;
            }


            if (pass)
            {
                return ScannedItem;
            }
            else
            {
                return null;
            }

            
        }


        public static MainWindow MainWindow { get; set; } = (MainWindow)Application.Current.MainWindow;
        public static Frame MainWindowFrame { get; set; } = MainWindow.MainFrame;

    }
}
