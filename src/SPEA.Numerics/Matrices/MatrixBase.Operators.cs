// ==================================================================================================
// <copyright file="MatrixBase.Operators.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Numerics.Matrices
{
    /// <summary>
    /// Specifies the supported ordering schemes.
    /// </summary>
    public abstract partial class MatrixBase : IEquatable<MatrixBase>
    {
        #region Methods

        public static MatrixBase operator +(MatrixBase left, MatrixBase right) => Add(left, right);

        #endregion Methods
    }
}
