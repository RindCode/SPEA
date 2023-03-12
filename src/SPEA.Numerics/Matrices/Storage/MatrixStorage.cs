// ==================================================================================================
// <copyright file="MatrixStorage.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Numerics.Matrices.Storage
{
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
    /// Specifies the storage type and the way how
    /// its data is stored internally.
    /// </summary>
    public enum MatrixStorageType
    {
        /// <summary>
        /// Dense storage type.
        /// </summary>
        Dense,
    }

    /// <summary>
    /// Represents a base class for matrix storages.
    /// </summary>
    public abstract class MatrixStorage : IEquatable<MatrixStorage>
    {
        #region Fields

        private readonly int _rows;
        private readonly int _columns;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MatrixStorage"/> class.
        /// </summary>
        /// <param name="rows">The number of rows.</param>
        /// <param name="columns">The number of columns.</param>
        protected MatrixStorage(int rows, int columns)
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
        /// Gets the storage type.
        /// </summary>
        public abstract MatrixStorageType StorageType { get; }

        /// <summary>
        /// Gets the storage data order.
        /// </summary>
        public abstract MatrixDataOrderType OrderType { get; }

        /// <summary>
        /// Gets the raw matrix data stored as a single-dimension array.
        /// </summary>
        public abstract double[] Data { get; }

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
        public void Fill(Func<int, int, double> func)
        {
            if (func == null)
            {
                throw new ArgumentNullException(nameof(func));
            }

            for (int r = 0; r < RowCount; r++)
            {
                for (int c = 0; c < ColumnCount; c++)
                {
                    At(r, c, func(r, c));
                }
            }
        }

        /// <summary>
        /// Copies values of the current storage to the <paramref name="target"/>.
        /// </summary>
        /// <param name="target">A target storage values are copied to.</param>
        /// <exception cref="ArgumentOutOfRangeException">Is thrown when the rows and columns have different sizes.</exception>
        public void CopyTo(MatrixStorage target)
        {
            ArgumentNullException.ThrowIfNull(target, nameof(target));

            if (ReferenceEquals(this, target))
            {
                return;
            }

            ThrowArgumentOutOfRangeExceptionIfDimMismatch(target);

            CopyToUnchecked(target);
        }

        /// <summary>
        /// Applies a function to each value of the current storage.
        /// </summary>
        /// <param name="function">A function to be applied.</param>
        public void MapInplace(Func<double, double> function)
        {
            for (int r = 0; r < RowCount; r++)
            {
                for (int c = 0; c < ColumnCount; c++)
                {
                    At(r, c, function(At(r, c)));
                }
            }
        }

        /// <summary>
        /// Applies a function to each value of the current storage
        /// and sets <paramref name="result"/> values using the function returned value.
        /// </summary>
        /// <param name="function">A function to be applied.</param>
        /// <param name="target">The target storage the function is applied to.</param>
        public void MapTo(Func<double, double> function, MatrixStorage target)
        {
            ArgumentNullException.ThrowIfNull(target, nameof(target));
            ThrowArgumentOutOfRangeExceptionIfDimMismatch(target);

            MapToUnchecked(function, target);
        }

        /// <summary>
        /// Applies a function to each value pair of two storages (the current one and <paramref name="other"/>,
        /// and sets <paramref name="target"/> values using the function returned value.
        /// </summary>
        /// <param name="function">A function to be applied.</param>
        /// <param name="other">Another storage used for mapping.</param>
        /// <param name="target">The target storage the function is applied to.</param>
        /// <exception cref="ArgumentOutOfRangeException">Is thrown when the rows and columns have different sizes.</exception>
        public void Map2To(Func<double, double, double> function, MatrixStorage other, MatrixStorage target)
        {
            ArgumentNullException.ThrowIfNull(other, nameof(other));
            ArgumentNullException.ThrowIfNull(target, nameof(target));
            ThrowArgumentOutOfRangeExceptionIfDimMismatch(other);
            ThrowArgumentOutOfRangeExceptionIfDimMismatch(target);

            Map2Unchecked(function, other, target);
        }

        /// <summary>
        /// Copies values of the current storage to the <paramref name="target"/>.
        /// </summary>
        /// <remarks>
        /// This method exists for optimization purposes and will NOT check storages boundaries.
        /// </remarks>
        /// <param name="target">A target storage values are copied to.</param>
        /// <exception cref="ArgumentOutOfRangeException">Is thrown when the rows and columns have different sizes.</exception>
        internal void CopyToUnchecked(MatrixStorage target)
        {
            for (int r = 0; r < RowCount; r++)
            {
                for (int c = 0; c < ColumnCount; c++)
                {
                    target.At(r, c, At(r, c));
                }
            }
        }

        /// <summary>
        /// Applies a function to each value of the current storage
        /// and sets <paramref name="result"/> values using the function returned value.
        /// </summary>
        /// <remarks>
        /// This method exists for optimization purposes and will NOT check storages boundaries.
        /// </remarks>
        /// <param name="function">A function to be applied.</param>
        /// <param name="target">The target storage the function is applied to.</param>
        internal void MapToUnchecked(Func<double, double> function, MatrixStorage target)
        {
            for (int r = 0; r < RowCount; r++)
            {
                for (int c = 0; c < ColumnCount; c++)
                {
                    target.At(r, c, function(At(r, c)));
                }
            }
        }

        /// <summary>
        /// Applies a function to each value pair of two storages (the current one and <paramref name="other"/>),
        /// and sets <paramref name="result"/> values using the function returned value.
        /// </summary>
        /// <remarks>
        /// This method exists for optimization purposes and will NOT check storages boundaries.
        /// </remarks>
        /// <param name="function">A function to be applied.</param>
        /// <param name="other">Another storage used for mapping.</param>
        /// <param name="result">The resulting storage the function is applied to.</param>
        internal void Map2Unchecked(Func<double, double, double> function, MatrixStorage other, MatrixStorage result)
        {
            for (int r = 0; r < RowCount; r++)
            {
                for (int c = 0; c < ColumnCount; c++)
                {
                    result.At(r, c, function(At(r, c), other.At(r, c)));
                }
            }
        }

        #region IEquatable interface

        /// <inheritdoc/>
        public bool Equals(MatrixStorage? other)
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
            return Equals(obj as MatrixStorage);
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
        private static bool EqualsInternal(MatrixStorage left, MatrixStorage right)
        {
            if (left == null)
            {
                throw new ArgumentNullException(nameof(left));
            }

            if (right == null)
            {
                throw new ArgumentNullException(nameof(right));
            }

            if (left.RowCount != right.RowCount)
            {
                return false;
            }

            if (left.ColumnCount != right.ColumnCount)
            {
                return false;
            }

            // The order doesn't matter, we check all elements.
            for (int r = 0; r < left.RowCount; r++)
            {
                for (int c = 0; c < left.ColumnCount; c++)
                {
                    if (!left[r, c].Equals(right[r, c]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        // Throws ArgumentOutOfRangeException if rows and columns don't match.
        private void ThrowArgumentOutOfRangeExceptionIfDimMismatch(MatrixStorage target)
        {
            if (RowCount != target.RowCount || ColumnCount != target.ColumnCount)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(target),
                    $"Incompatible storage dimensions: this={RowCount}x{ColumnCount}, {nameof(target)}={target.RowCount}x{target.ColumnCount}");
            }
        }

        #endregion IEquatable interface

        #endregion Methods
    }
}
