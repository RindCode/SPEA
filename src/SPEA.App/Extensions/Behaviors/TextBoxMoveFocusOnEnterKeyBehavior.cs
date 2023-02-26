// ==================================================================================================
// <copyright file="TextBoxMoveFocusOnEnterKeyBehavior.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.Extensions.Behaviors
{
    using System.Windows.Controls;
    using System.Windows.Input;
    using Microsoft.Xaml.Behaviors;

    /// <summary>
    /// Defines a behavior that moves focus from the <see cref="TextBox"/>
    /// to the next focusable element in the tree, when Enter button is clicked.
    /// </summary>
    public class TextBoxMoveFocusOnEnterKeyBehavior : Behavior<TextBox>
    {
        /// <inheritdoc/>
        protected override void OnAttached()
        {
            if (AssociatedObject != null)
            {
                base.OnAttached();
                AssociatedObject.KeyDown += AssociatedObject_KeyDown;
            }
        }

        /// <inheritdoc/>
        protected override void OnDetaching()
        {
            if (AssociatedObject != null)
            {
                AssociatedObject.KeyDown -= AssociatedObject_KeyDown;
                base.OnDetaching();
            }
        }

        // Moves focus from the attached TextBox to the next focusable element in the tree.
        private void AssociatedObject_KeyDown(object sender, KeyEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null)
            {
                if (e.Key == Key.Return || e.Key == Key.Enter)
                {
                    textBox.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                }
            }
        }
    }
}
