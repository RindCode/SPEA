// ==================================================================================================
// <copyright file="CrossSection.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Core.CrossSections
{
    using System;
    using SPEA.Core.Factories;
    using SPEA.Core.Geometry;
    using SPEA.Geometry.Core;

    /// <summary>
    /// A base class of a built-up cross-section model.
    /// Any cross-section model must derive from this class.
    /// </summary>
    public abstract class CrossSection : IDisposable
    {
        #region Fields

        private static readonly Dictionary<Type, object> _registeredFactories = new Dictionary<Type, object>();
        private readonly Guid _guid;
        private readonly CrossSectionGeometry _geometry;
        private bool _disposed;
        private string _name;

        #endregion Fields

        #region Constructors

        static CrossSection()
        {
            RegisterFactory(new MetallicCrossSectionFactory());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CrossSection"/> class.
        /// </summary>
        protected CrossSection()
            : this(string.Empty)
        {
            // Blank.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CrossSection"/> class.
        /// </summary>
        /// <param name="name">Section name.</param>
        protected CrossSection(string name)
        {
            _guid = Guid.NewGuid();
            _name = string.IsNullOrEmpty(name) ? _guid.ToString() : name;
            _geometry = new CrossSectionGeometry();
        }

        #endregion Constructors

        #region Destructors

        /*
        // Override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        ~CrossSection()
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
        /// Gets a <see cref="Dictionary{TKey, TValue}"/> of registered factories, where
        /// each key represents a cross-section model type and its corresponding value is
        /// a boxed implementation factory.
        /// Use <see cref="RegisterFactory{T}(CrossSectionFactory{T})"/> static method
        /// to register new implementations.
        /// </summary>
        public static Dictionary<Type, object> RegisteredFactories => _registeredFactories;

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
        /// Registers the specified factory.
        /// </summary>
        /// <typeparam name="T">Cross-section model type.</typeparam>
        /// <param name="implementationFactory">A factory used for creating service instances.</param>
        /// <exception cref="InvalidOperationException">If the given cross-section model type is already registered.</exception>
        public static void RegisterFactory<T>(CrossSectionFactory<T> implementationFactory)
            where T : CrossSection
        {
            var added = _registeredFactories.TryAdd(implementationFactory.ModelType, implementationFactory);
            if (!added)
            {
                throw new InvalidOperationException($"The following type is already registered: {typeof(T)}");
            }
        }

        /// <summary>
        /// Returns a new instance of the requested cross-section model type.
        /// </summary>
        /// <typeparam name="T">Cross-section model type.</typeparam>
        /// <returns>The requested cross-section instance.</returns>
        /// <exception cref="KeyNotFoundException">Item is not registered.</exception>
        public static T Create<T>()
            where T : CrossSection
        {
            return Create<T>(string.Empty);
        }

        /// <summary>
        /// Returns a new instance of the requested cross-section model type.
        /// </summary>
        /// <typeparam name="T">Cross-section model type.</typeparam>
        /// <param name="name">Model name.</param>
        /// <returns>The requested cross-section instance.</returns>
        /// <exception cref="KeyNotFoundException">Item is not registered.</exception>
        public static T Create<T>(string name)
            where T : CrossSection
        {
            if (_registeredFactories.TryGetValue(typeof(T), out var factoryReference))
            {
                var model = ((CrossSectionFactory<T>)factoryReference).Create(name);
                return model;
            }
            else
            {
                throw new KeyNotFoundException($"The requested model type is not registered: {typeof(T)}");
            }
        }

        /// <summary>
        /// Adds a <see cref="SPolygon"/> object into the geometry collection.
        /// </summary>
        /// <param name="polygon">A polygon object to be added.</param>
        public void AddPolygon(SPolygon polygon)
        {
            Geometry.Actual.Items.Add(polygon);
        }

        #endregion Methods
    }
}
