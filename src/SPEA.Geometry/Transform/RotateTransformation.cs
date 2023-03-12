// ==================================================================================================
// <copyright file="RotateTransformation.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Geometry.Transform
{
    using SPEA.Numerics.Matrices;

    /// <summary>
    /// Represents a rotation transform.
    /// </summary>
    public class RotateTransformation : AffineTransformation
    {
        #region Fields

        private readonly DenseSquareMatrix _value;
        private readonly bool _isIdentity;
        private readonly double _angle;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RotateTransformation"/> class.
        /// </summary>
        public RotateTransformation()
        {
            _value = IdentityTransform;
            _isIdentity = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RotateTransformation"/> class.
        /// </summary>
        /// <param name="angle">The angle of rotation.</param>
        public RotateTransformation(double angle)
        {
            var matrix = IdentityTransform;
            matrix[0, 0] = Math.Cos(angle);
            matrix[0, 1] = -1 * Math.Sin(angle);
            matrix[1, 0] = Math.Sin(angle);
            matrix[1, 1] = Math.Cos(angle);

            _value = matrix;
            _isIdentity = false;
            _angle = angle;
        }

        #endregion Constructors

        #region Properties

        /// <inheritdoc/>
        public override DenseSquareMatrix Value => _value;

        /// <inheritdoc/>
        public override bool IsIdentity => _isIdentity;

        /// <summary>
        /// Gets the angle of rotation.
        /// </summary>
        public double Angle => _angle;

        #endregion Properties
    }
}
