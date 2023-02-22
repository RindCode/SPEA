// ==================================================================================================
// <copyright file="CrossSectionGeometry.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Core.Geometry
{
    using SPEA.Core.CrossSection;
    using SPEA.Geometry.Core;

    /// <summary>
    /// Represents a <see cref="CrossSectionBase"/> geometry.
    /// </summary>
    public class CrossSectionGeometry
    {
        private SObjectCollection<SPolygonBase> _actual;
        private SObjectCollection<SPolygonBase> _clipped;

        /// <summary>
        /// Initializes a new instance of the <see cref="CrossSectionGeometry"/> class.
        /// </summary>
        public CrossSectionGeometry()
        {
            _actual = new SObjectCollection<SPolygonBase>();
            _clipped = new SObjectCollection<SPolygonBase>();
        }

        /// <summary>
        /// Gets a collection of added (non-clipped) polygons.
        /// </summary>
        public SObjectCollection<SPolygonBase> Actual => _actual;

        /// <summary>
        /// Gets a collection of clipped polygons.
        /// </summary>
        public SObjectCollection<SPolygonBase> Clipped => _clipped;
    }
}
