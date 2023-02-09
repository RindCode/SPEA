// ==================================================================================================
// <copyright file="SPrimitive.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Geometry.Core
{
    /// <summary>
    /// Represents a base class for all primitives.
    /// </summary>
    public abstract class SPrimitive : SObject
    {
        #region Fields

        private SGeometry _geometry = SGeometry.Empty;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SPrimitive"/> class.
        /// </summary>
        protected SPrimitive()
            : base()
        {
            // Blank.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SPrimitive"/> class.
        /// </summary>
        /// <param name="x">The X-coordinate of the origin.</param>
        /// <param name="y">The Y-coordinate of the origin.</param>
        protected SPrimitive(double x, double y)
            : base(x, y)
        {
            // Blank.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SPrimitive"/> class.
        /// </summary>
        /// <param name="origin">The origin location.</param>
        protected SPrimitive(SPoint origin)
            : base(origin.X, origin.Y)
        {
            // Blank.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SPrimitive"/> class.
        /// </summary>
        /// <param name="x">The X-coordinate of the origin.</param>
        /// <param name="y">The Y-coordinate of the origin.</param>
        /// <param name="geometry">Geometry data.</param>
        protected SPrimitive(double x, double y, SGeometry geometry)
            : base(x, y)
        {
            _geometry = geometry;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SPrimitive"/> class.
        /// </summary>
        /// <param name="origin">The origin location.</param>
        /// <param name="geometry">Geometry data.</param>
        protected SPrimitive(SPoint origin, SGeometry geometry)
            : base(origin.X, origin.Y)
        {
            _geometry = geometry;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the actual geometry data.
        /// </summary>
        public SGeometry Geometry
        {
            get => _geometry;
            protected init => _geometry = value;
        }

        #endregion Properties
    }
}
