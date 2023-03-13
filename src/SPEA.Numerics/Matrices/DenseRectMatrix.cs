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
    public partial class DenseRectMatrix : DenseMatrix
    {
        #region Fields

        private static readonly RectMatrixBuilder _matrixBuilder = new RectMatrixBuilder();

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DenseRectMatrix"/> class.
        /// </summary>
        /// <param name="storage">The matrix storage used for creation.</param>
        public DenseRectMatrix(MatrixStorage storage)
            : base(storage)
        {
            // Blank.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DenseRectMatrix"/> class.
        /// </summary>
        /// <remarks>
        /// The default dimension is <see cref="MatrixDataOrderType.ColumMajor"/>.
        /// </remarks>
        /// <param name="rows">The number of rows.</param>
        /// <param name="columns">The number of columns.</param>
        /// <param name="order">The dimension type.</param>
        public DenseRectMatrix(int rows, int columns, MatrixDataOrderType order = MatrixDataOrderType.ColumMajor)
            : base(rows, columns, order)
        {
            // Blank.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DenseRectMatrix"/> class.
        /// </summary>
        /// <param name="dimension">The dimension value (rowsNum = columnsNum).</param>
        /// <param name="order">The dimension type.</param>
        public DenseRectMatrix(int dimension, MatrixDataOrderType order = MatrixDataOrderType.ColumMajor)
            : base(dimension, dimension, order)
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
        /// Gets a value indicating whether the current matrix is square.
        /// </summary>
        public bool IsSquare => RowCount == ColumnCount;

        #endregion Properties

        #region Methods

        #region Initializers

        /// <summary>
        /// Creates a new square indentity matrix, where all elements are set to zero, and
        /// the main diaginal elements are set to 1.0.
        /// </summary>
        /// <param name="dimension">The dimension value (rowsNum = columnsNum).</param>
        /// <param name="order">The dimension type.</param>
        /// <returns>A new identity matrix.</returns>
        /// <exception cref="NotSupportedException">Is thrown if the selected data order is not supported.</exception>
        public static DenseRectMatrix CreateIdentity(int dimension, MatrixDataOrderType order)
        {
            switch (order)
            {
                case MatrixDataOrderType.ColumMajor:
                    return new DenseRectMatrix(DenseColumnMajorStorage.OfDiagonalInit(dimension, dimension, _ => 1.0d));
                case MatrixDataOrderType.RowMajor:
                    return new DenseRectMatrix(DenseRowMajorStorage.OfDiagonalInit(dimension, dimension, _ => 1.0d));
                default:
                    throw new NotSupportedException($"Unsupported matrix storage type.");
            }
        }

        #endregion Initializers

        /// <inheritdoc/>
        public override DenseRectMatrix DeepCopy()
        {
            var result = Build.SameAs(this);
            Storage.CopyToUnchecked(result.Storage);
            return result;
        }

        #endregion Methods
    }
}
