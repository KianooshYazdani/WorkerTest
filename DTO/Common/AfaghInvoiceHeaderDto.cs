#nullable disable
using System.ComponentModel.DataAnnotations;
using TaxCollectData.Library.Dto.Content;

namespace AfaghInvoiceManager.DTO
{
    public class AfaghInvoiceHeaderDto
    {
        public long? Indati2m { get; set; }
        public long? Indatim { get; set; }
        public int? Inty { get; set; }
        public int? Ft { get; set; }
        [Display(Name = "شماره صورت حساب")]
        public string Inno { get; set; }
        public string Irtaxid { get; set; }
        public string Scln { get; set; }
        public int? Setm { get; set; }
        public string Tins { get; set; }
        [Display(Name = "Cap")]
        public decimal? Cap { get; set; }
        public string Bid { get; set; }
        public decimal? Insp { get; set; }
        public decimal? Tvop { get; set; }
        public string Bpc { get; set; }
        public decimal? Tax17 { get; set; }
        public string Taxid { get; set; }
        public int? Inp { get; set; }
        public string Scc { get; set; }
        public int? Ins { get; set; }
        public string Billid { get; set; }
        public decimal? Tprdis { get; set; }
        public decimal? Tdis { get; set; }
        public decimal? Tadis { get; set; }
        public decimal? Tvam { get; set; }
        public decimal? Todam { get; set; }
        public decimal? Tbill { get; set; }
        public int? Tob { get; set; }
        public string Tinb { get; set; }
        public string Sbc { get; set; }
        public string Bbc { get; set; }
        public string Bpn { get; set; }
        public string Crn { get; set; }
        public int? Cdcd { get; set; }
        public string Cdcn { get; set; }
        public decimal? Tocv { get; set; }
        public decimal? Tonw { get; set; }
        public decimal? Torv { get; set; }

        public DateTime? IndatimDate { get; set; }

        public DateTime? Indati2mDate { get; set; }
        
        public AfaghInvoiceHeaderDto(InvoiceHeaderDto original)
        {
            Indati2m = original.Indati2m;
            Indatim = original.Indatim;
            Inty = original.Inty;
            Ft = original.Ft;
            Inno = original.Inno;
            Irtaxid = original.Irtaxid;
            Scln = original.Scln;
            Setm = original.Setm;
            Tins = original.Tins;
            Cap = original.Cap;
            Bid = original.Bid;
            Insp = original.Insp;
            Tvop = original.Tvop;
            Bpc = original.Bpc;
            Tax17 = original.Tax17;
            Taxid = original.Taxid;
            Inp = original.Inp;
            Scc = original.Scc;
            Ins = original.Ins;
            Billid = original.Billid;
            Tprdis = original.Tprdis;
            Tdis = original.Tdis;
            Tadis = original.Tadis;
            Tvam = original.Tvam;
            Todam = original.Todam;
            Tbill = original.Tbill;
            Tob = original.Tob;
            Tinb = original.Tinb;
            Sbc = original.Sbc;
            Bbc = original.Bbc;
            Bpn = original.Bpn;
            Crn = original.Crn;
            Cdcd = original.Cdcd;
            Cdcn = original.Cdcn;
            Tocv = original.Tocv;
            Tonw = original.Tonw;
            Torv = original.Torv;
        }

        public AfaghInvoiceHeaderDto()
        {
        }

        public InvoiceHeaderDto GetDto()
        {
            return new()
            {
                Indati2m = Indati2m.GetValueOrDefault(), //new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds(),
                //Indatim = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds(),
                Indatim = (IndatimDate == null ? Indatim : new DateTimeOffset(IndatimDate.Value).ToUnixTimeMilliseconds()) ?? 0,
                Inty = Inty ?? 0,
                Ft = Ft,
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
                Cdcd = Cdcd,
                Cdcn = Cdcn,
                Tocv = Tocv?.TruncateIfNonDecimal(4),
                Tonw = Tonw?.TruncateIfNonDecimal(8),
                Torv = Torv?.TruncateIfNonDecimal(),
            };
        }
    }
}
