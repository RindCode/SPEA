// ==================================================================================================
// <copyright file="TransformBase.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Geometry.Transform
{
    using SPEA.Numerics.Matrices;

    /// <summary>
    /// Represents a base class for any transformations.
    /// </summary>
    public abstract class TransformBase
    {
        /// <summary>
        /// Gets the current transformation matrix.
        /// </summary>
        public abstract SquareMatrix Value { get; }

        /// <summary>
        /// Gets a value indicating whether the transformation matrix is an identity matrix.
        /// </summary>
        public abstract bool IsIdentity { get; }
    }
}
