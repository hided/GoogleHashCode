using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCode._2022
{
    public static class Problem
    {
        public static StringBuilder Solve(string[] lines)
        {
            var builder = new StringBuilder();

            // parse input
            var header = lines[0].Split(' '); // first line

            // parse line of int's separated by space
            lines[1].Split(' ').Select(x => int.Parse(x)).ToArray();

            // ...



            return builder;
        }
    }
}
