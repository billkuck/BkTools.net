using System.Collections;

namespace BkTools.Tools.GitTool
{
    public class GitBranch
    {
        public string ProjectName { get; set; } = string.Empty;
        public string RepoName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string CanonicalName { get; set; } = string.Empty;
        public GitCommit.Set Commits { get; set; } = new GitCommit.Set();

        public class Set : GenericSet<GitBranch>
        {
        }
    }
}
