#nullable disable
using System.ComponentModel.DataAnnotations;
using TaxCollectData.Library.Dto.Content;

namespace AfaghInvoiceManager.DTO
{
    public class PlainInvoice
    {
        public int Id { get; set; }

        #region Properties
        [Display(Name = "Indati2m")]
        public long? Indati2m { get; set; }

        [Display(Name = "Indatim")]
        public long? Indatim { get; set; }


        [Display(Name = "Inty")]
        public int? Inty { get; set; }


        [Display(Name = "Ft")]
        public int? Ft { get; set; }

        [Display(Name = "شماره صورت حساب")]
        public string Inno { get; set; }

        [Display(Name = "Irtaxid")]
        public string Irtaxid { get; set; }


        [Display(Name = "Scln")]
        public string Scln { get; set; }


        [Display(Name = "Setm")]
        public int? Setm { get; set; }


        [Display(Name = "Tins")]
        public string Tins { get; set; }


        [Display(Name = "Cap")]
        public decimal? Cap { get; set; }

        [Display(Name = "Bid")]
        public string Bid { get; set; }


        [Display(Name = "Insp")]
        public decimal? Insp { get; set; }


        [Display(Name = "Tvop")]
        public decimal? Tvop { get; set; }


        [Display(Name = "Bpc")]
        public string Bpc { get; set; }


        [Display(Name = "Tax17")]
        public decimal? Tax17 { get; set; }

        [Display(Name = "Taxid")]
        public string Taxid { get; set; }


        [Display(Name = "Inp")]
        public int? Inp { get; set; }


        [Display(Name = "Scc")]
        public string Scc { get; set; }


        [Display(Name = "Ins")]
        public int? Ins { get; set; }


        [Display(Name = "Billid")]
        public string Billid { get; set; }


        [Display(Name = "Tprdis")]
        public decimal? Tprdis { get; set; }


        [Display(Name = "Tdis")]
        public decimal? Tdis { get; set; }


        [Display(Name = "Tadis")]
        public decimal? Tadis { get; set; }


        [Display(Name = "Tvam")]
        public decimal? Tvam { get; set; }


        [Display(Name = "Todam")]
        public decimal? Todam { get; set; }


        [Display(Name = "Tbill")]
        public decimal? Tbill { get; set; }


        [Display(Name = "Tob")]
        public int? Tob { get; set; }


        [Display(Name = "Tinb")]
        public string Tinb { get; set; }


        [Display(Name = "Sbc")]
        public string Sbc { get; set; }


        [Display(Name = "Bbc")]
        public string Bbc { get; set; }


        [Display(Name = "Bpn")]
        public string Bpn { get; set; }


        [Display(Name = "Crn")]
        public string Crn { get; set; }

        [Display(Name = "IndatimDate")]
        public DateTime? IndatimDate { get; set; }
        [Display(Name = "Indati2mDate")]
        public DateTime? Indati2mDate { get; set; }

        //**********Body*************

        [Display(Name = "Sstid")]
        public string Sstid { get; set; }


        [Display(Name = "Sstt")]
        public string Sstt { get; set; }



        [Display(Name = "Mu")]
        public string Mu { get; set; }


        [Display(Name = "Am")]
        public decimal? Am { get; set; }


        [Display(Name = "Fee")]
        public decimal? Fee { get; set; }


        [Display(Name = "Cfee")]
        public decimal? Cfee { get; set; }


        [Display(Name = "Cut")]
        public string Cut { get; set; }


        [Display(Name = "Exr")]
        public decimal? Exr { get; set; }


        [Display(Name = "Prdis")]
        public decimal? Prdis { get; set; }


        [Display(Name = "Dis")]
        public decimal? Dis { get; set; }


        [Display(Name = "Adis")]
        public decimal? Adis { get; set; }


        [Display(Name = "Vra")]
        public decimal? Vra { get; set; }


        [Display(Name = "Vam")]
        public decimal? Vam { get; set; }


