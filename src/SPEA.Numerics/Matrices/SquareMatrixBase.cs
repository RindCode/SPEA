// ==================================================================================================
// <copyright file="SquareMatrixBase.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Numerics.Matrices
{
    /// <summary>
    /// Represents a base class for square matrices.
    /// </summary>
    public abstract class SquareMatrixBase : RectMatrixBase
    {
        #region Fields

        private readonly int _dimension;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SquareMatrixBase"/> class.
        /// </summary>
        /// <param name="dimension">The square matrix dimension (dim = rows = columns).</param>
        protected SquareMatrixBase(int dimension)
            : this(dimension, dimension)
        {
            _dimension = dimension;
        }

        private SquareMatrixBase(int rows, int columns)
            : base(rows, columns)
        {
            // Blank.
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the square matrix dimension.
        /// </summary>
        public int Dimension => _dimension;

        #endregion Properties

        #region Methods

        #endregion Methods
    }
}
