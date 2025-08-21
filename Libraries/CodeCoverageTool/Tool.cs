using BkTools.Tools.CodeCoverage.CoberteraXml;

namespace BkTools.Tools.CodeCoverage
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
    }
}
