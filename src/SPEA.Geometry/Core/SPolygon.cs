// ==================================================================================================
// <copyright file="SPolygon.cs" company="Dmitry Poberezhnyy">
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
    /// Represents a base class for polygons.
    /// </summary>
    public class SPolygon : SObject
    {
        #region Fields

        /// <summary>
        /// Gets the internal type of this entity.
        /// </summary>
        public new const SEntityType InternalType = SEntityType.SPOLYGON;

        private readonly SLinearRing _shell;
        private readonly SLinearRing[] _holes;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SPolygon"/> class.
        /// </summary>
        public SPolygon()
        {
            _shell = new SLinearRing();
            _holes = Array.Empty<SLinearRing>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SPolygon"/> class.
        /// </summary>
        /// <param name="shell">The outer boundary of the polygon.</param>
        public SPolygon(SLinearRing shell)
            : this(shell, Array.Empty<SLinearRing>())
        {
            // Blank.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SPolygon"/> class.
        /// </summary>
        /// <param name="shell">The outer boundary of the polygon.</param>
        /// <param name="holes">The inner boundaries array of the polygon. Must be empty if an empty shell is provided.</param>
        /// <exception cref="ArgumentNullException">Is thrown when <paramref name="shell"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentNullException">Is thrown when <paramref name="holes"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentNullException">Is thrown when <paramref name="holes"/> contains <see langword="null"/> elements.</exception>
        /// <exception cref="ArgumentException">Is thrown when <paramref name="shell"/> is not empty, but non-empty holes were provided.</exception>
        public SPolygon(SLinearRing shell, SLinearRing[] holes)
        {
            ArgumentNullException.ThrowIfNull(shell);
            ArgumentNullException.ThrowIfNull(holes);

            // TODO: Complete the underlying checks in a single pass?

            if (HasNullElements(holes))
            {
                throw new ArgumentNullException("The array of holes must not contain null elements.", nameof(holes));
            }

            if (shell.IsEmpty && HasNonEmptyElements(holes))
            {
                throw new ArgumentException("The shell is empty, but non-empty holes were provided.");
            }

            _shell = shell;
            _holes = holes;
        }

        // Copy constructor.
        private SPolygon(SPolygon template)
            : this(template.Shell, template.Holes)
        {
            LocalSystem.TransformInGlobal(template.LocalSystem.GlobalTransform, TransformAction.Replace);
        }

        #endregion Constructors

        #region Properties

        /// <inheritdoc/>
        public override CartesianSystem LocalSystem => Shell.LocalSystem;

        /// <inheritdoc/>
        public override SPoint Origin => Shell.LocalSystem.Origin;

        /// <summary>
        /// Gets the polygon shell.
        /// </summary>
        public virtual SLinearRing Shell => _shell;

        /// <summary>
        /// Gets the polygon holes array.
        /// </summary>
        public virtual SLinearRing[] Holes => _holes;

        /// <summary>
        /// Gets a value indicating whether the current <see cref="SPolygon"/> is empty
        /// by testing is its <see cref="Shell"/> storage has no elements.
        /// </summary>
        public override bool IsEmpty => Shell.IsEmpty;

        #endregion Properties

        #region Methods

        /// <inheritdoc/>
        public override SPolygon DeepCopy()
        {
            return new SPolygon(this);
        }

        /// <inheritdoc/>
        public override BoundingBox GetBoundingBox()
        {
            return Shell.GetBoundingBox();
        }

        #endregion Methods
    }
}
