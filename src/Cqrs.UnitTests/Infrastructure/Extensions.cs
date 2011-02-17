using System;
using System.Collections.Generic;
using Xunit;

namespace Cqrs.UnitTests
{
	public static class AssertionHelpers
	{
		/// <summary>
		/// Verifies that the string contains a given substring, using the current culture
		/// </summary>
		/// <param name="actualString"></param>
		/// <param name="expectedSubstring">The sub-string expected to be in the string</param>
		public static void ShouldContain(this string actualString, string expectedSubstring)
		{
			Assert.Contains(expectedSubstring, actualString);
		}

		/// <summary>
		/// Verifies that the string contains a given substring, using the given comparison type
		/// </summary>
		/// <param name="actualString"></param>
		/// <param name="expectedSubstring">The sub-string expected to be in the string</param>
		/// <param name="comparisonType">The type of string comparison to perform</param>
		public static void ShouldContain(this string actualString, string expectedSubstring, StringComparison comparisonType)
		{
			Assert.Contains(expectedSubstring, actualString, comparisonType);
		}

		/// <summary>
		/// Verifies that the collection contains a given object
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="collection"></param>
		/// <param name="expected">The object expected to be in the collection</param>
		public static void ShouldContain<T>(this IEnumerable<T> collection, T expected)
		{
			Assert.Contains(expected, collection);
		}

		/// <summary>
		/// Verifies that the collection contains a given object, using the given comparer
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="collection"></param>
		/// <param name="expected">The object expected to be in the collection</param>
		/// <param name="comparer">The comparer to use</param>
		public static void ShouldContain<T>(this IEnumerable<T> collection, T expected, IEqualityComparer<T> comparer)
		{
			Assert.Contains(expected, collection, comparer);
		}

		/// <summary>
		/// Verifies that the string does not contain a given substring, using the current culture
		/// </summary>
		/// <param name="actualString"></param>
		/// <param name="expectedSubstring"></param>
		public static void ShouldNotContain(this string actualString, string expectedSubstring)
		{
			Assert.DoesNotContain(expectedSubstring, actualString);
		}

		/// <summary>
		/// Verifies that the string does not contain a given substring, using the given comparison type
		/// </summary>
		/// <param name="actualString"></param>
		/// <param name="expectedSubstring"></param>
		/// <param name="comparisonType"></param>
		public static void ShouldNotContain(this string actualString, string expectedSubstring, StringComparison comparisonType)
		{
			Assert.DoesNotContain(expectedSubstring, actualString, comparisonType);
		}

		/// <summary>
		/// Verifies that the collection does not contain a given object
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="collection"></param>
		/// <param name="expected"></param>
		public static void ShouldNotContain<T>(this IEnumerable<T> collection, T expected)
		{
			Assert.DoesNotContain(expected, collection);
		}

		/// <summary>
		/// Verifies that the collection does not contain a given object, using the given comparer
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="collection"></param>
		/// <param name="expected"></param>
		/// <param name="comparer"></param>
		public static void ShouldNotContain<T>(this IEnumerable<T> collection, T expected, IEqualityComparer<T> comparer)
		{
			Assert.DoesNotContain(expected, collection, comparer);
		}

		/// <summary>
		/// Verifies that the collection is empty
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="collection"></param>
		public static void ShouldBeEmpty<T>(this IEnumerable<T> collection)
		{
			Assert.Empty(collection);
		}

		/// <summary>
		/// Verifies that the objects is equal to the given object, using a default comparer
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="actual"></param>
		/// <param name="expected"></param>
		public static void ShouldBeEqualTo<T>(this T actual, T expected)
		{
			Assert.Equal(actual, expected);
		}

		/// <summary>
		/// Verifies that the object is equal to the given object, using a custom comparer
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="actual"></param>
		/// <param name="expected"></param>
		/// <param name="comparer"></param>
		public static void ShouldBeEqualTo<T>(this T actual, T expected, IEqualityComparer<T> comparer)
		{
			Assert.Equal(actual, expected, comparer);
		}

		/// <summary>
		/// Verifies that the condition is false
		/// </summary>
		/// <param name="condition"></param>
		public static void ShouldBeFalse(this bool condition)
		{
			Assert.False(condition);
		}

		/// <summary>
		/// Verifies that the object is in within a given range
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="actual"></param>
		/// <param name="low">The inclusive low value</param>
		/// <param name="high">The inclusive high value</param>
		public static void ShouldBeInRange<T>(this T actual, T low, T high) where T : IComparable
		{
			Assert.InRange(actual, low, high);
		}

		/// <summary>
		/// Verifies that the object is within a given range, using a custom comparer
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="actual"></param>
		/// <param name="low">The inclusive low value</param>
		/// <param name="high">The inclusive high value</param>
		/// <param name="comparer">The custom comparer to use</param>
		public static void ShouldBeInRange<T>(this T actual, T low, T high, IComparer<T> comparer)
		{
			Assert.InRange(actual, low, high, comparer);
		}

