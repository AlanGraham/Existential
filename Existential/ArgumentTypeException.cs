// <copyright file="ArgumentTypeException.cs" company="Gavin Greig">
//     Copyright (c) Dr. Gavin T.D. Greig, 2020.
// </copyright>

namespace Existential
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>An exception that indicates the use of an unexpected type.</summary>
    [Serializable]
    public class ArgumentTypeException : ArgumentException
    {
        /// <summary>Initialises a new instance of the <see cref="ArgumentTypeException" /> class.</summary>
        public ArgumentTypeException()
            : base()
        {
        }

        /// <summary>Initialises a new instance of the <see cref="ArgumentTypeException" /> class.</summary>
        /// <param name="inMessage">The error message.</param>
        public ArgumentTypeException(string inMessage)
            : base(inMessage)
        {
        }

        /// <summary>Initialises a new instance of the <see cref="ArgumentTypeException" /> class.</summary>
        /// <param name="inMessage">The error message.</param>
        /// <param name="inException">The inner exception.</param>
        public ArgumentTypeException(string inMessage, Exception inException)
            : base(inMessage, inException)
        {
        }

        /// <summary>Initialises a new instance of the <see cref="ArgumentTypeException" /> class.</summary>
        /// <param name="inMessage">The error message.</param>
        /// <param name="inParameterName">The name of the parameter with the unexpected type.</param>
        public ArgumentTypeException(string inMessage, string inParameterName)
            : base(inMessage, inParameterName)
        {
        }

        /// <summary>Initialises a new instance of the <see cref="ArgumentTypeException" /> class with serialised data.</summary>
        /// <param name="inInfo">The object that holds the serialized object data.</param>
        /// <param name="inContext">The contextual information about the source or destination.</param>
        /// <remarks>
        ///     This constructor is called during deserialisation to reconstitute the exception object transmitted over a
        ///     stream. For more information, see
        ///     <a href="https://docs.microsoft.com/en-us/dotnet/standard/serialization/xml-and-soap-serialization">
        ///         XML and SOAP Serialization
        ///     </a>
        ///     .
        /// </remarks>
        protected ArgumentTypeException(SerializationInfo inInfo, StreamingContext inContext)
            : base(inInfo, inContext)
        {
        }
    }
}