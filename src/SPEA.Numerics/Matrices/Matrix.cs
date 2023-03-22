// ==================================================================================================
// <copyright file="Matrix.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Numerics.Matrices
{
    using System.Runtime.CompilerServices;
    using SPEA.Numerics.Matrices.Builder;
    using SPEA.Numerics.Matrices.Storage;

    /// <summary>
    /// Represents a base class for matrices of an arbitrary size.
    /// </summary>
    public abstract partial class Matrix : IEquatable<Matrix>
    {
        #region Fields

        private readonly MatrixStorage _storage;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix"/> class.
        /// </summary>
        /// <param name="rows">The number of rows.</param>
        /// <param name="columns">The number of columns.</param>
        /// <param name="order">The order type.</param>
        protected internal Matrix(int rows, int columns, MatrixDataOrderType order = MatrixDataOrderType.ColumMajor)
        {
            // The permitted number of rows and columns is checked within a storage ctor.

            switch (order)
            {
                case MatrixDataOrderType.RowMajor:
                    _storage = new DenseRowMajorStorage(rows, columns);
                    break;
                case MatrixDataOrderType.ColumMajor:
                    _storage = new DenseColumnMajorStorage(rows, columns);
                    break;
                default:
                    throw new NotSupportedException($"The specified matrix order type is not supported: {order}");
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix"/> class.
        /// </summary>
        /// <param name="storage">The matrix storage used for creation.</param>
        protected Matrix(MatrixStorage storage)
        {
            _storage = storage;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets a matrix builder instance.
        /// </summary>
        public abstract MatrixBuilder Builder { get; }

        /// <summary>
        /// Gets the number of matrix rows.
        /// </summary>
        public virtual int RowCount => Storage.RowCount;

        /// <summary>
        /// Gets the number of matrix columns.
        /// </summary>
        public virtual int ColumnCount => Storage.ColumnCount;

        /// <summary>
        /// Gets the storage type.
        /// </summary>
        public MatrixStorageType StorageType => Storage.StorageType;

        /// <summary>
        /// Gets the matrix data ordering scheme.
        /// </summary>
        public MatrixDataOrderType OrderType => Storage.OrderType;

        /// <summary>
        /// Gets the matrix data storage.
        /// </summary>
        public MatrixStorage Storage => _storage;

        /// <summary>
        /// Gets a value indicating whether the storage represents the identity matrix.
        /// </summary>
        /// <remarks>
        /// Accessing this property will result in looping through all matrix elements
        /// to check if they meet the condition.
        /// </remarks>
        public bool IsIdentity => Storage.IsIdentity;

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
        public virtual double At(int row, int column)
        {
            return Storage.At(row, column);
        }

        /// <summary>
        /// Sets the element.
        /// </summary>
        /// <param name="row">The row index.</param>
        /// <param name="column">The column index.</param>
        /// <param name="value">The value to be set.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual void At(int row, int column, double value)
        {
            Storage.At(row, column, value);
        }

        /// <summary>
        /// Creates a deep copy of the matrix.
        /// </summary>
        /// <returns>A deep copy of the matrix.</returns>
        public virtual Matrix DeepCopy()
        {
            var result = Builder.SameAs(this);
            Storage.CopyToUnchecked(result.Storage);
            return result;
        }

        /// <summary>
        /// Copies values of the current matrix to the <paramref name="target"/>.
        /// </summary>
        /// <param name="target">A target matrix values are copied to.</param>
        public void CopyTo(Matrix target)
        {
            Storage.CopyTo(target.Storage);
        }

        /// <summary>
        /// Sets all matrix entries using a defined fill function.
        /// </summary>
        /// <param name="func">Fill function.</param>
        public void Fill(Func<int, int, double> func)
        {
            Storage.Fill(func);
        }

        /// <summary>
        /// Applies a function to each value of the current matrix
        /// and sets <paramref name="target"/> values using the function returned value.
        /// </summary>
        /// <param name="function">A function to be applied.</param>
        /// <param name="target">The target matrix the function is applied to.</param>
        public void MapTo(Func<double, double> function, Matrix target)
        {
            if (ReferenceEquals(this, target))
            {
                Storage.MapInplace(function);
            }
            else
            {
                Storage.MapTo(function, target.Storage);
            }
        }

        /// <summary>
        /// Applies a function to each value pair of two matrices (the current one and <paramref name="other"/>,
        /// and sets <paramref name="target"/> values using the function returned value.
        /// </summary>
        /// <param name="function">A function to be applied.</param>
        /// <param name="other">Another matrix used for mapping.</param>
        /// <param name="target">The target matrix the function is applied to.</param>
        public void Map2To(Func<double, double, double> function, Matrix other, Matrix target)
        {
            Storage.Map2To(function, other.Storage, target.Storage);
        }

        /// <summary>
        /// Transposes the current matrix and saves the result to <paramref name="target"/>.
        /// </summary>
        /// <param name="target">The resulting transposed matrix.</param>
        public void TransposeTo(Matrix target)
        {
            Storage.TransposeTo(target.Storage);
        }

        /// <inheritdoc/>
        public bool Equals(Matrix? other)
        {
            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return EqualsInternal(this, other);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            return Equals(obj as Matrix);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        // Performs an equality complete check.
        private static bool EqualsInternal(Matrix left, Matrix right)
        {
            ArgumentNullException.ThrowIfNull(left, nameof(left));
            ArgumentNullException.ThrowIfNull(right, nameof(right));

            if (left.StorageType != right.StorageType)
            {
                return false;
            }

            if (left.OrderType != right.OrderType)
            {
                return false;
            }

            if (!left.Storage.Equals(right.Storage))
            {
                return false;
            }

            return true;
        }

        #endregion Methods
    }
}
