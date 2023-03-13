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
    public abstract partial class DenseMatrix : Matrix
    {
        #region Methods

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
        protected override void DoNegate(Matrix result)
        {
            if (result.StorageType == Matrices.Storage.MatrixStorageType.Dense)
            {
                ArrayHelper.ScaleArray(-1, Storage.Data, result.Storage.Data);
                return;
            }

            base.DoNegate(result);
        }

        /// <inheritdoc/>
        protected override void DoSubtract(double scalar, Matrix result)
        {
            if (result.StorageType == Matrices.Storage.MatrixStorageType.Dense)
            {
                for (int i = 0; i < Storage.Data.Length; i++)
                {
                    result.Storage.Data[i] = Storage.Data[i] - scalar;
                }

                return;
            }

            base.DoSubtract(scalar, result);
        }

        /// <inheritdoc/>
        protected override void DoSubtract(Matrix other, Matrix result)
        {
            // This = Dense (by definition), Other = Dense, Result = Dense.
            if (other.StorageType == Matrices.Storage.MatrixStorageType.Dense && result.StorageType == Matrices.Storage.MatrixStorageType.Dense)
            {
                ArrayHelper.SubtractArrays(Storage.Data, other.Storage.Data, result.Storage.Data);
                return;
            }

            base.DoSubtract(other, result);
        }

        /// <inheritdoc/>
        protected override void DoMultiply(double scalar, Matrix result)
        {
            if (result.StorageType == Matrices.Storage.MatrixStorageType.Dense)
            {
                ArrayHelper.ScaleArray(scalar, Storage.Data, result.Storage.Data);
                return;
            }

            base.DoMultiply(scalar, result);
        }

        /// <inheritdoc/>
        protected override void DoMultiply(Matrix other, Matrix result)
        {
            if (result.StorageType == Matrices.Storage.MatrixStorageType.Dense)
            {
                // Hints:
                // 1. https://stackoverflow.com/a/74881846
                base.DoMultiply(other, result);  // TODO: IMPORTANT! Replace with faster multiplication algorithm.
                return;
            }

            base.DoMultiply(other, result);
        }

        #endregion Methods
    }
}
