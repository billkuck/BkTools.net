using BkTools.Tools.PathUtility;

namespace BkTools.CLI
{
    public class BkTool : IBkTool
    {
        public void Crap(string coberteraFile, string outputFile)
        {
            Tools.CodeCoverage.Tool.CrapReport(coberteraFile, outputFile);
        }

        public void Path(EnvironmentVariableTarget target = EnvironmentVariableTarget.Process)
        {
            PathUtilities.GetPathVariables(target)
                .ToList()
                .ForEach(Console.WriteLine);    
        }

        public void PathAdd(string newPath, EnvironmentVariableTarget target = EnvironmentVariableTarget.Process)
        {
            if (string.IsNullOrWhiteSpace(newPath))
            {
                Console.WriteLine("Path cannot be empty.");
                return;
            }
            try
            {
                PathUtilities.AddPathVariable(newPath, target);
                Console.WriteLine($"Added path: {newPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding path: {ex.Message}");
            }
        }
    }
}
