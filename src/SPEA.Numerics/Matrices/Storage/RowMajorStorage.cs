// ==================================================================================================
// <copyright file="RowMajorStorage.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Numerics.Matrices.Storage
{
    /// <summary>
    /// Represents a row-major matrix storage type.
    /// </summary>
    /// <typeparam name="T">Matrix data type.</typeparam>
    public sealed class RowMajorStorage : StorageBase
    {
        #region Fields

        private readonly double[] _data;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RowMajorStorage"/> class.
        /// </summary>
        /// <param name="rows">The number of rows.</param>
        /// <param name="columns">The number of columns.</param>
        public RowMajorStorage(int rows, int columns)
            : base(rows, columns)
        {
            _data = new double[rows * columns];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RowMajorStorage"/> class.
        /// </summary>
        /// <param name="rows">The number of rows.</param>
        /// <param name="columns">The number of columns.</param>
        /// <param name="data">The array representing a data storage.</param>
        public RowMajorStorage(int rows, int columns, double[] data)
            : base(rows, columns)
        {
            _data = data;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the raw matrix data stored as a single-dimension array.
        /// </summary>
        public double[] Data => _data;

        #endregion Properties

        #region Methods

        /// <inheritdoc/>
        public override double At(int row, int column)
        {
            return Data[(row * ColumnCount) + column];
        }

        /// <inheritdoc/>
        public override void At(int row, int column, double value)
        {
            Data[(row * ColumnCount) + column] = value;
        }

        #endregion Methods
    }
}
