// ==================================================================================================
// <copyright file="DoubleUtilHelper.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.Utils.Helpers
{
    using System;

    /// <summary>
    /// Provides access to some compare methods for the <see cref="double"/> type.
    /// </summary>
    public static class DoubleUtilHelper
    {
        #region Fields

        /// <summary>
        /// The smallest such that 1.0+DoubleEpsilon != 1.0.
        /// </summary>
        public const double DoubleEpsilon = 2.2204460492503131e-016;

        #endregion Fields

        #region Methods

        /// <summary>
        /// Determines whether or not a given double value is "close" to zero.
        /// </summary>
        /// <param name="value">A value to be tested for zero equality.</param>
        /// <returns><see langword="true"/> if zero, otherwise <see langword="false"/>.</returns>
        public static bool IsZero(double value)
        {
            return Math.Abs(value) < 10.0 * DoubleEpsilon;
        }

        #endregion Methods
    }
}
