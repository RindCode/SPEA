// ==================================================================================================
// <copyright file="SElementViewModelBase.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.ViewModels.SElements
{
    using System;
    using System.Collections.ObjectModel;
    using CommunityToolkit.Mvvm.ComponentModel;
    using SPEA.App.Models.SElements;
    using SPEA.Geometry.Core;
    using SPEA.Geometry.Primitives;

    /// <summary>
    /// Represents a base class for all cross-section elements view models.
    /// </summary>
    public abstract class SElementViewModelBase : ObservableObject, IDisposable
    {
        #region Fields

        private bool _disposed;
        ////private double _top;
        ////private double _left;
        ////private double _width;
        ////private double _height;

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
            if (!_disposed)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                _disposed = true;
            }
        }

        #endregion IDisposable

        #region Properties

        /// <summary>
        /// Gets the collection of element's entity info by mapping its properties
        /// and some additional metadata related to its underlying model.
        /// </summary>
        public abstract ObservableCollection<SElementInfo> EntityInfoItems { get; }

        /// <summary>
        /// Gets or sets the X-coordinate of the element's origin.
        /// </summary>
        public abstract double X0 { get; set; }

        /// <summary>
        /// Gets or sets the Y-coordinate of the element's origin.
        /// </summary>
        /// This value maps the model coordinate system and is different from screen
        /// coordinate system (Y-axis is reversed).
        /// </remarks>
        public abstract double Y0 { get; set; }

        /// <summary>
        /// Gets or sets the angle of rotation.
        /// </summary>
        public abstract double Angle { get; set; }

        /////// <summary>
        /////// Gets or sets the top-most bound of the S-element bounding box.
        /////// </summary>
        ////public double Top
        ////{
        ////    get => _top;
        ////    set
        ////    {
        ////        SetProperty(ref _top, value);
        ////    }
        ////}

        /////// <summary>
        /////// Gets or sets the left-most bound of the S-element bounding box.
        /////// </summary>
        ////public double Left
        ////{
        ////    get => _left;
        ////    set
        ////    {
        ////        SetProperty(ref _left, value);
        ////    }
        ////}

        /////// <summary>
        /////// Gets or sets the width of the S-element bounding box.
        /////// </summary>
        ////public double W
        ////{
        ////    get => _width;
        ////    set
        ////    {
        ////        SetProperty(ref _width, value);
        ////    }
        ////}

        /////// <summary>
        /////// Gets or sets the heigh of the S-element bounding box.
        /////// </summary>
        ////public double H
        ////{
        ////    get => _height;
        ////    set
        ////    {
        ////        SetProperty(ref _height, value);
        ////    }
        ////}

        #endregion Properties

        #region Methods

        #endregion Methods
    }
}
