// ==================================================================================================
// <copyright file="IDisposableViewModel.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.ViewModels.Interfaces
{
    using System;
    using CommunityToolkit.Mvvm.Input;

    /// <summary>
    /// Represents a contact to specify a view model that can be disposed
    /// through a command call.
    /// </summary>
    /// <remarks>
    /// <para>
    /// A typical usage of this interface is a view model for a window.
    /// When invoked, <see cref="DisposeViewModelCommand"/> command should call
    /// <see cref="IDisposable.Dispose"/> method, where all managed and unmanaged
    /// resources can be disposed, as well events can be unsubscribed.
    /// </para>
    /// <para>
    /// This command can be called using EventTrigger mechanism, i.e. when
    /// <see cref="System.Windows.Window.Closed"/> event is raised.
    /// </para>
    /// </remarks>
    public interface IDisposableViewModel : IDisposable
    {
        /// <summary>
        /// Gets a command that requests the selected view model to be disposed.
        /// </summary>
        public RelayCommand DisposeViewModelCommand { get; }
    }
}
