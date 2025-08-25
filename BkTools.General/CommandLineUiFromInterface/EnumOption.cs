using System.CommandLine;
using System.Reflection;

namespace BkTools.General.CommandLineUiFromInterface
{
    public class EnumOption : Option<string>
    {
        private EnumOption(string name, params string[] aliases) : base(name, aliases)
        {
        }

        public EnumOption(ParameterInfo parameter) : this(GetOptionName(parameter.Name!), GetAliases(parameter.Name))
        {
        }

        private static string GetOptionName(string parameterName) 
            => $"--{parameterName}";

        private static string[] GetAliases(string? name)
        {
            return [ name!, $"--{name}" ];
        }

    }
}
