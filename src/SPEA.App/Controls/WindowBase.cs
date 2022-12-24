// ==================================================================================================
// <copyright file="WindowBase.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.Controls
{
    using System.Windows;

    /// <summary>
    /// Provides a base class for all windows within the application.
    /// </summary>
    public class WindowBase : Window
    {
        #region Dependency Properties

        /// <summary>
        /// <see cref="DependencyProperty"/> for <see cref="HeaderContent"/>.
        /// </summary>
        public static readonly DependencyProperty HeaderContentProperty =
            DependencyProperty.Register(
                "HeaderContent",
                typeof(FrameworkElement),
                typeof(WindowBase),
                new PropertyMetadata(null));

        #endregion Dependency Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowBase"/> class.
        /// </summary>
        public WindowBase()
            : base()
        {
            // Blank.
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets <see cref="WindowBase"/> header content.
        /// </summary>
        public FrameworkElement HeaderContent
        {
            get { return (FrameworkElement)GetValue(HeaderContentProperty); }
            set { SetValue(HeaderContentProperty, value); }
        }

        #endregion Properties
    }
}
