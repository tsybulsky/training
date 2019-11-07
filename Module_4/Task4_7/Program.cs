using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4_7
{
    class Program
    {
        enum SortDirection
        {
            Up,
            Down
        };

        static void Swap(ref int var1, ref int var2)
        {
            var temp = var1;
            var1 = var2;
            var2 = temp;
        }

        static int PartitionUp(int[] array, int start, int end)
        {
            int marker = start;            
            for (var i = start; i <= end; i++)
            {
                if (array[i] < array[end])
                {
                    Swap(ref array[i], ref array[marker]);
                    marker++;
                }
            }
            Swap(ref array[marker], ref array[end]);
            return marker;            
        }

        static int PartitionDown(int[] array, int start, int end)
        {
            int marker = start;
            for (var i = start; i <= end; i++)
            {
                if (array[i] > array[end])
                {
                    Swap(ref array[i], ref array[marker]);
                    marker++;
                }
            }
            Swap(ref array[marker], ref array[end]);
            return marker;
        }

        static void QuickSort(int[] array, int start, int end, SortDirection direction)
        {
            if (start < end)
            {
                int pivot = (direction == SortDirection.Up)?PartitionUp(array, start, end):PartitionDown(array, start, end);
                QuickSort(array, start, pivot-1, direction);
                QuickSort(array, pivot + 1, end, direction);
            }
        }

        static void OutputArray(int[] array)
        {
            foreach (var item in array)
            {
                Console.Write($" {item}");
            }
            Console.WriteLine("");
        }

        static void Main()
        {
            var rand = new Random();
            Console.Write("Введите размер массива (1..100): ");
            var inputValue = Console.ReadLine();
            if ((!int.TryParse(inputValue, out int len)) || (len <= 0) || (len > 100))
            {
                Console.WriteLine("Неверная длина массива");
                Console.ReadKey();
                return;
            }
            var array = new int[len];
            for (var i = 0; i < len;i++)
            {
                array[i] = rand.Next(-100, 100);            
            }
            Console.Write("Введите направление сортировки (0 - по возрастанию, 1 - по убыванию): ");
            inputValue = Console.ReadLine().Trim();
            if ((inputValue != "0") && (inputValue != "1"))
            {
                Console.WriteLine("Неверно указано направление сортировки");
                Console.ReadKey();
                return;
            }
            Console.Write("Исходный массив:");
            OutputArray(array);
            QuickSort(array, 0, len - 1, (inputValue == "0")?SortDirection.Up:SortDirection.Down);
            Console.Write("Отсортированный массив:");
            OutputArray(array);
            Console.ReadKey();
        }
    }
}
