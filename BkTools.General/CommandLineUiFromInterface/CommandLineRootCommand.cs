using System.CommandLine;

namespace BkTools.General.CommandLineUiFromInterface
{
    public class CommandLineRootCommand : Dictionary<string, CommandLineCommand>
    {
        public RootCommand  RootCommand { get; private set; }

        public CommandLineRootCommand(RootCommand rootCommand, IEnumerable<CommandLineCommand>? subCommands = null)
        {
            RootCommand = rootCommand;
            subCommands?.ToList().ForEach(AddCommand);
        }

        public void AddCommand(CommandLineCommand command)
        {
            this.Add(command.Command.Name, command);
            RootCommand.Add(command.Command);
        }
    }
}
