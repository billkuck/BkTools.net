namespace BkTools.Tools.GitTool
{
    public class GitCommit
    {
        public string Id { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty; 
        public string Author { get; set; } = string.Empty;
        public DateTimeOffset Created { get; set; }
        public string AuthorEmail { get; set; } = string.Empty;

//        public GitObject.Set Objects { get; set; } = new GitObject.Set();
        public GitDifference.Set Differences { get; set; } = new GitDifference.Set();

        public class Set : GenericSet<GitCommit>
        {
            public Set(IEnumerable<GitCommit>? items = null) : base(items)
            {
            }
        }
    }
}
