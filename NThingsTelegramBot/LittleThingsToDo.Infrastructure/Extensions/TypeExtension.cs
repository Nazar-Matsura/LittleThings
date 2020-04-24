using System;

namespace LittleThingsToDo.Infrastructure.Extensions
{
    public static class TypeExtension
    {
        public static bool Implements<TInterface>(this Type type)
        {
            return type.Implements(typeof(TInterface));
        }

        public static bool Implements(this Type type, Type implements)
        {
            return implements.IsAssignableFrom(type);
        }
    }
}