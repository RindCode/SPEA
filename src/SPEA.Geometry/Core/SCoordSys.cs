// ==================================================================================================
// <copyright file="SCoordSys.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Geometry.Core
{
    /// <summary>
    /// Represets a 2D coordinate system.
    /// </summary>
    public class SCoordSys
    {
        #region Fields

        private SPoint _origin;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SCoordSys"/> class.
        /// </summary>
        public SCoordSys()
        {
            _origin = default;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the coordinate system origin location.
        /// </summary>
        public SPoint Origin => _origin;

        #endregion Properties
    }
}
