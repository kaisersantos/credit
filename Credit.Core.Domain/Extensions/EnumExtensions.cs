using Credit.Core.Domain.Exceptions.EnumExtensions;

namespace Credit.Core.Domain.Extensions
{
    public static class EnumExtensions
    {
        public static T ToEnum<T>(this string value) where T : struct, Enum
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length != 1)
                throw new EnumExtensionsCoreDomainException(EnumExtensionsError.NullOrEmpty<T>(value));

            return ConvertToEnum<T>(value[0]);
        }

        public static T ToEnum<T>(this char value) where T : struct, Enum
        {
            if (value == '\0')
                throw new EnumExtensionsCoreDomainException(EnumExtensionsError.EmptyValue<T>(value));

            return ConvertToEnum<T>(value);
        }

        public static T ToEnumIgnoreCase<T>(this string value) where T : struct, Enum
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length != 1)
                throw new EnumExtensionsCoreDomainException(EnumExtensionsError.NullOrEmpty<T>(value));

            return ConvertToEnumIgnoreCase<T>(value[0]);
        }

        public static T ToEnumIgnoreCase<T>(this char value) where T : struct, Enum
        {
            if (value == '\0')
                throw new EnumExtensionsCoreDomainException(EnumExtensionsError.EmptyValue<T>(value));

            return ConvertToEnumIgnoreCase<T>(value);
        }

        private static T ConvertToEnum<T>(char value) where T : struct, Enum
        {
            string? name = Enum.GetName(typeof(T), value.ToString()[0]);

            bool isEnum = Enum.TryParse(name, false, out T result);

            if (!isEnum)
                throw new EnumExtensionsCoreDomainException(EnumExtensionsError.InvalidValue<T>(value));

            return result;
        }

        private static T ConvertToEnumIgnoreCase<T>(char value) where T : struct, Enum
        {
            string? nameUpper = Enum.GetName(typeof(T), value.ToString().ToUpper()[0]);
            string? nameLower = Enum.GetName(typeof(T), value.ToString().ToLower()[0]);

            bool isEnumUpper = Enum.TryParse(nameUpper, false, out T resultUpper);
            bool isEnumLower = Enum.TryParse(nameLower, false, out T resultLower);

            if (!isEnumLower && !isEnumUpper)
                throw new EnumExtensionsCoreDomainException(EnumExtensionsError.InvalidValue<T>(value));

            if (isEnumLower && isEnumUpper)
                throw new EnumExtensionsCoreDomainException(EnumExtensionsError.AmbiguousValue<T>(value));

            return isEnumUpper ? resultUpper : resultLower;
        }
    }
}
