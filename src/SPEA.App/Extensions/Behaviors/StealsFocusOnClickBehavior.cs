// ==================================================================================================
// <copyright file="StealsFocusOnClickBehavior.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.Extensions.Behaviors
{
    using System.Windows;
    using System.Windows.Input;
    using Microsoft.Xaml.Behaviors;

    /// <summary>
    /// Defines a behavior that allows to steal focus when clicked on the element it was attached to.
    /// </summary>
    public class StealsFocusOnClickBehavior : Behavior<FrameworkElement>
    {
        /// <inheritdoc/>
        protected override void OnAttached()
        {
            if (AssociatedObject != null)
            {
                base.OnAttached();
                AssociatedObject.MouseDown += AssociatedObject_MouseDown;
            }
        }

        /// <inheritdoc/>
        protected override void OnDetaching()
        {
            if (AssociatedObject != null)
            {
                base.OnAttached();
                AssociatedObject.MouseDown -= AssociatedObject_MouseDown;
            }
        }

        // Clears focus.
        private static void AssociatedObject_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Keyboard.ClearFocus();
            var element = sender as FrameworkElement;
            if (element != null)
            {
                ((FrameworkElement)element).Focus();
            }
        }
    }
}
