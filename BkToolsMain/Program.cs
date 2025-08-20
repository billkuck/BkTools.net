using BkTools.General.CommandLineUiFromInterface;

namespace BkToolsMain
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new CommandLineFromInterface<IBkTool>(new BkTool(), "BkTool").Execute(args);
        }
    }
}
