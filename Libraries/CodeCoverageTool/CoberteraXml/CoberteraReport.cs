
namespace BkTools.Tools.CodeCoverageTool.CoberteraXml
{
    public class CoberteraReport
    {
        public static string Report(CoberteraXmlFile coverageFile, string outputFileName)
        {
            using (StreamWriter output = new StreamWriter(outputFileName))
            {
                ReportHeadings(output);
                coverageFile?
                    .Coverage?
                    .Packages?
                    .Items?
                    .ForEach(package =>
                        package.Classes?.Items?.ForEach(classNode =>
                            classNode?.Methods?.Items?.ForEach(method =>
                                Report(output, coverageFile.Coverage, package, classNode, method))));
            }
            return outputFileName;
        }

        private static void ReportHeadings(StreamWriter output)
        {
            output.WriteLine(string.Join("\t",
                "Package",
                "Class",
                "Method",
                "Complexity",
                "LOC",
                "Lines Covered",
                "Lines Not Covered",
                "Coverage%",
                "Crap score",
                "ClassFile"));
        }

        private static void Report(
            StreamWriter output,
            CoberturaDefinitions.CoverageNode coverage,
            CoberturaDefinitions.PackageNode package,
            CoberturaDefinitions.ClassNode classNode,
            CoberturaDefinitions.MethodNode method)
        {
            var coveredLines = method.Lines?.Items?.Count(item => item.Hits > 0) ?? 0;
            var notCoveredLines = method.Lines?.Items?.Count(item => item.Hits == 0) ?? 0;
            var totalLines = coveredLines + notCoveredLines;
            var codeCoverage = totalLines == 0 
                ? 0
                : (double) coveredLines / totalLines;

            output.WriteLine(string.Join("\t",
                package.Name,
                classNode?.Name,
                method.Name,
                method.Complexity,
                totalLines,
                method.Lines?.Items?.Count(item => item.Hits > 0) ?? 0,
                method.Lines?.Items?.Count(item => item.Hits == 0) ?? 0,
                codeCoverage,
                GetCrapScore(methodComplexity: method.Complexity, methodLoc: totalLines, methodLocCovered: coveredLines),
                classNode?.FileName
                ));
        }

        private static double GetCrapScore(int methodComplexity, int methodLoc, int methodLocCovered)
        {
            var coverage = (double)methodLocCovered / methodLoc;
            var crap = Math.Pow(methodComplexity, 2) * Math.Pow(1 - coverage, 3) + methodComplexity;
            return crap;
        }
    }
}
