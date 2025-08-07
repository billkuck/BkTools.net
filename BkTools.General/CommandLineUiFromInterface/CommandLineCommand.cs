using System.CommandLine;

namespace BkTools.General.CommandLineUiFromInterface
{
    public class CommandLineCommand : Dictionary<string, CommandLineOption>
    {
        public Command Command { get; private set; }

        public CommandLineCommand(Command command, IEnumerable<CommandLineOption>? options = null)
        {
            Command = command;
            options?.ToList().ForEach(AddOption);
        }

        public void AddOption(CommandLineOption option)
        {
            this.Add(option.Option.Name, option);
            Command.AddOption(option.Option);
        }
    }
}
