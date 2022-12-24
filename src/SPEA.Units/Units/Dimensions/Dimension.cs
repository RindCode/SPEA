// ==================================================================================================
// <copyright file="Dimension.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Core.Units.Dimensions
{
    public class Dimension : IDimension
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Dimension"/> class.
        /// All dimentional exponents will be set to zero.
        /// </summary>
        public Dimension()
            : this(0, 0, 0, 0, 0, 0, 0)
        {
            // Blank.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Dimension"/> class.
        /// Default value for each exponent is zero.
        /// </summary>
        /// <param name="lengthExp">Length dimensional exponent.</param>
        /// <param name="massExp">Mass dimensional exponent.</param>
        /// <param name="timeExp">Time dimensional exponent.</param>
        /// <param name="electricCurrentExp">Electric Current dimensional exponent.</param>
        /// <param name="temperatureExp">Temperature dimensional exponent.</param>
        /// <param name="amountOfSubstanceExp">Amount of Substance dimensional exponent.</param>
        /// <param name="luminousIntensityExp">Luminous Intensity dimensional exponent.</param>
        public Dimension(
            int lengthExp = 0,
            int massExp = 0,
            int timeExp = 0,
            int electricCurrentExp = 0,
            int temperatureExp = 0,
            int amountOfSubstanceExp = 0,
            int luminousIntensityExp = 0)
        {

        }
    }
}
