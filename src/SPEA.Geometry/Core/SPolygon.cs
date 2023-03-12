// ==================================================================================================
// <copyright file="SPolygon.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Geometry.Core
{
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

        private readonly SPolygon _definingObject;
        private readonly SLinearRing _shell;
        private readonly SLinearRing[] _holes;
        ////private SPoint _origin;

        #endregion Fields

        #region Constructors

        /////// <summary>
        /////// Initializes a new instance of the <see cref="SPolygon"/> class.
        /////// </summary>
        ////public SPolygon()
        ////{
        ////    ////_shell = new SLinearRing();
        ////    ////_holes = Array.Empty<SLinearRing>();
        ////    ////_origin = default(SPoint);
        ////}

        /////// <summary>
        /////// Initializes a new instance of the <see cref="SPolygon"/> class.
        /////// </summary>
        /////// <param name="shell">The outer boundary of the polygon.</param>
        ////public SPolygon(SLinearRing shell)
        ////    : this(shell, Array.Empty<SLinearRing>())
        ////{
        ////    // Blank.
        ////}

        /////// <summary>
        /////// Initializes a new instance of the <see cref="SPolygon"/> class.
        /////// </summary>
        /////// <param name="shell">The outer boundary of the polygon.</param>
        /////// <param name="holes">The inner boundaries array of the polygon. Must be empty if an empty shell is provided.</param>
        /////// <exception cref="ArgumentNullException">Is thrown when <paramref name="shell"/> is <see langword="null"/>.</exception>
        /////// <exception cref="ArgumentNullException">Is thrown when <paramref name="holes"/> is <see langword="null"/>.</exception>
        /////// <exception cref="ArgumentNullException">Is thrown when <paramref name="holes"/> contains <see langword="null"/> elements.</exception>
        /////// <exception cref="ArgumentException">Is thrown when <paramref name="shell"/> is not empty, but non-empty holes were provided.</exception>
        ////public SPolygon(SLinearRing shell, SLinearRing[] holes)
        ////{
        ////    ArgumentNullException.ThrowIfNull(shell);
        ////    ArgumentNullException.ThrowIfNull(holes);

        ////    // TODO: Complete the underlying checks in a single pass?

        ////    if (HasNullElements(holes))
        ////    {
        ////        throw new ArgumentNullException("The array of holes must not contain null elements.", nameof(holes));
        ////    }

        ////    if (shell.IsEmpty && HasNonEmptyElements(holes))
        ////    {
        ////        throw new ArgumentException("The shell is empty, but non-empty holes were provided.");
        ////    }

        ////    _shell = shell;
        ////    _holes = holes;
        ////    _origin = shell.Origin;
        ////}

        /// <summary>
        /// Initializes a new instance of the <see cref="SPolygon"/> class.
        /// </summary>
        public SPolygon()
        {
            ////_shell = new SLinearRing();
            ////_holes = new SLinearRing[0];
            ////_definingObject = new SPolygon(this);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SPolygon"/> class.
        /// </summary>
        /// <param name="sPolygon"><see cref="SPolygon"/> object used for a "copy".</param>
        protected SPolygon(SPolygon sPolygon)
        {
            _shell = new SLinearRing(sPolygon.Shell.Points);

            var holes = new SLinearRing[sPolygon.Holes.Length];
            Array.Copy(sPolygon.Holes, holes, holes.Length);
            _holes = holes;

            _definingObject = sPolygon.DefiningObject;
        }

        #endregion Constructors

        #region Properties

        /// <inheritdoc/>
        public override SPoint Origin
        {
            get => Shell.Origin;
            set
            {
                if (Shell.Origin == value)
                {
                    return;
                }

                var d = value - Shell.Origin;
                Translate(d.X, d.Y);
            }
        }

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

        /// <inheritdoc/>
        public override SPolygon DefiningObject => _definingObject;

        #endregion Properties

        #region Methods

        /// <inheritdoc/>
        public override void ApplyTransformation(AffineTransformation transform, TransformationType transformationType)
        {
            ArgumentNullException.ThrowIfNull(transform, nameof(transform));

            if (transform.IsIdentity)
            {
                return;
            }

            var shell = Shell.Points;
            for (int i = 0; i < shell.Length; i++)
            {
                shell[i] = shell[i].Transform(transform);
            }

            for (int i = 0; i < Holes.Length; i++)
            {
                var hole = Holes[i].Points;
                for (int j = 0; j < hole.Length; j++)
                {
                    hole[i] = hole[i].Transform(transform);
                }
            }
        }

        #endregion Methods
    }
}
