using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using Path = System.IO.Path;

namespace PalletScanAndPrint.Services
{
    public class SettingsClass
    {
        public static int CountdownTimer = 5;

        public static string ConnectionString = @"Server=MKDEV01\MMR_DEV;Database=PS_ProdSimulator;USER ID=ps_fixedasset;Password=makita?2021;MultipleActiveResultSets=True";

        public static string PrinterIP { get; set; } = "192.168.34.249";

        public static int PrinterPort { get; set; } = 6101;

        public static bool AutoPrint { get; set; } = false;

        public static bool PrintLabel1 { get; set; } = true;

        public static bool PrintLabel2 { get; set; } = true;

        public static string SettingsFolderPath { get; set; } = @"C:\AppSettings";

        public static int LabelsCopies { get; set; } = 2;


        public SettingsClass()
        {

        }


        public SettingsClass(int countdownTimer, string connectionString, string printerIP, int printerPort, bool autoPrint,
            bool printLabel1, bool printLabel2, int labelCopies)
        {
            CountdownTimer = countdownTimer;
            ConnectionString = connectionString;
            PrinterIP = printerIP;
            PrinterPort = printerPort;
            AutoPrint = autoPrint;
            PrintLabel1 = printLabel1;
            PrintLabel2 = printLabel2;
            LabelsCopies = labelCopies;
        }




        public static bool isSettingFileLoaded = false;

        public class GetDataForStorage
        {
            public int CountdownTimer { get; set; } = SettingsClass.CountdownTimer;

            public string ConnectionString { get; set; } = SettingsClass.ConnectionString;

            public string PrinterIP { get; set; } = SettingsClass.PrinterIP;

            public int PrinterPort { get; set; } = SettingsClass.PrinterPort;

            public bool AutoPrint { get; set; } = SettingsClass.AutoPrint;

            public bool PrintLabel1 { get; set; } = SettingsClass.PrintLabel1;

            public bool PrintLabel2 { get; set; } = SettingsClass.PrintLabel2;

            public string SettingsFolderPath { get; set; } = SettingsClass.SettingsFolderPath;

            public int LabelsCopies { get; set; } = SettingsClass.LabelsCopies;

            public bool didAppOpened = false;

            public string SettingsPath
            {
                get
                {
                    string folderPath = SettingsFolderPath;
                    string fileName = "SettingsFile";
                    return Path.Combine(folderPath, fileName);
                }
            }
        }


        public static GetDataForStorage Settings = new GetDataForStorage();

        public static void LoadSettings()
        {
            if (File.Exists(Settings.SettingsPath))
            {
                string JsonString = File.ReadAllText(Settings.SettingsPath);
                Settings = JsonConvert.DeserializeObject<GetDataForStorage>(JsonString);
                CountdownTimer = Settings.CountdownTimer;
                ConnectionString = Settings.ConnectionString;
                PrinterIP = Settings.PrinterIP;
                PrinterPort = Settings.PrinterPort;
                AutoPrint = Settings.AutoPrint;
                PrintLabel1 = Settings.PrintLabel1;
                PrintLabel2 = Settings.PrintLabel2;
                SettingsFolderPath = Settings.SettingsFolderPath;
                LabelsCopies = Settings.LabelsCopies;
            }
            else
            {
                SaveSettings();
            }
        }

        public static void SaveSettings()
        {
            Settings.CountdownTimer = CountdownTimer;
            Settings.ConnectionString = ConnectionString;
            Settings.PrinterIP = PrinterIP;
            Settings.PrinterPort = PrinterPort;
            Settings.AutoPrint = AutoPrint;
            Settings.PrintLabel1 = PrintLabel1;
            Settings.PrintLabel2 = PrintLabel2;
            Settings.SettingsFolderPath = SettingsFolderPath;
            Settings.LabelsCopies = LabelsCopies;

            string serialString = JsonConvert.SerializeObject(Settings);
            Directory.CreateDirectory(SettingsFolderPath);//In case the directory for settings does not exist then create it
            File.WriteAllText(Settings.SettingsPath, serialString);
        }

    }
}