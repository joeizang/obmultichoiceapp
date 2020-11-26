using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ObmultichoiceRetailer.Domain.DomainModels
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum UnitMeasure
    {
        KG,
        MEASURE,
        PACKS,
        CRATE,
        DOZEN,
        HALF_DOZEN,
        PIECES,
        OTHER
    }
}
