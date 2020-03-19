namespace Shop.Library
{
    public class Good : IGood
    {
        private double purchasePrice;

        private double sellingPrice;

        private int quantity;

        public string Title { get; set; }
        public double PurchasePrice
        {
            get { return purchasePrice; }
            set
            {
                if (value > 0)
                    purchasePrice = value;
            }
        }
        public double SellingPrice
        {
            get { return sellingPrice; }
            set
            {
                if (value > 0)
                    sellingPrice = value;
            }
        }

        public int Quantity
        {
            get
            {
                return quantity;
            }
            set
            {
                if (value >= 0)
                    quantity = value;
            }
        }

        public Good()
        {

        }

        public Good(string title, double purchasePrice, double SellingPrice, int quantity)
        {
            Title = title;
            PurchasePrice = purchasePrice;
            this.SellingPrice = SellingPrice;
            Quantity = quantity;
        }
    }
}
