// ==================================================================================================
// <copyright file="MatrixBase{T}.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Numerics.Matrices
{
    using System.Numerics;
    using System.Runtime.CompilerServices;
    using SPEA.Numerics.Matrices.Storage;

    /// <summary>
    /// Specifies the supported ordering schemes.
    /// </summary>
    public enum MatrixDataOrderType
    {
        /// <summary>
        /// Row-major order.
        /// </summary>
        RowMajor,

        /// <summary>
        /// Column-major order.
        /// </summary>
        ColumMajor,
    }

    /// <summary>
    /// Represents a base class for matrices of an arbitrary size.
    /// </summary>
    /// <typeparam name="T">Matrix data type.</typeparam>
    public abstract class MatrixBase<T>
        where T : INumber<T>
    {
        #region Properties

        /// <summary>
        /// Gets the number of matrix rows.
        /// </summary>
        public virtual int RowCount => Storage.RowCount;

        /// <summary>
        /// Gets the number of matrix columns.
        /// </summary>
        public virtual int ColumnCount => Storage.ColumnCount;

        /// <summary>
        /// Gets the matrix data storage.
        /// </summary>
        protected abstract StorageBase<T> Storage { get; init; }

        #endregion Properties

        #region Indexers

        /// <summary>
        /// Gets or sets a value at the given row and column.
        /// </summary>
        /// <param name="row">The zero-based row index.</param>
        /// <param name="column">The zero-based column index.</param>
        /// <returns>The requested element.</returns>
        public T this[int row, int column]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return At(row, column);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                At(row, column, value);
            }
        }

        #endregion Indexers

        #region Methods

        /// <summary>
        /// Gets the requested element.
        /// </summary>
        /// <param name="row">The row index.</param>
        /// <param name="column">The column index.</param>
        /// <returns>The requested element.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract T At(int row, int column);

        /// <summary>
        /// Sets the element.
        /// </summary>
        /// <param name="row">The row index.</param>
        /// <param name="column">The column index.</param>
        /// <param name="value">The value to be set.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract void At(int row, int column, T value);

        #endregion Methods
    }
}
