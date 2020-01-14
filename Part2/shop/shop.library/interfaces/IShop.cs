namespace Shop.Library
{
    public interface IShop
    {
        IFixedCosts FixedCosts { get; }
        IGoods Goods { get; }        
        double GetProfit();
    }
}
