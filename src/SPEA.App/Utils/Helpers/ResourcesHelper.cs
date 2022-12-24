// ==================================================================================================
// <copyright file="ResourcesHelper.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.Utils.Helpers
{
    using System.Windows;

    /// <summary>
    /// Provides helping methods for application scope resources.
    /// </summary>
    public static class ResourcesHelper
    {
        /// <summary>
        /// Returns the requested application scope resource.
        /// </summary>
        /// <typeparam name="T">Resource type.</typeparam>
        /// <param name="uri">Resource Uniform Resource Identifier.</param>
        /// <returns>The requested resource.</returns>
        public static T GetApplicationResource<T>(string uri)
        {
            return (T)Application.Current.Resources[uri];
        }
    }
}
