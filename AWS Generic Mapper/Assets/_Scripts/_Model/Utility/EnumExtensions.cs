using System;

static class EnumExtensions
{
    public static T GetEnum<T>(this string itemName)
    {
        return (T) GetEnum(itemName, typeof(T));
    }

    public static object GetEnum(this string itemName, Type type)
    {
        return Enum.Parse(type, itemName, true);
    }
}
