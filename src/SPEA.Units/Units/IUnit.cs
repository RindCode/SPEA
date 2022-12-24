// ==================================================================================================
// <copyright file="IUnit.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Core.Units
{
    /// <summary>
    /// Represents a contract every unit must implement.
    /// </summary>
    public interface IUnit
    {
        /// <summary>
        /// Gets the unit name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the unit symbol.
        /// </summary>
        public string Symbol { get; }
    }
}
