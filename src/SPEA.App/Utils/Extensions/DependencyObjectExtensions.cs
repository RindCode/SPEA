// ==================================================================================================
// <copyright file="DependencyObjectExtensions.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.Utils.Extensions
{
    using System.Reflection;
    using System.Windows;

    /// <summary>
    /// Provides access to extension helpers for <see cref="DependencyProperty"/> objects.
    /// </summary>
    public static class DependencyObjectExtensions
    {
        #region Methods

        /// <summary>
        /// Finds a dependency property by the given name inside the object provided as a parameter.
        /// </summary>
        /// <param name="target">An object where a dependency property is searched for.</param>
        /// <param name="dpname">A dependency property name to search for.</param>
        /// <returns> if dependency property is found, otherwise <see langword="null"/>.</returns>
        public static DependencyProperty FindDependencyProperty(this DependencyObject target, string dpname)
        {
            var fieldInfo = target.GetType().GetField(dpname, BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

            // We put null as an argument since DependencyProperty is a static field and we don't need to provide specific instance.
            return fieldInfo == null ? null : (DependencyProperty)fieldInfo.GetValue(null);
        }

        /// <summary>
        /// Checks if the provided object has a dependency property of a given name.
        /// </summary>
        /// <param name="target">An object where a dependency property is searched for.</param>
        /// <param name="dpname">A dependency property name to search for.</param>
        /// <returns><see langword="true"/> if a such depency property is found, otherwise <see langword="fasle"/>.</returns>
        public static bool HasDependencyProperty(this DependencyObject target, string dpname)
        {
            return target.FindDependencyProperty(dpname) != null;
        }

        #endregion Methods
    }
}
