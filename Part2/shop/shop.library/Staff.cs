using System.Collections.Generic;

namespace Shop.Library
{    
    public class Staff: IStaff
    {
        private List<IEmployee> staff;
        
        public Staff()
        {
            staff = new List<IEmployee>();
        }

        public List<IEmployee> GetStaff()
        {
            return staff;
        }
    }
}
