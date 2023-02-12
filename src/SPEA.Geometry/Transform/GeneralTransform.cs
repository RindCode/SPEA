// ==================================================================================================
// <copyright file="GeneralTransform.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Geometry.Transform
{
    using SPEA.Numerics.Matrices;

    public abstract class GeneralTransform
    {
        /// <summary>
        /// Gets the current transformation matrix.
        /// </summary>
        public virtual SquareMatrix Value { get; }
    }
}
