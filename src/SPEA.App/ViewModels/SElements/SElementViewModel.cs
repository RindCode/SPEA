﻿// ==================================================================================================
// <copyright file="SElementViewModel.cs" company="Dmitry Poberezhnyy">
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
    using CommunityToolkit.Mvvm.Messaging.Messages;
    using SPEA.App.Messaging.Tokens;
    using SPEA.App.Utils.Helpers;
    using SPEA.Geometry.Core;
    using SPEA.Geometry.Events;
    using SPEA.Geometry.Transform;

    /// <summary>
    /// Represents a base class for all cross-section elements view models.
    /// </summary>
    public abstract class SElementViewModel : ObservableObject, IDisposable
    {
        #region Fields

        private readonly IMessenger _messenger;
        private readonly SElementViewModelToken _entityInfoMessageToken;
        private bool _disposed;
        private bool _isUpdatingFromModel = false;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SElementViewModel"/> class.
        /// </summary>
        /// <param name="messenger">A reference to <see cref="IMessenger"/> instance.</param>
        protected SElementViewModel(IMessenger messenger)
        {
            _messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
            _entityInfoMessageToken = new SElementViewModelToken(Guid.NewGuid());

            Messenger.Register<PropertyChangedMessage<object>, SElementViewModelToken>(this, _entityInfoMessageToken, (r, m) => OnPropertyChangeMessageReceived(m));
        }

        #endregion Constructors

        #region Destructors

        ////// Override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        ////~SElementViewModel()
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
        /// Gets a messenger token for communicating with <see cref="SElementInfoViewModel"/> instances.
        /// </summary>
        public SElementViewModelToken EntityInfoMessageToken => _entityInfoMessageToken;

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

        /// <summary>
        /// Gets or sets a value indicating whether the view model
        /// is being updated from the model data, and not from some other view model.
        /// </summary>
        protected bool IsUpdatingFromModel
        {
            get => _isUpdatingFromModel;
            set => _isUpdatingFromModel = value;
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Sets the shape origin position based on given input values.
        /// </summary>
        /// <param name="x">The origin X coordinate.</param>
        /// <param name="y">The origin Y coordinate.</param>
        /// The rotation angle around the origin.
        /// The positive direction is counter clockwise.
        /// </param>
        protected virtual void MoveOrigin(double x, double y)
        {
            Model.MoveOrigin(x, y);
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
            Model.RotateAroundCenter(angle);
        }

        /// <summary>
        /// Converts the model transformation object into a UI (screen) matrix suitable for binding.
        /// </summary>
        /// <param name="matrix"><see cref="GeneralTransformation"/> matrix used for creation of the UI <see cref="Matrix"/>.</param>
        /// <returns><see cref="Matrix"/> object.</returns>
        protected virtual Matrix ConvertToScreenTransformMatrix(GeneralTransformation matrix)
        {
            // Transpose.
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

        /// <summary>
        /// Provides derived classes an opportunity to implement its own logic on updating their
        /// properties in response to <see cref="PropertyChangedMessage{T}"/> message.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method is mainly used for <see cref="EntityInfoItems"/> sync purposes.
        /// </para>
        /// <para>
        /// A base implementation must be called when overridden.
        /// </para>
        /// </remarks>
        /// <param name="message">A property changed object.</param>
        protected virtual void OnPropertyChangeMessageReceived(PropertyChangedMessage<object> message)
        {
            var sender = message.Sender as SElementInfoViewModel;
            var targetProperty = message.PropertyName;
            if (sender == null || string.IsNullOrEmpty(targetProperty))
            {
                return;
            }

            switch (targetProperty)
            {
                case nameof(X0):
                    if (sender.DataType == typeof(double))
                    {
                        var isConverted = DoubleUtilHelper.SafeConvert(message.NewValue, out var value);
                        X0 = (isConverted == true && value != X0) ? value : X0;
                    }

                    break;
                case nameof(Y0):
                    if (sender.DataType == typeof(double))
                    {
                        var isConverted = DoubleUtilHelper.SafeConvert(message.NewValue, out var value);
                        Y0 = (isConverted == true && value != Y0) ? value : Y0;
                    }

                    break;
                case nameof(Angle):
                    if (sender.DataType == typeof(double))
                    {
                        var isConverted = DoubleUtilHelper.SafeConvert(message.NewValue, out var value);
                        Angle = (isConverted == true && value != Angle) ? value : Angle;
                    }

                    break;

                default:
                    return;
            }
        }

        /// <summary>
        /// Provides derived classes an opportunity to override <see cref="SObject.LocationChanged"/> event handling.
        /// </summary>
        /// <param name="sender">A reference to the oject which raised the event.</param>
        /// <param name="e">Events arguments data.</param>
        protected virtual void Model_LocationChanged(object sender, LocationChangedEventArgs e)
        {
            TransformMatrix = ConvertToScreenTransformMatrix(Model.LocalSystem.GlobalTransform);

            IsUpdatingFromModel = true;

            X0 = Model.LocalSystem.Origin.X;
            Y0 = Model.LocalSystem.Origin.Y;
            Angle = Model.LocalSystem.Angle;

            IsUpdatingFromModel = false;

            Messenger.Send(new PropertyChangedMessage<object>(this, nameof(X0), e.OldOrigin.X, Model.LocalSystem.Origin.X), EntityInfoMessageToken);
            Messenger.Send(new PropertyChangedMessage<object>(this, nameof(Y0), e.OldOrigin.Y, Model.LocalSystem.Origin.Y), EntityInfoMessageToken);
            Messenger.Send(new PropertyChangedMessage<object>(this, nameof(Angle), e.OldOrigin, Model.LocalSystem.Angle), EntityInfoMessageToken);
        }

        /// <summary>
        /// Provides derived classes an opportunity to use a centralized way
        /// to subscribe to the model's events.
        /// </summary>
        protected virtual void SubscribeModelEvents()
        {
            ArgumentNullException.ThrowIfNull(nameof(Model));
        }

        /// <summary>
        /// Provides derived classes an opportunity to use a centralized way
        /// to unsubscribe from the model's events.
        /// </summary>
        protected virtual void UnsubscribeModelEvents()
        {
            ArgumentNullException.ThrowIfNull(nameof(Model));
        }

        #endregion Methods
    }
}
