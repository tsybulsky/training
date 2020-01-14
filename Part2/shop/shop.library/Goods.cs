using System.Collections.Generic;

namespace Shop.Library
{
    public class Goods: IGoods
    {
        List<IGood> goods;
        public List<IGood> GetGoods()
        {
            return goods;
        }

        public void AddGood(IGood item)
        {
            goods.Add(item);
        }

        public void RemoveGood(IGood item)
        {
            goods.Remove(item);
        }

        public Goods()
        {
            goods = new List<IGood>();
        }

    }
}
