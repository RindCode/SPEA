// ==================================================================================================
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
        private CompositeTransformation _composite;
        private TranslationTransformation _translate;
        private RotateTransformation _rotate;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppliedTransformations"/> class.
        /// </summary>
        public AppliedTransformations()
        {
            _composite = new CompositeTransformation();
            _translate = new TranslationTransformation();
            _rotate = new RotateTransformation();
        }

        /// <summary>
        /// Gets or sets a custom composite transform.
        /// </summary>
        public CompositeTransformation Composite
        {
            get => _composite;
            set => _composite = value;
        }

        /// <summary>
        /// Gets or sets a translation transform.
        /// </summary>
        public TranslationTransformation Translate
        {
            get => _translate;
            set => _translate = value;
        }

        /// <summary>
        /// Gets or sets a translation transform.
        /// </summary>
        public RotateTransformation Rotate
        {
            get => _rotate;
            set => _rotate = value;
        }
    }
}
