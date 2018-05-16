// Copyright (c) 2018 Centare

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Centare.Extensions
{
    public static class TypeExtensions
    {
        public static IEnumerable<Type> GetInstantiableImplementors(this Type type, params Assembly[] assemblies)
            => type.GetImplementors(assemblies).Where(IsInstantiable);

        public static IEnumerable<Type> GetImplementors(this Type type, params Assembly[] assemblies)
            => assemblies.SelectMany(assembly => assembly.GetTypes()).Where(type.IsAssignableFrom);

        public static bool IsInstantiable(this Type type)
            => !(type.IsAbstract || type.IsGenericTypeDefinition || type.IsInterface);

        public static Dictionary<string, TEnum> ToDictionary<TEnum>(this Type type)
            where TEnum : struct, Enum
        {
            if (!type.IsEnum) new Dictionary<string, TEnum>();

            return Enum.GetValues(type)
                       .Cast<TEnum>()
                       .ToDictionary(value => Enum.GetName(type, value), value => value);
        }
    }
}