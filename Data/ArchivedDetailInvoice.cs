namespace TaxService.Data
{
    public class ArchivedDetailInvoice
    {
        public int Id { get; set; }
        public int ArchivedInvoiceId { get; set; }
        public string Sstid { get; set; }
        public string Sstt { get; set; }
        public string Mu { get; set; }
        public decimal Am { get; set; }
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
        public decimal Odr { get; set; }
        public decimal Odam { get; set; }
        public string Olt { get; set; }
        public decimal? Olr { get; set; }
        public decimal? Olam { get; set; }
        public decimal? Consfee { get; set; }
        public decimal? Spro { get; set; }
        public decimal? Bros { get; set; }
        public decimal? Tcpbs { get; set; }
        public decimal? Cop { get; set; }
        public string Bsrn { get; set; }
        public decimal? Vop { get; set; }
        public decimal? Tsstam { get; set; }
        public string Iinn { get; set; }
        public string Acn { get; set; }
        public string Trmn { get; set; }
        public string Trn { get; set; }
        public string Pcn { get; set; }
        public string Pid { get; set; }
        public long? Pdt { get; set; }
        public decimal? Nw { get; init; }
        public decimal? Ssrv { get; init; }
        public decimal? Sscv { get; init; }
        public int Pmt { get; init; }
        public long Pv { get; init; }
        public DateTime? Indati2mDate { get; set; }
        public DateTime? IndatimDate { get; set; }
        public DateTime? PdtDate { get; set; }
        public ArchivedInvoice ArchivedInvoice { get; set; }
    }
}
