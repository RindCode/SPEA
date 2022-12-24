// ==================================================================================================
// <copyright file="Length.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Core.Units.Quantities
{
    /// <summary>
    /// Represents Length physical quantity.
    /// </summary>
    public sealed class Length : QuantityBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Length"/> class.
        /// </summary>
        public Length()
            : base("Length", "L")
        {
            // Blank
        }
    }
}
