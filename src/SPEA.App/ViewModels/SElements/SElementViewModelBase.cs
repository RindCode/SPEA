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
    using System.Windows.Media;
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Messaging;
    using SPEA.Geometry.Core;
    using SPEA.Geometry.Transform;

    /// <summary>
    /// Represents a base class for all cross-section elements view models.
    /// </summary>
    public abstract class SElementViewModelBase : ObservableObject, IDisposable
    {
        #region Fields

        private readonly IMessenger _messenger;
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
        /// <param name="messenger">A reference to <see cref="IMessenger"/> instance.</param>
        protected SElementViewModelBase(IMessenger messenger)
        {
            _messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
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
        /// Gets a messenger reference.
        /// </summary>
        public IMessenger Messenger => _messenger;

        /// <summary>
        /// Gets a model object.
        /// </summary>
        public abstract SObject Model { get; }

        /// <summary>
        /// Gets the collection of element's entity info by mapping its properties
        /// and some additional metadata related to its underlying model.
        /// </summary>
        public abstract ObservableCollection<SElementInfoViewModel> EntityInfoItems { get; }

        /// <summary>
        /// Gets or sets the UI transformation matrix.
        /// </summary>
        /// <remarks>
        /// This matrix is different from the model one, which is defined in GCS.
        /// </remarks>
        public abstract Matrix TransformMatrix { get; set; }

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

        /// <summary>
        /// Sets the shape origin position based on given input values.
        /// </summary>
        /// <param name="x">The origin X coordinate.</param>
        /// <param name="y">The origin Y coordinate.</param>
        /// <param name="angle">
        /// The rotation angle around the origin.
        /// The positive direction is counter clockwise.
        /// </param>
        protected virtual void MoveOrigin(double x, double y, double angle)
        {
            // Blank.
        }

        /// <summary>
        /// Rotates the current shape around its bounding box center.
        /// </summary>
        /// <param name="angle">
        /// The rotation angle around the center of the bounding box.
        /// The positive direction is counter clockwise.
        /// </param>
        protected virtual void RotateAroundCenter(double angle)
        {
            // Blank.
        }

        /// <summary>
        /// Converts the model transformation object into a UI (screen) matrix suitable for binding.
        /// </summary>
        /// <param name="matrix"><see cref="GeneralTransformation"/> matrix used for creation of the UI <see cref="Matrix"/>.</param>
        /// <returns><see cref="Matrix"/> object.</returns>
        protected virtual Matrix ConvertToScreenTransformMatrix(GeneralTransformation matrix)
        {
            // We transpose the matrix and reflect it to convert it
            // from global coordinates into screen coordinates.

            var uiMatrix = new Matrix()
            {
                M11 = matrix.M00,
                M12 = matrix.M10,
                M21 = matrix.M01,
                M22 = matrix.M11,
                OffsetX = matrix.M02,
                OffsetY = matrix.M12,
            };

            uiMatrix.Scale(1, -1);

            return uiMatrix;
        }

        #endregion Methods
    }
}
