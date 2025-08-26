using System.CommandLine;
using System.ComponentModel;
using System.Reflection;

namespace BkTools.General.CommandLineUiFromInterface
{
    public class CommandLineFromInterface<INTERFACE_TYPE>
    {
        private readonly INTERFACE_TYPE _implementation;

        private readonly CommandLineRootCommand _rootCommand;
        private readonly string _description;

        public CommandLineFromInterface(INTERFACE_TYPE implementation, string description)
        {
            _implementation = implementation;
            _rootCommand = GetRootCommand();
            _description = description;
        }

        public void Execute(string[] args)
            => _rootCommand.Invoke(args);

        private CommandLineRootCommand GetRootCommand()
            => new CommandLineRootCommand(
                rootCommand: new RootCommand(description: _description),
                subCommands:
                    typeof(INTERFACE_TYPE)
                        .GetTypeInfo()
                        .GetMethods()
                        .Where(method => method.IsPublic)
                        .Select(GetMethodCommand));

        private CommandLineCommand GetMethodCommand(MethodInfo method)
        {
            var methodCommand = new CommandLineCommand(
                command: new Command(method.Name, $"Method {method.Name}"),
                options: method.GetParameters().Select(parameter => CreateOption(parameter)));

            methodCommand.Command.SetAction(InvokeMethod(method));
            return methodCommand;
        }
        private Option CreateOption(ParameterInfo parameter)
        {
            return new EnumOption(parameter);
        }

        private static string GetOptionName(ParameterInfo parameter)
        {
            var defaultParameterName = "DEFAULT";
            var optionName = $"--{parameter.Name ?? defaultParameterName}";
            return optionName;
        }

        private Action<ParseResult> InvokeMethod(MethodInfo method)
            => (context) => InvokeMethodWithContext(context, method);

        private void InvokeMethodWithContext(ParseResult context, MethodInfo method)
            => method.Invoke(_implementation, GetArguments(context, method));

        private object?[]? GetArguments(ParseResult context, MethodInfo method)
            => [.. method.GetParameters()
                .Select(parameter => GetArgumentValue(method, parameter, context))];

        private object? GetArgumentValue(MethodInfo method, ParameterInfo parameter, ParseResult context)
        {
            object? result = null;
            var parameterName = GetOptionName(parameter)!;
            switch (parameter.ParameterType.Name)
            {
                case "String":
                    result = context.GetValue<string>(parameterName);
                    break;
                case "Int":
                    result = context.GetValue<int>(parameterName);
                    break;
                case "Bool":
                    result = context.GetValue<bool>(parameterName);
                    break;
                default:
                    result = FromString(parameter.ParameterType, context.GetValue<string>(parameterName)!);
                    break;
            }
            return result;
        }

        public object? FromString(Type type, string value)
        {
            object? result = default;
            try
            {
                result = TypeDescriptor
                    .GetConverter(type)
                    .ConvertFromInvariantString(value)!;
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
    }
}
