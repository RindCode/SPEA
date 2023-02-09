// ==================================================================================================
// <copyright file="RectMatrixBase{T}.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Numerics.Matrices
{
    using System.Numerics;
    using SPEA.Numerics.Matrices.Storage;

    /// <summary>
    /// Represents a base class for rectangular matrices.
    /// </summary>
    /// <typeparam name="T">Matrix data type.</typeparam>
    public abstract class RectMatrixBase<T> : MatrixBase<T>
        where T : INumber<T>
    {

    }
}
