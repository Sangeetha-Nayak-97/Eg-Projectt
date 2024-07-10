using System;

namespace InvoiceSystem.Models
{
    public class Invoice
    {
        public string Id { get; set; }
        public decimal Amount { get; set; }
        public decimal PaidAmount { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; } // pending, paid, void

        public Invoice(decimal amount, DateTime dueDate)
        {
            Id = Guid.NewGuid().ToString(); // Generate a unique ID
            Amount = amount;
            DueDate = dueDate;
            Status = "pending";
        }
    }
}