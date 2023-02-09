// ==================================================================================================
// <copyright file="SquareMatrix{T}.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Numerics.Matrices
{
    using System.Numerics;
    using SPEA.Numerics.Matrices.Storage;

    /// <summary>
    /// Represents a square matrix.
    /// </summary>
    /// <typeparam name="T">Matrix data type.</typeparam>
    public class SquareMatrix<T> : SquareMatrixBase<T>
        where T : INumber<T>
    {
        #region Fields

        private StorageBase<T> _storage;
        private int _dimension;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SquareMatrix{T}"/> class.
        /// </summary>
        /// <param name="dimension">The square matrix dimension (dim = rows = columns).</param>
        /// <param name="order">The order type.</param>
        public SquareMatrix(int dimension, MatrixDataOrderType order = MatrixDataOrderType.ColumMajor)
        {
            switch (order)
            {
                case MatrixDataOrderType.RowMajor:
                    _storage = new RowMajorStorage<T>(dimension, dimension);
                    break;
                case MatrixDataOrderType.ColumMajor:
                    _storage = new ColumnMajorStorage<T>(dimension, dimension);
                    break;
                default:
                    throw new NotSupportedException($"The specified matrix order type is not supported: {order}");
            }

            _dimension = dimension;
        }

        #endregion Constructors

        #region Properties

        /// <inheritdoc/>
        public override int Dimension => _dimension;

        /// <inheritdoc/>
        protected override StorageBase<T> Storage
        {
            get => _storage;
            init
            {
                _storage = value;
            }
        }

        #endregion Properties
    }
}
