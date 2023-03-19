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
    public enum TransformAction
    {
        /// <summary>
        /// The transformation will replace the existing one.
        /// </summary>
        Replace,

        /// <summary>
        /// The transformation will be applied (appended) relative to the current transformation.
        /// </summary>
        Append,
    }

    /// <summary>
    /// Represents a base class for any transformations.
    /// </summary>
    public abstract class TransformationBase
    {
        #region Properties

        /// <summary>
        /// Gets the current transformation matrix.
        /// </summary>
        public abstract DenseRectMatrix Value { get; }

        /// <summary>
        /// Gets a value indicating whether the transformation matrix is an identity matrix.
        /// </summary>
        public abstract bool IsIdentity { get; }

        #endregion Properties
    }
}
