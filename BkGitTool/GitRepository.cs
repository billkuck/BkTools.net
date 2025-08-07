namespace BkTools.Tools.GitTool
{
    public class GitRepository
    {
        public string LocalFilePath { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string ProjectCollectionName { get; set; } = string.Empty;
        public string ProjectName { get; set; } = string.Empty;
        public string ValidLocalPath { get; set; } = string.Empty;
        public string? CurrentBranchName { get; set; } = string.Empty;
        public GitBranch.Set Branches { get; set; } = new GitBranch.Set();
        public List<string> AllCommitIds { get; set; } = new List<string>();
        public Dictionary<string, GitCommit> AllCommits { get; private set; } = new Dictionary<string, GitCommit>();

        private LibGit2Sharp.Repository? _repository;
        public GitRepository(string projectGroupName, string projectName, string rootDirectory, string name)
        {
            ProjectCollectionName = projectGroupName;
            ProjectName = projectName;
            Name = name;
            LocalFilePath = rootDirectory;
            ValidLocalPath = LibGit2Sharp.Repository.Discover(LocalFilePath);
            _repository = string.IsNullOrEmpty(ValidLocalPath) ? null
                : new LibGit2Sharp.Repository(ValidLocalPath);
            CurrentBranchName = _repository?.Head.FriendlyName;
            if (string.IsNullOrEmpty(CurrentBranchName))
            { }
        }

        public void Populate() 
            => _repository?.Branches.ToList().ForEach(branch 
                => Branches.Add(GetGitBranch(branch)));

        private GitBranch GetGitBranch(LibGit2Sharp.Branch b) 
            => new GitBranch()
                {
                    ProjectName = ProjectName,
                    RepoName = this.Name,
                    Name = b.FriendlyName,
                    CanonicalName = b.CanonicalName,
                    Commits = GetCommits(b)
                };

        private GitCommit.Set GetCommits(LibGit2Sharp.Branch b) 
            => [.. b.Commits.Select(GetCommit)];

        private GitCommit GetCommit(LibGit2Sharp.Commit c)
        {
            var id = c.Id.Sha;
            var exists = AllCommits.ContainsKey(id);
            var result = exists ? AllCommits[id]
                : new GitCommit()
                {
                    Id = id,
                    Message = c.Message,
                    Author = c.Author.Name,
                    AuthorEmail = c.Author.Email,
                    Created = c.Author.When,
                    Differences = GetDifferences(c),
            };

            if (!exists)
            {
                AllCommitIds.Add(id);
                AllCommits.Add(id, result);
            }
            return result;
        }

        private GitDifference.Set GetDifferences(LibGit2Sharp.Commit c)
        {
            var result = new GitDifference.Set();
            if (c.Parents?.FirstOrDefault() != null)
            {
                var diffs = _repository?.Diff.Compare<LibGit2Sharp.TreeChanges>(c.Tree, c.Parents?.FirstOrDefault()?.Tree);
                diffs?.ToList().ForEach(diff => result.Add(GetDifference(c, diff)));
            }
            return result;
        }

        private GitDifference GetDifference(LibGit2Sharp.Commit c, LibGit2Sharp.TreeEntryChanges diff)
        {
            return new GitDifference()
            {
                Path = diff.Path,
                OldModulePath = diff.Path != diff.OldPath ? diff.OldPath : string.Empty,
                ChangeType = diff.Status.ToString()
            };
        }

        private GitObject.Set Traverse(LibGit2Sharp.Tree? tree)
        {
            var result = new GitObject.Set();
            tree?.ToList().ForEach(
                obj =>
                {
                    result.Add(CreateObject(obj));
                    if (obj.TargetType == LibGit2Sharp.TreeEntryTargetType.Tree)
                    {
                        result.AddRange(Traverse(obj.Target as LibGit2Sharp.Tree));
                    }
                });
            return result;
        }

        private GitObject CreateObject(LibGit2Sharp.TreeEntry obj)
        {
            return new GitObject
            {
                Name = obj.Name,
                Mode = obj.Mode.ToString(),
                Path = obj.Path
            };
        }
    }
}