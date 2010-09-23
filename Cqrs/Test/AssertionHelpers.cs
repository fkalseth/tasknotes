using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Cqrs.Test
{
    public static class AssertionHelpers
    {

        /// <summary>
        /// Verifies that the collection contains a given object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="expected">The object expected to be in the collection</param>
        public static void ShouldContain<T>(this IEnumerable<T> collection, T expected)
        {
            Assert.True(collection.Contains(expected), 
                "The expected object was not found in the collection");
        }
        
        /// <summary>
        /// Verifies that the collection does not contain a given object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="expected"></param>
        public static void ShouldNotContain<T>(this IEnumerable<T> collection, T expected)
        {
            Assert.False(collection.Contains(expected), 
                "The expected object was unexpectedly found in the collection");
        }
        
        /// <summary>
        /// Verifies that the collection is empty
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        public static void ShouldBeEmpty<T>(this IEnumerable<T> collection)
        {
            Assert.True(collection.Count() == 0, "Collection was not empty");
        }

        /// <summary>
        /// Verifies that the objects is equal to the given object, using a default comparer
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="actual"></param>
        /// <param name="expected"></param>
        /// <param name="message"></param>
        public static void ShouldBeEqualTo<T>(this T actual, T expected, string message = null)
        {
            if(String.IsNullOrEmpty(message)) Assert.AreEqual(actual, expected);
            else Assert.AreEqual(actual, expected, message);
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
            Assert.IsNotInstanceOf(expectedType, @object);
        }

        /// <summary>
        /// Verifies that the object is of the given type
        /// </summary>
        /// <param name="object"></param>
        /// <param name="expectedType">The type the object should be</param>
        public static void ShouldBeOfType(this object @object, Type expectedType)
        {
            Assert.IsInstanceOf(expectedType, @object);
        }

        public static void ShouldBeOfType<T>(this object @object)
        {
            Assert.IsInstanceOf(typeof (T), @object);
        }

        /// <summary>
        /// Verifies that the collection is not empty
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        public static void ShouldNotBeEmpty<T>(this IEnumerable<T> collection)
        {
            Assert.IsNotEmpty(collection.ToList());
        }

        /// <summary>
        /// Verifies that the object is not equal to a given object, using a default comparer
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="actual"></param>
        /// <param name="expected"></param>
        public static void ShouldNotBeEqualTo<T>(this T actual, T expected)
        {
            Assert.AreNotEqual(expected, actual);
        }
        
        /// <summary>
        /// Verifies that the object is not null
        /// </summary>
        /// <param name="object"></param>
        public static void ShouldNotBeNull(this object @object, string message = null)
        {
            if(String.IsNullOrEmpty(message)) Assert.NotNull(@object);
            Assert.NotNull(@object, message);
        }

        /// <summary>
        /// Verifies that the object is not the same as the given object
        /// </summary>
        /// <param name="actual"></param>
        /// <param name="expected"></param>
        public static void ShouldNotBeSameAs(this object actual, object expected)
        {
            Assert.AreNotSame(expected, actual);
        }

        /// <summary>
        /// Verifies that the object is null
        /// </summary>
        /// <param name="object"></param>
        public static void ShouldBeNull(this object @object, string message = null)
        {
            if(String.IsNullOrEmpty(message)) Assert.Null(@object);
            else Assert.Null(@object, message);
        }
        
        /// <summary>
        /// Verifies that the object is the same as the given object
        /// </summary>
        /// <param name="actual"></param>
        /// <param name="expected"></param>
        public static void ShouldBeSameAs(this object actual, object expected, string message = null)
        {
            if(String.IsNullOrEmpty(message)) Assert.AreSame(expected, actual);
            else Assert.AreSame(expected, actual, message);
        }

        /// <summary>
        /// Verifies that the condition is true
        /// </summary>
        /// <param name="condition"></param>
        public static void ShouldBeTrue(this bool condition, string message = null)
        {
            if(String.IsNullOrEmpty(message)) Assert.True(condition);
            else Assert.True(condition, message);
        }

        /// <summary>
        /// Verifies that the delegate throws the given exception
        /// </summary>
        /// <typeparam name="EXCEPTION"></typeparam>
        /// <param name="action"></param>
        public static void ShouldThrow<EXCEPTION>(this Action action) 
            where EXCEPTION : Exception
        {
            Assert.Throws<EXCEPTION>(new TestDelegate(action));
        }

        /// <summary>
        /// Verifies that the delegate does not throw any exceptions
        /// </summary>
        /// <param name="action"></param>
        public static void ShouldNotThrow(this Action action)
        {
            Assert.DoesNotThrow(new TestDelegate(action));
        }
    }

}