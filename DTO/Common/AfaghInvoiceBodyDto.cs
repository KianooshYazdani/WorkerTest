#nullable disable
using System.ComponentModel.DataAnnotations;
using TaxCollectData.Library.Dto.Content;

namespace AfaghInvoiceManager.DTO
{
    public class AfaghInvoiceBodyDto
    {
        public string Sstid { get; set; }

        [Display(Name = "Sstt")]
        public string Sstt { get; set; }
        public string Mu { get; set; }
        public decimal? Am { get; set; }
        public decimal? Fee { get; set; }
        public decimal? Cfee { get; set; }
        public string Cut { get; set; }
        public decimal? Exr { get; set; }
        public decimal? Prdis { get; set; }
        public decimal? Dis { get; set; }
        public decimal? Adis { get; set; }
        public decimal? Vra { get; set; }
        public decimal? Vam { get; set; }
        public string Odt { get; set; }
        public decimal? Odr { get; set; }
        public decimal? Odam { get; set; }
        public string Olt { get; set; }
        public decimal? Olr { get; set; }
        public decimal? Olam { get; set; }
        public decimal? Consfee { get; set; }
        public decimal? Spro { get; set; }
        public decimal? Bros { get; set; }
        public decimal? Tcpbs { get; set; }
        [Display(Name = "Cop")]
        public decimal? Cop { get; set; }
        public string Bsrn { get; set; }
        public decimal? Vop { get; set; }
        public decimal? Tsstam { get; set; }
        public decimal? Nw { get; set; }
        public decimal? Ssrv { get; set; }
        public decimal? Sscv { get; set; }

        public AfaghInvoiceBodyDto() { }
        public AfaghInvoiceBodyDto(InvoiceBodyDto dto)
        {
            Sstid = dto.Sstid;
            Sstt = dto.Sstt;
            Mu = dto.Mu;
            Am = dto.Am;
            Fee = dto.Fee;
            Cfee = dto.Cfee;
            Cut = dto.Cut;
            Exr = dto.Exr;
            Prdis = dto.Prdis;
            Dis = dto.Dis;
            Adis = dto.Adis;
            Vra = dto.Vra;
            Vam = dto.Vam;
            Odt = dto.Odt;
            Odr = dto.Odr;
            Odam = dto.Odam;
            Olt = dto.Olt;
            Olr = dto.Olr;
            Olam = dto.Olam;
            Consfee = dto.Consfee;
            Spro = dto.Spro;
            Bros = dto.Bros;
            Tcpbs = dto.Tcpbs;
            Cop = dto.Cop;
            Bsrn = dto.Bsrn;
            Vop = dto.Vop;
            Tsstam = dto.Tsstam;
            Sscv = dto.Sscv;
            Ssrv = dto.Ssrv;
            Nw = dto.Nw;
        }

        public InvoiceBodyDto GetDto()
        {
            return new()
            {
                Sstid = Sstid,
                Sstt = Sstt,
                Mu = Mu,
                Am = (Am ?? 0).TruncateIfNonDecimal(8),
                Fee = (Fee ?? 0).TruncateIfNonDecimal(8),
                Cfee = Cfee?.TruncateIfNonDecimal(4),
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
                Nw = Nw?.TruncateIfNonDecimal(8),
                Sscv = Sscv?.TruncateIfNonDecimal(4),
                Ssrv = Ssrv?.TruncateIfNonDecimal(),
            };
        }
    }
}
