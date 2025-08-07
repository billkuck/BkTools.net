using System.CommandLine;

namespace FCSAmerica.Shared.Toolkit.ApiVerification.General.CommandLineUiFromInterface
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
