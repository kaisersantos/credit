namespace Credit.Core.Domain.Extensions
{
    public static class EnumExtensions
    {
        public static T ToEnum<T>(this string value) where T : struct, Enum
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length != 1)
                throw new ArgumentOutOfRangeException(nameof(value),
                    $"{value ?? "null/empty"} is invalid value to {typeof(T)} enum");

            return ConvertToEnum<T>(value[0]);
        }

        public static T ToEnum<T>(this char value) where T : struct, Enum
        {
            if (value == '\0')
                throw new ArgumentOutOfRangeException(nameof(value), $"empty is invalid value to {typeof(T)} enum");

            return ConvertToEnum<T>(value);
        }

        private static T ConvertToEnum<T>(char value) where T : struct, Enum
        {
            string? name = Enum.GetName(typeof(T), value.ToString().ToUpper()[0]);

            bool isEnum = Enum.TryParse(name, true, out T result);

            if (!isEnum)
                throw new ArgumentOutOfRangeException(nameof(value), $"{value} is invalid value to {typeof(T)} enum");

            return result;
        }
    }
}
