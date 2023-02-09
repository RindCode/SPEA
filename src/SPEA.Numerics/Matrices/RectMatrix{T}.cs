// ==================================================================================================
// <copyright file="RectMatrix{T}.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Numerics.Matrices
{
    using System.Numerics;
    using SPEA.Numerics.Matrices.Storage;

    /// <summary>
    /// Represents a rectangular matrix.
    /// </summary>
    /// <typeparam name="T">Matrix data type.</typeparam>
    public class RectMatrix<T> : RectMatrixBase<T>
        where T : INumber<T>
    {
        #region Fields

        private StorageBase<T> _storage;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RectMatrix{T}"/> class.
        /// </summary>
        /// <param name="rows">The number of rows.</param>
        /// <param name="columns">The number of columns.</param>
        /// <param name="order">The order type.</param>
        public RectMatrix(int rows, int columns, MatrixDataOrderType order = MatrixDataOrderType.ColumMajor)
        {
            switch (order)
            {
                case MatrixDataOrderType.RowMajor:
                    _storage = new RowMajorStorage<T>(rows, columns);
                    break;
                case MatrixDataOrderType.ColumMajor:
                    _storage = new ColumnMajorStorage<T>(rows, columns);
                    break;
                default:
                    throw new NotSupportedException($"The specified matrix order type is not supported: {order}");
            }
        }

        #endregion Constructors

        #region Properties

        /// <inheritdoc/>
        protected override StorageBase<T> Storage
        {
            get => _storage;
            init
            {
                _storage = value;
            }
        }

        #endregion Properties

        #region Methods

        /// <inheritdoc/>
        public override T At(int row, int column)
        {
            return Storage.At(row, column);
        }

        /// <inheritdoc/>
        public override void At(int row, int column, T value)
        {
            Storage.At(row, column, value);
        }

        #endregion Methods
    }
}
