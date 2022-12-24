// ==================================================================================================
// <copyright file="SObject.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Geometry.Base
{
    /// <summary>
    /// Represents a base class for all cross-section elements.
    /// Any element which can be considered to be a part of a cross-section and to be involved
    /// in calculations process must be derived from this class.
    /// </summary>
    public abstract class SObject
    {
        #region Fields

        // Holds the reference to the actual geometry data.
        private SGeometryData _geoData;

        // Holds the origin location.
        private SPoint _origin = default(SPoint);

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SObject"/> class.
        /// </summary>
        /// <param name="x">The X-coordinate of the origin.</param>
        /// <param name="y">The Y-coordinate of the origin.</param>
        protected SObject(double x, double y)
        {
            var origin = new SPoint(x, y);
            _origin = origin;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SObject"/> class.
        /// </summary>
        /// <param name="origin">Location of the origin.</param>
        protected SObject(SPoint origin)
            : this(origin.X, origin.Y)
        {
            // Blank.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SObject"/> class.
        /// </summary>
        protected SObject()
            : this(new SPoint(0.0d, 0.0d))
        {
            // Blank.
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the actual geometry data.
        /// </summary>
        public SGeometryData GeoData
        {
            get => _geoData;
            private set
            {
                _geoData ??= value;
            }
        }

        /// <summary>
        /// Gets or sets the origin location.
        /// </summary>
        public SPoint Origin
        {
            get => _origin;
            set => _origin = value;
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Generates a custom <see cref="SObject"/> geometry as a sequence of <see cref="SPoint"/>.
        /// </summary>
        /// <remarks>
        /// Classes derived from <see cref="SObject"/> must implement this method.
        /// </remarks>
        /// <param name="geometry">The actual geometry data.</param>
        protected abstract void GenerateGeometry();

        #endregion Methods
    }
}
