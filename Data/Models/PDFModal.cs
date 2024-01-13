using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCW.Data
{
    public class PDFModal

    {
        public List<Order> Orders { get; set; }
        public string ReportType { get; set; }
        public double TotalRevenue { get; set; } = 0;
        public List<OrderedProduct> Coffees { get; set; }
        public List<OrderedProduct> AddIns { get; set; }

        public string ReportDate { get; set; }
    }
}
