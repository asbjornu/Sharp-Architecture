using System;
using System.Diagnostics;

namespace SharpArch.Core
{
    /// <summary>
    /// An assertion delegate, returning <c>true</c> if the assertion
    /// is met, or <c>false</c> to throw a <see cref="AssertionException"/>.
    /// </summary>
    public delegate bool Assertion<T>(T value);

    /// <summary>
    /// Exception raised when an assertion fails.
    /// </summary>
    public class AssertionException : DesignByContractException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssertionException"/> class.
        /// </summary>
        /// <param name="message">The message explaining why the assertion failed.</param>
        public AssertionException(string message) : base(message)
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="AssertionException"/> class.
        /// </summary>
        /// <param name="message">The message explaining why the assertion failed.</param>
        /// <param name="inner">The exception containing details on why the assertion failed.</param>
        public AssertionException(string message, Exception inner) : base(message, inner)
        {
        }
    }

    public static partial class Check
    {
        /// <summary>
        /// Assertion check.
        /// </summary>
        /// <typeparam name="T">The <see cref="T:System.Type"/> of <paramref name="value"/>.</typeparam>
        /// <param name="value">The value to check.</param>
        /// <param name="assertion">The assertion that checks the <paramref name="value"/>. If it returns
        /// <c>false</c>, an <see cref="AssertionException"/> is thrown.</param>
        /// <remarks>
        /// An assertion instead of a <see cref="AssertionException"/> is invoked if
        /// <see cref="UseAssertions"/> is set to <c>true</c>.
        /// </remarks>
        public static void Assert<T>(T value, Assertion<T> assertion)
        {
            bool assertionValue = assertion.Invoke(value);
            // TODO: Convert the predicate to a System.Linq.Expression and dig into it to find out what it's doing to give a meaningful error message, like the ReflectionHelper in Fluent NHibernate does.
            Assert(assertionValue);
        }


        /// <summary>
        /// Assertion check.
        /// </summary>
        /// <typeparam name="T">The <see cref="T:System.Type"/> of <paramref name="value"/>.</typeparam>
        /// <param name="value">The value to check.</param>
        /// <param name="assertion">The assertion that checks the <paramref name="value"/>. If it returns
        /// <c>false</c>, an <see cref="AssertionException"/> is thrown.</param>
        /// <param name="message">The message explaining why the assertion failed.</param>
        /// <remarks>
        /// An assertion instead of a <see cref="AssertionException"/> is invoked if
        /// <see cref="UseAssertions"/> is set to <c>true</c>.
        /// </remarks>
        public static void Assert<T>(T value, Assertion<T> assertion, string message)
        {
            bool assertionValue = assertion.Invoke(value);
            // TODO: Convert the predicate to a System.Linq.Expression and dig into it to find out what it's doing to give a meaningful error message, like the ReflectionHelper in Fluent NHibernate does.
            Assert(assertionValue, message);
        }


        /// <summary>
        /// Assertion check.
        /// </summary>
        /// <typeparam name="T">The <see cref="T:System.Type"/> of <paramref name="value"/>.</typeparam>
        /// <param name="value">The value to check.</param>
        /// <param name="assertion">The assertion that checks the <paramref name="value"/>. If it returns
        /// <c>false</c>, an <see cref="AssertionException"/> is thrown.</param>
        /// <param name="message">The message explaining why the assertion failed.</param>
        /// <param name="inner">The exception containing details on why the assertion failed.</param>
        /// <remarks>
        /// An assertion instead of a <see cref="AssertionException"/> is invoked if
        /// <see cref="UseAssertions"/> is set to <c>true</c>.
        /// </remarks>
        public static void Assert<T>(T value, Assertion<T> assertion, string message, Exception inner)
        {
            bool assertionValue = assertion.Invoke(value);
            // TODO: Convert the predicate to a System.Linq.Expression and dig into it to find out what it's doing to give a meaningful error message, like the ReflectionHelper in Fluent NHibernate does.
            Assert(assertionValue, message, inner);
        }


        /// <summary>
        /// Assertion check.
        /// </summary>
        /// <param name="assertion">If set to <c>false</c>, throws an <see cref="AssertionException"/>
        /// with the given <paramref name="message"/>.</param>
        /// <param name="message">The message explaining why the assertion failed.</param>
        /// <remarks>
        /// An assertion instead of a <see cref="AssertionException"/> is invoked if
        /// <see cref="UseAssertions"/> is set to <c>true</c>.
        /// </remarks>
        public static void Assert(bool assertion, string message)
        {
            if (UseExceptions)
            {
                if (!assertion)
                    throw new AssertionException(message);
            }
            else
            {
                Trace.Assert(assertion, "Assertion: " + message);
            }
        }


        /// <summary>
        /// Assertion check.
        /// </summary>
        /// <param name="assertion">If set to <c>false</c>, throws an <see cref="AssertionException"/>
        /// with the given <paramref name="message"/>.</param>
        /// <param name="message">The message explaining why the assertion failed.</param>
        /// <param name="inner">The exception containing details on why the assertion failed.</param>
        /// <remarks>
        /// An assertion instead of an <see cref="AssertionException"/> is invoked if
        /// <see cref="UseAssertions"/> is set to <c>true</c>.
        /// </remarks>
        public static void Assert(bool assertion, string message, Exception inner)
        {
            if (UseExceptions)
            {
                if (!assertion)
                    throw new AssertionException(message, inner);
            }
            else
            {
                Trace.Assert(assertion, "Assertion: " + message);
            }
        }


        /// <summary>
        /// Assertion check.
        /// </summary>
        /// <param name="assertion">If set to <c>false</c>, throws an <see cref="AssertionException"/>.</param>
        /// <remarks>
        /// An assertion instead of an <see cref="AssertionException"/> is invoked if
        /// <see cref="UseAssertions"/> is set to <c>true</c>.
        /// </remarks>
        public static void Assert(bool assertion)
        {
            if (UseExceptions)
            {
                if (!assertion)
                    throw new AssertionException("Assertion failed.");
            }
            else
            {
                Trace.Assert(assertion, "Assertion failed.");
            }
        }
    }
}