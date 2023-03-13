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
            return (DenseRectMatrix)base.SameAs(example, example.RowCount, example.ColumnCount);
        }

        /// <inheritdoc/>
        public override DenseRectMatrix SameAs(Matrix example, int rows, int columns)
        {
            return (DenseRectMatrix)base.SameAs(example, rows, columns);
        }

        /// <inheritdoc/>
        public override DenseRectMatrix SameAs(Matrix example1, Matrix example2, int rows, int columns)
        {
            return (DenseRectMatrix)base.SameAs(example1, example2, rows, columns);
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
            return (DenseRectMatrix)base.Dense(rows, columns, orderType);
        }

        /// <inheritdoc/>
        public override Matrix DenseIdentity(int rows, int columns, MatrixDataOrderType orderType)
        {
            return (DenseRectMatrix)base.DenseIdentity(rows, columns, orderType);
        }

        #endregion Methods
    }
}
