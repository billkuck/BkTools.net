namespace BkToolsMain
{
    public class BkTool : IBkTool
    {
        public void Crap(string coberteraFile, string outputFile)
        {
            BkTools.Tools.CodeCoverageTool.Tool.CrapReport(coberteraFile, outputFile);
        }
    }
}
