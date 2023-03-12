// ==================================================================================================
// <copyright file="DenseMatrix.Arithmetic.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Numerics.Matrices
{
    using SPEA.Numerics.Helpers;

    /// <summary>
    /// Represents a base class for dense matrices.
    /// </summary>
    public abstract partial class DenseMatrix
    {
        #region Methods

        /// <inheritdoc/>
        protected override void DoAdd(Matrix other, Matrix result)
        {
            // This = Dense (by definition), Other = Dense, Result = Dense.
            if (other.StorageType == Matrices.Storage.MatrixStorageType.Dense && result.StorageType == Matrices.Storage.MatrixStorageType.Dense)
            {
                ArrayHelper.AddArrays(Storage.Data, other.Storage.Data, result.Storage.Data);
                return;
            }

            base.DoAdd(other, result);
        }

        /// <inheritdoc/>
        protected override void DoAdd(double scalar, Matrix result)
        {
            if (result.StorageType == Matrices.Storage.MatrixStorageType.Dense)
            {
                for (int i = 0; i < Storage.Data.Length; i++)
                {
                    result.Storage.Data[i] = Storage.Data[i] + scalar;
                }

                return;
            }

            base.DoAdd(scalar, result);
        }

        #endregion Methods
    }
}
