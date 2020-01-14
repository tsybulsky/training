namespace Shop.Library
{
    class Employee : IEmployee
    {
        private double salary;
        public string Position
        {
            get;
            set;
        }
        public double Salary
        {
            get { return salary; }
            set
            {
                if (value > 0)
                    salary = value;
            }
        }

        public Employee(): this("") { }

        public Employee(string position)
        {
            Position = position;
            salary = 0;
        }
    }
}
