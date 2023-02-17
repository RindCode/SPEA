﻿// ==================================================================================================
// <copyright file="SPolygonBase.cs" company="Dmitry Poberezhnyy">
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
    public abstract class SPolygonBase : SObject
    {
        #region Fields

        ////private readonly SLinearRing _shell;
        ////private readonly SLinearRing[] _holes;
        ////private SPoint _origin;

        #endregion Fields

        #region Constructors

        /////// <summary>
        /////// Initializes a new instance of the <see cref="SPolygonBase"/> class.
        /////// </summary>
        ////public SPolygonBase()
        ////{
        ////    ////_shell = new SLinearRing();
        ////    ////_holes = Array.Empty<SLinearRing>();
        ////    ////_origin = default(SPoint);
        ////}

        /////// <summary>
        /////// Initializes a new instance of the <see cref="SPolygonBase"/> class.
        /////// </summary>
        /////// <param name="shell">The outer boundary of the polygon.</param>
        ////public SPolygonBase(SLinearRing shell)
        ////    : this(shell, Array.Empty<SLinearRing>())
        ////{
        ////    // Blank.
        ////}

        /////// <summary>
        /////// Initializes a new instance of the <see cref="SPolygonBase"/> class.
        /////// </summary>
        /////// <param name="shell">The outer boundary of the polygon.</param>
        /////// <param name="holes">The inner boundaries array of the polygon. Must be empty if an empty shell is provided.</param>
        /////// <exception cref="ArgumentNullException">Is thrown when <paramref name="shell"/> is <see langword="null"/>.</exception>
        /////// <exception cref="ArgumentNullException">Is thrown when <paramref name="holes"/> is <see langword="null"/>.</exception>
        /////// <exception cref="ArgumentNullException">Is thrown when <paramref name="holes"/> contains <see langword="null"/> elements.</exception>
        /////// <exception cref="ArgumentException">Is thrown when <paramref name="shell"/> is not empty, but non-empty holes were provided.</exception>
        ////public SPolygonBase(SLinearRing shell, SLinearRing[] holes)
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

                var dx = value.X - Shell.Origin.X;
                var dy = value.Y - Shell.Origin.Y;
                var transform = new TranslationTransformation(dx, dy);
                ApplyTransformation(transform);
            }
        }

        /// <summary>
        /// Gets the polygon shell.
        /// </summary>
        public abstract SLinearRing Shell { get; }

        /// <summary>
        /// Gets the polygon holes array.
        /// </summary>
        public abstract SLinearRing[] Holes { get; }

        /// <summary>
        /// Gets a value indicating whether the current <see cref="SPolygonBase"/> is empty
        /// by testing is its <see cref="Shell"/> storage has no elements.
        /// </summary>
        public override bool IsEmpty => Shell.IsEmpty;

        #endregion Properties

        #region Methods

        /// <inheritdoc/>
        public override void ApplyTransformation(AffineTransformation transform)
        {
            ArgumentNullException.ThrowIfNull(transform);

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
