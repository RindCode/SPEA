// ==================================================================================================
// <copyright file="LocalSystem.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Geometry.Systems
{
    using SPEA.Geometry.Core;
    using SPEA.Geometry.Transform;

    /// <summary>
    /// Represents a base class for coordinate systems.
    /// </summary>
    public abstract class CoordinateSystem
    {
        /// <summary>
        /// Gets the coordinate system origin in GCS.
        /// </summary>
        public SPoint Origin => new SPoint(GlobalTransform.M02, GlobalTransform.M12);

        /// <summary>
        /// Gets or sets a transformation matrix representing a transformation of the current
        /// coordinate system in global coordinates.
        /// </summary>
        public abstract GeneralTransformation GlobalTransform { get; protected set; }

        /// <summary>
        /// Transforms the current coordinate system in a local <paramref name="system"/> coordinates.
        /// </summary>
        /// <param name="system">Another coordinate system the transformation is set in.</param>
        /// <param name="transform">A transformation to apply.</param>
        public void TransformIn(CoordinateSystem system, GeneralTransformation transform)
        {
            // TODO: Implementation.
            throw new NotImplementedException();
        }

        /// <summary>
        /// Transforms the current coordinate system in global coordinates.
        /// </summary>
        /// <param name="transform">A transformation to apply.</param>
        /// <param name="action">The way how the <paramref name="transform"/> will be applied.</param>
        public void TransformInGlobal(GeneralTransformation transform, TransformAction action = TransformAction.Append)
        {
            ArgumentNullException.ThrowIfNull(nameof(transform));

            if (action == TransformAction.Append)
            {
                GlobalTransform = new GeneralTransformation(transform.Value * GlobalTransform.Value);
            }
            else
            {
                GlobalTransform = transform;
            }
        }
    }
}
