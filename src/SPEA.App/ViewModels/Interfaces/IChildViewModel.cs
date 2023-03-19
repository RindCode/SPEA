// ==================================================================================================
// <copyright file="IChildViewModel.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.ViewModels.Interfaces
{
    using CommunityToolkit.Mvvm.ComponentModel;

    /// <summary>
    /// Provides a contract to specify a parent view model.
    /// </summary>
    /// <typeparam name="T">Parent view model type.</typeparam>
    public interface IChildViewModel<T>
        where T : ObservableObject
    {
        #region Properties

        /// <summary>
        /// Gets or sets a parent view model.
        /// </summary>
        public T ParentViewModel { get; set; }

        #endregion properties
    }
}