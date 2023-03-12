// ==================================================================================================
// <copyright file="SPolygonViewModel.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.ViewModels.SElements
{
    using System;
    using System.Collections.ObjectModel;
    using SPEA.App.Models.SElements;
    using SPEA.Geometry.Core;
    using SPEA.Geometry.Primitives;

    /// <summary>
    /// Represent a view model for <see cref="SPolygon"/> model object.
    /// </summary>
    public class SPolygonViewModel : SElementViewModelBase
    {
        #region Fields

        private readonly SPolygon _model;
        private readonly ObservableCollection<SElementInfo> _entityInfoItems = new ObservableCollection<SElementInfo>();

        private bool _disposed;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SPolygonViewModel"/> class.
        /// </summary>
        /// <param name="polygon"><see cref="SPolygon"/> model reference.</param>
        public SPolygonViewModel(SPolygon polygon)
        {
            ArgumentNullException.ThrowIfNull(polygon, nameof(polygon));

            _model = polygon;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets a model object.
        /// </summary>
        public virtual SPolygon Model => _model;

        /// <inheritdoc/>
        public override double X0
        {
            get => _model.Origin.X;
            set
            {
                SetProperty(_model.Origin.X, value, _model, (model, x) => model.Origin = new SPoint(x, model.Origin.Y));
            }
        }

        /// <inheritdoc/>
        public override double Y0
        {
            get => _model.Origin.Y;
            set
            {
                SetProperty(_model.Origin.Y, value, _model, (model, y) => model.Origin = new SPoint(model.Origin.X, y));
            }
        }

        /// <inheritdoc/>
        public override double Angle
        {
            get => _model.Origin.Y;
            set
            {
                ////SetProperty(_model.Origin.Y, value, _model, (model, y) => model.Origin = new SPoint(model.Origin.X, y));
            }
        }

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
    }
}
