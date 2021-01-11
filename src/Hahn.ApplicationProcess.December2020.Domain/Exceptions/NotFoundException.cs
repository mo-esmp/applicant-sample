using System;

namespace Hahn.ApplicationProcess.December2020.Domain.Exceptions
{
    /// <summary>
    /// NotFoundException class represents domain exception and throws an exception when an object is not present.
    /// Implements the <see cref="System.Exception" />
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class NotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public NotFoundException(string message)
            : base(message)
        {
        }
    }
}