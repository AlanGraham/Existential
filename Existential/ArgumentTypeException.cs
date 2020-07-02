// <copyright file="ArgumentTypeException.cs" company="Gavin Greig">
//     Copyright (c) Dr. Gavin T.D. Greig, 2020.
// </copyright>

namespace Existential
{
    /// <summary>An exception that indicates the use of an unexpected type.</summary>
    public class ArgumentTypeException : System.ArgumentException
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
        public ArgumentTypeException(string inMessage, System.Exception inException)
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
    }
}