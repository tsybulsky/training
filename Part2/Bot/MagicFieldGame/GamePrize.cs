using System;
using System.Collections.Generic;
using System.Text;

namespace MagicFieldGame
{
    class GamePrize : IComparable
    {
        public int Cost { get; private set; }
        public string Title { get; private set; }
        public int CompareTo(object obj)
        {
            int compareResult = Cost.CompareTo(((GamePrize)obj).Cost);
            if (compareResult == 0)
            {
                return Title.CompareTo(((GamePrize)obj).Title);
            }
            else
                return compareResult;
        }

        public GamePrize(string value)
        {
            string[] values = value.Split('|');
            if (values.Length != 2)
            {
                Cost = 0;
                Title = "Все что пожелаете";
            }
            else
            {
                int.TryParse(values[0], out int temp);
                Cost = temp;
                Title = values[1];
            }            
        }
    }
}
