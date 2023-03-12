// ==================================================================================================
// <copyright file="DenseMatrix.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Numerics.Matrices
{
    using SPEA.Numerics.Matrices.Storage;

    /// <summary>
    /// Represents a base class for dense matrices.
    /// </summary>
    public abstract partial class DenseMatrix : Matrix
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DenseMatrix"/> class.
        /// </summary>
        /// <remarks>
        /// The default order is <see cref="MatrixDataOrderType.ColumMajor"/>.
        /// </remarks>
        /// <param name="rows">The number of rows.</param>
        /// <param name="columns">The number of columns.</param>
        /// <param name="order">The order type.</param>
        public DenseMatrix(int rows, int columns, MatrixDataOrderType order = MatrixDataOrderType.ColumMajor)
            : base(rows, columns, order)
        {
            // Blank.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DenseMatrix"/> class.
        /// </summary>
        /// <param name="storage">The matrix storage used for creation.</param>
        public DenseMatrix(MatrixStorage storage)
            : base(storage)
        {
            // Blank.
        }

        #endregion Constructors
    }
}
