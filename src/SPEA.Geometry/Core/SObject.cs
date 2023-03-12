// ==================================================================================================
// <copyright file="SObject.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Geometry.Core
{
    using SPEA.Geometry.Transform;

    /// <summary>
    /// Represents valid internal types.
    /// </summary>
    public enum SEntityType
    {
        SLINEPATH,
        SLINERING,
        SLINESEG,
        SOBJECT,
        SOBJECTCOL,
        SPOINT,
        SPOLYGON,
        SRECT,
        SVECTOR,
    }

    /// <summary>
    /// Represents the base class for various geometric entities.
    /// </summary>
    public abstract class SObject
    {
        #region Fields

        /// <summary>
        /// Gets the internal type of this entity.
        /// </summary>
        public const SEntityType InternalType = SEntityType.SOBJECT;

        private readonly Guid guid = Guid.NewGuid();
        private readonly AppliedTransformations _appliedTransformations = new AppliedTransformations();

        #endregion Fields

        #region Constructors

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the unique ID of this object.
        /// </summary>
        public Guid Guid => guid;

        /// <summary>
        /// Gets or sets a value of the origin location.
        /// </summary>
        public abstract SPoint Origin { get; set; }

        /// <summary>
        /// Gets a value indicating whether the current <see cref="SObject"/> can be treated as empty.
        /// </summary>
        public abstract bool IsEmpty { get; }

        /// <summary>
        /// Gets the initial geometry (with no transformations applied)
        /// of the <see cref="SObject"/> or of a type of a derived class.
        /// </summary>
        public abstract SObject DefiningObject { get; }

        /// <summary>
        /// Gets an object that wraps all applied transformations.
        /// </summary>
        public AppliedTransformations AppliedTransformations => _appliedTransformations;

        #endregion Properties

        #region Methods

        /// <summary>
        /// Tests a collection for <see langword="null"/> elements.
        /// </summary>
        /// <typeparam name="T">Items type.</typeparam>
        /// <param name="collection">A collection to be tested.</param>
        /// <returns><see langword="true"/> if at least one <see langword="null"/> element found, otherwise <see langword="false"/>.</returns>
        public static bool HasNullElements<T>(IEnumerable<T> collection)
            where T : class
        {
            ArgumentNullException.ThrowIfNull(collection, nameof(collection));

            foreach (var item in collection)
            {
                if (item == null)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Tests a collection for non-empty elements.
        /// </summary>
        /// <param name="collection">A collection of <see cref="SObject"/> to be tested.</param>
        /// <returns><see langword="true"/> if at least one non-empty element found, otherwise <see langword="false"/>.</returns>
        public static bool HasNonEmptyElements(SObject[] collection)
        {
            ArgumentNullException.ThrowIfNull(collection, nameof(collection));

            foreach (var item in collection)
            {
                if (item != null && !item.IsEmpty)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Tests a collection for empty elements.
        /// </summary>
        /// <param name="collection">A collection of <see cref="SObject"/> to be tested.</param>
        /// <returns><see langword="true"/> if at least one empty element found, otherwise <see langword="false"/>.</returns>
        public static bool HasEmptyElements(SObject[] collection)
        {
            ArgumentNullException.ThrowIfNull(collection, nameof(collection));

            foreach (var item in collection)
            {
                if (item != null && item.IsEmpty)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Applies an affine transformation to the <see cref="SObject"/>.
        /// </summary>
        /// <param name="transform">An affine transformation to be applied.</param>
        /// <param name="transformationType">The transformation type.</param>
        public abstract void ApplyTransformation(AffineTransformation transform, TransformationType transformationType);

        /// <summary>
        /// Translates the current <see cref="SObject"/>.
        /// </summary>
        /// <param name="x">The displacement along the X axis.</param>
        /// <param name="y">The displacement along the Y axis.</param>
        /// <param name="transformationType">The transformation type.</param>
        public void Translate(double x, double y, TransformationType transformationType = TransformationType.RelativeToCurrent)
        {
            var transform = new TranslationTransformation(x, y);
            AppliedTransformations.Translate = transform;
            ApplyTransformation(transform, transformationType);
        }

        /// <summary>
        /// Rotates the current <see cref="SObject"/>.
        /// </summary>
        /// <param name="angle">The angle of rotation.</param>
        /// <param name="transformationType">The transformation type.</param>
        public void Rotate(double angle, TransformationType transformationType = TransformationType.RelativeToCurrent)
        {
            var transform = new RotateTransformation(angle);
            AppliedTransformations.Rotate = transform;
            ApplyTransformation(transform, transformationType);
        }

        #endregion Methods
    }
}
