// ==================================================================================================
// <copyright file="Matrix.Operators.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Numerics.Matrices
{
    /// <summary>
    /// Specifies the supported ordering schemes.
    /// </summary>
    public abstract partial class Matrix : IEquatable<Matrix>
    {
        #region Methods

        /// <summary>
        /// Sums two matrices and returns the result.
        /// </summary>
        /// <remarks>
        /// A new matrix object will be returned resulting in memory allocation.
        /// </remarks>
        /// <param name="left">The left matrix to add.</param>
        /// <param name="right">The right matrix to add.</param>
        /// <returns>A new resulting matrix.</returns>
        public static Matrix operator +(Matrix left, Matrix right) => left.Add(right);

        /// <summary>
        /// Returns a matrix containing the same values as <paramref name="right"/>.
        /// </summary>
        /// <remarks>
        /// A new matrix object will be returned resulting in memory allocation.
        /// </remarks>
        /// <param name="left">The matrix to add.</param>
        /// <param name="right">The right matrix to add.</param>
        /// <returns>A new resulting matrix.</returns>
        public static Matrix operator +(Matrix right) => right.DeepCopy();

        /// <summary>
        /// Adds a scalar to each element of the matrix.
        /// </summary>
        /// <param name="left">The matrix a scalar will be added to.</param>
        /// <param name="right">The scalar to be added.</param>
        /// <returns>A new resulting matrix.</returns>
        public static Matrix operator +(Matrix left, double right) => left.Add(right);

        /// <summary>
        /// Adds a scalar to each element of the matrix.
        /// </summary>
        /// <param name="left">The matrix a scalar will be added to.</param>
        /// <param name="right">The scalar to be added.</param>
        /// <returns>A new resulting matrix.</returns>
        public static Matrix operator +(double left, Matrix right) => right.Add(left);

        /// <summary>
        /// Adds another matrix to the current one and returns a new resulting matrix.
        /// </summary>
        /// <param name="other">A matrix to add.</param>
        /// <returns>The resulting matrix.</returns>
        public Matrix Add(Matrix other)
        {
            if (RowCount != other.RowCount || ColumnCount != other.ColumnCount)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(other),
                    $"Cannot add matrices of different dimensions: this={RowCount}x{ColumnCount}, {nameof(other)}={other.RowCount}x{other.ColumnCount}");
            }

            var result = Build.SameAs(this, other, RowCount, ColumnCount);
            DoAdd(other, result);
            return result;
        }

        /// <summary>
        /// Adds a scalar to the current matrix and returns a new resulting matrix.
        /// </summary>
        /// <param name="scalar">A scalar to add.</param>
        /// <returns>The resulting matrix.</returns>
        public Matrix Add(double scalar)
        {
            if (scalar == 0.0d)
            {
                return DeepCopy();
            }

            var result = Build.SameAs(this);
            DoAdd(scalar, result);
            return result;
        }

        #endregion Methods
    }
}
