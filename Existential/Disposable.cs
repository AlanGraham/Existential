// <copyright file="Disposable.cs" company="Gavin Greig">
//     Copyright (c) Dr. Gavin T.D. Greig, 2020.
// </copyright>

namespace GavinGreig
{
    /// <summary>
    /// Contains a method that supports the correct usage of <see cref="System.IDisposable" /> objects.
    /// </summary>
    public static class Disposable
    {
        /// <summary>
        /// Creates and initialises an <see cref="System.IDisposable" /> object of type
        /// <typeparamref name="T" /> using best practices so that it can be safely returned without
        /// violating Code Analysis rule CA2000.
        /// </summary>
        /// <param name="inInitialiser">
        /// A method that initialises a type <typeparamref name="T" /> object after it has been created.
        /// </param>
        /// <typeparam name="T">
        /// The type of the object to be created. It must be <see cref="System.IDisposable" /> and
        /// have a default constructor.
        /// </typeparam>
        /// <returns>
        /// An initialised object of type <typeparamref name="T" /> that can be safely returned.
        /// </returns>
        public static T SafelyReturn<T>(System.Action<T> inInitialiser)
            where T : class, System.IDisposable, new()
        {
            // When returning a disposable object, constructing it using a temporary variable inside
            // a try/finally block is best practice. https://docs.microsoft.com/en-us/visualstudio/code-quality/ca2000-dispose-objects-before-losing-scope#example
            T theObject;
            T theTemporaryObject = null;
            try
            {
                theTemporaryObject = new T();

                // Invoke the initialiser on the object if one has been provided.
                inInitialiser?.Invoke(theTemporaryObject);

                // Prepare the object for return.
                theObject = theTemporaryObject;
                theTemporaryObject = null;
            }
            finally
            {
                // Ensure that the temporary object is disposed if something goes wrong.
                theTemporaryObject?.Dispose();
            }

            return theObject;
        }
    }
}