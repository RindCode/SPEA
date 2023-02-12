// ==================================================================================================
// <copyright file="StorageBase.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Numerics.Matrices.Storage
{
    /// <summary>
    /// Represents a base class for matrix storages.
    /// </summary>
    public abstract class StorageBase : IEquatable<StorageBase>
    {
        #region Fields

        private readonly int _rows;
        private readonly int _columns;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StorageBase"/> class.
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

            _rows = rows;
            _columns = columns;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the number of matrix rows.
        /// </summary>
        public int RowCount => _rows;

        /// <summary>
        /// Gets the number of matrix columns.
        /// </summary>
        public int ColumnCount => _columns;

        #endregion Properties

        #region Indexers

        /// <summary>
        /// Gets or sets a value at the given row and column.
        /// </summary>
        /// <param name="row">The zero-based row index.</param>
        /// <param name="column">The zero-based column index.</param>
        /// <returns>The requested element.</returns>
        public double this[int row, int column]
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
        public abstract double At(int row, int column);

        /// <summary>
        /// Sets the element.
        /// </summary>
        /// <param name="row">The row index.</param>
        /// <param name="column">The column index.</param>
        /// <param name="value">The value to be set.</param>
        public abstract void At(int row, int column, double value);

        /// <summary>
        /// Sets all matrix entries using a defined fill function.
        /// </summary>
        /// <remarks>
        /// This method takes into acccount the storage order type.
        /// </remarks>
        /// <param name="func">Fill function.</param>
        public virtual void Fill(Func<int, int, double> func)
        {
            if (func == null)
            {
                throw new ArgumentNullException(nameof(func));
            }

            for (int r = 0; r < RowCount; r++)
            {
                for (int c = 0; c < ColumnCount; c++)
                {
                    this[r, c] = func(r, c);
                }
            }
        }

        /// <inheritdoc/>
        public bool Equals(StorageBase? other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }
            else
            {
                return EqualsInternal(this, other);
            }
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            return Equals(obj as StorageBase);
        }

        /// <inheritdoc/>
        /// <remarks>
        /// Always throws <see cref="NotImplementedException"/>.
        /// </remarks>
        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        // Performs an equality complete check.
        private static bool EqualsInternal(StorageBase A, StorageBase B)
        {
            if (A == null)
            {
                throw new ArgumentNullException(nameof(A));
            }

            if (B == null)
            {
                throw new ArgumentNullException(nameof(B));
            }

            if (A.RowCount != B.RowCount)
            {
                return false;
            }

            if (A.ColumnCount != B.ColumnCount)
            {
                return false;
            }

            // The order doesn't matter, we check all elements.
            for (int r = 0; r < A.RowCount; r++)
            {
                for (int c = 0; c < A.ColumnCount; c++)
                {
                    if (!A[r, c].Equals(B[r, c]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        #endregion Methods
    }
}
