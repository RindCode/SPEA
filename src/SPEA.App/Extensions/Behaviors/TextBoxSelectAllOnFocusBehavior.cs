// ==================================================================================================
// <copyright file="TextBoxSelectAllOnFocusBehavior.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.Extensions.Behaviors
{
    using System.Windows;
    using System.Windows.Controls;
    using Microsoft.Xaml.Behaviors;

    // TODO: Not working in DataGrid?

    /// <summary>
    /// Defines a behavior that allows to select all content of the <see cref="TextBox"/> it was attached to.
    /// </summary>
    public class TextBoxSelectAllOnFocusBehavior : Behavior<TextBox>
    {
        /// <inheritdoc/>
        protected override void OnAttached()
        {
            if (AssociatedObject != null)
            {
                AssociatedObject.GotFocus += AssociatedObject_GotFocus;
                base.OnAttached();
            }
        }

        /// <inheritdoc/>
        protected override void OnDetaching()
        {
            if (AssociatedObject != null)
            {
                AssociatedObject.GotFocus -= AssociatedObject_GotFocus;
                base.OnDetaching();
            }
        }

        // Selects all.
        private void AssociatedObject_GotFocus(object sender, RoutedEventArgs routedEventArgs)
        {
            if (AssociatedObject != null)
            {
                AssociatedObject.SelectAll();
            }
        }
    }
}
