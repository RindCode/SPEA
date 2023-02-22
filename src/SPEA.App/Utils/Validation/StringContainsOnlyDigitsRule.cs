// ==================================================================================================
// <copyright file="StringContainsOnlyDigitsRule.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.Utils.Validation
{
    using System;
    using System.Globalization;
    using System.Windows.Controls;
    using SPEA.App.Utils.Helpers;

    /// <summary>
    /// Implements a <see cref="ValidationRule"/> to check if a given string contains only digits.
    /// </summary>
    public class StringContainsOnlyDigitsRule : ValidationRule
    {
        #region Fields

        private readonly string _errorMessage = ResourcesHelper.GetApplicationResource<string>("S.Common.ValidationText.StringContainsOnlyDigits");

        #endregion Fields

        #region Methods

        /// <inheritdoc/>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var str = Convert.ToString(value);

            if (string.IsNullOrEmpty(str) || !double.TryParse(str, out _))
            {
                return new ValidationResult(false, _errorMessage);
            }

            return ValidationResult.ValidResult;
        }

        #endregion Methods
    }
}
