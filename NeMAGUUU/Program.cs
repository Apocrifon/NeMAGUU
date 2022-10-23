using System;
using System.Collections.Generic;
using System.Text;

namespace NeMAGUUU
{
    public class Graph
    {
        public int[,]  Matrix;
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

        public void SetConnection( ) //сделать сплит
        {
            while (true)
            {
                int firstNode= int.Parse(Console.ReadLine());
                if (firstNode == -1)
                    break;
                int secondNode = int.Parse(Console.ReadLine());
                Matrix[firstNode - 1, secondNode - 1] = 1;
            }
        }

        public void PrintMatrix()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write(Matrix[i,j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

    }

    class Program
    {
        static void Main()
        {
            var sets = new List<List<int>>();
            var lines = new List<string>();
            var counters = new List<int>();
            var lineOfNumbers = new StringBuilder();
            Console.Write("Укажите колличество вершин в графе: ");
            var size = int.Parse(Console.ReadLine());
            var matrix = new Graph(size);
            matrix.SetConnection();
            matrix.PrintMatrix();
            for (int i = 0; i < size; i++)
            {
                var list = new List<int>();
                list.Add(i+1);
                for (int j = 0; j < size; j++)
                {
                    if (matrix.Matrix[i, j] == 1)
                        list.Add(j+1);
                }
                list.Sort();
                foreach (var item in list)
                    lineOfNumbers.Append(item);
                sets.Add(list);
                lines.Add(lineOfNumbers.ToString());
                lineOfNumbers.Clear();

            }
            //foreach (var set in sets)
            //{

            //    for (int i = 0; i < set.Count; i++)
            //    {
            //        Console.Write(set[i] + " ");
            //    }
            //    Console.WriteLine();
            //}
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (i == j)
                        continue;
                    if (lines[j].Contains(lines[i]))
                    {
                        if (!counters.Contains(j))
                        counters.Add(j);
                    }
                }
            }
            var newSets = new List<List<int>>();
                for (int i = 0; i < size; i++)
                {
                    if (!counters.Contains(i))
                        newSets.Add(sets[i]);
                }
            var finalString = new StringBuilder();

            //foreach (var set in newSets)
            //{

            //    for (int i = 0; i < set.Count; i++)
            //    {
            //        Console.Write(set[i] + " ");
            //    }
            //    Console.WriteLine();
            //}
        }
    }
}

