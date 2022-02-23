using System.IO;
using System.IO.Compression;
using HashCode._2022;
using NUnit.Framework;

namespace HashCode.Tests
{
    [TestFixture]
    public class Tests
    {
        public static readonly string INPUT_DIR = ".input";
        public static readonly string OUTPUT_DIR = ".output";

        [TestCase("1.in", "1.out")]
        [TestCase("2.in", "2.out")]
        [TestCase("3.in", "3.out")]
        [TestCase("4.in", "4.out")]
        [TestCase("5.in", "5.out")]
        public void RunFile(string inputFile, string outputFile)
        {
            Directory.CreateDirectory(INPUT_DIR);
            Directory.CreateDirectory(OUTPUT_DIR);
            string inputPath = Path.Combine(INPUT_DIR, inputFile);
            string outputPath = Path.Combine(OUTPUT_DIR, outputFile);
            var lines = File.ReadAllLines(inputPath);
            var output = Problem.Solve(lines);
            File.WriteAllText(outputPath, output.ToString());
        }

        [Test, Explicit]
        public void ZipSource()
        {
            string sourceDir = @"..\..\..\..\HashCode.2022";
            string outputFile = Path.Combine(OUTPUT_DIR, "source.zip");
            ZipFile.CreateFromDirectory(sourceDir, outputFile);
        }
    }
}