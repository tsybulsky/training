using System.Collections.Generic;

namespace Shop.Library
{
    public interface IFixedCosts
    {
        List<IEmployee> Staff { get; }
        IRent Rent { get; }
        double Calculate();
    }
}
