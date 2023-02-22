// ==================================================================================================
// <copyright file="BooleanAndMultiConverter.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.Utils.Converters
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Windows.Data;

    /// <summary>
    /// Converts multiple boolean values into a single one using AND operation.
    /// </summary>
    public class BooleanAndMultiConverter : IMultiValueConverter
    {
        #region Methods

        /// <inheritdoc/>
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return values.OfType<IConvertible>().All(System.Convert.ToBoolean);
        }

        /// <inheritdoc/>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}
