// ==================================================================================================
// <copyright file="NotEmptyFieldRule.cs" company="Dmitry Poberezhnyy">
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
    /// Implements a <see cref="ValidationRule"/> to check if a given string
    /// is not null, empty or contains only whitespaces.
    /// </summary>
    public class NotEmptyFieldRule : ValidationRule
    {
        #region Fields

        private readonly string _errorMessage = ResourcesHelper.GetApplicationResource<string>("S.Common.ValidationText.NotEmptyField");

        #endregion Fields

        #region Methods

        /// <inheritdoc/>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var stringValue = Convert.ToString(value);

            if (string.IsNullOrWhiteSpace(stringValue))
            {
                return new ValidationResult(false, _errorMessage);
            }

            return ValidationResult.ValidResult;
        }

        #endregion Methods
    }
}
