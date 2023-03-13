// ==================================================================================================
// <copyright file="TranslationTransformation.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Geometry.Transform
{
    using SPEA.Numerics.Matrices;

    /// <summary>
    /// Represents a translation transform.
    /// </summary>
    public sealed class TranslationTransformation : AffineTransformation
    {
        #region Fields

        private readonly DenseRectMatrix _value;
        private readonly bool _isIdentity;
        private readonly double _x;
        private readonly double _y;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TranslationTransformation"/> class.
        /// </summary>
        public TranslationTransformation()
        {
            _value = IdentityTransform;
            _isIdentity = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TranslationTransformation"/> class.
        /// </summary>
        /// <param name="x">The displacement along the X axis.</param>
        /// <param name="y">The displacement along the Y axis.</param>
        public TranslationTransformation(double x, double y)
        {
            var matrix = IdentityTransform;
            matrix[0, 2] = x;
            matrix[1, 2] = y;

            _value = matrix;
            _isIdentity = false;
            _x = x;
            _y = y;
        }

        #endregion Constructors

        #region Properties

        /// <inheritdoc/>
        public override DenseRectMatrix Value => _value;

        /// <inheritdoc/>
        public override bool IsIdentity => _isIdentity;

        /// <summary>
        /// Gets the displacement along the X axis.
        /// </summary>
        public double X => _x;

        /// <summary>
        /// Gets the displacement along the Y axis.
        /// </summary>
        public double Y => _y;

        #endregion Properties
    }
}
