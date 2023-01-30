// ==================================================================================================
// <copyright file="AvalonActiveContentConverter.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.Utils.Converters
{
    using System;
    using System.Windows.Data;
    using SPEA.App.ViewModels.Interfaces;

    /// <summary>
    /// Converts a given object into <see cref="ISDocumentViewModel"/>.
    /// </summary>
    public class AvalonActiveContentConverter : IValueConverter
    {
        #region Methods

        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is ISDocumentViewModel)
            {
                return value;
            }

            return Binding.DoNothing;
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is ISDocumentViewModel)
            {
                return value;
            }

            return Binding.DoNothing;
        }

        #endregion Methods
    }
}
