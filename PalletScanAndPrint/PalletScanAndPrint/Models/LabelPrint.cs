using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalletScanAndPrint.Models
{
    public class LabelPrint
    {

        public string Specification { get; set; } = "";

        public int SeriesStart { get; set; } = 0;

        public int SeriesEnd { get; set; } = 0;

        public string Location { get; set; } = "";

        public int Quantity { get; set; } = 0;

        public string Customer { get; set; } = "";

        public string PalletNumber { get; set; } = "";

        public string Series { get; set; } = "";


        public string SearchString { get; set; } = "";

        public string SpecsAndSeries { get; set; } = "";


        public LabelPrint()
        {

        }

        
        public LabelPrint(string specification, int seriesStart, int seriesEnd, string location, int quantity, string customer, string palletNumber,
            string series, string searchString, string specsAndSeries)
        {
            Specification = specification;
            SeriesStart = seriesStart;
            SeriesEnd = seriesEnd;
            Location = location;
            Quantity = quantity;
            Customer = customer;
            PalletNumber = palletNumber;
            Series = series;
            SearchString = searchString;
            SpecsAndSeries = specsAndSeries;
        }

    }

}
