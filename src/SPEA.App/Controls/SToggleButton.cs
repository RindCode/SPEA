// ==================================================================================================
// <copyright file="SToggleButton.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.Controls
{
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Shapes;

    /// <summary>
    /// Represents an extended <see cref="ToggleButton"/> control with additional features.
    /// </summary>
    public class SToggleButton : ToggleButton
    {
        #region Dependency Properties

        /// <summary>
        /// <see cref="DependencyProperty"/> for <see cref="IconChecked"/>.
        /// </summary>
        public static readonly DependencyProperty IconCheckedProperty =
            DependencyProperty.Register(
                nameof(IconChecked),
                typeof(Path),
                typeof(SToggleButton),
                new PropertyMetadata(default(Path)));

        /// <summary>
        /// Gets or sets an icon for checked state.
        /// </summary>
        public Path IconChecked
        {
            get { return (Path)GetValue(IconCheckedProperty); }
            set { SetValue(IconCheckedProperty, value); }
        }

        /// <summary>
        /// <see cref="DependencyProperty"/> for <see cref="IconUnchecked"/>.
        /// </summary>
        public static readonly DependencyProperty IconUncheckedProperty =
            DependencyProperty.Register(
                nameof(IconUnchecked),
                typeof(Path),
                typeof(SToggleButton),
                new PropertyMetadata(default(Path)));

        /// <summary>
        /// Gets or sets an icon for unchecked state.
        /// </summary>
        public Path IconUnchecked
        {
            get { return (Path)GetValue(IconUncheckedProperty); }
            set { SetValue(IconUncheckedProperty, value); }
        }

        #endregion Dependency Properties
    }
}
