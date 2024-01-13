namespace AdventureWorks.Common
{
    public static class TypeExtensions
    {
        public static bool IsIntegralValueType(this object value)
        {
            switch (value)
            {
                case sbyte:
                case byte:
                case short:
                case ushort:
                case int:
                case uint:
                case long:
                case ulong:
                case nint:
                case nuint:
                    return true;
                default:
                    return false;
            }
        }
    }
}