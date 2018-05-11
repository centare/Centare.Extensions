// Copyright (c) 2018 Centare

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Centare.Extensions
{
    public static class EnumExtensions
    {
        public static bool HasAttribute<TEnum, TAttr>(this TEnum enumValue) where TAttr : Attribute
            => typeof(TEnum).GetMember(enumValue.ToString())
                            .SelectMany(m => m.GetCustomAttributes(typeof(TAttr), false))
                            .Any();

        public static IDictionary<TEnum, TAttr> EnumAttributeValues<TEnum, TAttr>() where TAttr : Attribute
            => Enum.GetValues(typeof(TEnum))
                   .Cast<TEnum>()
                   .ToDictionary(e => e, e => e.HasAttribute<TEnum, TAttr>() ? e.GetAttributes<TEnum, TAttr>().First() : null);

        public static IList<TEnum> EnumValuesWithAttribute<TEnum, TAttr>() where TAttr : Attribute
            => Enum.GetValues(typeof(TEnum))
                   .Cast<TEnum>()
                   .Where(e => e.HasAttribute<TEnum, TAttr>())
                   .ToList();

        public static IList<TEnum> EnumValuesWithAttributeValue<TEnum, TAttr>(Func<IEnumerable<TAttr>, bool> predicate) where TAttr : Attribute
            => Enum.GetValues(typeof(TEnum))
                   .Cast<TEnum>()
                   .Where(e => e.HasAttribute<TEnum, TAttr>() && predicate(e.GetAttributes<TEnum, TAttr>()))
                   .ToList();

        public static IEnumerable<TAttr> GetAttributes<TEnum, TAttr>(this TEnum value) where TAttr : Attribute
        {
            try
            {
                return value.GetType()
                            .GetField(value.ToString())
                            .GetCustomAttributes<TAttr>(false);
            }
            catch
            {
                return default;
            }
        }
    }
}
