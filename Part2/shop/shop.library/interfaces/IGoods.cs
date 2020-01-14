using System.Collections.Generic;

namespace Shop.Library
{
    public interface IGoods
    {
        void AddGood(IGood item);

        void RemoveGood(IGood item);

        List<IGood> GetGoods();
    }
}
