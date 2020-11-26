
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ObmultichoiceRetailer.Domain.DomainModels
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SaleType
    {
        Paid,
        Credit,
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum PaymentType
    {
        Cash,
        Credit,
        Electronic,
        USSD,
        Cheque,
        Other
    }
}