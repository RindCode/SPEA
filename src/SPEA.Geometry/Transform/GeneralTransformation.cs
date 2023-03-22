// ==================================================================================================
// <copyright file="GeneralTransformation.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Geometry.Transform
{
    using SPEA.Geometry.Core;
    using SPEA.Numerics.Matrices;

    /// <summary>
    /// Represents a general transformation class.
    /// </summary>
    /// <remarks>
    /// This matrix always has a size of 3x3.
    /// </remarks>
    public class GeneralTransformation : TransformationBase
    {
        #region Fields

        /// <summary>
        /// An transformation matrix dimension.
        /// </summary>
        public const int AffineMatrixDim = 3;

        private readonly DenseRectMatrix _value;
        private readonly bool _isIdentity;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GeneralTransformation"/> class.
        /// </summary>
        public GeneralTransformation()
        {
            _value = DenseRectMatrix.Build.DenseIdentity(AffineMatrixDim, AffineMatrixDim);
            _isIdentity = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeneralTransformation"/> class.
        /// </summary>
        /// <param name="matrix">A 3x3 transformation matrix.</param>
        public GeneralTransformation(DenseRectMatrix matrix)
        {
            ArgumentNullException.ThrowIfNull(matrix);

            if (!matrix.IsSquare || matrix.RowCount != 3)
            {
                throw new ArgumentException(
                    $"The matrix must be square of 3x3 size. Provided instead: {nameof(matrix)}={matrix.RowCount}x{matrix.ColumnCount}.",
                    nameof(matrix));
            }

            _value = matrix;
            _isIdentity = matrix.IsIdentity ? true : false;
        }

        #endregion Constructors

        #region Properties

        /// <inheritdoc/>
        public override DenseRectMatrix Value => _value;

        /// <inheritdoc/>
        public override bool IsIdentity => _isIdentity;

        /// <summary>
        /// Gets a transposed transformation.
        /// </summary>
        public GeneralTransformation Transposed => GetTransposedTransformation();

        /// <summary>
        /// Gets a value of the first row and the first column of the matrix.
        /// </summary>
        public double M00 => Value[0, 0];

        /// <summary>
        /// Gets a value of the first row and the second column of the matrix.
        /// </summary>
        public double M01 => Value[0, 1];

        /// <summary>
        /// Gets a value of the first row and the third column of the matrix.
        /// </summary>
        /// <remarks>
        /// Represents an X-offset value.
        /// </remarks>
        public double M02 => Value[0, 2];

        /// <summary>
        /// Gets a value of the second row and the first column of the matrix.
        /// </summary>
        public double M10 => Value[1, 0];

        /// <summary>
        /// Gets a value of the second row and the second column of the matrix.
        /// </summary>
        public double M11 => Value[1, 1];

        /// <summary>
        /// Gets a value of the second row and the third column of the matrix.
        /// </summary>
        /// <remarks>
        /// Represents an Y-offset value.
        /// </remarks>
        public double M12 => Value[1, 2];

        /// <summary>
        /// Gets a value of the third row and the first column of the matrix.
        /// </summary>
        /// <remarks>
        /// Must be equal to 0.
        /// </remarks>
        public double M20 => Value[2, 0];

        /// <summary>
        /// Gets a value of the third row and the second column of the matrix.
        /// </summary>
        /// <remarks>
        /// Must be equal to 0.
        /// </remarks>
        public double M21 => Value[2, 1];

        /// <summary>
        /// Gets a value of the third row and the third column of the matrix.
        /// </summary>
        /// <remarks>
        /// Must be equal to 1.
        /// </remarks>
        public double M22 => Value[2, 2];

        #endregion Properties

        #region Methods

        // Constructs a transposed affine transformation.
        private GeneralTransformation GetTransposedTransformation()
        {
            var resultMatrix = new DenseRectMatrix(AffineMatrixDim);
            Value.TransposeTo(resultMatrix);
            return new GeneralTransformation(resultMatrix);
        }

        #endregion Methods
    }
}
