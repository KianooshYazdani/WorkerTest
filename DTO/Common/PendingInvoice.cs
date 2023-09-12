#nullable disable
using TaxCollectData.Library.Dto.Content;

namespace AfaghInvoiceManager.DTO;

public class PendingInvoice : Invoice
    {
        public int Id { get; set; }

        public static PendingInvoice GetPendingInvoice(InvoiceDto dto)
        {
            return new PendingInvoice
            {
                Header = new(dto.Header),
                Body = new(dto.Body.FirstOrDefault()),
                Payment = new(dto.Payments.FirstOrDefault()),
            };
        }
    }
