// ==================================================================================================
// <copyright file="Temperature.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Core.Units.Quantities
{
    /// <summary>
    /// Represents Temperature physical quantity.
    /// </summary>
    public sealed class Temperature : QuantityBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Temperature"/> class.
        /// </summary>
        public Temperature()
            : base("Temperature", "Θ")
        {
            // Blank
        }
    }
}
