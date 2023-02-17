﻿// ==================================================================================================
// <copyright file="AppliedTransformations.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Geometry.Transform
{
    /// <summary>
    /// Represents a wrapper for applied transformations.
    /// </summary>
    public class AppliedTransformations
    {
        private TranslationTransformation _translate;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppliedTransformations"/> class.
        /// </summary>
        public AppliedTransformations()
        {
            _translate = new TranslationTransformation();
        }

        /// <summary>
        /// Gets or sets a translation transform.
        /// </summary>
        public TranslationTransformation Translate
        {
            get => _translate;
            set => _translate = value;
        }
    }
}
