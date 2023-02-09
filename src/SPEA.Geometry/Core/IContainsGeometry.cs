// ==================================================================================================
// <copyright file="IContainsGeometry.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Geometry.Core
{
    /// <summary>
    /// Represents a contract for all objects that contain geometry.
    /// </summary>
    public interface IContainsGeometry
    {
        /// <summary>
        /// Gets the element geometry data.
        /// </summary>
        public SGeometry Geometry { get; init; }
    }
}
