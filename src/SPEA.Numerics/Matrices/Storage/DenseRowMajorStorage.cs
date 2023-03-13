// ==================================================================================================
// <copyright file="DenseRowMajorStorage.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Numerics.Matrices.Storage
{
    /// <summary>
    /// Represents a row-major matrix storage type.
    /// </summary>
    public sealed class DenseRowMajorStorage : MatrixStorage
    {
        #region Fields

        private static readonly MatrixStorageType _storageType = MatrixStorageType.Dense;
        private static readonly MatrixDataOrderType _orderType = MatrixDataOrderType.RowMajor;
        private readonly double[] _data;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DenseRowMajorStorage"/> class.
        /// </summary>
        /// <param name="rows">The number of rows.</param>
        /// <param name="columns">The number of columns.</param>
        public DenseRowMajorStorage(int rows, int columns)
            : base(rows, columns)
        {
            _data = new double[rows * columns];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DenseRowMajorStorage"/> class.
        /// </summary>
        /// <param name="rows">The number of rows.</param>
        /// <param name="columns">The number of columns.</param>
        /// <param name="data">The array representing a data storage.</param>
        public DenseRowMajorStorage(int rows, int columns, double[] data)
            : base(rows, columns)
        {
            _data = data;
        }

        #endregion Constructors

        #region Properties

        /// <inheritdoc/>
        public override MatrixStorageType StorageType => _storageType;

        /// <inheritdoc/>
        public override MatrixDataOrderType OrderType => _orderType;

        /// <inheritdoc/>
        public override double[] Data => _data;

        #endregion Properties

        #region Methods

        #region Initializers

        /// <summary>
        /// Creates a new instance of <see cref="DenseRowMajorStorage"/> using the matrix source.
        /// </summary>
        /// <param name="source">A matrix used as a source.</param>
        /// <returns>A new storage.</returns>
        public static DenseRowMajorStorage OfMatrix(Matrix source)
        {
            var result = new DenseRowMajorStorage(source.RowCount, source.ColumnCount);
            source.Storage.CopyToUnchecked(result);
            return result;
        }

        /// <summary>
        /// Creates a new instance of <see cref="DenseRowMajorStorage"/> using the provided value.
        /// All elements will be set to <paramref name="value"/>.
        /// </summary>
        /// <param name="rows">The number of rows.</param>
        /// <param name="columns">The number of columns.</param>
        /// <param name="value">The value all elements will be assigned to.</param>
        /// <returns>A new storage.</returns>
        public static DenseRowMajorStorage OfValue(int rows, int columns, double value)
        {
            var result = new DenseRowMajorStorage(rows, columns);
            var data = result.Data;
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = value;
            }

            return result;
        }

        /// <summary>
        /// Creates a new instance of <see cref="DenseRowMajorStorage"/> using the provided function.
        /// </summary>
        /// <param name="rows">The number of rows.</param>
        /// <param name="columns">The number of columns.</param>
        /// <param name="function">A function that will be applied to all elements of a new storage.</param>
        /// <returns>A new storage.</returns>
        public static DenseRowMajorStorage OfInit(int rows, int columns, Func<int, int, double> function)
        {
            var result = new DenseRowMajorStorage(rows, columns);
            var data = result.Data;
            int i = 0;
            for (int r = 0; r < columns; r++)
            {
                for (int c = 0; c < rows; c++)
                {
                    data[i++] = function(r, c);
                }
            }

            return result;
        }

        /// <summary>
        /// Creates a new instance of <see cref="DenseRowMajorStorage"/> using the provided function.
        /// The function will be applied only to the main diagonal.
        /// </summary>
        /// <param name="rows">The number of rows.</param>
        /// <param name="columns">The number of columns.</param>
        /// <param name="function">A function that will be applied to all elements of a new storage.</param>
        /// <returns>A new storage.</returns>
        public static DenseRowMajorStorage OfDiagonalInit(int rows, int columns, Func<int, double> function)
        {
            var result = new DenseRowMajorStorage(rows, columns);
            var data = result.Data;
            int index = 0;
            int stride = columns + 1;
            for (int i = 0; i < Math.Min(rows, columns); i++)
            {
                data[index] = function(i);
                index += stride;
            }

            return result;
        }

        #endregion Initializers

        /// <inheritdoc/>
        public override double At(int row, int column)
        {
            return Data[(row * ColumnCount) + column];
        }

        /// <inheritdoc/>
        public override void At(int row, int column, double value)
        {
            Data[(row * ColumnCount) + column] = value;
        }

        #endregion Methods
    }
}
