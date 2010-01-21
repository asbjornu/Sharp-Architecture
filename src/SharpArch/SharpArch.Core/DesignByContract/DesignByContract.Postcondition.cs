using System;
using System.Diagnostics;

namespace SharpArch.Core
{
    /// <summary>
    /// A postcondition delegate, returning <c>true</c> if the postcondition
    /// is met, or <c>false</c> to throw a <see cref="PostconditionException"/>.
    /// </summary>
    public delegate bool Postcondition<T>(T value);

    /// <summary>
    /// Exception raised when a postcondition fails.
    /// </summary>
    public class PostconditionException : DesignByContractException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostconditionException"/> class.
        /// </summary>
        /// <param name="message">The message explainig why the postcondition failed.</param>
        public PostconditionException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PostconditionException"/> class.
        /// </summary>
        /// <param name="message">The message explainig why the postcondition failed.</param>
        /// <param name="inner">The exception containing details on why the postcondition failed.</param>
        public PostconditionException(string message, Exception inner) : base(message, inner) { }
    }

    public static partial class Check
    {
        /// <summary>
        /// Postcondition check.
        /// </summary>
        /// <typeparam name="T">The <see cref="T:System.Type"/> of <paramref name="value"/>.</typeparam>
        /// <param name="value">The value to check.</param>
        /// <param name="postcondition">The postcondition that checks the <paramref name="value"/>. If it returns
        /// <c>false</c>, a <see cref="PostconditionException"/> is thrown.</param>
        /// <remarks>
        /// An assertion instead of a <see cref="PostconditionException"/> is invoked if
        /// <see cref="UseAssertions"/> is set to <c>true</c>.
        /// </remarks>
        public static void Ensure<T>(T value, Postcondition<T> postcondition)
        {
            bool assertion = postcondition.Invoke(value);
            // TODO: Convert the predicate to a System.Linq.Expression and dig into it to find out what it's doing to give a meaningful error message, like the ReflectionHelper in Fluent NHibernate does.
            Ensure(assertion);
        }

        /// <summary>
        /// Postcondition check.
        /// </summary>
        /// <typeparam name="T">The <see cref="T:System.Type"/> of <paramref name="value"/>.</typeparam>
        /// <param name="value">The value to check.</param>
        /// <param name="postcondition">The postcondition that checks the <paramref name="value"/>. If it returns
        /// <c>false</c>, a <see cref="PostconditionException"/> is thrown.</param>
        /// <param name="message">The message explaining why the postcondition failed.</param>
        /// <remarks>
        /// An assertion instead of a <see cref="PostconditionException"/> is invoked if
        /// <see cref="UseAssertions"/> is set to <c>true</c>.
        /// </remarks>
        public static void Ensure<T>(T value, Postcondition<T> postcondition, string message)
        {
            bool assertion = postcondition.Invoke(value);
            // TODO: Convert the predicate to a System.Linq.Expression and dig into it to find out what it's doing to give a meaningful error message, like the ReflectionHelper in Fluent NHibernate does.
            Ensure(assertion, message);
        }

        /// <summary>
        /// Postcondition check.
        /// </summary>
        /// <typeparam name="T">The <see cref="T:System.Type"/> of <paramref name="value"/>.</typeparam>
        /// <param name="value">The value to check.</param>
        /// <param name="postcondition">The postcondition that checks the <paramref name="value"/>. If it returns
        /// <c>false</c>, a <see cref="PostconditionException"/> is thrown.</param>
        /// <param name="message">The message explaining why the postcondition failed.</param>
        /// <param name="inner">The exception containing details on why the postcondition failed.</param>
        /// <remarks>
        /// An assertion instead of a <see cref="PostconditionException"/> is invoked if
        /// <see cref="UseAssertions"/> is set to <c>true</c>.
        /// </remarks>
        public static void Ensure<T>(T value, Postcondition<T> postcondition, string message, Exception inner)
        {
            bool assertion = postcondition.Invoke(value);
            // TODO: Convert the predicate to a System.Linq.Expression and dig into it to find out what it's doing to give a meaningful error message, like the ReflectionHelper in Fluent NHibernate does.
            Ensure(assertion, message, inner);
        }

        /// <summary>
        /// Postcondition check.
        /// </summary>
        /// <param name="assertion">If set to <c>false</c>, throws a <see cref="PostconditionException"/>.</param>
        /// <param name="message">The message explaining why the postcondition failed.</param>
        /// <remarks>
        /// An assertion instead of a <see cref="PostconditionException"/> is invoked if
        /// <see cref="UseAssertions"/> is set to <c>true</c>.
        /// </remarks>
        public static void Ensure(bool assertion, string message)
        {
            if (UseExceptions)
            {
                if (!assertion) throw new PostconditionException(message);
            }
            else
            {
                Trace.Assert(assertion, "Postcondition: " + message);
            }
        }

        /// <summary>
        /// Postcondition check.
        /// </summary>
        /// <param name="assertion">If set to <c>false</c>, throws a <see cref="PostconditionException"/>
        /// with the given <paramref name="message"/>.</param>
        /// <param name="message">The message explaining why the postcondition failed.</param>
        /// <param name="inner">The exception containing details on why the precondition failed.</param>
        /// <remarks>
        /// An assertion instead of a <see cref="PostconditionException"/> is invoked if
        /// <see cref="UseAssertions"/> is set to <c>true</c>.
        /// </remarks>
        public static void Ensure(bool assertion, string message, Exception inner)
        {
            if (UseExceptions)
            {
                if (!assertion) throw new PostconditionException(message, inner);
            }
            else
            {
                Trace.Assert(assertion, "Postcondition: " + message);
            }
        }

        /// <summary>
        /// Postcondition check.
        /// </summary>
        /// <param name="assertion">If set to <c>false</c>, throws a <see cref="PostconditionException"/>.</param>
        /// <remarks>
        /// An assertion instead of a <see cref="PostconditionException"/> is invoked if
        /// <see cref="UseAssertions"/> is set to <c>true</c>.
        /// </remarks>
        public static void Ensure(bool assertion)
        {
            if (UseExceptions)
            {
                if (!assertion) throw new PostconditionException("Postcondition failed.");
            }
            else
            {
                Trace.Assert(assertion, "Postcondition failed.");
            }
        }
    }
}