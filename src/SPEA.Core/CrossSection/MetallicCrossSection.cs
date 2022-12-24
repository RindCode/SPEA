// ==================================================================================================
// <copyright file="MetallicCrossSection.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Core.CrossSection
{
    /// <summary>
    /// Represents a metallic built-up cross-section model.
    /// </summary>
    public class MetallicCrossSection : CrossSectionBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MetallicCrossSection"/> class.
        /// </summary>
        internal MetallicCrossSection()
            : base()
        {
            // Blank
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MetallicCrossSection"/> class.
        /// </summary>
        /// <param name="name">Model name.</param>
        internal MetallicCrossSection(string name)
            : base(name)
        {
            // Blank
        }
    }
}
