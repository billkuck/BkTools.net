namespace BkTools.Tools.GitTool
{
    public class GitDifference
    {
        public string Path { get; set; } = string.Empty;
        public string OldModulePath { get; set; } = string.Empty;
        public string ChangeType { get; set; } = string.Empty;

        public class Set : GenericSet<GitDifference> { }
    }
}
