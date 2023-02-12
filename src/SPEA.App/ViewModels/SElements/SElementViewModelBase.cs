// ==================================================================================================
// <copyright file="SElementViewModelBase.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.ViewModels.SElements
{
    using System;
    using CommunityToolkit.Mvvm.ComponentModel;

    /// <summary>
    /// Represents a base class for all cross-section elements view models.
    /// </summary>
    public abstract class SElementViewModelBase : ObservableObject, IDisposable
    {
        #region Fields

        private bool _disposedValue;
        private double _top;
        private double _left;
        private double _width;
        private double _height;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SElementViewModelBase"/> class.
        /// </summary>
        protected SElementViewModelBase()
        {
            // Blank.
        }

        #endregion Constructors

        #region Destructors

        ////// Override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        ////~SElementViewModelBase()
        ////{
        ////    // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        ////    Dispose(disposing: false);
        ////}

        #endregion Destructors

        #region IDisposable

        /// <inheritdoc/>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Implements Dispose pattern.
        /// </summary>
        /// <param name="disposing">Designates whether the method was called from Dispose() or not.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                _disposedValue = true;
            }
        }

        #endregion IDisposable

        #region Properties

        /// <summary>
        /// Gets or sets the top-most bound of the S-element bounding box.
        /// </summary>
        public double Top
        {
            get => _top;
            set
            {
                SetProperty(ref _top, value);
            }
        }

        /// <summary>
        /// Gets or sets the left-most bound of the S-element bounding box.
        /// </summary>
        public double Left
        {
            get => _left;
            set
            {
                SetProperty(ref _left, value);
            }
        }

        /// <summary>
        /// Gets or sets the width of the S-element bounding box.
        /// </summary>
        public double Width
        {
            get => _width;
            set
            {
                SetProperty(ref _width, value);
            }
        }

        /// <summary>
        /// Gets or sets the heigh of the S-element bounding box.
        /// </summary>
        public double Height
        {
            get => _height;
            set
            {
                SetProperty(ref _height, value);
            }
        }

        #endregion Properties

        #region Methods

        #endregion Methods
    }
}
