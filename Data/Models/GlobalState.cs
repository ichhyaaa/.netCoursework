using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCW.Data
{
    public class GlobalState
    {
        public User CurrentUser { get; set; }
        public List<OrderedProduct> ProductsInCart { get; set; } = new();
        
    }
}
