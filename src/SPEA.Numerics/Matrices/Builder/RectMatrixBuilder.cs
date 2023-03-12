// ==================================================================================================
// <copyright file="RectMatrixBuilder.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Numerics.Matrices.Builder
{
    using SPEA.Numerics.Matrices.Storage;

    /// <summary>
    /// Represents a builder calss for rectangular matrices.
    /// </summary>
    public class RectMatrixBuilder : MatrixBuilder
    {
        #region Methods

        ////public override DenseRectMatrix OfStorage(MatrixStorage storage)
        ////{
        ////    ArgumentNullException.ThrowIfNull(storage, nameof(storage));

        ////    return new DenseRectMatrix(storage);
        ////}

        /// <inheritdoc/>
        public override DenseRectMatrix SameAs(Matrix example)
        {
            return SameAs(example, example.RowCount, example.ColumnCount);
        }

        /// <inheritdoc/>
        public override DenseRectMatrix SameAs(Matrix example, int rows, int columns)
        {
            ArgumentNullException.ThrowIfNull(example, nameof(example));

            var storageType = example.StorageType;
            switch (storageType)
            {
                case MatrixStorageType.Dense:
                    return Dense(rows, columns, example.OrderType);
                default:
                    throw new NotSupportedException($"Matrix storage type {storageType.GetType().Name} is not supported.");
            }
        }

        /// <inheritdoc/>
        public override DenseRectMatrix SameAs(Matrix example1, Matrix example2, int rows, int columns)
        {
            ArgumentNullException.ThrowIfNull(example1, nameof(example1));
            ArgumentNullException.ThrowIfNull(example2, nameof(example2));

            var orderType = example1.OrderType == example2.OrderType ? example1.OrderType : MatrixDataOrderType.ColumMajor;

            if (example1.StorageType == MatrixStorageType.Dense || example2.StorageType == MatrixStorageType.Dense)
            {
                return Dense(rows, columns, orderType);
            }

            // Fallback to dense representation.
            return Dense(rows, columns, orderType);
        }

        /// <inheritdoc/>
        public override DenseRectMatrix Dense(MatrixStorage storage)
        {
            ArgumentNullException.ThrowIfNull(storage, nameof(storage));

            if (storage.StorageType != MatrixStorageType.Dense)
            {
                throw new NotSupportedException($"The storage type must be {MatrixStorageType.Dense}. Received instead: {storage.GetType().Name}.");
            }

            return new DenseRectMatrix(storage);
        }

        /// <inheritdoc/>
        public override DenseRectMatrix Dense(int rows, int columns, MatrixDataOrderType orderType)
        {
            switch (orderType)
            {
                case MatrixDataOrderType.ColumMajor:
                    return Dense(new DenseColumnMajorStorage(rows, columns));
                case MatrixDataOrderType.RowMajor:
                    return Dense(new DenseRowMajorStorage(rows, columns));
                default:
                    throw new NotSupportedException($"Unsupported matrix storage type.");
            }
        }

        #endregion Methods
    }
}
