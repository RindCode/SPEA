// ==================================================================================================
// <copyright file="SGeometryData.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Geometry
{
    using SPEA.Geometry.Base;

    /// <summary>
    /// Represents <see cref="SObject"/> points data.
    /// </summary>
    /// <remarks>
    /// This class provides an abstraction layer between <see cref="SObject"/>
    /// and the actual representation of an element geometry.
    /// </remarks>
    public sealed class SGeometryData
    {
        #region Fields

        private readonly SPoint[] _data;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SGeometryData"/> class.
        /// </summary>
        public SGeometryData()
        {
            _data = new SPoint[0];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SGeometryData"/> class.
        /// </summary>
        /// <param name="data">Geometry data as an array of <see cref="SPoint"/>.</param>
        /// <exception cref="ArgumentNullException">When <paramref name="data"/> is <see langword="null"/>.</exception>
        public SGeometryData(SPoint[] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            _data = data;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SGeometryData"/> class.
        /// </summary>
        /// <param name="data">Geometry data as an array of <see cref="SPoint"/>.</param>
        /// <exception cref="ArgumentNullException">When <paramref name="data"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">When <paramref name="data"/> length is zero or not even.</exception>
        public SGeometryData(double[] data)
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
        /// <returns><see cref="double"/> array of coordinate pairs.</returns>
        public double[] AsDoubleArray()
        {
            int len = Data.Length * 2;
            var arr = new double[len];
            for (int i = 0; i < Data.Length; i++)
            {
                var x = Data[i].X;
                var y = Data[i].Y;
                arr[2 * i] = x;
                arr[(2 * i) + 1] = y;
            }

            return arr;
        }

        #endregion Methods
    }
}
