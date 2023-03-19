// ==================================================================================================
// <copyright file="SObject.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Geometry.Core
{
    using SPEA.Geometry.Misc;
    using SPEA.Geometry.Systems;
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

        #endregion Fields

        #region Constructors

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the unique ID of this object.
        /// </summary>
        public Guid Guid => guid;

        /// <summary>
        /// Gets the <see cref="SObject"/> local coordinate system defined in GCS.
        /// </summary>
        public abstract CartesianSystem LocalSystem { get; }

        /// <summary>
        /// Gets a value of the origin location.
        /// </summary>
        public abstract SPoint Origin { get; }

        /// <summary>
        /// Gets a value indicating whether the current <see cref="SObject"/> can be treated as empty.
        /// </summary>
        public abstract bool IsEmpty { get; }

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
        /// Transforms the current <see cref="SObject"/> coordinate system in a local <paramref name="system"/> coordinates.
        /// </summary>
        /// <param name="system">Another coordinate system the transformation is set in.</param>
        /// <param name="transform">A transformation to apply.</param>
        public void TransformIn(CartesianSystem system, GeneralTransformation transform)
        {
            LocalSystem.TransformIn(system, transform);
        }

        /// <summary>
        /// Transforms the current <see cref="SObject"/> coordinate system in global coordinates.
        /// </summary>
        /// <param name="transform">A transformation to apply.</param>
        /// <param name="action">The way how the <paramref name="transform"/> will be applied.</param>
        public void TransformInGlobal(GeneralTransformation transform, TransformAction action = TransformAction.Append)
        {
            LocalSystem.TransformInGlobal(transform, action);
        }

        /// <summary>
        /// Calculates the bounding box in local coordinate system.
        /// </summary>
        /// <returns>The <see cref="SObject"/> bouding box in local coordinate system.</returns>
        public abstract BoundingBox GetBoundingBox();

        #endregion Methods
    }
}
