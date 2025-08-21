
namespace BkTools.Tools.CodeCoverage.TestCoverageXml
{
    public class TestCoverageReport
    {
        public static void Report(CodeCoverage coverage, string outputFileName)
        {
            using StreamWriter output = new StreamWriter(outputFileName);
            output.WriteLine(string.Join("\t", "Type", "Name", "Covered", "Not Covered", "Part Covered", "Scope", "Module", "Namespace", "Class", "Method"));
            Report(coverage, 0, output);
        }

        private static void Report(CodeCoverage coverage, int level, StreamWriter output)
        {
            var outputs = new List<string>()
            {
                coverage.LayerDefinition.LayerName,
                coverage.InstanceName!,
                $"{coverage.LinesCovered}",
                $"{coverage.LinesNotCovered}",
                $"{coverage.LinesPartiallyCovered}"
            };
            outputs.AddRange(GetNames(coverage));
            output.WriteLine(string.Join("\t", outputs.ToArray()));

            coverage
                .Children
                .ToList()
                .ForEach(coverage 
                    => Report(coverage, level + 1, output));
        }

        private static List<string> GetNames(CodeCoverage coverage)
        {
            var result = new List<string>();
            if (coverage.Parent != null)
            {
                result.AddRange(GetNames(coverage.Parent));
            }
            result.Add(coverage?.InstanceName ?? string.Empty);
            return result;
        }
    }
}
