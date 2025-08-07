using System.CommandLine;

namespace BkTools.General.CommandLineUiFromInterface
{
    public class CommandLineOption
    {
        public Option Option { get; private set; }

        public CommandLineOption(Option option)
        {
            Option = option;
        }
    }
}
