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
        /// Does a point-wise add of two arrays (x + y) and saves the result into <paramref name="result"/>.
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
            ArgumentNullException.ThrowIfNull(result, nameof(result));

            if (x.Length != y.Length || y.Length != result.Length)
            {
                throw new ArgumentException("All arrays must have the same length.");
            }

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = x[i] + y[i];
            }
        }

        /// <summary>
        /// Does a point-wise subtract of twoarrays (x - y) and saves the result into <paramref name="result"/>.
        /// </summary>
        /// <param name="x">The first array.</param>
        /// <param name="y">The second array.</param>
        /// <param name="result">The resulting array.</param>
        /// <exception cref="ArgumentNullException">If any of the arguments is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">If arrays don't have the same length.</exception>
        public static void SubtractArrays(double[] x, double[] y, double[] result)
        {
            ArgumentNullException.ThrowIfNull(x, nameof(x));
            ArgumentNullException.ThrowIfNull(y, nameof(y));
            ArgumentNullException.ThrowIfNull(result, nameof(result));

            if (x.Length != y.Length || y.Length != result.Length)
            {
                throw new ArgumentException("All arrays must have the same length.");
            }

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = x[i] - y[i];
            }
        }

        /// <summary>
        /// Scales an array.
        /// </summary>
        /// <param name="scale">The scaling factor.</param>
        /// <param name="x">An array containing values to scale.</param>
        /// <param name="result">The resulting array.</param>
        /// <exception cref="ArgumentNullException">If any of the arguments is <see langword="null"/>.</exception>
        public static void ScaleArray(double scale, double[] x, double[] result)
        {
            ArgumentNullException.ThrowIfNull(x, nameof(x));
            ArgumentNullException.ThrowIfNull(result, nameof(result));

            if (scale == 0.0)
            {
                Array.Clear(x, 0, result.Length);
            }
            else if (scale == 1.0)
            {
                x.CopyTo(result, 0);
            }
            else
            {
                for (int i = 0; i < result.Length; i++)
                {
                    result[i] = scale * x[i];
                }
            }


        }

        #endregion Methods
    }
}
