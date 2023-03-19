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
    using System.Windows.Media;
    using CommunityToolkit.Mvvm.Messaging;
    using SPEA.Geometry.Core;

    /// <summary>
    /// Represent a view model for <see cref="SPolygon"/> model object.
    /// </summary>
    public class SPolygonViewModel
    {
        ////#region Fields

        ////private readonly SPolygon _model;
        ////private readonly ObservableCollection<SElementInfoViewModel> _entityInfoItems = new ObservableCollection<SElementInfoViewModel>();

        ////private bool _disposed;
        ////private Matrix _transformMatrix;

        ////#endregion Fields

        ////#region Constructors

        /////// <summary>
        /////// Initializes a new instance of the <see cref="SPolygonViewModel"/> class.
        /////// </summary>
        /////// <param name="polygon"><see cref="SPolygon"/> model reference.</param>
        /////// <param name="messenger">A reference to <see cref="IMessenger"/> instance.</param>
        ////public SPolygonViewModel(
        ////    IMessenger messenger,
        ////    SPolygon polygon)
        ////    : base(messenger)
        ////{
        ////    ArgumentNullException.ThrowIfNull(polygon, nameof(polygon));

        ////    _model = polygon;
        ////}

        ////#endregion Constructors

        ////#region Properties

        /////// <inheritdoc/>
        ////public override SObject Model => throw new NotImplementedException();

        /////// <inheritdoc/>
        ////public override Matrix TransformMatrix
        ////{
        ////    get => _transformMatrix;
        ////    set
        ////    {
        ////        SetProperty(ref _transformMatrix, value);
        ////    }
        ////}

        /////// <inheritdoc/>
        ////public override double X0
        ////{
        ////    get => _model.Origin.X;
        ////    set
        ////    {
        ////        var newOrigin = new SPoint(value, _model.Origin.Y);
        ////        ////SetProperty(_model.Origin, newOrigin, _model, (model, origin) => model.SetOriginTo(origin));
        ////    }
        ////}

        /////// <inheritdoc/>
        ////public override double Y0
        ////{
        ////    get => _model.Origin.Y;
        ////    set
        ////    {
        ////        var newOrigin = new SPoint(_model.Origin.X, value);
        ////        ////SetProperty(_model.Origin, newOrigin, _model, (model, origin) => model.SetOriginTo(origin));
        ////    }
        ////}

        /////// <inheritdoc/>
        ////public override double Angle
        ////{
        ////    get => 0;
        ////    set
        ////    {
        ////        ////SetProperty(_model.Origin.Y, value, _model, (model, y) => model.Origin = new SPoint(model.Origin.X, y));
        ////    }
        ////}

        /////// <inheritdoc/>
        ////public override ObservableCollection<SElementInfoViewModel> EntityInfoItems => _entityInfoItems;

        ////#endregion Properties

        ////#region IDisposable

        /////// <inheritdoc/>
        ////protected override void Dispose(bool disposing)
        ////{
        ////    if (!_disposed)
        ////    {
        ////        if (disposing)
        ////        {
        ////            // Dispose managed state (managed objects)
        ////        }

        ////        // Free unmanaged resources (unmanaged objects) and override finalizer
        ////        // Set large fields to null
        ////        _disposed = true;
        ////    }

        ////    base.Dispose(disposing);
        ////}

        ////#endregion IDisposable
    }
}
