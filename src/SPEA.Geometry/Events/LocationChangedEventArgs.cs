// ==================================================================================================
// <copyright file="LocationChangedEventArgs.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Geometry.Events
{
    using SPEA.Geometry.Core;

    /// <summary>
    /// Wraps the LocationChagned event arguments.
    /// </summary>
    public class LocationChangedEventArgs
    {
        private readonly SPoint _oldOrigin;
        private readonly SPoint _newOrigin;
        private readonly double _oldAngle;
        private readonly double _newAngle;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationChangedEventArgs"/> class.
        /// </summary>
        /// <param name="newOrigin">A new origin value.</param>
        /// <param name="oldOrigin">An old origin value.</param>
        /// <param name="newAngle">A new angle value.</param>
        /// <param name="oldAngle">An old angle value.</param>
        public LocationChangedEventArgs(SPoint newOrigin, SPoint oldOrigin, double newAngle, double oldAngle)
        {
            _oldOrigin = oldOrigin;
            _newOrigin = newOrigin;
            _oldAngle = oldAngle;
            _newAngle = newAngle;
        }

        /// <summary>
        /// Gets an old origin value.
        /// </summary>
        public SPoint OldOrigin => _oldOrigin;

        /// <summary>
        /// Gets a new origin value.
        /// </summary>
        public SPoint NewOrigin => _newOrigin;

        /// <summary>
        /// Gets an old angle value.
        /// </summary>
        public double OldAngle => _oldAngle;

        /// <summary>
        /// Gets a new angle value.
        /// </summary>
        public double NewAngle => _newAngle;
    }
}
