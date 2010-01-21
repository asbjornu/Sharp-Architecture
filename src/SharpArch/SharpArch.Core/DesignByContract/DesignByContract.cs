using System;

namespace SharpArch.Core
{
    /// <summary>
    /// Design by Contract checks developed by http://www.codeproject.com/KB/cs/designbycontract.aspx.
    /// 
    /// Each method generates an exception or
    /// a trace assertion statement if the contract is broken.
    /// </summary>
    /// <remarks>
    /// This example shows how to call the Require method.
    /// Assume DBC_CHECK_PRECONDITION is defined.
    /// <code>
    /// public void Test(int x)
    /// {
    /// 	try
    /// 	{
    ///			Check.Require(x > 1, "x must be > 1");
    ///		}
    ///		catch (System.Exception ex)
    ///		{
    ///			Console.WriteLine(ex.ToString());
    ///		}
    ///	}
    /// </code>
    /// If you wish to use trace assertion statements, intended for Debug scenarios,
    /// rather than exception handling then set 
    /// 
    /// <code>Check.UseAssertions = true</code>
    /// 
    /// You can specify this in your application entry point and maybe make it
    /// dependent on conditional compilation flags or configuration file settings, e.g.,
    /// <code>
    /// #if DBC_USE_ASSERTIONS
    /// Check.UseAssertions = true;
    /// #endif
    /// </code>
    /// You can direct output to a Trace listener. For example, you could insert
    /// <code>
    /// Trace.Listeners.Clear();
    /// Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
    /// </code>
    /// 
    /// or direct output to a file or the Event Log.
    /// 
    /// (Note: For ASP.NET clients use the Listeners collection
    /// of the Debug, not the Trace, object and, for a Release build, only exception-handling
    /// is possible.)
    /// </remarks>
    /// 
    public static partial class Check
    {
        #region Interface

        /// <summary>
        /// Set this if you wish to use Trace Assert statements
        /// instead of exception handling.
        /// (The Check class uses exception handling by default.)
        /// </summary>
        /// <value><c>true</c> to use assertions instead of exceptions; otherwise, <c>false</c>.</value>
        public static bool UseAssertions { get; set; }

        #endregion // Interface

        #region Implementation

        // No creation

        /// <summary>
        /// Is exception handling being used?
        /// </summary>
        private static bool UseExceptions {
            get {
                return !UseAssertions;
            }
        }

        #endregion // Implementation

    } // End Check

    #region Exceptions

    /// <summary>
    /// Exception raised when a contract is broken.
    /// Catch this exception type if you wish to differentiate between 
    /// any DesignByContract exception and other runtime exceptions.  
    /// </summary>
    public class DesignByContractException : ApplicationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DesignByContractException"/> class.
        /// </summary>
        protected DesignByContractException() { }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="DesignByContractException"/> class.
        /// </summary>
        /// <param name="message">The message explaining why the contract is broken.</param>
        protected DesignByContractException(string message) : base(message) { }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="DesignByContractException"/> class.
        /// </summary>
        /// <param name="message">The message explaining why the contract is broken.</param>
        /// <param name="inner">The exception containing details on why the contract is broken.</param>
        protected DesignByContractException(string message, Exception inner) : base(message, inner) { }
    }

    #endregion // Exception classes
}