        [Display(Name = "Odt")]
        public string Odt { get; set; }


        [Display(Name = "Odr")]
        public decimal? Odr { get; set; }


        [Display(Name = "Odam")]
        public decimal? Odam { get; set; }


        [Display(Name = "Olt")]
        public string Olt { get; set; }


        [Display(Name = "Olr")]
        public decimal? Olr { get; set; }


        [Display(Name = "Olam")]
        public decimal? Olam { get; set; }


        [Display(Name = "Consfee")]
        public decimal? Consfee { get; set; }


        [Display(Name = "Spro")]
        public decimal? Spro { get; set; }


        [Display(Name = "Bros")]
        public decimal? Bros { get; set; }


        [Display(Name = "Tcpbs")]
        public decimal? Tcpbs { get; set; }


        [Display(Name = "Cop")]
        public decimal? Cop { get; set; }


        [Display(Name = "Bsrn")]
        public string Bsrn { get; set; }


        [Display(Name = "Vop")]
        public decimal? Vop { get; set; }


        [Display(Name = "Tsstam")]
        public decimal? Tsstam { get; set; }

        //**********Payment**********

        [Display(Name = "Iinn")]
        public string Iinn { get; set; }


        [Display(Name = "Acn")]
        public string Acn { get; set; }


        [Display(Name = "Trmn")]
        public string Trmn { get; set; }


        [Display(Name = "Trn")]
        public string Trn { get; set; }


        [Display(Name = "Pcn")]
        public string Pcn { get; set; }


        [Display(Name = "Pid")]
        public string Pid { get; set; }


        [Display(Name = "Pdt")]
        public long? Pdt { get; set; }

        [Display(Name = "Cdcn")]
        public string Cdcn { get; init; }

        [Display(Name = "Cdcd")]
        public int? Cdcd { get; init; }

        [Display(Name = "Tonw")]
        public Decimal? Tonw { get; init; }

        [Display(Name = "Torv")]
        public Decimal? Torv { get; init; }

        [Display(Name = "Tocv")]
        public Decimal? Tocv { get; init; }

        [Display(Name = "Nw")]
        public Decimal? Nw { get; init; }

        [Display(Name = "Ssrv")]
        public Decimal? Ssrv { get; init; }

        [Display(Name = "Sscv")]
        public Decimal? Sscv { get; init; }

        [Display(Name = "Pmt")]
        public int? Pmt { get; init; }

        [Display(Name = "Pv")]
        public long? Pv { get; init; }

        [Display(Name = "PdtDate")]
        public DateTime? PdtDate { get; set; }
        #endregion

        public InvoiceDto GetInvoiceDto()
        {
            return new InvoiceDto()
            {
                Header = GetHeaderDto(),
                Body = new List<InvoiceBodyDto> { GetBodyDto() },
                Payments = new List<PaymentDto> { GetPaymentDto() }
            };
        }

        public PendingInvoice GetPendingInvoice()
        {
            return PendingInvoice.GetPendingInvoice(GetInvoiceDto());
        }

