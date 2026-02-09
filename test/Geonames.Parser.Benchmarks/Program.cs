using BenchmarkDotNet.Running;

namespace Geonames.Parser.Benchmarks;

public class Program
{
    public static void Main(string[] args)
    {
        var _ = BenchmarkRunner.Run<ParserBenchmarks>();
    }
}
