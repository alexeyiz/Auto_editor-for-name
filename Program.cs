using System;
using System.IO;


namespace ConsoleApp1
{
    class Program
    {
        static int Minimum(int a, int b) => a < b ? a : b;

        static int Minimum(int a, int b, int c) => (a = a < b ? a : b) < c ? a : c;

        static int DamerauLevenshteinDistance(string firstText, string secondText)
        {
            var n = firstText.Length + 1;
            var m = secondText.Length + 1;
            var arrayD = new int[n, m];

            for (var i = 0; i < n; i++)
            {
                arrayD[i, 0] = i;
            }

            for (var j = 0; j < m; j++)
            {
                arrayD[0, j] = j;
            }

            for (var i = 1; i < n; i++)
            {
                for (var j = 1; j < m; j++)
                {
                    var cost = firstText[i - 1] == secondText[j - 1] ? 0 : 1;

                    arrayD[i, j] = Minimum(arrayD[i - 1, j] + 1,          // удаление
                                            arrayD[i, j - 1] + 1,         // вставка
                                            arrayD[i - 1, j - 1] + cost); // замена

                    if (i > 1 && j > 1
                        && firstText[i - 1] == secondText[j - 2]
                        && firstText[i - 2] == secondText[j - 1])
                    {
                        arrayD[i, j] = Minimum(arrayD[i, j],
                                           arrayD[i - 2, j - 2] + cost); // перестановка
                    }
                }
            }

            return arrayD[n - 1, m - 1];
        }

        static void Main(string[] args)
        {
            string[] str = File.ReadAllLines("us.txt");
            Console.WriteLine("Enter name:");
            var w2 = Console.ReadLine();
            if (String.IsNullOrEmpty(w2))
            {
                Console.WriteLine("Имя не введено");
                return;
            }
            foreach (int element in w2)
            {
                if ((element < 'A' || element > 'Z') && (element < 'a' || element > 'z') && element != ' ' && element != '-')
                {
                    Console.WriteLine("Ошибка ввода. Проверьте входные данные и повторите запрос");
                    return;
                }
            }
            int i = str.Length;
            int a = 0;
            int v = 0;
            while (a < i)
            {
                int c = DamerauLevenshteinDistance(str[a], w2);
                a++;
                if (c < 2)
                {
                    Console.WriteLine("\nDid you mean {0} ? Y/N", str[a]);
                    ConsoleKeyInfo result = Console.ReadKey();
                    if (result.KeyChar == 'Y' || result.KeyChar == 'y')
                    {
                        Console.WriteLine("Y\n");
                        Console.WriteLine("\n Hello, {0}!", str[a]);
                        v = 1;
                        break;
                    }
                    if (result.KeyChar == 'N' || result.KeyChar == 'n')
                    {
                        Console.WriteLine("N\n");
                    }
                    else
                        a--;
                }
            }
            if(v == 0)
                Console.WriteLine("Близкое имя не найдено");
            Console.ReadLine();
        }
    }
}
