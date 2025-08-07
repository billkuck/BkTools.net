
namespace BkTools.Tools.GitTool
{
    public class GenericSet<T> : List<T>
    {
        public GenericSet()
        {
        }

        public GenericSet(IEnumerable<T>? items) : base(items ?? [])
        {
        }
    }
}