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
    using System.ComponentModel;
    using SPEA.App.Models.SElements;
    using SPEA.App.Utils.Helpers;
    using SPEA.Geometry.Core;
    using SPEA.Geometry.Primitives;
    using SPEA.Geometry.Transform;

    /// <summary>
    /// Represents a view model for <see cref="Shapes.SRectPrimitive"/> shape.
    /// </summary>
    public class SRectViewModel : SElementViewModelBase
    {
        #region Fields

        private readonly ObservableCollection<SElementInfo> _entityInfoItems = new ObservableCollection<SElementInfo>();
        private readonly string _internalTypePropName = ResourcesHelper.GetApplicationResource<string>("S.SElements.EntityInfo.Common.InternalType");
        private bool _disposed;
        private SRect _model;
        private double _angle;
        private double _w;
        private double _h;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SRectViewModel"/> class.
        /// </summary>
        /// <param name="rect"><see cref="SRect"/> model reference.</param>
        public SRectViewModel(SRect rect)
        {
            ArgumentNullException.ThrowIfNull(rect, nameof(rect));

            _model = rect;
            _w = rect.W;
            _h = rect.H;

            // Elements order does matter.
            _entityInfoItems.Add(new SElementInfo(name: _internalTypePropName, value: SRect.InternalType, isReadOnly: true));
            _entityInfoItems.Add(new SElementInfo(name: nameof(X0), value: rect.Origin.X));
            _entityInfoItems.Add(new SElementInfo(name: nameof(Y0), value: rect.Origin.Y));
            _entityInfoItems.Add(new SElementInfo(name: nameof(Angle), value: 0));
            _entityInfoItems.Add(new SElementInfo(name: nameof(W), value: rect.W));
            _entityInfoItems.Add(new SElementInfo(name: nameof(H), value: rect.H));
            _entityInfoItems.Add(new SElementInfo(name: nameof(A), value: rect.A, isReadOnly: true));
            _entityInfoItems.Add(new SElementInfo(name: nameof(Cx), value: rect.Centroid.X, isReadOnly: true));
            _entityInfoItems.Add(new SElementInfo(name: nameof(Cy), value: rect.Centroid.Y, isReadOnly: true));

            // To track the changes done in DataGrid.
            for (int i = 1; i < _entityInfoItems.Count; i++)
            {
                PropertyChangedEventManager.AddHandler(EntityInfoItems[i], SRectViewModel_EntityInfoItemChanged, nameof(SElementInfo.Value));
            }
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets a model object.
        /// </summary>
        public SRect Model => _model;

        /// <inheritdoc/>
        public override double X0
        {
            get => _model.Origin.X;
            set
            {
                var newOrigin = new SPoint(value, _model.Origin.Y);
                SetProperty(_model.Origin, newOrigin, _model, (model, origin) => model.MoveOriginTo(origin));
                _entityInfoItems[1].Value = value;
            }
        }

        /// <inheritdoc/>
        public override double Y0
        {
            get => _model.Origin.Y;
            set
            {
                var newOrigin = new SPoint(_model.Origin.X, value);
                SetProperty(_model.Origin, newOrigin, _model, (model, origin) => model.MoveOriginTo(origin));
                _entityInfoItems[2].Value = value;
            }
        }

        /// <inheritdoc/>
        public override double Angle
        {
            get => _angle;
            set
            {
                var cg = _model.Shell.Centroid;
                _model.Rotate(value, cg, TransformationType.RelativeToInitial);
                SetProperty(ref _angle, value);
                _entityInfoItems[3].Value = value;
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
                SetProperty(ref _w, value);
                _entityInfoItems[4].Value = value;
                _model = new SRect(_model.Origin, value, _model.H);
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
                SetProperty(ref _h, value);
                _entityInfoItems[5].Value = value;
                _model = new SRect(_model.Origin, _model.W, value);
            }
        }

        public double A => _model.A;

        public double Cx => _model.Centroid.X;

        public double Cy => _model.Centroid.Y;

        /// <inheritdoc/>
        public override ObservableCollection<SElementInfo> EntityInfoItems => _entityInfoItems;

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

        private void SRectViewModel_EntityInfoItemChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (sender is not SElementInfo obj)
            {
                return;
            }

            if (obj.Value is not string strVal)
            {
                return;
            }

            var isParsed = double.TryParse(strVal, out double value);
            if (!isParsed)
            {
                return;
            }

            // If EntityInfo was changed first, then this code will merely update a VM property.
            // If a VM property was changed first, its setter will also update EntityInfo property,
            // and then this code will be invoked. To avoid recursion, we compare VM and EntityInfo
            // values since the event is raised after all properties are updated.

            var targetProperty = obj.Name;
            switch (targetProperty)
            {
                case nameof(X0):
                    if (X0 != value) { X0 = value; }
                    break;
                case nameof(Y0):
                    if (Y0 != value) { Y0 = value; }
                    break;
                case nameof(Angle):
                    if (Angle != value) { Angle = value; }
                    break;
                case nameof(W):
                    if (W != value) { W = value; }
                    break;
                case nameof(H):
                    if (H != value) { H = value; }
                    break;
                default:
                    return;
            }
        }

        #endregion Methods
    }
}
