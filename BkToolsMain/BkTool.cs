namespace BkToolsMain
{
    public class BkTool : IBkTool
    {
        public void Crap(string coberteraFile, string outputFile)
        {
            BkTools.Tools.CodeCoverage.Tool.CrapReport(coberteraFile, outputFile);
        }
    }
}
