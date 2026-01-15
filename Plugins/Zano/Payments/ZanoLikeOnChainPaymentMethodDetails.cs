namespace Zano.Payments
{
    public class ZanoLikeOnChainPaymentMethodDetails
    {
        public long AccountIndex { get; set; }
        //public string AddressIndex { get; set; }
        public long? InvoiceSettledConfirmationThreshold { get; set; }
    }
}