// ==================================================================================================
// <copyright file="SObjectCollection.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Geometry.Core
{
    using SPEA.Geometry.Transform;

    /// <summary>
    /// Represents an extendable collection of <see cref="T"/> elements.
    /// </summary>
    /// <typeparam name="T">A collection data type.</typeparam>
    public class SObjectCollection<T> : SObject
        where T : SObject
    {
        #region Fields

        private readonly List<T> _items;
        private SPoint _origin;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SObjectCollection{T}"/> class.
        /// </summary>
        public SObjectCollection()
        {
            _items = new List<T>();
            _origin = default(SPoint);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SObjectCollection{T}"/> class.
        /// </summary>
        /// <param name="objects">An array of <see cref="T"/> objects.</param>
        /// <exception cref="ArgumentNullException">Is thrown when <paramref name="objects"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentNullException">Is thrown when <paramref name="objects"/> contains <see langword="null"/> elements.</exception>
        public SObjectCollection(T[] objects)
        {
            ArgumentNullException.ThrowIfNull(objects);

            if (HasNullElements(objects))
            {
                throw new ArgumentNullException("The array of SObject objects must not contain null elements.", nameof(objects));
            }

            _items = new List<T>(objects);
            _origin = _items.Count > 0 ? _items[0].Origin : default(SPoint);
        }

        #endregion Constructors

        /// <summary>
        /// Gets a list of <see cref="T"/> elements.
        /// </summary>
        public List<T> Items => _items;

        /// <inheritdoc/>
        /// <remarks>
        /// <para>
        /// The origin location is taken as the origin of the first non-empty element in the collection.
        /// If there are no elements in the collection or all of them are empty, the origin is set to (0.0, 0.0).
        /// </para>
        /// <para>
        /// Changing the origin will lead to applying a transform operation for all
        /// <see cref="T"/> elements in the collection.
        /// </para>
        /// </remarks>
        public override SPoint Origin
        {
            get
            {
                _origin = default(SPoint);
                foreach (var item in Items)
                {
                    if (!item.IsEmpty)
                    {
                        _origin = item.Origin;
                    }
                }

                return _origin;
            }

            set
            {
                if (_origin == value)
                {
                    return;
                }

                var dx = value.X - _origin.X;
                var dy = value.Y - _origin.Y;
                var transform = new TranslationTransformation(dx, dy);
                ApplyTransformation(transform);

                _origin = Items[0].Origin;  // TODO: Use this instead of value to avoid precision errors? How close are they?
            }
        }

        /// <summary>
        /// Gets a value indicating whether the current collection is empty.
        /// </summary>
        public override bool IsEmpty
        {
            get
            {
                if (_items.Count == 0)
                {
                    return true;
                }

                foreach (var item in _items)
                {
                    if (!item.IsEmpty)
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        /// <summary>
        /// Applies an affine transformation to all <see cref="T"/> elements in the collection.
        /// </summary>
        /// <param name="transform">An affine transformation to be applied.</param>
        public override void ApplyTransformation(AffineTransformation transform)
        {
            ArgumentNullException.ThrowIfNull(transform);

            if (transform.IsIdentity)
            {
                return;
            }

            foreach (var item in Items)
            {
                item.ApplyTransformation(transform);
            }
        }
    }
}
