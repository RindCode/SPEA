// ==================================================================================================
// <copyright file="CrossSectionBase.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Core.CrossSection
{
    using System;
    using SPEA.Core.Geometry;
    using SPEA.Geometry.Core;

    /// <summary>
    /// A base class of a built-up cross-section model.
    /// Any cross-section model must derive from this class.
    /// </summary>
    public abstract class CrossSectionBase : IDisposable
    {
        #region Fields

        private readonly Guid _guid;
        private readonly CrossSectionGeometry _geometry;
        private bool _disposed;
        private string _name;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CrossSectionBase"/> class.
        /// </summary>
        protected CrossSectionBase()
            : this(string.Empty)
        {
            // Blank.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CrossSectionBase"/> class.
        /// </summary>
        /// <param name="name">Section name.</param>
        protected CrossSectionBase(string name)
        {
            _guid = Guid.NewGuid();
            _name = string.IsNullOrEmpty(name) ? _guid.ToString() : name;
            _geometry = new CrossSectionGeometry();
        }

        #endregion Constructors

        #region Destructors

        /*
        // Override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        ~CrossSectionBase()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
        }
        */

        #endregion Destructors

        #region IDisposable

        /// <inheritdoc/>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Implements Dispose pattern.
        /// </summary>
        /// <param name="disposing">Designates whether the method was called from Dispose() or not.</param>
        protected virtual void Dispose(bool disposing)
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
        }

        #endregion IDisposable

        #region Properties

        /// <summary>
        /// Gets the unique ID of this section.
        /// </summary>
        public Guid Guid => _guid;

        /// <summary>
        /// Gets or sets cross-section name.
        /// </summary>
        /// <remarks>
        /// If a name was not provided in a constructor, it will be automatically set to <see cref="Guid"/>.
        /// </remarks>
        public string Name
        {
            get => _name;
            set => _name = value;
        }

        /// <summary>
        /// Gets the cross-section geometry data.
        /// </summary>
        public CrossSectionGeometry Geometry => _geometry;

        #endregion Properties

        #region Methods

        /// <summary>
        /// Adds a <see cref="SPolygonBase"/> object into the geometry collection.
        /// </summary>
        /// <param name="polygon">A polygon object to be added.</param>
        public void AddPolygon(SPolygonBase polygon)
        {
            Geometry.Actual.Items.Add(polygon);
        }

        #endregion Methods
    }
}
