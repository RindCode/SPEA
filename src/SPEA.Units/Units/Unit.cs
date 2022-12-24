// ==================================================================================================
// <copyright file="Unit.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Core.Units
{
    /// <summary>
    /// A base class for all units.
    /// </summary>
    public abstract class Unit : IUnit
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Unit"/> class.
        /// </summary>
        /// <param name="name">Unit full name.</param>
        /// <param name="symbol">Unit symbol.</param>
        public Unit(string name, string symbol)
        {
            if (name.Length == 0)
            {
                throw new ArgumentException("Name cannot be empty.", nameof(name));
            }

            if (symbol.Length == 0)
            {
                throw new ArgumentException("Symbol cannot be empty.", nameof(symbol));
            }

            Name = name;
            Symbol = symbol;
        }

        /// <inheritdoc/>
        public string Name { get; }

        /// <inheritdoc/>
        public string Symbol { get; }
    }
}
