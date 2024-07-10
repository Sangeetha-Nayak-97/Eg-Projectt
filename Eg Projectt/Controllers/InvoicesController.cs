using Microsoft.AspNetCore.Mvc;
using InvoiceSystem.Services;
using InvoiceSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InvoiceSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InvoicesController : ControllerBase
    {
        private readonly InvoiceService _invoiceService;

        public InvoicesController(InvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpPost("invoices")]
        public ActionResult<Invoice> CreateInvoice([FromBody] InvoiceInputModel input)
        {
            var id = _invoiceService.CreateInvoice(input.Amount, input.DueDate);
            var invoice = _invoiceService.GetInvoices().FirstOrDefault(i => i.Id == id);
            return Created($"/invoices/{id}", invoice);
        }

        [HttpGet("invoices")]
        public ActionResult<List<Invoice>> GetInvoices()
        {
            var invoices = _invoiceService.GetInvoices();
            return Ok(invoices);
        }

        [HttpPost("invoices/{invoiceId}/payments")]
        public ActionResult PayInvoice(string invoiceId, [FromBody] PaymentInputModel payment)
        {
            try
            {
                _invoiceService.ProcessPayment(invoiceId, payment.Amount);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("invoices/process-overdue")]
        public ActionResult ProcessOverdueInvoices([FromBody] ProcessOverdueInputModel input)
        {
            try
            {
                _invoiceService.ProcessOverdueInvoices(input.LateFee, input.OverdueDays);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
