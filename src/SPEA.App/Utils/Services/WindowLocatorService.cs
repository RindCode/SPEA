// ==================================================================================================
// <copyright file="WindowLocatorService.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.Utils.Services
{
    using System;
    using System.Linq;
    using System.Reflection;
    using CommunityToolkit.Mvvm.ComponentModel;
    using SPEA.App.Controls;

    /// <summary>
    /// Provides a service for locating application windows.
    /// </summary>
    public static class WindowLocatorService
    {
        #region Methods

        /// <summary>
        /// Locates and returns a new instance of a window from the specified view model.
        /// </summary>
        /// <remarks>
        /// Each window has its own view model, and both of them should follow xxxWindow and xxxViewModel pattern,
        /// where "xxx" is the actual name of either view or view model, in example MainWindowView and MainWindowViewModel.
        /// That is, it's usually enough to provide a view model type as a generic parameter to locate a window related to it.
        /// However it's also possible to specify a view name directly is desired.
        /// </remarks>
        /// <typeparam name="T">A view model type used by a window which is searched for.</typeparam>
        /// <param name="viewName">A specific view name.</param>
        /// <returns><see cref="WindowBase"/> instance.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Unable to locate window type for the view model.</exception>
        public static WindowBase FindWindow<T>(string viewName = "")
            where T : ObservableObject
        {
            var windowName = string.Empty;
            var viewModelName = typeof(T).Name;
            if (string.IsNullOrEmpty(viewName))
            {
                // Assume that view and view model both follow xxxWindow and xxxViewModel pattern,
                // where xxx is the actual name of either view or view model,
                // in example: MainWindowView and MainWindowViewModel.
                windowName = viewModelName.Replace("ViewModel", "Window");
            }
            else
            {
                // Use the name provided in parameter.
                windowName = viewName;
            }

            // Search through all types defined in this assembly to find matching view (window) type, which:
            // a) Has the same name as provided in parameter or defined by naming convention.
            // b) Can be assigned to a type this methods return (some base Window class).
            var windowType = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(t => t.Name == windowName && typeof(WindowBase).IsAssignableFrom(t));
            if (windowType == null)
            {
                throw new ArgumentOutOfRangeException($"Unable to locate window type for the view model: {typeof(T)}");
            }

            return (WindowBase)Assembly.GetExecutingAssembly().CreateInstance(windowType.FullName);
        }

        #endregion Methods
    }
}
