// ==================================================================================================
// <copyright file="DenseSquareMatrix.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Numerics.Matrices
{
    using SPEA.Numerics.Matrices.Builder;
    using SPEA.Numerics.Matrices.Storage;

    /// <summary>
    /// Represents a square matrix.
    /// </summary>
    public class DenseSquareMatrix : DenseRectMatrix
    {
        #region Fields

        private static readonly RectMatrixBuilder _matrixBuilder = new RectMatrixBuilder();
        private readonly int _dimension;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DenseSquareMatrix"/> class.
        /// </summary>
        /// <param name="dimension">The square matrix dimension (dim = rows = columns).</param>
        /// <param name="order">The order type.</param>
        public DenseSquareMatrix(int dimension, MatrixDataOrderType order = MatrixDataOrderType.ColumMajor)
            : base(dimension, dimension, order)
        {
            // Blank.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DenseSquareMatrix"/> class.
        /// </summary>
        /// <param name="storage">The matrix storage used for creation.</param>
        public DenseSquareMatrix(MatrixStorage storage)
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

        /// <summary>
        /// Gets the square matrix dimension.
        /// </summary>
        public int Dimension => _dimension;

        #endregion Properties

        #region Methods

        /// <summary>
        /// Creates a new identity matrix of a given dimension.
        /// </summary>
        /// <param name="dimension">The square matrix dimension.</param>
        /// <returns>A square identity matrix.</returns>
        public static DenseSquareMatrix GetIdentity(int dimension)
        {
            var matrix = new DenseSquareMatrix(dimension);
            for (int i = 0; i < matrix.RowCount; i++)
            {
                matrix[i, i] = 1.0;
            }

            return matrix;
        }

        #endregion Methods
    }
}
