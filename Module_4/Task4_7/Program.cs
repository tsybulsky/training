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
            up,
            down
        };

        static void Swap(ref int var1, ref int var2)
        {
            var temp = var1;
            var1 = var2;
            var2 = temp;
        }

        static int PartitionUp(int[] array, int low, int high)
        {
            var pivot = array[(low + high) / 2];
            int i = low, j = high;
            while (i <= j)
            {
                while (array[i] < pivot)
                    i++;
                while (array[j] > pivot)
                    j--;
                if (i >= j)
                    break;
                Swap(ref array[i], ref array[j]);
            }
            return j;
        }

        static int PartitionDown(int[] array, int low, int high)
        {
            var pivot = array[(low + high) / 2];
            int i = low, j = high;
            while (i <= j)
            {
                while (array[i] > pivot)
                    i++;
                while (array[j] < pivot)
                    j--;
                if (i >= j)
                    break;
                Swap(ref array[i], ref array[j]);
            }
            return i;
        }

        static void QuickSort(int[] array, int low, int high, SortDirection direction)
        {
            if (low < high)
            {
                var p = (direction == SortDirection.up)?PartitionUp(array, low, high):PartitionDown(array, low, high);
                QuickSort(array, low, p, direction);
                QuickSort(array, p + 1, high, direction);
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
            if ((!int.TryParse(inputValue,out int Len)) || (Len <= 0) || (Len > 100))
            {
                Console.WriteLine("Неверная длина массива");
                Console.ReadKey();
                return;
            }
            var array = new int[Len];
            for (var i = 0; i < Len;i++)
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
            QuickSort(array, 0, Len - 1, (inputValue == "0")?SortDirection.up:SortDirection.down);
            Console.Write("Отсортированный массив:");
            OutputArray(array);
            Console.ReadKey();
        }
    }
}
