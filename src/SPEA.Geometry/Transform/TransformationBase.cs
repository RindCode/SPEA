// ==================================================================================================
// <copyright file="TransformationBase.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Geometry.Transform
{
    using SPEA.Numerics.Matrices;

    /// <summary>
    /// Defines the applied transformation type.
    /// </summary>
    public enum TransformationType
    {
        /// <summary>
        /// The transformation will be applied relative
        /// to the initial state. Normally the initial state
        /// contains no transofrmations.
        /// </summary>
        RelativeToInitial,

        /// <summary>
        /// The transformation will be applied relative
        /// to the current transformation (appended).
        /// </summary>
        RelativeToCurrent,
    }

    /// <summary>
    /// Represents a base class for any transformations.
    /// </summary>
    public abstract class TransformationBase
    {
        /// <summary>
        /// Gets the current transformation matrix.
        /// </summary>
        public abstract DenseSquareMatrix Value { get; }

        /// <summary>
        /// Gets a value indicating whether the transformation matrix is an identity matrix.
        /// </summary>
        public abstract bool IsIdentity { get; }
    }
}
