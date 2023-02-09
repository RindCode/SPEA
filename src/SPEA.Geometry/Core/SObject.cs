// ==================================================================================================
// <copyright file="SObject.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Geometry.Core
{
    /// <summary>
    /// Represents the base class for various geometric entities.
    /// </summary>
    public abstract class SObject
    {
        #region Fields

        private SPoint _origin = default;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SObject"/> class.
        /// </summary>
        protected SObject()
        {
            // Blank.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SObject"/> class.
        /// </summary>
        /// <param name="x">The X-coordinate of the origin.</param>
        /// <param name="y">The Y-coordinate of the origin.</param>
        protected SObject(double x, double y)
        {
            _origin = new SPoint(x, y);
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

        #endregion Constructors

        #region Properties

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

        #endregion Methods
    }
}
