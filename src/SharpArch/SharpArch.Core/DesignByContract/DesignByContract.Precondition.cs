using System;
using System.Diagnostics;

namespace SharpArch.Core
{
    /// <summary>
    /// A postcondition delegate, returning <c>true</c> if the precondition
    /// is met, or <c>false</c> to throw a <see cref="PreconditionException"/>.
    /// </summary>
    public delegate bool Precondition<T>(T value);

    /// <summary>
    /// Exception raised when a precondition fails.
    /// </summary>
    public class PreconditionException : DesignByContractException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PreconditionException"/> class.
        /// </summary>
        /// <param name="message">The message explaining why the precondition failed.</param>
        public PreconditionException(string message) : base(message)
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="PreconditionException"/> class.
        /// </summary>
        /// <param name="message">The message explaining why the precondition failed.</param>
        /// <param name="inner">The exception containing details on why the precondition failed.</param>
        public PreconditionException(string message, Exception inner) : base(message, inner)
        {
        }
    }

    public static partial class Check
    {
        /// <summary>
        /// Precondition check - should run regardless of preprocessor directives.
        /// </summary>
        /// <typeparam name="T">The <see cref="T:System.Type"/> of <paramref name="value"/>.</typeparam>
        /// <param name="value">The value to check.</param>
        /// <param name="precondition">The precondition that checks the <paramref name="value"/>. If it returns
        /// <c>false</c>, a <see cref="PreconditionException"/> is thrown.</param>
        /// <remarks>
        /// An assertion instead of a <see cref="PreconditionException"/> is invoked if
        /// <see cref="UseAssertions"/> is set to <c>true</c>.
        /// </remarks>
        public static void Require<T>(T value, Precondition<T> precondition)
        {
            bool assertion = precondition.Invoke(value);
            // TODO: Convert the predicate to a System.Linq.Expression and dig into it to find out what it's doing to give a meaningful error message, like the ReflectionHelper in Fluent NHibernate does.
            Require(assertion);
        }


        /// <summary>
        /// Precondition check - should run regardless of preprocessor directives.
        /// </summary>
        /// <typeparam name="T">The <see cref="T:System.Type"/> of <paramref name="value"/>.</typeparam>
        /// <param name="value">The value to check.</param>
        /// <param name="precondition">The precondition that checks the <paramref name="value"/>. If it returns
        /// <c>false</c>, a <see cref="PreconditionException"/> with the given <paramref name="message"/>
        /// is thrown.</param>
        /// <param name="message">The message explaining why the precondition failed.</param>
        /// <remarks>
        /// An assertion instead of a <see cref="PreconditionException"/> is invoked if
        /// <see cref="UseAssertions"/> is set to <c>true</c>.
        /// </remarks>
        public static void Require<T>(T value, Precondition<T> precondition, string message)
        {
            bool assertion = precondition.Invoke(value);
            // TODO: Convert the predicate to a System.Linq.Expression and dig into it to find out what it's doing to give a meaningful error message, like the ReflectionHelper in Fluent NHibernate does.
            Require(assertion, message);
        }


        /// <summary>
        /// Precondition check - should run regardless of preprocessor directives.
        /// </summary>
        /// <typeparam name="T">The <see cref="T:System.Type"/> of <paramref name="value"/>.</typeparam>
        /// <param name="value">The value to check.</param>
        /// <param name="precondition">The precondition that checks the <paramref name="value"/>. If it returns
        /// <c>false</c>, a <see cref="PreconditionException"/> with the given <paramref name="message"/>
        /// is thrown.</param>
        /// <param name="message">The message explaining why the precondition failed.</param>
        /// <param name="inner">The exception containing details on why the precondition failed.</param>
        /// <remarks>
        /// An assertion instead of a <see cref="PreconditionException"/> is invoked if
        /// <see cref="UseAssertions"/> is set to <c>true</c>.
        /// </remarks>
        public static void Require<T>(T value,
                                      Precondition<T> precondition,
                                      string message,
                                      Exception inner)
        {
            bool assertion = precondition.Invoke(value);
            // TODO: Convert the predicate to a System.Linq.Expression and dig into it to find out what it's doing to give a meaningful error message, like the ReflectionHelper in Fluent NHibernate does.
            Require(assertion, message, inner);
        }


        /// <summary>
        /// Precondition check - should run regardless of preprocessor directives.
        /// </summary>
        /// <param name="assertion">If set to <c>false</c>, throws a <see cref="PreconditionException"/>
        /// with the given <paramref name="message"/>.</param>
        /// <param name="message">The message explaining why the precondition failed.</param>
        /// <remarks>
        /// An assertion instead of a <see cref="PreconditionException"/> is invoked if
        /// <see cref="UseAssertions"/> is set to <c>true</c>.
        /// </remarks>
        public static void Require(bool assertion, string message)
        {
            if (UseExceptions)
            {
                if (!assertion)
                    throw new PreconditionException(message);
            }
            else
            {
                Trace.Assert(assertion, "Precondition: " + message);
            }
        }


        /// <summary>
        /// Precondition check - should run regardless of preprocessor directives.
        /// </summary>
        /// <param name="assertion">If set to <c>false</c>, throws a <see cref="PreconditionException"/>
        /// with the given <paramref name="message"/>.</param>
        /// <param name="message">The message explaining why the precondition failed.</param>
        /// <param name="inner">The exception containing details on why the precondition failed.</param>
        /// <remarks>
        /// An assertion instead of a <see cref="PreconditionException"/> is invoked if
        /// <see cref="UseAssertions"/> is set to <c>true</c>.
        /// </remarks>
        public static void Require(bool assertion, string message, Exception inner)
        {
            if (UseExceptions)
            {
                if (!assertion)
                    throw new PreconditionException(message, inner);
            }
            else
            {
                Trace.Assert(assertion, "Precondition: " + message);
            }
        }


        /// <summary>
        /// Precondition check - should run regardless of preprocessor directives.
        /// </summary>
        /// <param name="assertion">If set to <c>false</c>, throws a <see cref="PreconditionException"/>.</param>
        /// <remarks>
        /// An assertion instead of a <see cref="PreconditionException"/> is invoked if
        /// <see cref="UseAssertions"/> is set to <c>true</c>.
        /// </remarks>
        public static void Require(bool assertion)
        {
            if (UseExceptions)
            {
                if (!assertion)
                    throw new PreconditionException("Precondition failed.");
            }
            else
            {
                Trace.Assert(assertion, "Precondition failed.");
            }
        }
    }
}