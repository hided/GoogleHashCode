using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaProblem
{
    class Program
    {
        static string inputFile = "2.in";
        static string outputFile = "2.out";

        static void Main(string[] args)
        {
            var lines = File.ReadAllLines(inputFile);
            var header = lines[0].Split(' ');
            int maximumSlices = int.Parse(header[0]);
            int typeCount = int.Parse(header[1]);
            var inputTypes = lines[1].Split(' ').Select(x => int.Parse(x)).ToArray();
            var outputTypes = new Stack<int>();
            outputTypes.Push(inputTypes.Length - 1);
            while (true)
            {
                int newRight = outputTypes.Pop();
                int target = maximumSlices - outputTypes.Sum(i => inputTypes[i]);
                var bestPair = FindClosestPair(inputTypes, 0, newRight, target);
                if (bestPair.sum < newRight)
                {
                    // done for now
                    outputTypes.Push(newRight);
                    break;
                }
                outputTypes.Push(bestPair.rightIndex);
                outputTypes.Push(bestPair.leftIndex);
                if (bestPair.sum == target)
                {
                    // found most optimal solution
                    Console.WriteLine("most optimal solution found!");
                    break;
                }
                if (bestPair.leftIndex == 0)
                {
                    // all combinations tried
                    break;
                }
            }
            Console.WriteLine("sum is " + outputTypes.Sum(i => inputTypes[i]));
            var builder = new StringBuilder();
            builder.AppendLine(outputTypes.Count.ToString());
            builder.AppendLine(string.Join(" ", outputTypes));
            File.WriteAllText(outputFile, builder.ToString());
            Console.WriteLine("done");
            Console.ReadKey();
        }

        static (int leftIndex, int rightIndex, int sum) FindClosestPair(int[] arr, int left, int right, int target)
        {
            int bestError = int.MaxValue;
            (int, int, int) winningPair = (0, 0, 0);
            while (left < right)
            {
                int sum = arr[left] + arr[right];
                int error = target - sum;
                if (error == 0)
                {
                    return (left, right, sum);
                }
                if (error > 0 && error <= bestError)
                {
                    bestError = error;
                    winningPair = (left, right, sum);
                }
                if (sum > target)
                {
                    right--;
                }
                else
                {
                    left++;
                }   
            }
            return winningPair;
        }
    }
}
