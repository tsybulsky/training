using System.Collections.Generic;

namespace Shop.Library
{
    public class FixedCosts : IFixedCosts
    {
        List<IEmployee> staff;

        Rent rent;

        public List<IEmployee> Staff
        {
            get { return staff; }
        }

        public IRent Rent { get { return rent; } }

        public double Calculate()
        {
            double result = 0;
            foreach (var employee in staff)
                result += employee.Salary;
            return result + rent.Cost;
        }

        public FixedCosts()
        {
            rent = new Rent();
            staff = new List<IEmployee>()
            {
                new Employee("Грузчик"),
                new Employee("Закупщик"),
                new Employee("Продавец"),
                new Employee("Бухгалтер")
            };
        }
    }
}
