// ==================================================================================================
// <copyright file="DenseColumnMajorStorage.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Numerics.Matrices.Storage
{
    /// <summary>
    /// Represents a column-major matrix storage type.
    /// </summary>
    public sealed class DenseColumnMajorStorage : MatrixStorage
    {
        #region Fields

        private static readonly MatrixStorageType _storageType = MatrixStorageType.Dense;
        private static readonly MatrixDataOrderType _orderType = MatrixDataOrderType.ColumMajor;
        private readonly double[] _data;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DenseColumnMajorStorage"/> class.
        /// </summary>
        /// <param name="rows">The number of rows.</param>
        /// <param name="columns">The number of columns.</param>
        public DenseColumnMajorStorage(int rows, int columns)
            : base(rows, columns)
        {
            _data = new double[rows * columns];
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

        #region Initializers

        /// <summary>
        /// Creates a new instance of <see cref="DenseColumnMajorStorage"/> using the matrix source.
        /// </summary>
        /// <param name="source">A matrix used as a source.</param>
        /// <returns>A new storage.</returns>
        public static DenseColumnMajorStorage OfMatrix(Matrix source)
        {
            var result = new DenseColumnMajorStorage(source.RowCount, source.ColumnCount);
            source.Storage.CopyToUnchecked(result);
            return result;
        }

        /// <summary>
        /// Creates a new instance of <see cref="DenseColumnMajorStorage"/> using the provided value.
        /// All elements will be set to <paramref name="value"/>.
        /// </summary>
        /// <param name="rows">The number of rows.</param>
        /// <param name="columns">The number of columns.</param>
        /// <param name="value">The value all elements will be assigned to.</param>
        /// <returns>A new storage.</returns>
        public static DenseColumnMajorStorage OfValue(int rows, int columns, double value)
        {
            var result = new DenseColumnMajorStorage(rows, columns);
            var data = result.Data;
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = value;
            }

            return result;
        }

        /// <summary>
        /// Creates a new instance of <see cref="DenseColumnMajorStorage"/> using the provided function.
        /// </summary>
        /// <param name="rows">The number of rows.</param>
        /// <param name="columns">The number of columns.</param>
        /// <param name="function">A function that will be applied to all elements of a new storage.</param>
        /// <returns>A new storage.</returns>
        public static DenseColumnMajorStorage OfInit(int rows, int columns, Func<int, int, double> function)
        {
            var result = new DenseColumnMajorStorage(rows, columns);
            var data = result.Data;
            int i = 0;
            for (int c = 0; c < columns; c++)
            {
                for (int r = 0; r < rows; r++)
                {
                    data[i++] = function(r, c);
                }
            }

            return result;
        }

        /// <summary>
        /// Creates a new instance of <see cref="DenseColumnMajorStorage"/> using the provided function.
        /// The function will be applied only to the main diagonal.
        /// </summary>
        /// <param name="rows">The number of rows.</param>
        /// <param name="columns">The number of columns.</param>
        /// <param name="function">A function that will be applied to all elements of a new storage.</param>
        /// <returns>A new storage.</returns>
        public static DenseColumnMajorStorage OfDiagonalInit(int rows, int columns, Func<int, double> function)
        {
            var result = new DenseColumnMajorStorage(rows, columns);
            var data = result.Data;
            int index = 0;
            int stride = rows + 1;
            for (int i = 0; i < Math.Min(rows, columns); i++)
            {
                data[index] = function(i);
                index += stride;
            }

            return result;
        }

        #endregion Initializers

        #region Methods

        /// <inheritdoc/>
        public override double At(int row, int column)
        {
            return Data[(column * RowCount) + row];
        }

        /// <inheritdoc/>
        public override void At(int row, int column, double value)
        {
            Data[(column * RowCount) + row] = value;
        }

        #endregion Methods
    }
}
