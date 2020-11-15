namespace ObmultichoiceRetailer.Domain.DomainModels
{
  public enum SaleType
  {
    Paid,
    Credit,
  }

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