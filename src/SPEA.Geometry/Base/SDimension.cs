// ==================================================================================================
// <copyright file="SDimension.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Geometry.Base
{
    public enum SDimensionType
    {
        Linear = 0,
        Radial = 1,
    }

    /// <summary>
    /// Represents a custom <see cref="SObject"/> dimension.
    /// </summary>
    public readonly struct SDimension
    {
        #region Fields

        private readonly string _name;

        private readonly double _value;

        private readonly SDimensionType _type;

        #endregion Fields

        #region Constructors

        public SDimension()
        {
            _name = "";
            _value = 0;
            _type = SDimensionType.Linear;
        }

        #endregion Constructors
    }
}
