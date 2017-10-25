using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerreSystemWPF
{
    public class TicketItem
    {
        public int Nitem { get; set; }
        public int Quantity { get; set; }
        public Product Item { get; set; }
        public double UnitPrice { get; set; }
        public double TotalPrice { get; set; }
    }
}
