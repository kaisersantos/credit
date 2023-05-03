namespace Credit.Core.Domain.Exceptions.EnumExtensions
{
    public class EnumExtensionsError : CoreError
    {
        public static EnumExtensionsError NullOrEmpty<T>(string value) where T : struct, Enum =>
            new(nameof(InvalidValue),
                $"{value ?? "null/empty"} is invalid value to {typeof(T)} enum");

        public static EnumExtensionsError EmptyValue<T>(char value) where T : struct, Enum =>
            new(nameof(InvalidValue),
                $"empty is invalid value to {typeof(T)} enum");

        public static EnumExtensionsError InvalidValue<T>(int value) where T : struct, Enum =>
            new(nameof(InvalidValue),
                $"{value} is invalid value to {typeof(T)} enum");

        public static EnumExtensionsError AmbiguousValue<T>(int value) where T : struct, Enum =>
            new(nameof(AmbiguousValue),
                $"{value} is declared both lower case and upper case in the {typeof(T)} enum");

        public EnumExtensionsError(string key, string message) : base(key, message) { }
    }
}
