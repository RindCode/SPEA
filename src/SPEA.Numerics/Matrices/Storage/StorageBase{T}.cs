// ==================================================================================================
// <copyright file="StorageBase{T}.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Numerics.Matrices.Storage
{
    using System.Numerics;

    /// <summary>
    /// Represents a base class for matrix storages.
    /// </summary>
    /// <typeparam name="T">Matrix data type.</typeparam>
    public abstract class StorageBase<T>
        where T : INumber<T>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StorageBase{T}"/> class.
        /// </summary>
        /// <param name="rows">The number of rows.</param>
        /// <param name="columns">The number of columns.</param>
        protected StorageBase(int rows, int columns)
        {
            if (rows <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(rows), "The number of rows of a matrix must be non-negative.");
            }

            if (columns <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(columns), "The number of columns of a matrix must be non-negative.");
            }

            RowCount = rows;
            ColumnCount = columns;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the number of matrix rows.
        /// </summary>
        public int RowCount { get; private set; }

        /// <summary>
        /// Gets the number of matrix columns.
        /// </summary>
        public int ColumnCount { get; private set; }

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
            get
            {
                return At(row, column);
            }

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
        public abstract T At(int row, int column);

        /// <summary>
        /// Sets the element.
        /// </summary>
        /// <param name="row">The row index.</param>
        /// <param name="column">The column index.</param>
        /// <param name="value">The value to be set.</param>
        public abstract void At(int row, int column, T value);

        #endregion Methods
    }
}
