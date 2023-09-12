using System.Collections.Concurrent;

namespace TaxService.Worker
{
    public class ServicesDictionary
    {

        private static readonly ConcurrentDictionary<int, SendInvoiceService> _sendInvoiceServices = new();
        public static ConcurrentDictionary<int, SendInvoiceService> SendInvoiceServices => _sendInvoiceServices;

        private static readonly ConcurrentDictionary<int, InquiryService> _inquiryServices = new();
        public static ConcurrentDictionary<int, InquiryService> InquiryServices => _inquiryServices;
        
    }
}
