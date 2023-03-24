// ==================================================================================================
// <copyright file="SObjectCollection{T}.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Geometry.Core
{
    using SPEA.Geometry.Misc;
    using SPEA.Geometry.Systems;

    /// <summary>
    /// Represents an extendable sObjectCollection of <see cref="T"/> elements.
    /// </summary>
    /// <typeparam name="T">A sObjectCollection data type.</typeparam>
    public class SObjectCollection<T> : SObject
        where T : SObject
    {
        #region Fields

        /// <summary>
        /// Gets the internal type of this entity.
        /// </summary>
        public new const SEntityType InternalType = SEntityType.SOBJECTCOL;

        private readonly List<T> _items;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SObjectCollection{T}"/> class.
        /// </summary>
        public SObjectCollection()
        {
            _items = new List<T>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SObjectCollection{T}"/> class.
        /// </summary>
        /// <param name="objects">An array of <see cref="T"/> objects.</param>
        /// <exception cref="ArgumentNullException">Is thrown when <paramref name="objects"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentNullException">Is thrown when <paramref name="objects"/> contains <see langword="null"/> elements.</exception>
        public SObjectCollection(T[] objects)
        {
            ArgumentNullException.ThrowIfNull(objects, nameof(objects));

            if (HasNullElements(objects))
            {
                throw new ArgumentNullException("The array of SObject objects must not contain null elements.", nameof(objects));
            }

            _items = new List<T>(objects);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SObjectCollection{T}"/> class.
        /// </summary>
        /// <param name="sObjectCollection"><see cref="SObjectCollection{T}"/> object used for a "copy".</param>
        protected SObjectCollection(SObjectCollection<T> sObjectCollection)
        {
            _items = sObjectCollection.Items;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets a list of <see cref="T"/> elements.
        /// </summary>
        public List<T> Items => _items;

        /// <inheritdoc/>
        public override CartesianSystem LocalSystem
        {
            get
            {
                foreach (var item in Items)
                {
                    if (!item.IsEmpty)
                    {
                        return item.LocalSystem;
                    }
                }

                return new CartesianSystem();
            }
        }

        /// <inheritdoc/>
        /// <remarks>
        /// <para>
        /// The origin location is taken as the origin of the first non-empty element in the sObjectCollection.
        /// If there are no elements in the sObjectCollection or all of them are empty, the origin is set to (0.0, 0.0).
        /// </para>
        /// <para>
        /// Changing the origin will lead to applying a transform operation for all
        /// <see cref="T"/> elements in the sObjectCollection.
        /// </para>
        /// </remarks>
        public override SPoint Origin
        {
            get
            {
                var origin = default(SPoint);
                foreach (var item in Items)
                {
                    if (!item.IsEmpty)
                    {
                        origin = item.LocalSystem.Origin;
                        break;
                    }
                }

                return origin;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the current sObjectCollection is empty.
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

        #endregion Properties

        #region Methods

        /// <inheritdoc/>
        public override SObjectCollection<T> DeepCopy()
        {
            // TODO: Implementation.
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override BoundingBox GetBoundingBox()
        {
            var minX = double.MaxValue;
            var minY = double.MaxValue;
            var maxX = double.MinValue;
            var maxY = double.MinValue;

            for (int i = 0; i < Items.Count; i++)
            {
                var bb = Items[i].GetBoundingBox();
                minX = Math.Min(minX, bb.Left);
                minY = Math.Min(minY, bb.Bottom);
                maxX = Math.Max(maxX, bb.Right);
                maxY = Math.Max(maxY, bb.Top);
            }

            return new BoundingBox(minX, maxY, maxX, minY);
        }

        #endregion Methods
    }
}
