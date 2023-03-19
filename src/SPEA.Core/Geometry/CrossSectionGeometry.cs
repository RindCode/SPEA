// ==================================================================================================
// <copyright file="CrossSectionGeometry.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Core.Geometry
{
    using SPEA.Core.CrossSections;
    using SPEA.Geometry.Core;

    /// <summary>
    /// Represents a <see cref="CrossSection"/> geometry.
    /// </summary>
    public class CrossSectionGeometry
    {
        private SObjectCollection<SPolygon> _actual;
        private SObjectCollection<SPolygon> _clipped;

        /// <summary>
        /// Initializes a new instance of the <see cref="CrossSectionGeometry"/> class.
        /// </summary>
        public CrossSectionGeometry()
        {
            _actual = new SObjectCollection<SPolygon>();
            _clipped = new SObjectCollection<SPolygon>();
        }

        /// <summary>
        /// Gets a collection of added (non-clipped) polygons.
        /// </summary>
        public SObjectCollection<SPolygon> Actual => _actual;

        /// <summary>
        /// Gets a collection of clipped polygons.
        /// </summary>
        public SObjectCollection<SPolygon> Clipped => _clipped;
    }
}
