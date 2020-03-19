using System;
using Shop.Library;
using DataChecks;

namespace Shop.Application
{
    class Program
    {
        static IntChecker intChecker = new IntChecker();
        static DoubleChecker doubleChecker = new DoubleChecker();
        static bool InputPositiveNumber(string caption, out double value)
        {
            while (true)
            {
                value = 0;
                Console.Write(caption);
                string inputValue = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(inputValue))
                    return false;
                if (doubleChecker.PositiveCheck(inputValue, out value))
                    return true;
            }
        }

        static bool InputPositiveNumber(string caption, out int value)
        {
            while (true)
            {
                value = 0;
                Console.Write(caption);
                string inputValue = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(inputValue))
                    return false;
                if (intChecker.PositiveCheck(inputValue, out value))
                    return true;
            }
        }
        static void Main(string[] args)
        {
            doubleChecker = new DoubleChecker();
            Library.Shop shop = new Library.Shop();
            double averangeSalary = 0;            
            if (!InputPositiveNumber("Введите сумму арендной платы в месяц: ", out double value))
                return;
            shop.FixedCosts.Rent.Cost = value;
            foreach(var employee in shop.FixedCosts.Staff)
            {
                if (!InputPositiveNumber($"Введите зарплату {employee.Position}: ", out value))
                    return;
                employee.Salary = value;
                averangeSalary += value;
            }
            averangeSalary /= shop.FixedCosts.Staff.Count;
            foreach(var good in shop.Goods.GetGoods())
            {
                if (!InputPositiveNumber($"Введите цену закупки {good.Title}: ", out value))
                    return;
                good.PurchasePrice = value;
                if (!InputPositiveNumber($"Введите цену продажи {good.Title}: ", out value))
                    return;
                good.SellingPrice = value;
                if (!InputPositiveNumber($"Количество единиц {good.Title}: ", out int quantity))
                    return;
                good.Quantity = quantity;
            }
            double profit = shop.GetProfit();
            if (profit > 0)
            {
                if (profit > averangeSalary)
                {
                    Console.WriteLine($"Определенно Ваш бизнес с такими параметрами должен быть успешным,\r\n" +
                      $"поскольку Ваш доход будет {profit} рублей/месяц");
                }
                else
                {
                    Console.WriteLine($"Бизнес-то будет приносить прибыль {profit} рублей/месяц,\r\n" +
                        $"но она ниже средней зарплаты сотрудников магазина {averangeSalary:N2}");
                }
            }
            else
            {
                Console.WriteLine($"С такими параметрами бизнес открывать нельзя. У Вас будет убыток {-profit} рублей/месяц");
            }
            Console.ReadLine();
        }
    }
}
