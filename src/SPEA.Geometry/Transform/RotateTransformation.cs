// ==================================================================================================
// <copyright file="RotateTransformation.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Geometry.Transform
{
    using SPEA.Geometry.Core;
    using SPEA.Numerics.Matrices;

    /// <summary>
    /// Represents a rotation transform.
    /// </summary>
    public sealed class RotateTransformation : GeneralTransformation
    {
        #region Fields

        private readonly double _angle;
        private readonly double _centerX;
        private readonly double _centerY;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RotateTransformation"/> class.
        /// </summary>
        public RotateTransformation()
            : base()
        {
            // Blank.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RotateTransformation"/> class.
        /// </summary>
        /// <param name="angle">The angle of rotation.</param>
        public RotateTransformation(double angle)
            : this(angle, 0.0d, 0.0d)
        {
            _angle = angle;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RotateTransformation"/> class.
        /// </summary>
        /// <param name="angle">The angle of rotation.</param>
        /// <param name="center">The rotation point.</param>
        public RotateTransformation(double angle, SPoint center)
            : this(angle, center.X, center.Y)
        {
            // Blank.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RotateTransformation"/> class.
        /// </summary>
        /// <param name="angle">The angle of rotation.</param>
        /// <param name="centerX">The X-coordinate of the rotation point.</param>
        /// <param name="centerY">The Y-coordinate of the rotation point.</param>
        public RotateTransformation(double angle, double centerX, double centerY)
            : base(BuildMatrix(angle, centerX, centerY))
        {
            _angle = angle;
            _centerX = centerX;
            _centerY = centerY;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the angle of rotation.
        /// </summary>
        public double Angle => _angle;

        /// <summary>
        /// Gets the X-coordinate of the rotation point.
        /// </summary>
        public double CenterX => _centerX;

        /// <summary>
        /// Gets the Y-coordinate of the rotation point.
        /// </summary>
        public double CenterY => _centerY;

        #endregion Properties

        #region Methods

        // Generates a new matrix.
        private static DenseRectMatrix BuildMatrix(double angle, double centerX, double centerY)
        {
            var matrix = DenseRectMatrix.Build.DenseIdentity(AffineMatrixDim, AffineMatrixDim);
            var rad = angle * Math.PI / 180;

            if (centerX == 0.0d && centerY == 0.0d)
            {
                matrix[0, 0] = Math.Cos(rad);
                matrix[0, 1] = -Math.Sin(rad);
                matrix[1, 0] = Math.Sin(rad);
                matrix[1, 1] = Math.Cos(rad);

                return matrix;
            }
            else
            {
                var translateBackMatrix = new TranslationTransformation(-centerX, -centerY).Value;
                var rotateMatrix = new RotateTransformation(angle).Value;
                var translateMatrix = new TranslationTransformation(centerX, centerY).Value;

                return translateBackMatrix * rotateMatrix * translateMatrix;
            }
        }

        #endregion Methods
    }
}
