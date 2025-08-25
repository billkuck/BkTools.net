using System.CommandLine;

namespace BkTools.General.CommandLineUiFromInterface
{
    public class CommandLineCommand 
    {
        public Command Command { get; private set; }

        public CommandLineCommand(Command command, IEnumerable<Option>? options = null)
        {
            Command = command;
            options?.ToList().ForEach(Command.Options.Add);
        }
    }
}
