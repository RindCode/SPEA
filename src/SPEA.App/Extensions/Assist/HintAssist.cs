﻿// ==================================================================================================
// <copyright file="HintAssist.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.Extensions.Assist
{
    using System.Windows;

    /// <summary>
    /// Provides various attached properties for a control's placeholders and hints.
    /// </summary>
    public static class HintAssist
    {
        #region Attached Properties

        /// <summary>
        /// <see cref="DependencyProperty"/> for <see cref="GetHint(DependencyObject)"/> getter
        /// and <see cref="SetHint(DependencyObject, object)"/> setter.
        /// </summary>
        public static readonly DependencyProperty HintProperty =
            DependencyProperty.RegisterAttached(
                "Hint",
                typeof(object),
                typeof(HintAssist),
                new PropertyMetadata(null));

        #endregion Attached Properties

        #region Attached Properties Methods

        /// <summary>
        /// Gets the value of <see cref="HintProperty"/>.
        /// </summary>
        /// <param name="obj">An object the value is get from.</param>
        /// <returns>Hint content.</returns>
        public static object GetHint(DependencyObject obj)
        {
            return (object)obj.GetValue(HintProperty);
        }

        /// <summary>
        /// Sets the value of <see cref="HintProperty"/>.
        /// </summary>
        /// <param name="obj">An object the value is set to.</param>
        /// <param name="value">Hint content.</param>
        public static void SetHint(DependencyObject obj, object value)
        {
            obj.SetValue(HintProperty, value);
        }

        #endregion Attached Properties Methods
    }
}
