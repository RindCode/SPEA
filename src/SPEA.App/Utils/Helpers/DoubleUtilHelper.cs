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
        /// Determines whether a given string represents a valid double value.
        /// </summary>
        /// <param name="value">A string to test.</param>
        /// <returns><see langword="true"/> if valid, otherwise <see langword="false"/>.</returns>
        public static bool IsValidDouble(string value)
        {
            return double.TryParse(value, out double parsed) && !(double.IsNaN(parsed) || double.IsInfinity(parsed));
        }

        /// <summary>
        /// Determines whether a given double value is "close" to zero.
        /// </summary>
        /// <param name="value">A value to be tested for zero equality.</param>
        /// <returns><see langword="true"/> if zero, otherwise <see langword="false"/>.</returns>
        public static bool IsZero(double value)
        {
            return Math.Abs(value) < 10.0 * DoubleEpsilon;
        }

        /// <summary>
        /// Performs a safe conversion to <see cref="double"/> type without throwing an exception.
        /// If the conversion succeeds, then stores the converted <paramref name="result"/>
        /// and returns <see langword="true"/>. If the conversion fails, doesn't throw any exceptions,
        /// stores a default value for <see cref="double"/> type and returns <see langword="false"/>.
        /// </summary>
        /// <param name="value">A value to convert.</param>
        /// <param name="result">The converted value.</param>
        /// <returns><see langword="true"/> if the conversion was successful, otherwise <see langword="false"/>.</returns>
        public static bool SafeConvert(object value, out double result)
        {
            try
            {
                result = (double)Convert.ChangeType(value, typeof(double));
            }
            catch (Exception ex) when (
                ex is InvalidCastException ||
                ex is FormatException ||
                ex is OverflowException ||
                ex is ArgumentNullException)
            {
                result = default;
                return false;
            }

            return true;
        }

        #endregion Methods
    }
}
