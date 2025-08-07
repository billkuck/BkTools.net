using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;
using System.Reflection;

namespace FCSAmerica.Shared.Toolkit.ApiVerification.General.CommandLineUiFromInterface
{
    public class CommandLineFromInterface<INTERFACE_TYPE>
    {
        private readonly INTERFACE_TYPE _implementation;
        private readonly CommandLineRootCommand _rootCommand;

        public CommandLineFromInterface(INTERFACE_TYPE implementation)
        {
            _implementation = implementation;
            _rootCommand = GetRootCommand();
        }

        public void Execute(string[] args) 
            => _rootCommand.RootCommand.Invoke(args);

        private CommandLineRootCommand GetRootCommand() 
            => new CommandLineRootCommand(
                rootCommand: new RootCommand(description: $"RootCommand for type: {typeof(INTERFACE_TYPE).Name}"),
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
                options: method.GetParameters().Select(parameter => GetOption(parameter)));

            methodCommand.Command.SetHandler(InvokeMethod(method));
            return methodCommand;
        }

        private Action<InvocationContext> InvokeMethod(MethodInfo method) 
            => (context) => InvokeMethodWithContext(context, method);

        private void InvokeMethodWithContext(InvocationContext context, MethodInfo method) 
            => method.Invoke(_implementation, GetArguments(context, method));

        private object?[]? GetArguments(InvocationContext context, MethodInfo method) 
            => [.. method.GetParameters()
                .Select(parameter => GetArgumentValue(method, parameter, context))];

        private object? GetArgumentValue(MethodInfo method, ParameterInfo parameter, InvocationContext context)
            => context
                .ParseResult
                .FindResultFor(
                    _rootCommand[method.Name][parameter.Name!].Option
                )?.GetValueOrDefault();

        private static readonly Dictionary<string, Func<ParameterInfo, Option>> GetValueByDataType 
            = new Dictionary<string, Func<ParameterInfo, Option>>()
        {
            { "Int", p => GetOption(p, (arg) => int.Parse(arg)) },
            { "String", p => GetOption(p, (arg) => arg) },
            { "Boolean", p => GetOption(p, (arg) => bool.Parse(arg)) }
        };

        private CommandLineOption GetOption(ParameterInfo parameter, bool isRequired = true) 
            => GetValueByDataType.TryGetValue(parameter.ParameterType.Name, out var conversionMethod)
                ? new CommandLineOption(conversionMethod.Invoke(parameter))
                : throw new Exception($"Conversion from {parameter.ParameterType.Name} is not implemented");

        private static Option GetOption<T>(ParameterInfo parameter, Func<string, T> fromString, bool isRequired = true)
        {
            const string defaultParameterName = "DEFAULT";
            var optionName = $"--{parameter.Name ?? defaultParameterName}";

            var resultOption = new Option<T>(
                aliases: [optionName, parameter.Name ?? defaultParameterName],
                description: string.Empty,
                parseArgument: arg => InvokeArgumentParser(arg, optionName, fromString)!);

            resultOption.IsRequired = isRequired;
            resultOption.ArgumentHelpName = optionName;
            
            return resultOption;
        }

        private static T? InvokeArgumentParser<T>(ArgumentResult argumentResult, string optionName, Func<string, T> fromString)
        {
            try
            {
                var stringValue = argumentResult.Tokens.FirstOrDefault()?.Value ?? string.Empty;
                return fromString.Invoke(stringValue);
            }
            catch (Exception ex)
            {
                argumentResult.ErrorMessage = ex.Message;
                return default;
            }
        }
    }
}
