namespace General.CommandLineUiFromInterface
{
    public static class Conversions
    {
        private static Dictionary<string, Func<string, object?>> StringToObject { get; } = new()
        {
            { "string", s => s },
            { "int", s => int.TryParse(s, out int i) ? i : null },
            { "bool", s => bool.TryParse(s, out bool b) ? b : null },
            { "double", s => double.TryParse(s, out double d) ? d : null },
            { "float", s => float.TryParse(s, out float f) ? f : null },
            { "long", s => long.TryParse(s, out long l) ? l : null },
            { "short", s => short.TryParse(s, out short sh) ? sh : null },
            { "byte", s => byte.TryParse(s, out byte by) ? by : null },
            { "char", s => char.TryParse(s, out char c) ? c : null },
            { "decimal", s => decimal.TryParse(s, out decimal dc) ? dc : null },
            { "datetime", s => DateTime.TryParse(s, out DateTime dt) ? dt : null },
            { "guid", s => Guid.TryParse(s, out Guid g) ? g : null },
            { "timespan", s => TimeSpan.TryParse(s, out TimeSpan ts) ? ts : null },
            { "uint", s => uint.TryParse(s, out uint ui) ? ui : null }
        };

        static Conversions()
        {
            StringToObject.Add("EnvironmentVariableTarget", s => Enum.TryParse< EnvironmentVariableTarget>(s, out EnvironmentVariableTarget ui) ? ui : null);
        }
    }
}
