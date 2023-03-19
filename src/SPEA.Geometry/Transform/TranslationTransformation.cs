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
    public sealed class TranslationTransformation : GeneralTransformation
    {
        #region Fields

        private readonly double _x;
        private readonly double _y;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TranslationTransformation"/> class.
        /// </summary>
        public TranslationTransformation()
            : base()
        {
            // Blank.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TranslationTransformation"/> class.
        /// </summary>
        /// <param name="x">The displacement along the X axis.</param>
        /// <param name="y">The displacement along the Y axis.</param>
        public TranslationTransformation(double x, double y)
            : base(BuildMatrix(x, y))
        {
            _x = x;
            _y = y;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the displacement along the X axis.
        /// </summary>
        public double X => _x;

        /// <summary>
        /// Gets the displacement along the Y axis.
        /// </summary>
        public double Y => _y;

        #endregion Properties

        #region Methods

        // Generates a new matrix.
        private static DenseRectMatrix BuildMatrix(double x, double y)
        {
            var matrix = DenseRectMatrix.Build.DenseIdentity(AffineMatrixDim, AffineMatrixDim);
            matrix[0, 2] = x;
            matrix[1, 2] = y;

            return matrix;
        }

        #endregion Methods
    }
}
