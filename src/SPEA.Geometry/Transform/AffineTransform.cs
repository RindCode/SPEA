// ==================================================================================================
// <copyright file="AffineTransform.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Geometry.Transform
{
    using SPEA.Numerics.Matrices;

    /// <summary>
    /// Represents a base class for affine transformations.
    /// </summary>
    /// <remarks>
    /// This matrix always has a size of 3x3.
    /// </remarks>
    public abstract class AffineTransform : TransformBase
    {
        #region Fields

        private const int AffineMatrixDim = 3;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets a default transformation matrix as an identity matrix.
        /// </summary>
        /// <remarks>
        /// Applying this matrix doesn't produce any transformation.
        /// </remarks>
        public static SquareMatrix IdentityTransform => SquareMatrix.GetIdentity(AffineMatrixDim);

        /// <summary>
        /// Gets a value of the first row and the first column of the matrix.
        /// </summary>
        public double M11 => Value[0, 0];

        /// <summary>
        /// Gets a value of the first row and the second column of the matrix.
        /// </summary>
        public double M12 => Value[0, 1];

        /// <summary>
        /// Gets a value of the first row and the third column of the matrix.
        /// </summary>
        /// <remarks>
        /// Represents an X-offset value.
        /// </remarks>
        public double OffsetX => Value[0, 2];

        /// <summary>
        /// Gets a value of the second row and the first column of the matrix.
        /// </summary>
        public double M21 => Value[1, 0];

        /// <summary>
        /// Gets a value of the second row and the second column of the matrix.
        /// </summary>
        public double M22 => Value[1, 1];

        /// <summary>
        /// Gets a value of the second row and the third column of the matrix.
        /// </summary>
        /// <remarks>
        /// Represents an Y-offset value.
        /// </remarks>
        public double OffsetY => Value[1, 2];

        /// <summary>
        /// Gets a value of the third row and the first column of the matrix.
        /// </summary>
        /// <remarks>
        /// Must be equal to 0.
        /// </remarks>
        public double M31 => Value[2, 0];

        /// <summary>
        /// Gets a value of the third row and the second column of the matrix.
        /// </summary>
        /// <remarks>
        /// Must be equal to 0.
        /// </remarks>
        public double M32 => Value[2, 1];

        /// <summary>
        /// Gets a value of the third row and the third column of the matrix.
        /// </summary>
        /// <remarks>
        /// Must be equal to 1.
        /// </remarks>
        public double M33 => Value[2, 2];

        #endregion Properties
    }
}
