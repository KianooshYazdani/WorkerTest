using TaxService.Data.Enums;

namespace TaxService.Data
{
    public class ArchivedInvoice
    {

        public int Id { get; set; }
        public int FileHistoryId { get; set; }
        public string ReferenceNumber { get; set; }
        public DateTime SentDate { get; set; }
        public DateTime ResultDate { get; set; }
        public int Result { get; set; }
        public string ResultDetail { get; set; }
        public long? Indati2m { get; set; }
        public long? Indatim { get; set; }
        public InvoiceType? Inty { get; set; }
        public int? Ft { get; set; }
        public long? LongInno { get; set; }
        public string Inno { get; set; }
        public string Irtaxid { get; set; }
        public string Scln { get; set; }
        public int? Setm { get; set; }
        public string Tins { get; set; }
        public decimal? Cap { get; set; }
        public string Bid { get; set; }
        public decimal Insp { get; set; }
        public decimal Tvop { get; set; }
        public string Bpc { get; set; }
        public decimal? Tax17 { get; set; }
        public string Taxid { get; set; }
        public InvoicePattern? Inp { get; set; }
        public string Scc { get; set; }
        public InvoiceSubject? Ins { get; set; }
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
        public long? InnoIns { get; set; }
        public string Cdcn { get; init; }
        public int? Cdcd { get; init; }
        public decimal? Tonw { get; init; }
        public decimal? Torv { get; init; }
        public decimal? Tocv { get; init; }
        public string SentJsonstr { get; set; }
        public StatusType Status { get; set; }
        public FileHistory FileHistory { get; set; }
        public List<ArchivedDetailInvoice> ArchivedDetailInvoice { get; set; }
        public List<InvoiceResult> InvoiceResults { get; set; }

    }
}
