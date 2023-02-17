// ==================================================================================================
// <copyright file="RectMatrix.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Numerics.Matrices
{
    /// <summary>
    /// Represents a rectangular matrix.
    /// </summary>
    public class RectMatrix : RectMatrixBase
    {
        #region Fields

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RectMatrix"/> class.
        /// </summary>
        /// <param name="rows">The number of rows.</param>
        /// <param name="columns">The number of columns.</param>
        /// <param name="order">The order type.</param>
        public RectMatrix(int rows, int columns, MatrixDataOrderType order)
            : base(rows, columns, order)
        {
            // Blank.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RectMatrix"/> class.
        /// </summary>
        /// <remarks>
        /// The default order is <see cref="MatrixDataOrderType.ColumMajor"/>.
        /// </remarks>
        /// <param name="rows">The number of rows.</param>
        /// <param name="columns">The number of columns.</param>
        public RectMatrix(int rows, int columns)
            : this(rows, columns, MatrixDataOrderType.ColumMajor)
        {
            // Blank.
        }

        #endregion Constructors

        #region Properties

        #endregion Properties

        #region Methods

        #endregion Methods
    }
}
