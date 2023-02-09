// ==================================================================================================
// <copyright file="SquareMatrixBase{T}.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Numerics.Matrices
{
    using System.Numerics;

    /// <summary>
    /// Represents a base class for square matrices.
    /// </summary>
    /// <typeparam name="T">Matrix data type.</typeparam>
    public abstract class SquareMatrixBase<T> : RectMatrixBase<T>
        where T : INumber<T>
    {
        #region Properties

        /// <summary>
        /// Gets the square matrix dimension.
        /// </summary>
        public abstract int Dimension { get; }

        #endregion Properties

        #region Methods

        /// <inheritdoc/>
        public override T At(int row, int column)
        {
            return Storage.At(row, column);
        }

        /// <inheritdoc/>
        public override void At(int row, int column, T value)
        {
            Storage.At(row, column, value);
        }

        #endregion Methods
    }
}
