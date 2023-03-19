// ==================================================================================================
// <copyright file="MetallicCrossSection.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Core.CrossSections
{
    /// <summary>
    /// Represents a metallic built-up cross-section model.
    /// </summary>
    public class MetallicCrossSection : CrossSection
    {
        #region Fields

        private bool _disposed;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MetallicCrossSection"/> class.
        /// </summary>
        internal MetallicCrossSection()
            : base()
        {
            // Blank
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MetallicCrossSection"/> class.
        /// </summary>
        /// <param name="name">Model name.</param>
        internal MetallicCrossSection(string name)
            : base(name)
        {
            // Blank
        }

        #endregion Constructors

        #region Destructors

        /*
        // Override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        ~MetallicCrossSection()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
        }
        */

        #endregion Destructors

        #region IDisposable

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Dispose managed state (managed objects)
                }

                // Free unmanaged resources (unmanaged objects) and override finalizer
                // Set large fields to null
                _disposed = true;
            }

            base.Dispose(disposing);
        }

        #endregion IDisposable
    }
}
