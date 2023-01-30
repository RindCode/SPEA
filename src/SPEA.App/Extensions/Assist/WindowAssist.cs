// ==================================================================================================
// <copyright file="WindowAssist.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.Extensions.Assist
{
    using System.Windows;
    using System.Windows.Input;

    /// <summary>
    /// Provides attached properties for windows.
    /// </summary>
    public class WindowAssist
    {
        #region Attached Properties

        /// <summary>
        /// <see cref="DependencyProperty"/> for <see cref="GetMinimizeWindowCommand(DependencyObject)"/> getter
        /// and <see cref="SetMinimizeWindowCommand(DependencyObject, ICommand)"/> setter.
        /// </summary>
        public static readonly DependencyProperty MinimizeWindowCommandProperty =
            DependencyProperty.RegisterAttached(
                "MinimizeWindowCommand",
                typeof(ICommand),
                typeof(WindowAssist),
                new PropertyMetadata(null));

        /// <summary>
        /// Gets the value of <see cref="MinimizeWindowCommandProperty"/>.
        /// </summary>
        /// <param name="obj">An object the value is get from.</param>
        /// <returns><see cref="ICommand"/> instance.</returns>
        public static ICommand GetMinimizeWindowCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(MinimizeWindowCommandProperty);
        }

        /// <summary>
        /// Sets the value of <see cref="MinimizeWindowCommandProperty"/>.
        /// </summary>
        /// <param name="obj">An object the value is set to.</param>
        /// <param name="value">A reference to <see cref="ICommand"/> instance.</param>
        public static void SetMinimizeWindowCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(MinimizeWindowCommandProperty, value);
        }

        /// <summary>
        /// <see cref="DependencyProperty"/> for <see cref="GetMaximizeWindowCommand(DependencyObject)"/> getter
        /// and <see cref="SetMaximizeWindowCommand(DependencyObject, ICommand)"/> setter.
        /// </summary>
        public static readonly DependencyProperty MaximizeWindowCommandProperty =
            DependencyProperty.RegisterAttached(
                "MaximizeWindowCommand",
                typeof(ICommand),
                typeof(WindowAssist),
                new PropertyMetadata(null));

        /// <summary>
        /// Gets the value of <see cref="MaximizeWindowCommandProperty"/>.
        /// </summary>
        /// <param name="obj">An object the value is get from.</param>
        /// <returns><see cref="ICommand"/> instance.</returns>
        public static ICommand GetMaximizeWindowCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(MaximizeWindowCommandProperty);
        }

        /// <summary>
        /// Sets the value of <see cref="MaximizeWindowCommandProperty"/>.
        /// </summary>
        /// <param name="obj">An object the value is set to.</param>
        /// <param name="value">A reference to <see cref="ICommand"/> instance.</param>
        public static void SetMaximizeWindowCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(MaximizeWindowCommandProperty, value);
        }

        /// <summary>
        /// <see cref="DependencyProperty"/> for <see cref="GetRestoreWindowCommand(DependencyObject)"/> getter
        /// and <see cref="SetRestoreWindowCommand(DependencyObject, ICommand)"/> setter.
        /// </summary>
        public static readonly DependencyProperty RestoreWindowCommandProperty =
            DependencyProperty.RegisterAttached(
                "RestoreWindowCommand",
                typeof(ICommand),
                typeof(WindowAssist),
                new PropertyMetadata(null));

        /// <summary>
        /// Gets the value of <see cref="RestoreWindowCommandProperty"/>.
        /// </summary>
        /// <param name="obj">An object the value is get from.</param>
        /// <returns><see cref="ICommand"/> instance.</returns>
        public static ICommand GetRestoreWindowCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(RestoreWindowCommandProperty);
        }

        /// <summary>
        /// Sets the value of <see cref="RestoreWindowCommandProperty"/>.
        /// </summary>
        /// <param name="obj">An object the value is set to.</param>
        /// <param name="value">A reference to <see cref="ICommand"/> instance.</param>
        public static void SetRestoreWindowCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(RestoreWindowCommandProperty, value);
        }

        /// <summary>
        /// <see cref="DependencyProperty"/> for <see cref="GetCloseWindowCommand(DependencyObject)"/> getter
        /// and <see cref="SetCloseWindowCommand(DependencyObject, ICommand)"/> setter.
        /// </summary>
        public static readonly DependencyProperty CloseWindowCommandProperty =
            DependencyProperty.RegisterAttached(
                "CloseWindowCommand",
                typeof(ICommand),
                typeof(WindowAssist),
                new PropertyMetadata(null));

        /// <summary>
        /// Gets the value of <see cref="CloseWindowCommandProperty"/>.
        /// </summary>
        /// <param name="obj">An object the value is get from.</param>
        /// <returns><see cref="ICommand"/> instance.</returns>
        public static ICommand GetCloseWindowCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(CloseWindowCommandProperty);
        }

        /// <summary>
        /// Sets the value of <see cref="CloseWindowCommandProperty"/>.
        /// </summary>
        /// <param name="obj">An object the value is set to.</param>
        /// <param name="value">A reference to <see cref="ICommand"/> instance.</param>
        public static void SetCloseWindowCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(CloseWindowCommandProperty, value);
        }

        #endregion Attached Properties
    }
}
