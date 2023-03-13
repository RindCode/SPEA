// ==================================================================================================
// <copyright file="CompositeTransformation.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Geometry.Transform
{
    using SPEA.Numerics.Matrices;
    using System.Diagnostics;

    /// <summary>
    /// Represents a composite transformation matrix that may be a product of multiple transformations.
    /// </summary>
    public class CompositeTransformation : AffineTransformation
    {
        #region Fields

        private readonly DenseRectMatrix _value;
        private readonly bool _isIdentity;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeTransformation"/> class.
        /// </summary>
        public CompositeTransformation()
        {
            _value = IdentityTransform;
            _isIdentity = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeTransformation"/> class.
        /// </summary>
        /// <param name="matrix">The 3x3 matrix used for creation.</param>
        public CompositeTransformation(DenseRectMatrix matrix)
        {
            ArgumentNullException.ThrowIfNull(matrix);

            if (!matrix.IsSquare || matrix.RowCount != 3)
            {
                throw new ArgumentException(
                    $"The matrix must be square of 3x3 size. Provided instead: {nameof(matrix)}={matrix.RowCount}x{matrix.ColumnCount}.",
                    nameof(matrix));
            }

            _value = matrix.DeepCopy();
            _isIdentity = false;
        }

        #endregion Constructors

        #region Properties

        /// <inheritdoc/>
        public override DenseRectMatrix Value => _value;

        /// <inheritdoc/>
        public override bool IsIdentity => _isIdentity;

        #endregion Properties
    }
}
