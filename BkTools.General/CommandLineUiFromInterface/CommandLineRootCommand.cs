using System.CommandLine;
using System.CommandLine.Parsing;

namespace BkTools.General.CommandLineUiFromInterface
{
    public class CommandLineRootCommand : Dictionary<string, CommandLineCommand>
    {
        public RootCommand RootCommand { get; private set; }

        public CommandLineRootCommand(RootCommand rootCommand, IEnumerable<CommandLineCommand>? subCommands = null)
        {
            RootCommand = rootCommand;
            subCommands?.ToList().ForEach(AddCommand);
        }

        public void Invoke(string[] args)
        {
            ParseResult parseResult = RootCommand.Parse(args);
            parseResult?.Invoke();
        }

        public void AddCommand(CommandLineCommand command)
        {
            this.Add(command.Command.Name, command);
            RootCommand.Add(command.Command);
        }
    }
}
