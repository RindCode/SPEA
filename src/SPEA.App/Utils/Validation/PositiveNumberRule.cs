// ==================================================================================================
// <copyright file="PositiveNumberRule.cs" company="Dmitry Poberezhnyy">
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
    /// Implements a <see cref="ValidationRule"/> to check if a given number is positive.
    /// </summary>
    public class PositiveNumberRule : ValidationRule
    {
        #region Fields

        private readonly string _errorMessage = ResourcesHelper.GetApplicationResource<string>("S.Common.ValidationText.PositiveNumber");

        #endregion Fields

        #region Methods

        /// <inheritdoc/>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var number = Convert.ToDouble(value);  // = 0.0 if value == null

            if (number <= 0)
            {
                return new ValidationResult(false, _errorMessage);
            }

            return ValidationResult.ValidResult;
        }

        #endregion Methods
    }
}
