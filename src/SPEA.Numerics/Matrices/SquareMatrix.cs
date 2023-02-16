// ==================================================================================================
// <copyright file="SquareMatrix.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Numerics.Matrices
{
    /// <summary>
    /// Represents a square matrix.
    /// </summary>
    public class SquareMatrix : SquareMatrixBase
    {
        #region Fields

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SquareMatrix"/> class.
        /// </summary>
        /// <param name="dimension">The square matrix dimension (dim = rows = columns).</param>
        public SquareMatrix(int dimension)
            : base(dimension)
        {
            // Blank.
        }

        #endregion Constructors

        #region Properties

        #endregion Properties

        #region Methods

        /// <summary>
        /// Creates a new identity matrix of a given dimension.
        /// </summary>
        /// <param name="dimension">The square matrix dimension.</param>
        /// <returns>A square identity matrix.</returns>
        public static SquareMatrix GetIdentity(int dimension)
        {
            var matrix = new SquareMatrix(dimension);
            for (int i = 0; i < matrix.RowCount; i++)
            {
                matrix[i, i] = 1.0;
            }

            return matrix;
        }

        #endregion Methods
    }
}
