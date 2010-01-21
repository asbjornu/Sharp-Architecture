using System;
using System.Diagnostics;

namespace SharpArch.Core
{
    /// <summary>
    /// An invariant delegate, returning <c>true</c> if the invariant
    /// is met, or <c>false</c> to throw a <see cref="AssertionException"/>.
    /// </summary>
    public delegate bool Invariant<T>(T value);


    /// <summary>
    /// Exception raised when an invariant fails.
    /// </summary>
    public class InvariantException : DesignByContractException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvariantException"/> class.
        /// </summary>
        /// <param name="message">The message explaining why the invariant failed.</param>
        public InvariantException(string message) : base(message)
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="InvariantException"/> class.
        /// </summary>
        /// <param name="message">The message explaining why the invariant failed.</param>
        /// <param name="inner">The exception containing details on why the invariant failed.</param>
        public InvariantException(string message, Exception inner) : base(message, inner)
        {
        }
    }

    public static partial class Check
    {
        /// <summary>
        /// Invariant check.
        /// </summary>
        /// <typeparam name="T">The <see cref="T:System.Type"/> of <paramref name="value"/>.</typeparam>
        /// <param name="value">The value to check.</param>
        /// <param name="invariant">The invariant that checks the <paramref name="value"/>. If it returns
        /// <c>false</c>, an <see cref="InvariantException"/> is thrown.</param>
        /// <remarks>
        /// An assertion instead of a <see cref="InvariantException"/> is invoked if
        /// <see cref="UseAssertions"/> is set to <c>true</c>.
        /// </remarks>
        public static void Invariant<T>(T value, Invariant<T> invariant)
        {
            bool assertion = invariant.Invoke(value);
            // TODO: Convert the predicate to a System.Linq.Expression and dig into it to find out what it's doing to give a meaningful error message, like the ReflectionHelper in Fluent NHibernate does.
            Invariant(assertion);
        }


        /// <summary>
        /// Invariant check.
        /// </summary>
        /// <typeparam name="T">The <see cref="T:System.Type"/> of <paramref name="value"/>.</typeparam>
        /// <param name="value">The value to check.</param>
        /// <param name="invariant">The invariant that checks the <paramref name="value"/>. If it returns
        /// <c>false</c>, an <see cref="InvariantException"/> is thrown.</param>
        /// <param name="message">The message explaining why the invariant failed.</param>
        /// <remarks>
        /// An assertion instead of a <see cref="InvariantException"/> is invoked if
        /// <see cref="UseAssertions"/> is set to <c>true</c>.
        /// </remarks>
        public static void Invariant<T>(T value, Invariant<T> invariant, string message)
        {
            bool assertion = invariant.Invoke(value);
            // TODO: Convert the predicate to a System.Linq.Expression and dig into it to find out what it's doing to give a meaningful error message, like the ReflectionHelper in Fluent NHibernate does.
            Invariant(assertion, message);
        }


        /// <summary>
        /// Invariant check.
        /// </summary>
        /// <typeparam name="T">The <see cref="T:System.Type"/> of <paramref name="value"/>.</typeparam>
        /// <param name="value">The value to check.</param>
        /// <param name="invariant">The invariant that checks the <paramref name="value"/>. If it returns
        /// <c>false</c>, an <see cref="InvariantException"/> is thrown.</param>
        /// <param name="message">The message explaining why the invariant failed.</param>
        /// <param name="inner">The exception containing details on why the invariant failed.</param>
        /// <remarks>
        /// An assertion instead of a <see cref="InvariantException"/> is invoked if
        /// <see cref="UseAssertions"/> is set to <c>true</c>.
        /// </remarks>
        public static void Invariant<T>(T value, Invariant<T> invariant, string message, Exception inner)
        {
            bool assertion = invariant.Invoke(value);
            // TODO: Convert the predicate to a System.Linq.Expression and dig into it to find out what it's doing to give a meaningful error message, like the ReflectionHelper in Fluent NHibernate does.
            Invariant(assertion, message, inner);
        }


        /// <summary>
        /// Invariant check.
        /// </summary>
        /// <param name="assertion">If set to <c>false</c>, throws an <see cref="InvariantException"/>
        /// with the given <paramref name="message"/>.</param>
        /// <param name="message">The message explaining why the invariant failed.</param>
        /// <remarks>
        /// An assertion instead of a <see cref="InvariantException"/> is invoked if
        /// <see cref="UseAssertions"/> is set to <c>true</c>.
        /// </remarks>
        public static void Invariant(bool assertion, string message)
        {
            if (UseExceptions)
            {
                if (!assertion)
                    throw new InvariantException(message);
            }
            else
            {
                Trace.Assert(assertion, "Invariant: " + message);
            }
        }


        /// <summary>
        /// Invariant check.
        /// </summary>
        /// <param name="assertion">If set to <c>false</c>, throws an <see cref="InvariantException"/>
        /// with the given <paramref name="message"/>.</param>
        /// <param name="message">The message explaining why the invariant failed.</param>
        /// <param name="inner">The exception containing details on why the ivariant failed.</param>
        /// <remarks>
        /// An assertion instead of a <see cref="InvariantException"/> is invoked if
        /// <see cref="UseAssertions"/> is set to <c>true</c>.
        /// </remarks>
        public static void Invariant(bool assertion, string message, Exception inner)
        {
            if (UseExceptions)
            {
                if (!assertion)
                    throw new InvariantException(message, inner);
            }
            else
            {
                Trace.Assert(assertion, "Invariant: " + message);
            }
        }


        /// <summary>
        /// Invariant check.
        /// </summary>
        /// <param name="assertion">If set to <c>false</c>, throws an <see cref="InvariantException"/>.</param>
        /// <remarks>
        /// An assertion instead of a <see cref="InvariantException"/> is invoked if
        /// <see cref="UseAssertions"/> is set to <c>true</c>.
        /// </remarks>
        public static void Invariant(bool assertion)
        {
            if (UseExceptions)
            {
                if (!assertion)
                    throw new InvariantException("Invariant failed.");
            }
            else
            {
                Trace.Assert(assertion, "Invariant failed.");
            }
        }
    }
}