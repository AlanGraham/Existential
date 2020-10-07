// <copyright file="TestDummyDisposable.cs" company="Gavin Greig">
//   Copyright (c) Dr. Gavin T.D. Greig, 2020.
// </copyright>

namespace Existential.Test
{
    using System;
    using System.IO;

    public class TestDummyDisposable : IDisposable
    {
        public TestDummyDisposable()
            : this(new MemoryStream())
        {
        }

        public TestDummyDisposable(Stream inStream) => ManagedObject = inStream;

        ~TestDummyDisposable() => Dispose(false);

        public Stream ManagedObject { get; set; }

        public string UnmanagedObject { get; internal set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool inDisposing)
        {
            ReleaseUnmanagedResources();
            if (inDisposing)
            {
                ManagedObject?.Dispose();
            }
        }

        private void ReleaseUnmanagedResources() =>
            UnmanagedObject = null;
    }
}