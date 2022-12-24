// ==================================================================================================
// <copyright file="MessageBoxService.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.Utils.Services
{
    using System.Windows;

    // TODO: This needs to be replaced with a better custom implementation, which should be more flexible.
    //       We also need something for exceptions to show a stacktrace in the popup.
    // TODO: Create interface - something like IMessageBoxService?

    /// <summary>
    /// Provides a service to show a native message box.
    /// </summary>
    public static class MessageBoxService
    {
        /// <summary>
        /// Displays a native message box with specified text, caption, and style.
        /// </summary>
        /// <param name="text">Message text.</param>
        /// <param name="caption">Message box title.</param>
        /// <param name="button">Button(s) to be displayed.</param>
        /// <param name="icon">The icon image to be displayed.</param>
        /// <param name="defaultResult">A default message box result.</param>
        /// <param name="options">Special display options for a message box.</param>
        /// <returns><see cref="MessageBoxResult"/> result.</returns>
        public static MessageBoxResult Show(
            string text,
            string caption,
            MessageBoxButton button,
            MessageBoxImage icon,
            MessageBoxResult defaultResult,
            MessageBoxOptions options)
        {
            return MessageBox.Show(text, caption, button, icon, defaultResult, options);
        }

        /// <summary>
        /// Displays a native message box with specified text, caption, and style.
        /// </summary>
        /// <param name="text">Message text.</param>
        /// <param name="caption">Message box title.</param>
        /// <param name="button">Button(s) to be displayed.</param>
        /// <param name="icon">The icon image to be displayed.</param>
        /// <param name="defaultResult">A default message box result.</param>
        /// <returns><see cref="MessageBoxResult"/> result.</returns>
        public static MessageBoxResult Show(
            string text,
            string caption,
            MessageBoxButton button,
            MessageBoxImage icon,
            MessageBoxResult defaultResult)
        {
            return Show(text, caption, button, icon, defaultResult, MessageBoxOptions.None);
        }

        /// <summary>
        /// Displays a native message box with specified text, caption, and style.
        /// </summary>
        /// <param name="text">Message text.</param>
        /// <param name="caption">Message box title.</param>
        /// <param name="button">Button(s) to be displayed.</param>
        /// <param name="icon">The icon image to be displayed.</param>
        /// <returns><see cref="MessageBoxResult"/> result.</returns>
        public static MessageBoxResult Show(string text, string caption, MessageBoxButton button, MessageBoxImage icon)
        {
            return Show(text, caption, button, icon, MessageBoxResult.None, MessageBoxOptions.None);
        }

        /// <summary>
        /// Displays a native message box with specified text, caption, and style.
        /// </summary>
        /// <param name="text">Message text.</param>
        /// <param name="caption">Message box title.</param>
        /// <param name="button">Button(s) to be displayed.</param>
        /// <returns><see cref="MessageBoxResult"/> result.</returns>
        public static MessageBoxResult Show(string text, string caption, MessageBoxButton button)
        {
            return Show(text, caption, button, MessageBoxImage.None, MessageBoxResult.None, MessageBoxOptions.None);
        }

        /// <summary>
        /// Displays a native message box with specified text, caption, and style.
        /// </summary>
        /// <param name="text">Message text.</param>
        /// <param name="caption">Message box title.</param>
        /// <returns><see cref="MessageBoxResult"/> result.</returns>
        public static MessageBoxResult Show(string text, string caption)
        {
            return Show(text, caption, MessageBoxButton.OK, MessageBoxImage.None, MessageBoxResult.None, MessageBoxOptions.None);
        }

        /// <summary>
        /// Displays a native message box with specified text, caption, and style.
        /// </summary>
        /// <param name="text">Message text.</param>
        /// <returns><see cref="MessageBoxResult"/> result.</returns>
        public static MessageBoxResult Show(string text)
        {
            return Show(text, string.Empty, MessageBoxButton.OK, MessageBoxImage.None, MessageBoxResult.None, MessageBoxOptions.None);
        }
    }
}
