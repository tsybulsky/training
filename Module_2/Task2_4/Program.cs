using System;

namespace Task2_4
{
    class Program
    {
        // Параметр param указывает на то, что мы вычисляем. 
        // param = 0 - вычисляем периметр, 
        // param = 1 - вычисляем площадь
        static void CalculateCircle(int param)
        {
            Console.Write("Введите радиус круга: ");
            string inputValue = Console.ReadLine();
            if ((!double.TryParse(inputValue, out double r))&&(r <= 0))
            {                
                Console.WriteLine("Неверное значение радиуса");
                return;
            }
            if (param == 0)
            {
                double p = 2 * Math.PI * r;
                Console.WriteLine($"Периметр круга с радиусом {r:F3} равен {p:F3}");
                double a = 0.3 * p;
                double b = p / 2 - a;
                Console.WriteLine($"Прямоугольник со сторонами {a:F3} и {b:F3} имеет такой же периметр");
                a = p / 3;
                Console.WriteLine($"Равносторонний треугольник со сторонами {a:F3} имеет такой же периметр");
            }
            else
            {
                double s = Math.PI * r * r;
                Console.WriteLine($"Площадь круга с радиусом {r:F3} равна {s:F3}");
                double a = Math.Sqrt(s / 0.4);
                double b = 0.4 * a;
                Console.WriteLine($"Прямоугольник со сторонаями {a:F3} и {b:F3} имеет такую же площадь");
                a *= 2;
                b *= 2;
                Console.WriteLine($"Прямоугольный треугольник со катетами {a:F3} и {b:F3} имеет такую же площадь");
            }
        }

        // Вычисление параметров четырехугольника.
        static void CalculateQuadrangle(int param)
        {
            Console.Write("Введите стороны четырехугольника: ");
            string inputValue = Console.ReadLine();
            string[] values = inputValue.Split(' ');
            if (values.Length != 4)
            {
                Console.WriteLine("У четырехугольника 4 стороны");
                return;
            }
            double[] edges = new double[4];
            for (int i = 0; i < 4; i++)
            {
                if (!double.TryParse(values[i], out edges[i]))
                {
                    Console.WriteLine($"Значение длины {i + 1} стороны не является числом");
                    return;
                }
                if (edges[i] <= 0)
                {
                    Console.WriteLine($"Неверное числовое значение длины у стороны {i + 1}");
                    return;
                }
            }
            if (param == 0)
            {
                double p = 0.0;
                for (int i = 0; i < 4; i++)
                {
                    p += edges[i];
                }
                Console.WriteLine($"Периметр четырехугольника равен {p}");
                double r = p / (2 * Math.PI);
                Console.WriteLine($"Окружность с радиусом {r} имеет такую же длину");
                edges[0] += edges[3] / 2;
                edges[1] += edges[3] / 2;
                Console.WriteLine($"Треугольник со сторонами {edges[0]} {edges[1]} и {edges[2]} имеет такой же периметр");
            }
            else
            {
                double p = 0.0;
                for (int i = 0; i < 4; i++)
                {
                    p += edges[i];
                }
                p /= 2;
                double s = p - edges[3];
                Console.Write($"{s:N3} ");
                for (int i = 0; i < 3; i++)
                {
                    s *= p - edges[i];
                    Console.Write($"{s:N3} ");
                }
                s = Math.Sqrt(s);
                Console.WriteLine($"\nПлощадь четырехугольника {s}");
                double r = Math.Sqrt(s / Math.PI);
                Console.WriteLine($"Круг с радиусом {r} имеет такую же площадь");
                double a = Math.Sqrt(4 * s / Math.Sqrt(3));
                Console.WriteLine($"Равносторонний треугольник со стороной {a} имеет такую же площадь");
            }
        }

        // Вычисление параметров треугольника.
        static void CalculateTriangle(int param)
        {
            Console.Write("Введите стороны треугольника: ");
            string inputValue = Console.ReadLine();
            string[] values = inputValue.Split(' ');
            // edges - стороны треугольника. Какой он будет, определится по количеству
            // введенных пользователем данных.
            double[] edges = new double[3];
            if (values.Length > 3)
            {
                Console.WriteLine("Введено более 3-х значений");
                return;
            }
            for (int i = 0; i < values.Length; i++)
            {
                if (!double.TryParse(values[i], out edges[i]))
                {
                    Console.WriteLine($"Неверное число стороны {i + 1}");
                    return;
                }
            }
            if (values.Length == 1)
            {
                Console.WriteLine("Вы указали равносторонний треугольник");
                edges[1] = edges[0];
                edges[2] = edges[0];
            }
            else
            if (values.Length == 2)
            {
                Console.WriteLine("Вы указали равнобедренный треугольник");
                edges[2] = edges[0];
            }
            if (param == 0)
            {
                double p = 0;
                for (int i = 0; i < 3; i++)
                {
                    p += edges[i];
                }
                Console.WriteLine($"Периметр треугольника {p}");
                double r = p / (2 * Math.PI);
                Console.WriteLine($"Окружность с радиусом {r} имеет такую же длину");
                double a = 0.3 * p;
                double b = p / 2 - a;
                Console.WriteLine($"Прямоугольник со сторонами {a} и {b} имеет такой же периметр");
            }
            else
            {
                double p = (edges[0] + edges[1] + edges[2]) / 2;
                double s = Math.Sqrt(p * (p - edges[0]) * (p - edges[1]) * (p - edges[2]));
                Console.WriteLine($"Площадь треугольника {s}");
                double r = Math.Sqrt(s / Math.PI);
                Console.WriteLine($"Круг с радиусом {r} имеет такую же площадь");
                double a = Math.Sqrt(s);
                Console.WriteLine($"Квадрат со стороной {a} имеет такую же площадь");
            }
        }
        static void Main(string[] args)
        {
            Console.Write("Укажите тип фигуры (0 - круг, 3 - треугольник, 4 - четырехугольник): ");
            string inputValue = Console.ReadLine();
            if ((inputValue != "0") && (inputValue != "3") && (inputValue != "4"))
            {
                Console.WriteLine("Указана неверная фигура");
                Console.ReadKey();
                return;
            }
            int figureKind = int.Parse(inputValue);
            Console.Write("Укажите вычисляемый параметр (0 - периметр, 1 - площадь): ");
            inputValue = Console.ReadLine();
            if ((inputValue != "0") && (inputValue != "1"))
            {
                Console.WriteLine("Неверный вычисляемый параметр");
                Console.ReadKey();
                return;
            }
            int param = int.Parse(inputValue);
            switch (figureKind)
            {
                case 0:
                    {
                        CalculateCircle(param);
                        break;
                    }
                case 3:
                    {
                        CalculateTriangle(param);
                        break;
                    }
                case 4:
                    {
                        CalculateQuadrangle(param);
                        break;
                    }
            }
            Console.ReadKey();
        }
    }
}
