// ==================================================================================================
// <copyright file="IQuantity.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Core.Units.Quantities
{
    /// <summary>
    /// Represents a contract every physical quantity must implement.
    /// </summary>
    public interface IQuantity
    {
        /// <summary>
        /// Gets the quantity name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the quantity symbol.
        /// </summary>
        public string Symbol { get; }
    }
}
