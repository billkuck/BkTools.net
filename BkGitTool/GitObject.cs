namespace BkTools.Tools.GitTool
{
    public class GitObject
    {
        public string Mode { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;

        public class Set : GenericSet<GitObject> { }
    }
}
