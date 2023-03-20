// ==================================================================================================
// <copyright file="SRectViewModel.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.ViewModels.SElements
{
    using System;
    using System.Collections.ObjectModel;
    using System.Windows.Media;
    using CommunityToolkit.Mvvm.Messaging;
    using CommunityToolkit.Mvvm.Messaging.Messages;
    using SPEA.App.Utils.Helpers;
    using SPEA.Geometry.Events;
    using SPEA.Geometry.Primitives;

    /// <summary>
    /// Represents a view model for <see cref="Shapes.SRectPrimitive"/> shape.
    /// </summary>
    public class SRectViewModel : SElementViewModelBase
    {
        #region Fields

        private readonly ObservableCollection<SElementInfoViewModel> _entityInfoItems = new ObservableCollection<SElementInfoViewModel>();
        private readonly string _internalTypePropName = ResourcesHelper.GetApplicationResource<string>("S.SElements.EntityInfo.Common.InternalType");
        private bool _disposed;
        private SRect _model;
        private Matrix _transformMatrix;
        private double _x0;
        private double _y0;
        private double _angle;
        private double _h;
        private double _w;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SRectViewModel"/> class.
        /// </summary>
        /// <param name="rect"><see cref="SRect"/> model reference.</param>
        /// <param name="messenger">A reference to <see cref="IMessenger"/> instance.</param>
        public SRectViewModel(
            IMessenger messenger,
            SRect rect)
            : base(messenger)
        {
            _model = rect ?? throw new ArgumentNullException(nameof(messenger));

            _x0 = rect.LocalSystem.Origin.X;
            _y0 = rect.LocalSystem.Origin.Y;
            _angle = rect.LocalSystem.Angle;
            _h = rect.H;
            _w = rect.W;
            _transformMatrix = ConvertToScreenTransformMatrix(_model.LocalSystem.GlobalTransform);

            InitializeEntityInfoItems(rect);

            Messenger.Register<PropertyChangedMessage<object>>(this, (r, m) => OnValueUpdated(m));

            Model.LocationChanged += OnLocationChanged;
        }

        #endregion Constructors

        #region Properties

        /// <inheritdoc/>
        public override ObservableCollection<SElementInfoViewModel> EntityInfoItems => _entityInfoItems;

        /// <inheritdoc/>
        public override SRect Model => _model;

        /// <inheritdoc/>
        public override Matrix TransformMatrix
        {
            get => _transformMatrix;
            set
            {
                SetProperty(ref _transformMatrix, value);
            }
        }

        /// <inheritdoc/>
        public override double X0
        {
            get => _x0;
            set
            {
                if (IsUpdatingFromModel)
                {
                    SetProperty(ref _x0, value);
                }
                else
                {
                    MoveOrigin(value, Y0, Angle);
                }
            }
        }

        /// <inheritdoc/>
        public override double Y0
        {
            get => _model.Origin.Y;
            set
            {
                if (IsUpdatingFromModel)
                {
                    SetProperty(ref _y0, value);
                }
                else
                {
                    MoveOrigin(X0, value, Angle);
                }
            }
        }

        /// <inheritdoc/>
        public override double Angle
        {
            get => _angle;
            set
            {
                if (IsUpdatingFromModel)
                {
                    SetProperty(ref _angle, value);
                }
                else
                {
                    RotateAroundCenter(value);
                }
            }
        }

        /// <summary>
        /// Gets or sets the rectangle width.
        /// </summary>
        public double W
        {
            get => _w;
            set
            {
                _model = new SRect(_model.Origin, value, H);
                Messenger.Send(new PropertyChangedMessage<object>(this, nameof(W), _w, _model.W));
                SetProperty(ref _w, _model.H);
            }
        }

        /// <summary>
        /// Gets or sets the rectangle height.
        /// </summary>
        public double H
        {
            get => _h;
            set
            {
                _model = new SRect(_model.Origin, W, value);
                Messenger.Send(new PropertyChangedMessage<object>(this, nameof(H), _h, _model.H));
                SetProperty(ref _h, _model.H);
            }
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

        #region Initializers

        // Elements will appear in the table in the following order.
        private void InitializeEntityInfoItems(SRect model)
        {
            _entityInfoItems.Add(new SElementInfoViewModel(Messenger, _internalTypePropName, typeof(string), SRect.InternalType, true));
            _entityInfoItems.Add(new SElementInfoViewModel(Messenger, nameof(X0), typeof(double), _x0));
            _entityInfoItems.Add(new SElementInfoViewModel(Messenger, nameof(Y0), typeof(double), _y0));
            _entityInfoItems.Add(new SElementInfoViewModel(Messenger, nameof(Angle), typeof(double), _angle));
            _entityInfoItems.Add(new SElementInfoViewModel(Messenger, nameof(W), typeof(double), model.W));
            _entityInfoItems.Add(new SElementInfoViewModel(Messenger, nameof(H), typeof(double), model.H));
        }

        #endregion Initializers

        #region Methods

        // Updates a property value when the update request comes from messaging.
        private void OnValueUpdated(PropertyChangedMessage<object> message)
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
                        var value = (double)Convert.ChangeType(message.NewValue, sender.DataType);
                        X0 = value != X0 ? value : X0;
                    }

                    break;
                case nameof(Y0):
                    if (sender.DataType == typeof(double))
                    {
                        var value = (double)Convert.ChangeType(message.NewValue, sender.DataType);
                        Y0 = value != Y0 ? value : Y0;
                    }

                    break;
                case nameof(Angle):
                    if (sender.DataType == typeof(double))
                    {
                        var value = (double)Convert.ChangeType(message.NewValue, sender.DataType);
                        Angle = value != Angle ? value : Angle;
                    }

                    break;
                case nameof(W):
                    if (sender.DataType == typeof(double))
                    {
                        var value = (double)Convert.ChangeType(message.NewValue, sender.DataType);
                        W = value != W ? value : W;
                    }

                    break;
                case nameof(H):
                    if (sender.DataType == typeof(double))
                    {
                        var value = (double)Convert.ChangeType(message.NewValue, sender.DataType);
                        H = value != H ? value : H;
                    }

                    break;
                default:
                    return;
            }
        }

        #endregion Methods
    }
}
