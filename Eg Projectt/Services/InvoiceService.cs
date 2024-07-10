using System;
using System.Collections.Generic;
using System.Linq;
using InvoiceSystem.Models;

namespace InvoiceSystem.Services
{
    public class InvoiceService
    {
        private List<Invoice> invoices = new List<Invoice>();

        public string CreateInvoice(decimal amount, DateTime dueDate)
        {
            var invoice = new Invoice(amount, dueDate);
            invoices.Add(invoice);
            return invoice.Id;
        }

        public List<Invoice> GetInvoices()
        {
            return invoices;
        }

        public void ProcessPayment(string invoiceId, decimal paymentAmount)
        {
            var invoice = invoices.FirstOrDefault(i => i.Id == invoiceId);
            if (invoice == null)
            {
                throw new Exception("Invoice not found");
            }

            invoice.PaidAmount += paymentAmount;

            if (invoice.PaidAmount >= invoice.Amount)
            {
                invoice.Status = "paid";
            }
        }

        public void ProcessOverdueInvoices(decimal lateFeePerDay, int overdueDays)
        {
            DateTime today = DateTime.Today;
            List<Invoice> newInvoices = new List<Invoice>();

            foreach (var invoice in invoices.ToList()) // ToList() to avoid modifying collection while iterating
            {
                if (invoice.Status == "pending" && invoice.DueDate < today)
                {
                    // Calculate late fees
                    decimal lateFees = lateFeePerDay * (today - invoice.DueDate).Days;

                    if (invoice.PaidAmount > 0 && invoice.PaidAmount < invoice.Amount)
                    {
                        // Create a new invoice for the remaining amount plus late fees
                        var remainingAmount = invoice.Amount - invoice.PaidAmount;
                        var newInvoice = new Invoice(remainingAmount + lateFees, invoice.DueDate);
                        newInvoices.Add(newInvoice);

                        // Mark the original invoice as paid
                        invoice.Status = "paid";
                    }
                    else if (invoice.PaidAmount == 0)
                    {
                        // Mark the original invoice as void
                        invoice.Status = "void";

                        // Create a new invoice for the full amount plus late fees
                        var newInvoice = new Invoice(invoice.Amount + lateFees, invoice.DueDate);
                        newInvoices.Add(newInvoice);
                    }
                }
            }

            // Add new invoices created due to overdue processing
            invoices.AddRange(newInvoices);
        }
    }
}
