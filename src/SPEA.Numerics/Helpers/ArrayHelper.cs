// ==================================================================================================
// <copyright file="ArrayHelper.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Numerics.Helpers
{
    /// <summary>
    /// Provides various methods for array operations.
    /// </summary>
    public static class ArrayHelper
    {
        #region Methods

        /// <summary>
        /// Does a point-wise add of two array and saves the result into <paramref name="result"/>.
        /// </summary>
        /// <param name="x">The first array.</param>
        /// <param name="y">The second array.</param>
        /// <param name="result">The resulting array.</param>
        /// <exception cref="ArgumentNullException">If any of the arguments is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">If arrays don't have the same length.</exception>
        public static void AddArrays(double[] x, double[] y, double[] result)
        {
            ArgumentNullException.ThrowIfNull(x, nameof(x));
            ArgumentNullException.ThrowIfNull(y, nameof(y));

            if (x.Length != y.Length || y.Length != result.Length)
            {
                throw new ArgumentException("All arrays must have the same length.");
            }

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = x[i] + y[i];
            }
        }

        #endregion Methods
    }
}
