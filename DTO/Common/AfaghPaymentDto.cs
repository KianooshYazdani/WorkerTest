#nullable disable
using TaxCollectData.Library.Dto.Content;

namespace AfaghInvoiceManager.DTO
{
    public class AfaghPaymentDto
    {
        public string Iinn { get; set; }

        public string Acn { get; set; }

        public string Trmn { get; set; }

        public string Trn { get; set; }

        public string Pcn { get; set; }

        public string Pid { get; set; }

        public long? Pdt { get; set; }

        public int? Pmt { get; init; }

        public long? Pv { get; init; }
        public DateTime? PdtDate { get; set; }

        public AfaghPaymentDto(PaymentDto original)
        {
            Iinn = original.Iinn;
            Acn = original.Acn;
            Trmn = original.Trmn;
            Trn = original.Trn;
            Pcn = original.Pcn;
            Pid = original.Pid;
            Pdt = original.Pdt;
            Pmt = original.Pmt;
            Pv = original.Pv;
        }

        public AfaghPaymentDto()
        {
        }

        public PaymentDto GetDto()
        {
            return new()
            {
                Iinn = this.Iinn,
                Acn = this.Acn,
                Trmn = this.Trmn,
                Trn = this.Trn,
                Pcn = this.Pcn,
                Pid = this.Pid,
                Pdt = (PdtDate == null ? this.Pdt : new DateTimeOffset(PdtDate.Value).ToUnixTimeMilliseconds()) ?? 0,
                Pv = Pv ?? 0,
                Pmt = Pmt ?? 0

            };
        }
    }
}
