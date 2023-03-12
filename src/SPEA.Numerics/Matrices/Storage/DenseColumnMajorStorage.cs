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
