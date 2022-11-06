using System;
using System.Collections.Generic;
using System.Text;

namespace NeMAGUUU
{
    public class Graph
    {
        public int[,] Matrix;
        int size;

        public Graph(int size)
        {
            Matrix = new int[size, size];
            this.size = size;
        }

        public void SetConnection()
        {
            while (true)
            {
                Console.Write("Введите пару вершин: ");
                var line = Console.ReadLine();
                if (line == "-1")
                    break;
                var numbers = line.Split(' ');
                Matrix[int.Parse(numbers[0]) - 1, int.Parse(numbers[1]) - 1] = 1;
            }
        }

        public void PrintMatrix()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write(Matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }

    class Program
    {
        public static void Info()
        {
            Console.WriteLine("Данная программа предназначена для поиска минимального устойчивого внешнего множества в орграфе.");// write info
            Console.WriteLine("Результат выводить в виде чисел обозначающих номера вершин.");
            Console.WriteLine("Для получения результата укажите колличество вершин, а затем пары вершин,\n  образующих дуги, где первое число означает начало дуги, а второе конец.");
            Console.WriteLine("Для выхода напишите -1.");
        }

        public static void PrintSets(List<List<int>> sets)
        {
            for (int j = 0; j < sets.Count; j++)
            {
                for (int i = 0; i < sets[j].Count; i++)
                {
                    if (i==0)
                        Console.Write(sets[j][i]);
                    else
                        Console.Write(" + " + sets[j][i]);
                }
                if (j != sets.Count-1)
                    Console.Write(" * ");
            }
            Console.WriteLine();
        }

        public static void AddSetsToList(List<List<int>> sets, List<string> lines, int[,] matrix, int size)
        {
            for (int i = 0; i < size; i++)
            {
                var list = new List<int>();
                var line = new StringBuilder();
                for (int j = 0; j < size; j++)
                {
                    if (matrix[i, j] == 1)
                    {
                        list.Add(j + 1);
                        line.Append(j + 1);
                    }
                }
                sets.Add(list);
                lines.Add(line.ToString());
            }
        }

        private static void Absorption(List<List<int>> sets)
        {
            for (int i = 0; i < sets.Count; i++)
            {
                for (int j = 0; j < sets.Count; j++)
                {
                    if (i == j)
                        continue;
                    var flag = true;
                    foreach (var number in sets[j])
                    {
                        if (!sets[i].Contains(number))
                            flag = false;
                    }
                    if (flag)
                    {
                        sets.RemoveAt(i);
                        i--;
                        break;
                    }
                }
            }
        }

        private static void PrintResults(List<List<int>> result)
        {
            foreach (var set in result)
            {
                foreach (var item in set)
                    Console.Write(item + " ");
                Console.WriteLine();
            }
        }

        static void Main()
        {
            Info();
            var sets = new List<List<int>>();
            var lines = new List<string>();
            Console.Write("Укажите колличество вершин в графе: ");
            var size = int.Parse(Console.ReadLine());
            var matrix = new Graph(size);
            matrix.SetConnection();
            for (int i = 0; i < matrix.Matrix.GetLength(0); i++)
                matrix.Matrix[i, i] = 1;
            matrix.PrintMatrix();
            AddSetsToList(sets, lines, matrix.Matrix, size);
            Absorption(sets);
            PrintSets(sets);
            var setsSize = sets.Count;
            var counter = new int[setsSize - 1];  // чудо счётчик
            var partOfresult = new List<int>();
            var result = new List<List<int>>();
            if (counter.Length >= 2)
            {
                while (counter[setsSize - 2] < sets[sets.Count - 1].Count)
                {
                    for (int i = 0; i < sets[0].Count; i++)
                    {
                        partOfresult = new List<int>();
                        partOfresult.Add(sets[0][i]);
                        for (int j = 0; j < setsSize - 1; j++)
                        {
                            if (!partOfresult.Contains(sets[j + 1][counter[j]]))
                            {
                                partOfresult.Add(sets[j + 1][counter[j]]);
                            }
                        }
                        partOfresult.Sort();
                        result.Add(partOfresult);
                    }
                    counter[0]++;
                    for (int m = 0; m < counter.Length - 1; m++)
                    {
                        if (counter[m] == sets[m + 1].Count)
                        {
                            counter[m] = 0;
                            counter[m + 1]++;
                        }
                    }
                }
            }
            else if (counter.Length == 1)
            {
                for (int i = 0; i < sets[0].Count; i++)
                {
                    for (int j = 0; j < sets[1].Count; j++)
                    {
                        partOfresult = new List<int>();
                        partOfresult.Add(sets[0][i]);
                        if (!partOfresult.Contains(sets[1][j]))
                        {
                            partOfresult.Add(sets[1][j]);

                        }
                        result.Add(partOfresult);
                    }
                }
            }
            else
            {
                result.Add(sets[0]);
            }
            var totalLines = new List<string>();
            foreach (var set in result)
            {
                var line = new StringBuilder();
                foreach (var numbers in set)
                {
                    line.Append(numbers);
                }
                totalLines.Add(line.ToString());
            }
            for (int i = 0; i < totalLines.Count; i++)
            {
                for (int j = 0; j < totalLines.Count; j++)
                {
                    if ((string.Compare(totalLines[i], totalLines[j]) == 0 && i != j))
                    {
                        totalLines.RemoveAt(j);
                        result.RemoveAt(j);
                        j--;
                    }
                }
            }
            Absorption(result);
            PrintResults(result);
        }     
    }
}
    
