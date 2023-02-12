// ==================================================================================================
// <copyright file="RectMatrixBase.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Numerics.Matrices
{
    using System.Numerics;
    using SPEA.Numerics.Matrices.Storage;

    /// <summary>
    /// Represents a base class for rectangular matrices.
    /// </summary>
    public abstract class RectMatrixBase : MatrixBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RectMatrixBase"/> class.
        /// </summary>
        /// <param name="rows">The number of rows.</param>
        /// <param name="columns">The number of columns.</param>
        /// <param name="order">The order type.</param>
        public RectMatrixBase(int rows, int columns, MatrixDataOrderType order = MatrixDataOrderType.ColumMajor)
            : base(rows, columns, order)
        {
            // Blank.
        }

        #endregion Constructors
    }
}
