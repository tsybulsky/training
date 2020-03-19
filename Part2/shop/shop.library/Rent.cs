namespace Shop.Library
{
    class Rent : IRent
    {
        private double rentCost;
        public double Cost
        {
            get
            {
                return rentCost;
            }
            set
            {
                if (value >= 0)
                    rentCost = value;
            }
        }

        public Rent(): this(0) { }
        public Rent(double rentCost)
        {
            this.rentCost = rentCost;
        }
    }
}
