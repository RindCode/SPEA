// ==================================================================================================
// <copyright file="DenseRectMatrix.Operators.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Numerics.Matrices
{
    /// <summary>
    /// Represents a rectangular matrix.
    /// </summary>
    public partial class DenseRectMatrix : DenseMatrix
    {
        #region Methods

        /// <summary>
        /// Returns <see langword="true"/> if the left matrix is equal to the right one,
        /// otherwise returns <see langword="false"/>.
        /// </summary>
        /// <param name="left">The left matrix to compare.</param>
        /// <param name="right">The right matrix to compare.</param>
        /// <returns><see langword="true"/> if the matrices are equal, otherwise returns <see langword="false"/>.</returns>
        public static bool operator ==(DenseRectMatrix left, DenseRectMatrix right) => left.Equals(right);

        /// <summary>
        /// Returns <see langword="true"/> if the left matrix is NOT equal to the right one,
        /// otherwise returns <see langword="false"/>.
        /// </summary>
        /// <param name="left">The left matrix to compare.</param>
        /// <param name="right">The right matrix to compare.</param>
        /// <returns><see langword="true"/> if the matrices are NOT equal, otherwise returns <see langword="false"/>.</returns>
        public static bool operator !=(DenseRectMatrix left, DenseRectMatrix right) => !(left == right);

        /// <summary>
        /// Returns a matrix containing the same values as <paramref name="right"/>.
        /// </summary>
        /// <remarks>
        /// A new matrix object will be returned resulting in a memory allocation.
        /// </remarks>
        /// <param name="right">The right matrix to add.</param>
        /// <returns>A new resulting matrix.</returns>
        public static DenseRectMatrix operator +(DenseRectMatrix right) => right.DeepCopy();

        /// <summary>
        /// Adds a scalar to each element of the matrix.
        /// </summary>
        /// <param name="left">The matrix a scalar will be added to.</param>
        /// <param name="right">The scalar to be added.</param>
        /// <returns>A new resulting matrix.</returns>
        public static DenseRectMatrix operator +(DenseRectMatrix left, double right) => (DenseRectMatrix)left.Add(right);

        /// <summary>
        /// Adds a scalar to each element of the matrix.
        /// </summary>
        /// <param name="left">The matrix a scalar will be added to.</param>
        /// <param name="right">The scalar to be added.</param>
        /// <returns>A new resulting matrix.</returns>
        public static DenseRectMatrix operator +(double left, DenseRectMatrix right) => (DenseRectMatrix)right.Add(left);

        /// <summary>
        /// Adds two matrices and returns the result.
        /// </summary>
        /// <remarks>
        /// A new matrix object will be returned resulting in a memory allocation.
        /// </remarks>
        /// <param name="left">The left matrix to add.</param>
        /// <param name="right">The right matrix to add.</param>
        /// <returns>A new resulting matrix.</returns>
        public static DenseRectMatrix operator +(DenseRectMatrix left, DenseRectMatrix right) => (DenseRectMatrix)left.Add(right);

        /// <summary>
        /// Negates each element of the matrix.
        /// </summary>
        /// <remarks>
        /// A new matrix object will be returned resulting in a memory allocation.
        /// </remarks>
        /// <param name="right">The right matrix to add.</param>
        /// <returns>A new resulting matrix.</returns>
        public static DenseRectMatrix operator -(DenseRectMatrix right) => (DenseRectMatrix)right.Negate();

        /// <summary>
        /// Subtracts a scalar from each element of the matrix.
        /// </summary>
        /// <param name="left">The matrix a scalar will be added to.</param>
        /// <param name="right">The scalar to be added.</param>
        /// <returns>A new resulting matrix.</returns>
        public static DenseRectMatrix operator -(DenseRectMatrix left, double right) => (DenseRectMatrix)left.Subtract(right);

        /// <summary>
        /// Subtracts two matrices and returns the result.
        /// </summary>
        /// <remarks>
        /// A new matrix object will be returned resulting in a memory allocation.
        /// </remarks>
        /// <param name="left">The left matrix to add.</param>
        /// <param name="right">The right matrix to add.</param>
        /// <returns>A new resulting matrix.</returns>
        public static DenseRectMatrix operator -(DenseRectMatrix left, DenseRectMatrix right) => (DenseRectMatrix)left.Subtract(right);

        /// <summary>
        /// Multiplies the matrix by a scalar and returns a new resulting matrix.
        /// </summary>
        /// <param name="left">The matrix to multiply.</param>
        /// <param name="right">The scalar to multiply the matrix by.</param>
        /// <returns>A new resulting matrix.</returns>
        public static DenseRectMatrix operator *(DenseRectMatrix left, double right) => (DenseRectMatrix)left.Multiply(right);

        /// <summary>
        /// Multiplies the matrix by a scalar and returns a new resulting matrix.
        /// </summary>
        /// <param name="left">The scalar to multiply the matrix by.</param>
        /// <param name="right">The matrix to multiply.</param>
        /// <returns>A new resulting matrix.</returns>
        public static DenseRectMatrix operator *(double left, DenseRectMatrix right) => (DenseRectMatrix)right.Multiply(left);

        /// <summary>
        /// Multiplies two matrices and returns a new resulting matrix.
        /// </summary>
        /// <param name="left">The left matrix to multiply.</param>
        /// <param name="right">The right matrix to multiply.</param>
        /// <returns>A new resulting matrix.</returns>
        public static DenseRectMatrix operator *(DenseRectMatrix left, DenseRectMatrix right) => (DenseRectMatrix)right.Multiply(left);

        #endregion Methods
    }
}
