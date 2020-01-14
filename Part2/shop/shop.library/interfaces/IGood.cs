namespace Shop.Library
{
    public interface IGood
    {
        string Title { get; set; }
        double PurchasePrice{get; set;}
        double SellingPrice { get; set; }

        int Quantity { get; set; }
    }
}
