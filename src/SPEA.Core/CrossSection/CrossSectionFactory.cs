// ==================================================================================================
// <copyright file="CrossSectionFactory.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Core.CrossSection
{
    /// <summary>
    /// An abstract factory of various implementations of a built-up cross-section.
    /// </summary>
    /// <typeparam name="T">Cross-section model type.</typeparam>
    public abstract class CrossSectionFactory<T>
        where T : CrossSectionBase
    {
        /// <summary>
        /// Gets a type of the created cross-section.
        /// </summary>
        public Type ModelType => typeof(T);

        /// <summary>
        /// Creates a new instance of the requested model type.
        /// Actual creation will occur in <see cref="CreateCore"/>.
        /// </summary>
        /// <returns>The requested cross-section instance.</returns>
        public virtual T Create()
        {
            return CreateCore(string.Empty);
        }

        /// <summary>
        /// Creates a new instance of the requested model type.
        /// Actual creation will occur in <see cref="CreateCore"/>.
        /// </summary>
        /// <param name="name">Model name.</param>
        /// <returns>The requested cross-section instance.</returns>
        public virtual T Create(string name)
        {
            return CreateCore(name);
        }

        /// <summary>
        /// Subclasses must implement this to create instances of themselves.
        /// </summary>
        /// <param name="name">Model name.</param>
        /// <returns>The requested cross-section instance.</returns>
        protected abstract T CreateCore(string name);
    }
}
