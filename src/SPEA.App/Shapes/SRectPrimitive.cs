// ==================================================================================================
// <copyright file="SRectPrimitive.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.Shapes
{
    using System.Windows.Media;
    using System.Windows.Shapes;

    /// <summary>
    /// Represents a rectangle primitive shape.
    /// </summary>
    public sealed class SRectPrimitive : Shape
    {
        #region Fields

        private Geometry _geometry = Geometry.Empty;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SRectPrimitive"/> class.
        /// </summary>
        public SRectPrimitive()
        {
            // Blank.
        }

        #endregion Constructors

        #region Properties

        /// <inheritdoc/>
        public override Geometry RenderedGeometry => _geometry;

        /// <inheritdoc/>
        protected override Geometry DefiningGeometry => _geometry;

        #endregion Properties

        #region Methods

        private Geometry GenerateGeometry()
        {
            return _geometry;
        }

        #endregion Methods
    }
}
