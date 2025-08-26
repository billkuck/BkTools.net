using BkTools.General.CommandLineUiFromInterface;

namespace BkTools.CLI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new CommandLineFromInterface<IBkTool>(new BkTool(), "BkTool").Execute(args);
        }
    }
}
