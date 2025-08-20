using BkTools.Tools.CodeCoverageTool.CoberteraXml;
using BkTools.Tools.CodeCoverageTool.TestCoverageXml;

namespace BkTools.Tools.CodeCoverageTool
{
    public class Tool
    {
        public static void Go()
        {
            TestCoverage1();
//            TestCoverage2();
        }

        private static void TestCoverage1()
        {
            var coberteraFilePath = @"C:\DEV\ADT\Wsp\AVT-Alpha\Sandbox\SE1.FcsaApiTestTool\Tests\IntegrationTesting\ApiVerification.Tests\TestResults\Coverage\coverage.cobertura.xml";
            var coverage = new CoberteraXml.CoberteraXmlFile(coberteraFilePath);
            var outputFileName = BkTools.Infrastructure.BkToolsConfig.GetReportFileName("CodeCoverage-avt.txt");
            CoberteraReport.Report(coverage, outputFileName);
        }

        public static string CrapReport(string coberteraFilePath, string outputFileName) 
            => CoberteraReport.Report(new CoberteraXml.CoberteraXmlFile(coberteraFilePath), outputFileName);

        private static void TestCoverage2()
        {
            var filePath = @"C:\DEV\CART-API-2024-06-27-001.coveragexml";
            var coverageFile = new TestCoverageXmlFile(filePath);
            TestCoverageReport.Report(coverageFile.Coverage, @"C:\DEV\CART-Coverage.txt");
        }
    }
}
