using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCW.Data
{
    public class Product
    {
        public Guid ProductID { get; set; } = Guid.NewGuid();
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public string ProductType { get; set; }
    }
}
