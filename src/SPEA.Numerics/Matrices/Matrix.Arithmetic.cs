// ==================================================================================================
// <copyright file="Matrix.Arithmetic.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Numerics.Matrices
{
    /// <summary>
    /// Specifies the supported ordering schemes.
    /// </summary>
    public abstract partial class Matrix : IEquatable<Matrix>
    {
        #region Methods

        /// <summary>
        /// Applies a function to each value of the current matrix.
        /// </summary>
        /// <param name="function">A function to be applied.</param>
        public void MapInplace(Func<double, double> function)
        {
            Storage.MapInplace(function);
        }

        /// <summary>
        /// Applies a function to each value of the current matrix
        /// and sets <paramref name="result"/> values using the function returned value.
        /// </summary>
        /// <param name="function">A function to be applied.</param>
        /// <param name="result">The resulting matrix the function is applied to.</param>
        public void Map(Func<double, double> function, Matrix result)
        {
            if (ReferenceEquals(this, result))
            {
                Storage.MapInplace(function);
            }
            else
            {
                Storage.MapTo(function, result.Storage);
            }
        }

        /// <summary>
        /// Applies a function to each value pair of two matrices (the current one and <paramref name="other"/>,
        /// and sets <paramref name="result"/> values using the function returned value.
        /// </summary>
        /// <param name="function">A function to be applied.</param>
        /// <param name="other">Another matrix used for mapping.</param>
        /// <param name="result">The resulting matrix the function is applied to.</param>
        public void Map2(Func<double, double, double> function, Matrix other, Matrix result)
        {
            Storage.Map2To(function, other.Storage, result.Storage);
        }

        /// <summary>
        /// Adds another matrix to a current one and stores the result
        /// in <paramref name="result"/> matrix.
        /// </summary>
        /// <param name="other">Another matrix to add.</param>
        /// <param name="result">The resulting matrix.</param>
        protected virtual void DoAdd(Matrix other, Matrix result)
        {
            Map2((x, y) => x + y, other, result);
        }

        /// <summary>
        /// Adds a scalar to the current matrix and returns a new resulting matrix.
        /// </summary>
        /// <param name="scalar">A scalar to add.</param>
        /// <param name="result">The resulting matrix.</param>
        protected virtual void DoAdd(double scalar, Matrix result)
        {
            Map(x => x + scalar, result);
        }

        #endregion Methods
    }
}
