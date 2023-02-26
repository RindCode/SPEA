// ==================================================================================================
// <copyright file="SRectViewModel.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.ViewModels.SElements
{
    /// <summary>
    /// Represents a view model for <see cref="Shapes.SRectPrimitive"/> shape.
    /// </summary>
    public class SRectViewModel : SElementViewModelBase
    {
        #region Fields

        private bool _disposed;
        private double _w;
        private double _h;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets the rectangle width.
        /// </summary>
        /// <remarks>
        /// Do not use <see cref="SElementViewModelBase.Width"/> property to define
        /// or access the shape width. Use this property instead.
        /// </remarks>
        public double W
        {
            get => _w;
            set => SetProperty(ref _w, value);
        }

        /// <summary>
        /// Gets or sets the rectangle height.
        /// </summary>
        /// <remarks>
        /// Do not use <see cref="SElementViewModelBase.Width"/> property to define
        /// or access the shape height. Use this property instead.
        /// </remarks>
        public double H
        {
            get => _h;
            set => SetProperty(ref _h, value);
        }

        #endregion Properties

        #region IDisposable

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Dispose managed state (managed objects)
                }

                // Free unmanaged resources (unmanaged objects) and override finalizer
                // Set large fields to null
                _disposed = true;
            }

            base.Dispose(disposing);
        }

        #endregion IDisposable

        #region Methods


        #endregion Methods
    }
}
