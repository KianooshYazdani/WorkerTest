#nullable disable
using System.Collections.Generic;
using TaxCollectData.Library.Dto.Content;

namespace AfaghInvoiceManager.DTO
{
    public abstract class Invoice
    {
        public AfaghInvoiceHeaderDto Header { get; set; }
        public AfaghInvoiceBodyDto Body { get; set; }
        public AfaghPaymentDto Payment { get; set; }

        public InvoiceDto GetInvoiceDto()
        {
            return new InvoiceDto()
            {
                Header = Header.GetDto(),
                Body = Body == null? null : new List<InvoiceBodyDto> { Body?.GetDto() },
                Payments = Payment == null ? null : new List<PaymentDto> { Payment?.GetDto() }
            };
        }

        public virtual PlainInvoice GetPlainInvoice()
        {
            return new PlainInvoice
            {
                Indati2m = Header.Indati2m,
                Indatim = Header.Indatim,
                Inty = Header.Inty ?? 0,
                Ft = Header.Ft ?? 0,
                Inno = Header.Inno,
                Irtaxid = Header.Irtaxid,
                Scln = Header.Scln,
                Setm = Header.Setm ?? 0,
                Tins = Header.Tins,
                Cap = (Header.Cap ?? 0).TruncateIfNonDecimal(),
                Bid = Header.Bid,
                Insp = (Header.Insp ?? 0).TruncateIfNonDecimal(),
                Tvop = (Header.Tvop ?? 0).TruncateIfNonDecimal(),
                Bpc = Header.Bpc,
                Tax17 = (Header.Tax17 ?? 0).TruncateIfNonDecimal(),
                Taxid = Header.Taxid,
                Inp = Header.Inp ?? 0,
                Scc = Header.Scc,
                Ins = Header.Ins ?? 0,
                Billid = Header.Billid,
                Tprdis = (Header.Tprdis ?? 0).TruncateIfNonDecimal(),
                Tdis = (Header.Tdis ?? 0).TruncateIfNonDecimal(),
                Tadis = (Header.Tadis ?? 0).TruncateIfNonDecimal(),
                Tvam = (Header.Tvam ?? 0).TruncateIfNonDecimal(),
                Todam = (Header.Todam ?? 0).TruncateIfNonDecimal(),
                Tbill = (Header.Tbill ?? 0).TruncateIfNonDecimal(),
                Tob = Header.Tob ?? 0,
                Tinb = Header.Tinb,
                Sbc = Header.Sbc,
                Bbc = Header.Bbc,
                Bpn = Header.Bpn,
                Crn = Header.Crn,
                Cdcd = Header.Cdcd,
                Cdcn = Header.Cdcn,
                Tocv = Header.Tocv,
                Tonw = Header.Tonw,
                Torv = Header.Torv,

                Sstid = Body.Sstid,
                Sstt = Body.Sstt,
                Mu = Body.Mu,
                Am = (Body.Am ?? 0).TruncateIfNonDecimal(),
                Fee = (Body.Fee ?? 0).TruncateIfNonDecimal(),
                Cfee = (Body.Cfee ?? 0).TruncateIfNonDecimal(),
                Cut = Body.Cut,
                Exr = Body.Exr,
                Prdis = (Body.Prdis ?? 0).TruncateIfNonDecimal(),
                Dis = (Body.Dis ?? 0).TruncateIfNonDecimal(),
                Adis = (Body.Adis ?? 0).TruncateIfNonDecimal(),
                Vra = (Body.Vra ?? 0).TruncateIfNonDecimal(),
                Vam = (Body.Vam ?? 0).TruncateIfNonDecimal(),
                Odt = Body.Odt,
                Odr = (Body.Odr ?? 0).TruncateIfNonDecimal(),
                Odam = (Body.Odam ?? 0).TruncateIfNonDecimal(),
                Olt = Body.Olt,
                Olr = (Body.Olr ?? 0).TruncateIfNonDecimal(),
                Olam = (Body.Olam ?? 0).TruncateIfNonDecimal(),
                Consfee = (Body.Consfee ?? 0).TruncateIfNonDecimal(),
                Spro = (Body.Spro ?? 0).TruncateIfNonDecimal(),
                Bros = (Body.Bros ?? 0).TruncateIfNonDecimal(),
                Tcpbs = (Body.Tcpbs ?? 0).TruncateIfNonDecimal(),
                Cop = (Body.Cop ?? 0).TruncateIfNonDecimal(),
                Bsrn = Body.Bsrn,
                Vop = Body.Vop,
                Tsstam = (Body.Tsstam ?? 0).TruncateIfNonDecimal(),
                Nw = (Body.Nw ?? 0).TruncateIfNonDecimal(),
                Sscv = (Body.Sscv ?? 0).TruncateIfNonDecimal(4),
                Ssrv = (Body.Ssrv ?? 0).TruncateIfNonDecimal(),

                Iinn = Payment.Iinn,
                Acn = Payment.Acn,
                Trmn = Payment.Trmn,
                Trn = Payment.Trn,
                Pcn = Payment.Pcn,
                Pid = Payment.Pid,
                Pdt = Payment.Pdt,
                Pv = Payment.Pv,
                Pmt=Payment.Pmt
            };
        }

        
    }
}
