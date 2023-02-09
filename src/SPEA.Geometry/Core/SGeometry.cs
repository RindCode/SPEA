// ==================================================================================================
// <copyright file="SGeometry.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Geometry.Core
{
    /// <summary>
    /// Represents points data stored as a sequence of <see cref="SPoint"/> objects.
    /// </summary>
    public readonly struct SGeometry
    {
        #region Fields

        private readonly SPoint[] _data = Array.Empty<SPoint>();

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SGeometry"/> struct.
        /// </summary>
        public SGeometry()
        {
            // Blank.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SGeometry"/> struct.
        /// </summary>
        /// <param name="data">Geometry data as an array of <see cref="SPoint"/>.</param>
        /// <exception cref="ArgumentNullException">When <paramref name="data"/> is <see langword="null"/>.</exception>
        public SGeometry(params SPoint[] data)
        {
            _data = data ?? throw new ArgumentNullException(nameof(data));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SGeometry"/> struct.
        /// </summary>
        /// <param name="data">Geometry data as an array of <see cref="SPoint"/>.</param>
        /// <exception cref="ArgumentNullException">When <paramref name="data"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">When <paramref name="data"/> length is zero or not even.</exception>
        public SGeometry(params double[] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (data.Length != 0 || data.Length % 2 != 0)
            {
                throw new ArgumentException("Coordinates array cannot have zero or odd (not even) length.", nameof(data));
            }

            int len = data.Length / 2;
            var arr = new SPoint[len];
            for (int i = 0; i < data.Length; i++)
            {
                var x = data[2 * i];
                var y = data[(2 * i) + 1];
                var p = new SPoint(x, y);
                arr[i] = p;
            }

            _data = arr;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets a new <see cref="SGeometry"/> instance with empty data.
        /// </summary>
        /// <remarks>
        /// This property can be used when a new <see cref="SObject"/> needs to be created with no
        /// geometric data associated with it to avoid unnecessary memory allocations.
        /// </remarks>
        public static SGeometry Empty => EmptyGeometry.GetEmpty();

        /// <summary>
        /// Gets the actual raw data as a sequence of coordinates.
        /// </summary>
        public SPoint[] Data => _data;

        #endregion Properties

        #region Methods

        /// <summary>
        /// Returns points data as an array of <see cref="double"/> type
        /// with coordinates stored as X and Y pairs: x1, y1, x2, y2, ..., xn, yn.
        /// The returned array will be twice the length of the <see cref="Data"/> array.
        /// </summary>
        /// <param name="points">Array of <see cref="SPoint"/> objects.</param>
        /// <returns><see cref="double"/> array of coordinate pairs.</returns>
        public static double[] AsDoubleArray(SPoint[] points)
        {
            int len = points.Length * 2;
            var arr = new double[len];
            for (int i = 0; i < points.Length; i++)
            {
                var x = points[i].X;
                var y = points[i].Y;
                arr[2 * i] = x;
                arr[(2 * i) + 1] = y;
            }

            return arr;
        }

        #endregion Methods

        #region Classes

        // Stores empty data.
        private static class EmptyGeometry
        {
            private static readonly SGeometry _empty = new SGeometry(Array.Empty<SPoint>());

            /// <summary>
            /// Returns an empty geometry.
            /// </summary>
            /// <returns>An empty <see cref="SGeometry"/>.</returns>
            internal static SGeometry GetEmpty()
            {
                return _empty;
            }
        }

        #endregion Classes
    }
}
