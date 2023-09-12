using TaxCollectData.Library.Dto.Content;
using TaxCollectData.Library.Dto.Properties;
using TaxCollectData.Library.Enums;

namespace AfaghInvoiceManager
{
    public static class NetworkUilities
    {
        internal static InvoiceDto MergeInvoices(IEnumerable<InvoiceDto> invoices)
        {
            return new InvoiceDto()
            {
                Header = invoices.First().Header,
                Body = invoices.SelectMany(x => x.Body).ToList(),
                Payments = invoices.First().Payments,
            };
        }
    }

    public static class Extenssions
    {
        internal static decimal TruncateIfNonDecimal(this decimal value, int digits = 0)
        {
            int pow = (int)Math.Pow(10, digits);
            return decimal.Truncate(value * pow) / pow;
        }
    }
}
