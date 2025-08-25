namespace BkTools.CLI
{
    public interface IBkTool
    {
        void Crap(string coberteraFile, string outputFile);

        void Path(EnvironmentVariableTarget target = EnvironmentVariableTarget.Process);

        void PathAdd(string newPath, EnvironmentVariableTarget target = EnvironmentVariableTarget.Process);
    }
}
