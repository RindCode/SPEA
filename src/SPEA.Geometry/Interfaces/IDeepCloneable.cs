// ==================================================================================================
// <copyright file="IDeepCloneable.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Geometry.Interfaces
{
    /// <summary>
    /// Represents a contract for object which can create their deep copies.
    /// </summary>
    /// <typeparam name="T">A type of an object to clone.</typeparam>
    public interface IDeepCloneable<T>
    {
        /// <summary>
        /// Creates a deep copy of an object.
        /// </summary>
        /// <returns>A deep copy of an object.</returns>
        public T DeepCopy();
    }
}
