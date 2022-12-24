// ==================================================================================================
// <copyright file="Quantity.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Core.Units.Quantities
{
    /// <summary>
    /// A base class for all physical quantities.
    /// </summary>
    public abstract class QuantityBase : IQuantity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QuantityBase"/> class.
        /// </summary>
        /// <param name="name">Quantity full name.</param>
        /// <param name="symbol">Quantity symbol.</param>
        public QuantityBase(string name, string symbol)
        {
            if (name.Length == 0)
            {
                throw new ArgumentException("Name cannot be empty.", nameof(name));
            }

            if (symbol.Length != 1)
            {
                throw new ArgumentException("Symbol must contain exactly 1 character.", nameof(symbol));
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
