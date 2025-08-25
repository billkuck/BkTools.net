namespace BkTools.Tools.PathUtility
{
    public class PathUtilities
    {
        public static void AddPathVariable(string newPath, EnvironmentVariableTarget target = EnvironmentVariableTarget.Process)
        {
            if (string.IsNullOrWhiteSpace(newPath))
            {
                throw new ArgumentException("Path cannot be empty.", nameof(newPath));
            }
            var currentPaths = GetPathVariables(target).ToList();
            if (!currentPaths.Contains(newPath, StringComparer.OrdinalIgnoreCase))
            {
                currentPaths.Add(newPath);
                Environment.SetEnvironmentVariable("PATH", string.Join(Path.PathSeparator.ToString(), currentPaths), target);
            }
        }

        public static IEnumerable<string> GetPathVariables(EnvironmentVariableTarget target = EnvironmentVariableTarget.Process)
        {
            var pathVariables = Environment.GetEnvironmentVariable("PATH", target)?.Split(Path.PathSeparator) ?? Array.Empty<string>();
            return pathVariables.Select(p => p.Trim()).Where(p => !string.IsNullOrEmpty(p));
        }
    }
}
