using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using System.IO;
using System.IO.Ports;
using System.Windows.Controls;
using System.Printing;
using System.Windows.Documents;
using System.Windows.Xps.Packaging;
using System.Windows.Xps;
using System.Windows;
using PalletScanAndPrint.Models;
using System.ComponentModel.Design;
using Microsoft.Owin;
using System.Data.SqlTypes;
using Lextm.SharpSnmpLib.Security;
using Microsoft.Owin.BuilderProperties;
using System.Numerics;
using static System.Net.Mime.MediaTypeNames;

namespace PalletScanAndPrint.Services
{
    public partial class PrintClass
    {

        public static bool Print(string ipAddress, int printerPort, string specs, string customer, string palNo,
            int seriesStart, int seriesEnd, string scannerString, string series, bool printLabel1, bool printLabel2, int labelsCopies)
        {

            string combinedQr = "(" + specs.Substring(0, 3) + ")" + series;
            int quantity = seriesEnd - seriesStart + 1;
            string customerAndPalNo = customer + palNo;

            // Printer IP Address and communication port
            int port = printerPort;


            string label1path = SettingsClass.SettingsFolderPath + @"\label 1 Config File.txt";

            string label2path = SettingsClass.SettingsFolderPath + @"\label 2 Config File.txt";


            if (!File.Exists(label1path) || !File.Exists(label2path))
            {
                Directory.CreateDirectory(SettingsClass.SettingsFolderPath);

                GenerateTextFilesWithLabelSettings(combinedQr, quantity, customerAndPalNo, customer, palNo, seriesStart, seriesEnd, specs, scannerString);
            }

            //ZPL Command
            string label1text = File.ReadAllText(label1path);

            label1text = label1text.Replace("#customer#", customer);
            label1text = label1text.Replace("#combinedQr#", combinedQr);
            label1text = label1text.Replace("#palNo#", palNo);
            label1text = label1text.Replace("#seriesStart#", Convert.ToString(seriesStart));
            label1text = label1text.Replace("#seriesEnd#", Convert.ToString(seriesEnd));
            label1text = label1text.Replace("#quantity#", Convert.ToString(quantity));
            label1text = label1text.Replace("#specs#", specs);
            label1text = label1text.Replace("#scannerString#", scannerString);


            //ZPL Command
            string label2text = File.ReadAllText(label2path);

            label2text = label2text.Replace("#customer#", customer);
            label2text = label2text.Replace("#palNo#", palNo);
            label2text = label2text.Replace("#customerAndPalNo#", customerAndPalNo);


            try
            {
                // Open connection
                System.Net.Sockets.TcpClient client = new System.Net.Sockets.TcpClient();
                client.Connect(ipAddress, port);

                // Write ZPL String to connection
                StreamWriter writer = new StreamWriter(client.GetStream());
                for (int i = 0; i < labelsCopies; i++)
                {
                    if (printLabel1)
                    {
                        writer.Write(label1text);
                    }
                    if (printLabel2)
                    {
                        writer.Write(label2text);
                    }
                }
                writer.Flush();

                // Close Connection
                writer.Close();
                client.Close();
            }
            catch (Exception ex)
            {
                    MessageBox.Show(ex + "\n\n\nCONEXIUNEA LA PRINTER NU A PUTUT FI REALIZATA!");
                return false;
            }
            return true;
        }


        public static void GenerateTextFilesWithLabelSettings(string CombinedQr, int Quantity, string CustomerAndPalNo, string Customer, string PalNo,
            int SeriesStart, int SeriesEnd, string Specs, string ScannerString)
        {
            string fileLabel1path = SettingsClass.SettingsFolderPath + @"\label 1 Config File.txt";

            //Create first label text file
            string fileLabel1text = "^XA" + Environment.NewLine +
                " " + Environment.NewLine +
                "^FWR" + Environment.NewLine +
                " " + Environment.NewLine +
                "^CF0,60" + Environment.NewLine +
                "^FO300,70^FD#customer#^FS" + Environment.NewLine +
                " " + Environment.NewLine +
                "^CF0,50" + Environment.NewLine +
                "^FO240,50^FD#combinedQr#^FS" + Environment.NewLine +
                " " + Environment.NewLine +
                "^CF0,90" + Environment.NewLine +
                "^FO290,230^FD#palNo#^FS" + Environment.NewLine +
                " " + Environment.NewLine +
                "^CF0,40" + Environment.NewLine +
                "^FO180,0^A0R,40,40^FB380,1,0,C^FD#seriesStart#R^FS" + Environment.NewLine +
                "^FO180,300^FD-^FS" + Environment.NewLine +
                "^FO180,0^A0R,40,40^FB900,1,0,C^FD#seriesEnd#R^FS" + Environment.NewLine +
                " " + Environment.NewLine +
                "^CF0,40" + Environment.NewLine +
                "^FO120,60^FDQTY #quantity#^FS" + Environment.NewLine +
                " " + Environment.NewLine +
                "^CF0,40" + Environment.NewLine +
                "^FO330,580^FD1/2^FS" + Environment.NewLine +
                " " + Environment.NewLine +
                " " + Environment.NewLine +
                "^FO270,460" + Environment.NewLine +
                "^BQN,0,5,H" + Environment.NewLine +
                "^FD&gt#specs#^FS" + Environment.NewLine +
                " " + Environment.NewLine +
                "^BY2,3" + Environment.NewLine +
                "^FO20,50^B7R,10,5,,8,N" + Environment.NewLine +
                "^^FD#scannerString#^FS" + Environment.NewLine +
                " " + Environment.NewLine +
                "^XZ";

            File.WriteAllText(fileLabel1path, fileLabel1text, Encoding.Unicode);


            string fileLabel2path = SettingsClass.SettingsFolderPath + @"\label 2 Config File.txt";

            //Create second label text file
            string fileLabel2text = "^XA" + Environment.NewLine +
                " " + Environment.NewLine +
                "^FWR" + Environment.NewLine +
                " " + Environment.NewLine +
                "^CF0,100" + Environment.NewLine +
                "^FO210,70^FD#customer#^FS" + Environment.NewLine +
                " " + Environment.NewLine +
                "^CF0,45" + Environment.NewLine +
                "^FO40,60^FDPALLET NO.^FS" + Environment.NewLine +
                "^CF0,90" + Environment.NewLine +
                "^FO20,290^FD#palNo#^FS" + Environment.NewLine +
                " " + Environment.NewLine +
                "^CF0,40" + Environment.NewLine +
                "^FO340,580^FD2/2^FS" + Environment.NewLine +
                " " + Environment.NewLine +
                "^FO130,376" + Environment.NewLine +
                "^BQN,0,88,H" + Environment.NewLine +
                "^FD&gt#customerAndPalNo#^FS" + Environment.NewLine +
                " " + Environment.NewLine +
                "^XZ";

            File.WriteAllText(fileLabel2path, fileLabel2text, Encoding.Unicode);


        }
    }


}