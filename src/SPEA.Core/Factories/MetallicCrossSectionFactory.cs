// ==================================================================================================
// <copyright file="MetallicCrossSectionFactory.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Core.Factories
{
    using SPEA.Core.CrossSections;

    /// <summary>
    /// A factory responsible for creating <see cref="MetallicCrossSection"/> model.
    /// </summary>
    public class MetallicCrossSectionFactory : CrossSectionFactory<MetallicCrossSection>
    {
        /// <inheritdoc/>
        protected override MetallicCrossSection CreateCore(string name)
        {
            return new MetallicCrossSection(name);
        }
    }
}
