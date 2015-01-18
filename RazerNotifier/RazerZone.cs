namespace RazerNotifier
{
    public class RazerProductInfoList
    {
        public RazerProductInfo productInfo { get; set; }
    }

    public class RazerProductInfo
    {
        public RazerProduct product { get; set; }
    }

    public class RazerProduct
    {
        public string productID { get; set; }
        public string purchasable { get; set; }
        public string productParentID { get; set; }
        public string displayName { get; set; }
        public RazerProductPrice price { get; set; }
        public string estShipDate { get; set; }
        public string NEWestShipDate { get; set; }
        public string stockStatus { get; set; }
        public string availableQuantity { get; set; }
        public string backOrderQty { get; set; }
        public string preOrderQty { get; set; }
        public string buyLink { get; set; }
        public string weight { get; set; }
    }

    public class RazerProductPrice
    {
        public string taxIncludedInPrice { get; set; }
        public string discounted { get; set; }
        public string unitPrice { get; set; }
        public string unitPriceWithDiscount { get; set; }
    }
}
