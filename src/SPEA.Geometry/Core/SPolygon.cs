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
    /// Represents a generic polygon object.
    /// </summary>
    public class SPolygon : SObject
    {
        #region Fields

        private readonly SLinearRing _shell;
        private readonly SLinearRing[] _holes;
        private SPoint _origin;

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
        public SPolygon(SLinearRing shell, SLinearRing[] holes)
        {
            if (shell == null)
            {
                throw new ArgumentNullException(nameof(shell));
            }

            if (holes == null)
            {
                throw new ArgumentNullException(nameof(holes));
            }

            // TODO: Complete the underlying checks in a single pass?

            if (HasNullElements(holes))
            {
                throw new ArgumentException("The array of holes must not contain null elements.", nameof(holes));
            }

            if (shell.IsEmpty && HasNonEmptyElements(holes))
            {
                throw new ArgumentException("The shell is empty, but non-empty holes were provided.");
            }

            _shell = shell;
            _holes = holes;
        }

        #endregion Constructors

        #region Properties

        /// <inheritdoc/>
        public override SPoint Origin
        {
            get => _origin;
            set => _origin = value;
        }

        /// <summary>
        /// Gets the polygon shell.
        /// </summary>
        public SLinearRing Shell => _shell;

        /// <summary>
        /// Gets the polygon holes array.
        /// </summary>
        public SLinearRing[] Holes => _holes;

        /// <summary>
        /// Gets a value indicating whether the current <see cref="SPolygon"/> is empty
        /// by testing is its <see cref="Shell"/> storage has no elements.
        /// </summary>
        public override bool IsEmpty => _shell.IsEmpty;

        #endregion Properties

        #region Methods

        /// <inheritdoc/>
        public override void ApplyTransformation(AffineTransform transform)
        {
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
