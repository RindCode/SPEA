// ==================================================================================================
// <copyright file="Time.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Core.Units.Quantities
{
    /// <summary>
    /// Represents Time physical quantity.
    /// </summary>
    public sealed class Time : QuantityBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Time"/> class.
        /// </summary>
        public Time()
            : base("Time", "T")
        {
            // Blank
        }
    }
}
