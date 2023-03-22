// ==================================================================================================
// <copyright file="CartesianSystem.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Geometry.Systems
{
    using SPEA.Geometry.Core;
    using SPEA.Geometry.Transform;

    /// <summary>
    /// Represents a cartesian coordinate system.
    /// </summary>
    public sealed class CartesianSystem : CoordinateSystem
    {
        #region Fields

        private GeneralTransformation _globalTransform;
        private double _angle;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CartesianSystem"/> class.
        /// </summary>
        public CartesianSystem()
        {
            _globalTransform = new GeneralTransformation();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CartesianSystem"/> class.
        /// </summary>
        /// <param name="x">The X coordinate of the origin of a new local coordinate system, defined in GCS.</param>
        /// <param name="y">The Y coordinate of the origin of a new local coordinate system, defined in GCS.</param>
        public CartesianSystem(double x, double y)
        {
            _globalTransform = new TranslationTransformation(x, y);
        }

        #endregion Constructors

        #region Properties

        /// <inheritdoc/>
        public override GeneralTransformation GlobalTransform
        {
            get => _globalTransform;
            protected set
            {
                _globalTransform = value;
                _angle = Math.Atan2(GlobalTransform.M10, GlobalTransform.M00) * (180 / Math.PI);
            }
        }

        /// <summary>
        /// Gets the angle from the defined <see cref="GlobalTransform"/> value.
        /// </summary>
        public double Angle => _angle;

        #endregion Properties
    }
}
