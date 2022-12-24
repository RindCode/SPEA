// ==================================================================================================
// <copyright file="CrossSection.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Core.CrossSection
{
    /// <summary>
    /// Provides various methods to create new instaces of a cross-section model.
    /// A factory implementation derived from <see cref="CrossSectionFactory{T}"/> base class
    /// must be registered first prior calling <see cref="Create{T}"/> method.
    /// </summary>
    public static class CrossSection
    {
        // A list of registered factories.
        // TODO: avoid boxing somehow?
        private static readonly Dictionary<Type, object> _registeredFactories = new ();

        // Register pre-defined factories.
        static CrossSection()
        {
            RegisterFactory(new MetallicCrossSectionFactory());
        }

        /// <summary>
        /// Gets a <see cref="Dictionary{TKey, TValue}"/> of registered factories, where
        /// each key represents a cross-section model type and its corresponding value is
        /// a boxed implementation factory.
        /// Use <see cref="RegisterFactory{T}(CrossSectionFactory{T})"/> static method
        /// to register new implementations.
        /// </summary>
        public static Dictionary<Type, object> RegisteredFactories => _registeredFactories;

        /// <summary>
        /// Registers the specified factory.
        /// </summary>
        /// <typeparam name="T">Cross-section model type.</typeparam>
        /// <param name="implementationFactory">A factory used for creating service instances.</param>
        /// <exception cref="InvalidOperationException">If the given cross-section model type is already registered.</exception>
        public static void RegisterFactory<T>(CrossSectionFactory<T> implementationFactory)
            where T : CrossSectionBase
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
            where T : CrossSectionBase
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
            where T : CrossSectionBase
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
    }
}