		/// <summary>
		/// Verifies that the object is assignable from the given type
		/// </summary>
		/// <param name="object"></param>
		/// <param name="expectedType">The type the object should be</param>
		public static void ShouldBeAssignableFromType(this object @object, Type expectedType)
		{
			Assert.IsAssignableFrom(expectedType, @object);
		}

		/// <summary>
		/// Verifies that the object is not of the given type
		/// </summary>
		/// <param name="object"></param>
		/// <param name="expectedType">The type the object should not be</param>
		public static void ShouldNotBeOfType(this object @object, Type expectedType)
		{
			Assert.IsNotType(expectedType, @object);
		}

		/// <summary>
		/// Verifies that the object is of the given type
		/// </summary>
		/// <param name="object"></param>
		/// <param name="expectedType">The type the object should be</param>
		public static void ShouldBeOfType(this object @object, Type expectedType)
		{
			Assert.IsType(expectedType, @object);
		}

		/// <summary>
		/// Verifies that the collection is not empty
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="collection"></param>
		public static void ShouldNotBeEmpty<T>(this IEnumerable<T> collection)
		{
			Assert.NotEmpty(collection);
		}

		/// <summary>
		/// Verifies that the object is not equal to a given object, using a default comparer
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="actual"></param>
		/// <param name="expected"></param>
		public static void ShouldNotBeEqualTo<T>(this T actual, T expected)
		{
			Assert.NotEqual(expected, actual);
		}

		/// <summary>
		/// Verifies that the object is not equal to a given object, using a custom comparer
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="actual"></param>
		/// <param name="expected"></param>
		/// <param name="comparer"></param>
		public static void ShouldNotBeEqualTo<T>(this T actual, T expected, IEqualityComparer<T> comparer)
		{
			Assert.NotEqual(expected, actual, comparer);
		}

		/// <summary>
		/// Verifies that the object is not in the given range
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="actual"></param>
		/// <param name="low">The inclusive low value</param>
		/// <param name="high">The inclusive high value</param>
		public static void ShouldNotBeInRange<T>(this T actual, T low, T high) where T : IComparable
		{
			Assert.NotInRange(actual, low, high);
		}

		/// <summary>
		/// Verifies that the object is not in the given range, using a custom comparer
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="actual"></param>
		/// <param name="low">The inclusive low value</param>
		/// <param name="high">The inclusive high value</param>
		/// <param name="comparer">The custom comparer</param>
		public static void ShouldNotBeInRange<T>(this T actual, T low, T high, IComparer<T> comparer)
		{
			Assert.NotInRange(actual, low, high, comparer);
		}

		/// <summary>
		/// Verifies that the object is not null
		/// </summary>
		/// <param name="object"></param>
		public static void ShouldNotBeNull(object @object)
		{
			Assert.NotNull(@object);
		}

		/// <summary>
		/// Verifies that the object is not the same as the given object
		/// </summary>
		/// <param name="actual"></param>
		/// <param name="expected"></param>
		public static void ShouldNotBeSameAs(this object actual, object expected)
		{
			Assert.NotSame(expected, actual);
		}

		/// <summary>
		/// Verifies that the object is null
		/// </summary>
		/// <param name="object"></param>
		public static void ShouldBeNull(this object @object)
		{
			Assert.Null(@object);
		}

		/// <summary>
		/// Verifies that the object is the same as the given object
		/// </summary>
		/// <param name="actual"></param>
		/// <param name="expected"></param>
		public static void ShouldBeSameAs(this object actual, object expected)
		{
			Assert.Same(expected, actual);
		}

		/// <summary>
		/// Verifies that the condition is true
		/// </summary>
		/// <param name="condition"></param>
		public static void ShouldBeTrue(this bool condition)
		{
			Assert.True(condition);
		}

		/// <summary>
		/// Verifies that the delegate throws the given exception
		/// </summary>
		/// <typeparam name="EXCEPTION"></typeparam>
		/// <param name="action"></param>
		public static void ShouldThrow<EXCEPTION>(this Action action)
			where EXCEPTION : Exception
		{
			Assert.Throws<EXCEPTION>(new Assert.ThrowsDelegate(action));
		}

		/// <summary>
		/// Verifies that the delegate does not throw any exceptions
		/// </summary>
		/// <param name="action"></param>
		public static void ShouldNotThrow(this Action action)
		{
			Assert.DoesNotThrow(new Assert.ThrowsDelegate(action));
		}
	}

	/// <summary>
	/// Static gateway for building actions fluently
	/// </summary>
	public static class The
	{
		public static Action Action(Action action)
		{
			return action;
		}
	}
}
