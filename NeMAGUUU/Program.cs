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
        public static void Info()
        {
            Console.WriteLine("info ");// write info
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

        static void Main()
        {
            var sets = new List<List<int>>();
            var lines = new List<string>();
            Console.Write("Укажите колличество вершин в графе: ");
            var size = int.Parse(Console.ReadLine());
            var matrix = new Graph(size);
            matrix.SetConnection();
            matrix.PrintMatrix();
            for (int i = 0; i < matrix.Matrix.GetLength(0); i++)
                matrix.Matrix[i, i] = 1;
            AddSetsToList(sets, lines, matrix.Matrix, size);
            PrintSets(sets);
            for (int i = 0; i < lines.Count; i++)
            {
                for (int j = 0; j < lines.Count; j++)
                {
                    if (i == j)
                        continue;
                    if (lines[j].Contains(lines[i]))
                    {
                        lines.RemoveAt(j);
                        sets.RemoveAt(j);
                    }
                }
            }
            //sets = new List<List<int>> { new List<int> { 1, 2, 3 }, new List<int> { 1, 2, 4 }, new List<int> { 7, 8, 9 } };
            var setsSize = sets.Count;
            var counter = new int[setsSize - 1];  // чудо счётчик
            var partOfresult = new List<int>();
            var result = new List<List<int>>();
            while (counter[setsSize - 2] < sets[sets.Count - 1].Count)
            {

                for (int i = 0; i < sets[0].Count; i++)
                {
                    partOfresult = new List<int>();
                    partOfresult.Add(sets[0][i]);
                    for (int j = 0; j < setsSize - 1; j++)
                    {
                        partOfresult.Add(sets[j + 1][counter[j]]);
                    }
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

            foreach (var set in result)
            {

                for (int i = 0; i < set.Count; i++)
                {
                    Console.Write(set[i] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}

