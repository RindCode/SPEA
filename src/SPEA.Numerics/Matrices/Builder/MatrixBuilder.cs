// ==================================================================================================
// <copyright file="MatrixBuilder.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Numerics.Matrices.Builder
{
    using SPEA.Numerics.Matrices.Storage;

    /// <summary>
    /// Represents a base class for matrix builders.
    /// </summary>
    public abstract class MatrixBuilder
    {
        #region Methods

        /////// <summary>
        /////// Creates a new matrix a given storage instance.
        /////// The storage instance will be used directy
        /////// </summary>
        /////// <param name="storage"></param>
        /////// <returns></returns>
        ////public abstract Matrix OfStorage(MatrixStorage storage);

        /// <summary>
        /// Creates a new empty (zeroed) matrix of the same kind and dimension as the provided example.
        /// </summary>
        /// <param name="example">The example matrix.</param>
        /// <returns>A new matrix of the same kind and dimension.</returns>
        public virtual Matrix SameAs(Matrix example)
        {
            return SameAs(example, example.RowCount, example.ColumnCount);
        }

        /// <summary>
        /// Creates a new empty (zeroed) matrix of the same kind and dimension as the provided example and parameters.
        /// </summary>
        /// <param name="example">The example matrix.</param>
        /// <param name="rows">The required number of rows of a resulting matrix.</param>
        /// <param name="columns">The required number of columns of a resulting matrix.</param>
        /// <returns>A new matrix of the same kind and the requested dimension.</returns>
        public virtual Matrix SameAs(Matrix example, int rows, int columns)
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

        /// <summary>
        /// Creates a new empty (zeroed) matrix of a specified size and with the type closest to both examples provided.
        /// </summary>
        /// <remarks>
        /// If both example use the same storage type, the resulting matrix will use it as well.
        /// Otherwise <see cref="MatrixDataOrderType.ColumMajor"/> will be used as a default one.
        /// </remarks>
        /// <param name="example1">The first matrix used as an example.</param>
        /// <param name="example2">The second matrix used as an example.</param>
        /// <param name="rows">The required number of rows of a resulting matrix.</param>
        /// <param name="columns">The required number of columns of a resulting matrix.</param>
        /// <returns>A new matrix of the requested size.</returns>
        public virtual Matrix SameAs(Matrix example1, Matrix example2, int rows, int columns)
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

        /// <summary>
        /// Creates a new dense matrix using the initialized storage instance.
        /// The storage will NOT be copied and its reference will be used directly.
        /// </summary>
        /// <param name="storage">A storage reference.</param>
        /// <returns>A new dense matrix with a given storage.</returns>
        /// <exception cref="NotSupportedException">Is thrown when the storage type is not supported.</exception>
        public abstract Matrix Dense(MatrixStorage storage);

        /// <summary>
        /// Creates a new dense matrix using a given number of rows and columns and storage order type.
        /// </summary>
        /// <param name="rows">The required number of rows of a resulting matrix.</param>
        /// <param name="columns">The required number of columns of a resulting matrix.</param>
        /// <param name="orderType">The order type.</param>
        /// <returns>A new dense matrix of the requested size.</returns>
        /// <exception cref="NotSupportedException">Is thrown if the selected data order is not supported.</exception>
        public virtual Matrix Dense(int rows, int columns, MatrixDataOrderType orderType = MatrixDataOrderType.ColumMajor)
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

        /// <summary>
        /// Creates a new identity dense matrix using a given number of rows and columns and storage order type.
        /// All elements will be set to zero, while the main diagonal elements will be set to 1.0.
        /// </summary>
        /// <param name="rows">The required number of rows of a resulting matrix.</param>
        /// <param name="columns">The required number of columns of a resulting matrix.</param>
        /// <param name="orderType">The order type.</param>
        /// <returns>A new dense matrix of the requested size.</returns>
        /// <exception cref="NotSupportedException">Is thrown if the selected data order is not supported.</exception>
        public virtual Matrix DenseIdentity(int rows, int columns, MatrixDataOrderType orderType = MatrixDataOrderType.ColumMajor)
        {
            switch (orderType)
            {
                case MatrixDataOrderType.ColumMajor:
                    return Dense(DenseColumnMajorStorage.OfDiagonalInit(rows, columns, _ => 1.0d));
                case MatrixDataOrderType.RowMajor:
                    return Dense(DenseRowMajorStorage.OfDiagonalInit(rows, columns, _ => 1.0d));
                default:
                    throw new NotSupportedException($"Unsupported matrix storage type.");
            }
        }

        #endregion Methods
    }
}
