// Copyright (c) 2018 Centare

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Xunit;

namespace Centare.Extensions.Tests
{
    [ExcludeFromCodeCoverage]
    public class EnumExtensionTests
    {
        public class MarkerOneAttribute : Attribute { }

        public class MarkerTwoAttribute : Attribute { }

        public class AttrWithValueAttribute : Attribute
        {
            public int Value { get; }

            public AttrWithValueAttribute(int value)
                => Value = value;
        }

        public enum Tester
        {
            [MarkerOne, AttrWithValue(13)] HasMarker1,
            [MarkerTwo] HasMarker2,
            [MarkerOne, MarkerTwo, AttrWithValue(12)] HasBothMarkers,
            HasNone
        }

        [
            Theory,
            InlineData(Tester.HasMarker1, true, false),
            InlineData(Tester.HasMarker2, false, true),
            InlineData(Tester.HasBothMarkers, true, true),
            InlineData(Tester.HasNone, false, false)
        ]
        public void AttributeTest(Tester enumValue, bool hasMarker1, bool hasMarker2)
        {
            Assert.Equal(enumValue.HasAttribute<Tester, MarkerOneAttribute>(), hasMarker1);
            if (hasMarker1)
            {
                AssertContains<MarkerOneAttribute>();
            }
            else
            {
                AssertDoesNotContain<MarkerOneAttribute>();
            }

            Assert.Equal(enumValue.HasAttribute<Tester, MarkerTwoAttribute>(), hasMarker2);
            if (hasMarker2)
            {
                AssertContains<MarkerTwoAttribute>();
            }
            else
            {
                AssertDoesNotContain<MarkerTwoAttribute>();
            }

            bool EnumValueMatches(Tester value) => value == enumValue;
            void AssertContains<TAttribute>() where TAttribute : Attribute
                => Assert.Contains(EnumExtensions.EnumValuesWithAttribute<Tester, TAttribute>(), EnumValueMatches);
            void AssertDoesNotContain<TAttribute>() where TAttribute : Attribute
                => Assert.DoesNotContain(EnumExtensions.EnumValuesWithAttribute<Tester, TAttribute>(), EnumValueMatches);
        }

        [
            Theory,
            InlineData(Tester.HasMarker1, 13),
            InlineData(Tester.HasMarker2, null),
            InlineData(Tester.HasBothMarkers, 12)
        ]
        public void AttributeValueTest(Tester enumValue, int? value) 
            => Assert.Equal(
                EnumExtensions.EnumAttributeValues<Tester, AttrWithValueAttribute>()[enumValue]?.Value, value);

        [Fact]
        public void EnumValuesWithAttributeValueTest()
        {
            var list =
                EnumExtensions.EnumValuesWithAttributeValue<Tester, AttrWithValueAttribute>(
                    values => values.Any(attr => attr.Value == 13));

            Assert.Single(list, li => li == Tester.HasMarker1);
        }
    }
}