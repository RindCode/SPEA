// ==================================================================================================
// <copyright file="ColumnMajorStorage{T}.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Numerics.Matrices.Storage
{
    using System.Numerics;

    /// <summary>
    /// Represents a column-major matrix storage type.
    /// </summary>
    /// <typeparam name="T">Matrix data type.</typeparam>
    public sealed class ColumnMajorStorage<T> : StorageBase<T>
        where T : INumber<T>
    {
        #region Fields

        private readonly T[] _data;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnMajorStorage{T}"/> class.
        /// </summary>
        /// <param name="rows">The number of rows.</param>
        /// <param name="columns">The number of columns.</param>
        public ColumnMajorStorage(int rows, int columns)
            : base(rows, columns)
        {
            _data = new T[rows * columns];
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the raw matrix data stored as a single-dimension array.
        /// </summary>
        public T[] Data => _data;

        #endregion Properties

        #region Methods

        /// <inheritdoc/>
        public override T At(int row, int column)
        {
            return Data[(column * RowCount) + row];
        }

        /// <inheritdoc/>
        public override void At(int row, int column, T value)
        {
            Data[(column * RowCount) + row] = value;
        }

        #endregion Methods
    }
}
