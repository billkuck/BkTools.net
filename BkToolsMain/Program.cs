using BkTools.General.CommandLineUiFromInterface;

namespace BkTools.CLI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to BkTool CLI! Connect to debug and press enter to continue.");
            Console.ReadLine();
            new CommandLineFromInterface<IBkTool>(new BkTool(), "BkTool").Execute(args);
        }
    }
}
