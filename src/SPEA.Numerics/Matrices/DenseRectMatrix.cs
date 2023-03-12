// ==================================================================================================
// <copyright file="DenseRectMatrix.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Numerics.Matrices
{
    using SPEA.Numerics.Matrices.Builder;
    using SPEA.Numerics.Matrices.Storage;

    /// <summary>
    /// Represents a rectangular matrix.
    /// </summary>
    public class DenseRectMatrix : DenseMatrix
    {
        #region Fields

        private static readonly RectMatrixBuilder _matrixBuilder = new RectMatrixBuilder();

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DenseRectMatrix"/> class.
        /// </summary>
        /// <remarks>
        /// The default order is <see cref="MatrixDataOrderType.ColumMajor"/>.
        /// </remarks>
        /// <param name="rows">The number of rows.</param>
        /// <param name="columns">The number of columns.</param>
        /// <param name="order">The order type.</param>
        public DenseRectMatrix(int rows, int columns, MatrixDataOrderType order = MatrixDataOrderType.ColumMajor)
            : base(rows, columns, order)
        {
            // Blank.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DenseRectMatrix"/> class.
        /// </summary>
        /// <param name="storage">The matrix storage used for creation.</param>
        public DenseRectMatrix(MatrixStorage storage)
            : base(storage)
        {
            // Blank.
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets a matrix builder instance.
        /// </summary>
        public override RectMatrixBuilder Build => _matrixBuilder;

        #endregion Properties

        #region Methods

        #endregion Methods
    }
}
