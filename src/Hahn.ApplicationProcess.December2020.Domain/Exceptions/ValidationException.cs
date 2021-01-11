using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hahn.ApplicationProcess.December2020.Domain.Exceptions
{
    /// <summary>
    /// ValidationException class represents the exceptions that occurs during validation of properties of a class
    /// when <see cref="FluentValidation" /> rules is used.
    /// Implements the <see cref="System.Exception" />
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class ValidationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException"/> class.
        /// </summary>
        public ValidationException()
            : base("One or more validation failures have occurred.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException"/> class.
        /// </summary>
        /// <param name="failures">The failures.</param>
        public ValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }

        /// <summary>
        /// Gets the errors.
        /// </summary>
        /// <value>The errors.</value>
        public IDictionary<string, string[]> Errors { get; }
    }
}