        public InvoiceBodyDto GetBodyDto()
        {
            return new()
            {
                Sstid = Sstid,
                Sstt = Sstt,
                Mu = Mu,
                Am = (Am ?? 0).TruncateIfNonDecimal(8),
                Fee = (Fee ?? 0).TruncateIfNonDecimal(8),
                Cfee = (Cfee ?? 0).TruncateIfNonDecimal(4),
                Cut = Cut,
                Exr = (Exr ?? 0).TruncateIfNonDecimal(),
                Prdis = (Prdis ?? 0).TruncateIfNonDecimal(),
                Dis = (Dis ?? 0).TruncateIfNonDecimal(),
                Adis = (Adis ?? 0).TruncateIfNonDecimal(),
                Vra = (Vra ?? 0).TruncateIfNonDecimal(2),
                Vam = (Vam ?? 0).TruncateIfNonDecimal(),
                Odt = Odt,
                Odr = (Odr ?? 0).TruncateIfNonDecimal(2),
                Odam = (Odam ?? 0).TruncateIfNonDecimal(),
                Olt = Olt,
                Olr = (Olr ?? 0).TruncateIfNonDecimal(2),
                Olam = (Olam ?? 0).TruncateIfNonDecimal(),
                Consfee = (Consfee ?? 0).TruncateIfNonDecimal(),
                Spro = (Spro ?? 0).TruncateIfNonDecimal(),
                Bros = (Bros ?? 0).TruncateIfNonDecimal(),
                Tcpbs = (Tcpbs ?? 0).TruncateIfNonDecimal(),
                Cop = (Cop ?? 0).TruncateIfNonDecimal(),
                Bsrn = Bsrn,
                Vop = (Vop ?? 0).TruncateIfNonDecimal(),
                Tsstam = (Tsstam ?? 0).TruncateIfNonDecimal(),
                Nw = (Nw ?? 0).TruncateIfNonDecimal(8),
                Sscv = (Sscv ?? 0).TruncateIfNonDecimal(4),
                Ssrv = (Ssrv ?? 0).TruncateIfNonDecimal(),
            };
        }

        public InvoiceHeaderDto GetHeaderDto()
        {
            return new()
            {
                Indati2m = (Indati2mDate == null ? Indati2m : new DateTimeOffset(Indati2mDate.Value).ToUnixTimeMilliseconds()) ?? 0,
                Indatim = (IndatimDate == null ? Indatim : new DateTimeOffset(IndatimDate.Value).ToUnixTimeMilliseconds()) ?? 0,
                Inty = Inty ?? 0,
                Ft = Ft ?? 0,
                Inno = Inno,
                Irtaxid = Irtaxid,
                Scln = Scln,
                Setm = Setm ?? 0,
                Tins = Tins,
                Cap = (Cap ?? 0).TruncateIfNonDecimal(),
                Bid = Bid,
                Insp = (Insp ?? 0).TruncateIfNonDecimal(),
                Tvop = (Tvop ?? 0).TruncateIfNonDecimal(),
                Bpc = Bpc,
                Tax17 = (Tax17 ?? 0).TruncateIfNonDecimal(),
                Taxid = Taxid,
                Inp = Inp ?? 0,
                Scc = Scc,
                Ins = Ins ?? 0,
                Billid = Billid,
                Tprdis = (Tprdis ?? 0).TruncateIfNonDecimal(),
                Tdis = (Tdis ?? 0).TruncateIfNonDecimal(),
                Tadis = (Tadis ?? 0).TruncateIfNonDecimal(),
                Tvam = (Tvam ?? 0).TruncateIfNonDecimal(),
                Todam = (Todam ?? 0).TruncateIfNonDecimal(),
                Tbill = (Tbill ?? 0).TruncateIfNonDecimal(),
                Tob = Tob ?? 0,
                Tinb = Tinb,
                Sbc = Sbc,
                Bbc = Bbc,
                Bpn = Bpn,
                Crn = Crn,
                Cdcd = Cdcd ?? 0,
                Cdcn = Cdcn,
                Tocv = (Tocv ?? 0).TruncateIfNonDecimal(4),
                Tonw = (Tonw ?? 0).TruncateIfNonDecimal(8),
                Torv = (Torv ?? 0).TruncateIfNonDecimal(),
            };
        }

        public PaymentDto GetPaymentDto()
        {
            return new()
            {
                Iinn = Iinn,
                Acn = Acn,
                Trmn = Trmn,
                Trn = Trn,
                Pcn = Pcn,
                Pid = Pid,
                Pmt = Pmt.GetValueOrDefault(),
                Pv = Pv.GetValueOrDefault(),
                Pdt = (PdtDate == null ? Pdt : new DateTimeOffset(PdtDate.Value).ToUnixTimeMilliseconds()) ?? 0
            };
        }
    }
}
