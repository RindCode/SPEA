// ==================================================================================================
// <copyright file="ValidFileNameRule.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.Utils.Validation
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Windows.Controls;
    using SPEA.App.Utils.Helpers;

    /// <summary>
    /// Implements a <see cref="ValidationRule"/> to check if a given string is a valid file name.
    /// </summary>
    public class ValidFileNameRule : ValidationRule
    {
        #region Fields

        private readonly string _errorMessage = ResourcesHelper.GetApplicationResource<string>("S.Common.ValidationText.ValidFileName");

        #endregion Fields

        #region Methods

        /// <inheritdoc/>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var stringValue = Convert.ToString(value);

            // TODO: "    .txt" is okay, while "    " is an illegal file name.
            //       Change to IsNullOrWhiteSpace?
            if (string.IsNullOrEmpty(stringValue) ||
                stringValue.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
            {
                return new ValidationResult(false, _errorMessage);
            }

            return ValidationResult.ValidResult;
        }

        #endregion Methods
    }
}
