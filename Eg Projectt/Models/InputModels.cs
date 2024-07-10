using System;

namespace InvoiceSystem.Models
{
    public class InvoiceInputModel
    {
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
    }

    public class PaymentInputModel
    {
        public decimal Amount { get; set; }
    }

    public class ProcessOverdueInputModel
    {
        public decimal LateFee { get; set; }
        public int OverdueDays { get; set; }
    }
}
