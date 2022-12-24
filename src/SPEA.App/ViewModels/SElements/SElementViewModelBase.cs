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
    using SPEA.App.ViewModels;
    using SPEA.App.ViewModels.Interfaces;

    /// <summary>
    /// Represents a base class for all cross-section elements view models.
    /// </summary>
    public abstract class SElementViewModelBase : ObservableObject, IDisposable, IChildViewModel<MainViewModel>
    {
        #region Fields

        // To detect redundant calls in IDisposable.
        private bool _disposedValue;

        // Hold the coordinate for the top-most bound of the S-element bounding box.
        private double _top;

        // Hold the coordinate for the left-most bound of the S-element bounding box.
        private double _left;

        // Holds the width of the S-element bounding box.
        private double _width;

        // Holds the heigh of the S-element bounding box.
        private double _height;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SElementViewModelBase"/> class.
        /// </summary>
        /// <param name="owner">A reference to a parent view model.</param>
        public SElementViewModelBase(MainViewModel owner)
            : this()
        {
            Owner = owner;
        }

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

        /// <inheritdoc/>
        public MainViewModel Owner { get; set; }

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

        #region Commands

        #endregion Commands

        #region Commands Logic

        #endregion Commands Logic

        #region Methods

        #endregion Methods
    }
}
