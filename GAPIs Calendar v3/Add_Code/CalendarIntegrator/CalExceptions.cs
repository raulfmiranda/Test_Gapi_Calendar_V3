using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GAPIs_Calendar_v3.Add_Code.CalendarIntegrator
{
    /// <summary>
    /// Uncertain error. Called when an unexpected occurs.
    /// </summary>
    public class UnknownException : Exception
    {
        public UnknownException() : base("Caused undefined error.") { }
        public UnknownException(string message) : base("Caused undefined error.", new Exception(message)) { }
        public UnknownException(string message, Exception innerException) { }
    }

    /// <summary>
    /// An error that is thrown if the user declines the invitation.
    /// </summary>
    public class UserRejectException : Exception
    {
        public UserRejectException() : base("User rejected the invitation.") { }
        public UserRejectException(string message) : base(message) { }
        public UserRejectException(string message, Exception innerException) { }
    }
}