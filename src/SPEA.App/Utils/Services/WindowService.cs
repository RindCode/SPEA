// ==================================================================================================
// <copyright file="WindowService.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.Utils.Services
{
    using CommunityToolkit.Mvvm.ComponentModel;

    /// <summary>
    /// Provides a service for application windows.
    /// </summary>
    public static class WindowService
    {
        #region Methods

        /// <summary>
        /// Show a simple window.
        /// </summary>
        /// <typeparam name="T">A view model type used by a window which needs to be shown.</typeparam>
        /// <param name="viewName">A specific view name.</param>
        public static void ShowWindow<T>(string viewName = "")
            where T : ObservableObject
        {
            WindowLocatorService.FindWindow<T>(viewName).Show();
        }

        /// <summary>
        /// Show a modal window.
        /// </summary>
        /// <typeparam name="T">A view model type used by a window which needs to be shown.</typeparam>
        /// <param name="viewName">A specific view name.</param>
        public static void ShowModalWindow<T>(string viewName = "")
            where T : ObservableObject
        {
            WindowLocatorService.FindWindow<T>(viewName).ShowDialog();
        }

        #endregion Methods
    }
}
