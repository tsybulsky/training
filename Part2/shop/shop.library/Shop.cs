namespace Shop.Library
{
    public class Shop : IShop
    {
        private Goods goods;
        private FixedCosts fixedCosts;        
        public IFixedCosts FixedCosts
        {
            get 
            {
                return fixedCosts;
            }
        }
        
        public IGoods Goods
        {
            get
            {
                return goods;
            }
        }

        public double GetProfit()
        {
            double result=0;
            foreach (var good in goods.GetGoods())
                result += (good.SellingPrice - good.PurchasePrice) * good.Quantity;
            return result - fixedCosts.Calculate();
        }

        public Shop()
        {
            goods = new Goods();
            goods.AddGood(new Good("Сапоги женский", 150, 200, 10));
            fixedCosts = new FixedCosts();
        }
    }
}
