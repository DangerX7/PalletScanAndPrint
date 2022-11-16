using PalletScanAndPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using PalletScanAndPrint.ViewModels;
using System.Windows.Controls.Primitives;
using System.Windows;
using System.Text.RegularExpressions;
using Org.BouncyCastle.Ocsp;
using System.Windows.Documents;
using System.Data;

namespace PalletScanAndPrint.Services
{
    public static class DataAccessClass
    {

        internal static LabelPrint GetData(ScanStringReaderClass lookUpClass, string searchString)
        {

            LabelPrint ReturnData = null;

            LabelPrint tempData = new();

            try
            {
                string SelectQuery = "SELECT  " +
                                     "[Specifications], " +
                                     "[Customer], " +
                                     "[Location], " +
                                     "[SerialStart], " +
                                     "[SerialEnd], " +
                                     "[PalletNo]," +
                                     "[ConversionModel] " +
                                     "FROM [PS_ProdSimulator].[dbo].[pallet_info] " +
                                     "WHERE (@Serial between SerialStart and SerialEnd) and ConversionModel=@Model ";



                using (SqlConnection conn = new SqlConnection(SettingsClass.ConnectionString))
                {
                    using (SqlCommand Command = new SqlCommand(SelectQuery, conn))
                    {
                        conn.Open();

                        Command.Parameters.AddWithValue("@Serial", lookUpClass.ScannedSeries);
                        Command.Parameters.AddWithValue("@Model", lookUpClass.ScannedModel);

                        Command.CommandTimeout = 0;

                        using (SqlDataReader rdr = Command.ExecuteReader())
                        {

                            while (rdr.Read())
                            {
                                tempData.Specification = rdr["Specifications"].ToString();
                                tempData.Customer = rdr["Customer"].ToString();
                                tempData.Location = rdr["Location"].ToString();
                                tempData.SeriesStart = Convert.ToInt32(rdr["SerialStart"].ToString());
                                tempData.SeriesEnd = Convert.ToInt32(rdr["SerialEnd"].ToString());
                                tempData.PalletNumber = rdr["PalletNo"].ToString();
                                tempData.Quantity = tempData.SeriesEnd - tempData.SeriesStart +1;
                                tempData.Series = rdr["ConversionModel"].ToString();
                            }
                        }



                        conn.Close();
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }


            if (!string.IsNullOrWhiteSpace(tempData.Customer))
            {
                ReturnData = tempData;
                ReturnData.SearchString = searchString;
            }

            return ReturnData;
        }


        internal static LabelPrint StoreData(LabelPrint FoundLabel)
        {
            LabelPrint ReturnData = null;

            LabelPrint tempData = new();

            try
            {
                string SelectQuery = "INSERT INTO [PS_ProdSimulator].[dbo].[PrintHistoryTable] " +
                                     "([Specifications], " +
                                     "[Customer], " +
                                     "[PalletNumber], " +
                                     "[SeriesStart], " +
                                     "[SeriesEnd], " +
                                     "[Quantity]," +
                                     "[Series], " +
                                     "[Locations], " +
                                     "[PrintedDate], " +
                                     "[PcName]," +
                                     "[ScanString]) " +
                                     "VALUES (" +
                                     "@specification, " +
                                     "@customer, " +
                                     "@palletNumber, " +
                                     "@seriesStart, " +
                                     "@seriesEnd, " +
                                     "@quantity, " +
                                     "@series, " +
                                     "@locations, " +
                                     "@printDate," +
                                     "@pcName," +
                                     "@scanString )";



                using (SqlConnection conn = new SqlConnection(SettingsClass.ConnectionString))
                {
                    using (SqlCommand Command = new SqlCommand(SelectQuery, conn))
                    {
                        conn.Open();

                        Command.Parameters.AddWithValue("@specification", FoundLabel.Specification);
                        Command.Parameters.AddWithValue("@customer", FoundLabel.Customer);
                        Command.Parameters.AddWithValue("@palletNumber", FoundLabel.PalletNumber);
                        Command.Parameters.AddWithValue("@seriesStart", FoundLabel.SeriesStart);
                        Command.Parameters.AddWithValue("@seriesEnd", FoundLabel.SeriesEnd);
                        Command.Parameters.AddWithValue("@quantity", FoundLabel.Quantity);
                        Command.Parameters.AddWithValue("@series", FoundLabel.Series);
                        Command.Parameters.AddWithValue("@locations", FoundLabel.Location);
                        Command.Parameters.AddWithValue("@printDate",DateTime.Now);
                        Command.Parameters.AddWithValue("@pcName", Environment.MachineName);
                        Command.Parameters.AddWithValue("@scanString", FoundLabel.SearchString);
                        

                        Command.CommandTimeout = 0;

                        Command.ExecuteNonQuery();


                        conn.Close();
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }


            if (!string.IsNullOrWhiteSpace(tempData.Customer))
            {
                ReturnData = tempData;
            }

            return ReturnData;
        }


        internal static DataTable LoadReprintData()
        {
            DataTable dt = new DataTable();

            try
            {

                string SelectQuery = "SELECT [Specifications], [Customer], [PalletNumber], [SeriesStart], [SeriesEnd], [Quantity], [Series], [ScanString], " +
                                     "Max(PrintedDate) As [LastPrinted]" +
                                     "FROM [PS_ProdSimulator].[dbo].[PrintHistoryTable]" +
                                     "Group by [Specifications], [Customer], [PalletNumber], [SeriesStart], [SeriesEnd], [Quantity], [Series], [ScanString]" +
                                     "Order by [LastPrinted] DESC";


                using (SqlConnection conn = new SqlConnection(SettingsClass.ConnectionString))
                {
                    using (SqlCommand Command = new SqlCommand(SelectQuery, conn))
                    {
                        conn.Open();


                        Command.CommandTimeout = 0;

                        Command.ExecuteNonQuery();


                        dt.Columns.Add(new DataColumn("Client", Type.GetType("System.String")));
                        dt.Columns.Add(new DataColumn("Numar Palet", Type.GetType("System.String")));
                        dt.Columns.Add(new DataColumn("Specificatii", Type.GetType("System.String")));
                        dt.Columns.Add(new DataColumn("Model", Type.GetType("System.String")));
                        dt.Columns.Add(new DataColumn("Prima Serie", Type.GetType("System.Int32")));
                        dt.Columns.Add(new DataColumn("Ultima Serie", Type.GetType("System.Int32")));
                        dt.Columns.Add(new DataColumn("Cantitate", Type.GetType("System.Int32")));
                        dt.Columns.Add(new DataColumn("String Scanare", Type.GetType("System.String")));

                        using (SqlDataReader rdr = Command.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                DataRow row = dt.NewRow();
                                row["Client"] = (string)rdr["Customer"];
                                row["Numar Palet"] = (string)rdr["PalletNumber"];
                                row["Specificatii"] = (string)rdr["Specifications"];
                                row["Model"] = (string)rdr["Series"];
                                row["Prima Serie"] = (int)rdr["SeriesStart"];
                                row["Ultima Serie"] = (int)rdr["SeriesEnd"];
                                row["Cantitate"] = (int)rdr["Quantity"];
                                row["String Scanare"] = (string)rdr["ScanString"];
                                dt.Rows.Add(row);
                            }
                        }

                        conn.Close();
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return dt;
        }


        internal static void ClearHistory()
        {
            int idCount = 0;

            //check if the database it's past his limits
            try
            {
                string SelectQuery = "SELECT COUNT(*) as [id_count]" +
                                     "FROM [PS_ProdSimulator].[dbo].[PrintHistoryTable]";


                using (SqlConnection conn = new SqlConnection(SettingsClass.ConnectionString))
                {
                    using (SqlCommand Command = new SqlCommand(SelectQuery, conn))
                    {
                        conn.Open();

                        Command.Parameters.AddWithValue("@id_count", idCount);

                        Command.CommandTimeout = 0;

                        Command.ExecuteNonQuery();


                        using (SqlDataReader rdr = Command.ExecuteReader())
                        {


                            while (rdr.Read())
                            {
                                idCount = Convert.ToInt32(rdr["id_count"].ToString());
                            }
                        }


                        conn.Close();
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            //remove last values from database
            if (idCount > 1000)
            {
                try
                {
                    string SelectQuery = "DELETE FROM [PS_ProdSimulator].[dbo].[PrintHistoryTable] " +
                                         "WHERE ID IN " +
                                         "(SELECT TOP (200) ID " +
                                         "FROM [PS_ProdSimulator].[dbo].[PrintHistoryTable])";


                    using (SqlConnection conn = new SqlConnection(SettingsClass.ConnectionString))
                    {
                        using (SqlCommand Command = new SqlCommand(SelectQuery, conn))
                        {
                            conn.Open();

                            Command.CommandTimeout = 0;

                            Command.ExecuteNonQuery();

                            conn.Close();
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }

        }


        internal static int GetTodayPalletsCount()
        {
            int count = 0;

            try
            {

                string SelectQuery = "SELECT COUNT ( DISTINCT [PalletNumber] ) AS [pal_count] " +
                                     "FROM [PS_ProdSimulator].[dbo].[PrintHistoryTable] " +
                                     "WHERE CONVERT (DATE,[PrintedDate])=CONVERT(Date,GETDATE()) ";


                using (SqlConnection conn = new SqlConnection(SettingsClass.ConnectionString))
                {
                    using (SqlCommand Command = new SqlCommand(SelectQuery, conn))
                    {
                        conn.Open();

                        Command.Parameters.AddWithValue("@pal_count", count);

                        Command.CommandTimeout = 0;

                        Command.ExecuteNonQuery();

                        using (SqlDataReader rdr = Command.ExecuteReader())
                        {

                            while (rdr.Read())
                            {
                                count = Convert.ToInt32(rdr["pal_count"].ToString());
                            }
                        }

                        conn.Close();
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return count;
        }

    }
}
