// ==================================================================================================
// <copyright file="TranslateTransform.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Geometry.Transform
{
    using SPEA.Numerics.Matrices;

    /// <summary>
    /// Represents a translate transform.
    /// </summary>
    public sealed class TranslateTransform : AffineTransform
    {
        #region Fields

        private readonly SquareMatrix _value;
        private readonly bool _isIdentity;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TranslateTransform"/> class.
        /// </summary>
        public TranslateTransform()
        {
            _value = IdentityTransform;
            _isIdentity = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TranslateTransform"/> class.
        /// </summary>
        /// <param name="x">The displacement along the X axis.</param>
        /// <param name="y">The displacement along the Y axis.</param>
        public TranslateTransform(double x, double y)
        {
            var matrix = IdentityTransform;
            matrix[0, 2] = x;
            matrix[1, 2] = y;

            _value = matrix;
            _isIdentity = false;
        }

        #endregion Constructors

        #region Properties

        /// <inheritdoc/>
        public override SquareMatrix Value => _value;

        /// <inheritdoc/>
        public override bool IsIdentity => _isIdentity;

        #endregion Properties
    }
}
