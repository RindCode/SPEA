// ==================================================================================================
// <copyright file="MatrixBase.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Numerics.Matrices
{
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
    public abstract partial class MatrixBase : IEquatable<MatrixBase>
    {
        #region Fields

        private readonly StorageBase _storage;
        private readonly MatrixDataOrderType _orderType;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MatrixBase"/> class.
        /// </summary>
        /// <param name="rows">The number of rows.</param>
        /// <param name="columns">The number of columns.</param>
        /// <param name="order">The order type.</param>
        protected internal MatrixBase(int rows, int columns, MatrixDataOrderType order = MatrixDataOrderType.ColumMajor)
        {
            // The permitted number of rows and columns is checked within a storage ctor.

            switch (order)
            {
                case MatrixDataOrderType.RowMajor:
                    _storage = new RowMajorStorage(rows, columns);
                    break;
                case MatrixDataOrderType.ColumMajor:
                    _storage = new ColumnMajorStorage(rows, columns);
                    break;
                default:
                    throw new NotSupportedException($"The specified matrix order type is not supported: {order}");
            }

            _orderType = order;
        }

        #endregion Constructors

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
        /// Gets the matrix data ordering scheme.
        /// </summary>
        public MatrixDataOrderType OrderType => _orderType;

        /// <summary>
        /// Gets the matrix data storage.
        /// </summary>
        protected StorageBase Storage => _storage;

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
        /// Sets all matrix entries using a defined fill function.
        /// </summary>
        /// <remarks>
        /// This method takes into acccount the storage order type.
        /// </remarks>
        /// <param name="func">Fill function.</param>
        public virtual void Fill(Func<int, int, double> func)
        {
            Storage.Fill(func);
        }

        /// <inheritdoc/>
        public bool Equals(MatrixBase? other)
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
            return Equals(obj as MatrixBase);
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
        private static bool EqualsInternal(MatrixBase left, MatrixBase right)
        {
            if (left == null)
            {
                throw new ArgumentNullException(nameof(left));
            }

            if (right == null)
            {
                throw new ArgumentNullException(nameof(right));
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
