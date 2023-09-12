using System.ComponentModel;

namespace TaxService.Data.Enums
{

    public enum SortOrder
    {
        Asc,
        Desc
    }

    public enum SendType
    {
        Email,
        SMS
    }
    public enum EnvironmentType
    {
        Test,
        Prod
    }
    public enum StatusType
    {
        Succeed = 0,
        Failed = 1,
        Defective = 2,
        Sending = 3,
        Pending = 4,
        All
    }
    public enum ErrorLevel
    {
        Error,
        Warning,
    }
    public enum InvoiceType
    {
        [Description("-")]
        None = 0,

        [Description("نوع اول")]
        FirstType = 1,

        [Description("نوع دوم")]
        SecondType = 2,

        [Description("نوع سوم")]
        ThirdType = 3,

    }

    public enum InvoicePattern
    {
        [Description("-")]
        None = 0,

        [Description("فروش")]
        Sales = 1,

        [Description("فروش ارزی")]
        CurrencySales = 2,

        [Description("صورتحساب طلا، جواهر و پلاتین")]
        GoldJewelryPlatinumInvoice = 3,

        [Description("پیمانکاری")]
        Contracting = 4,

        [Description("قبوض خدماتی")]
        ServiceBill = 5,

        [Description("بلیت هواپیما")]
        PlaneTicket = 6,

        [Description("صادراتی")]
        Export = 7

    }

    public enum InvoiceSubject
    {
        [Description("-")]
        None = 0,

        [Description("اصلی")]
        Main = 1,

        [Description("اصلاحی")]
        Corrective = 2,

        [Description("ابطالی")]
        Cancellation = 3,

        [Description("برگشت از فروش")]
        ReturnFromSale = 4
    }

    public enum TaxPayerStatus
    {
        ENABLED
    }
}



