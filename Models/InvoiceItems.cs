using System;

namespace InvoiceManagement.Models
{

    public class InvoiceItems
    {

        public int ItemId { get; set; }
        public int InvoiceId { get; set; }
        public string ItemName { get; set; }
        public decimal ItemPrice { get; set; } = decimal.Zero;
    }

}