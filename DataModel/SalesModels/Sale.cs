using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.InformationModels;

namespace DataModel.SalesModels
{
    public class Sale
    {
        public int SaleId { get; set; }
        public Items Items { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal PendingPayment { get; set; }
        public decimal ReceivedPayment { get; set; }
        public bool PartialPayments { get; set; }
        public int NumberOfPayments { get; set; }
        public int ReceivedPayments { get; set; }
        public Client Client { get; set; }
    }
}
