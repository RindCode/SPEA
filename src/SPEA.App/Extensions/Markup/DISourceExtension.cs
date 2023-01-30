// ==================================================================================================
// <copyright file="DISourceExtension.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.Extensions.Markup
{
    using System;
    using System.Windows.Markup;

    /// <summary>
    /// Provides a XAML markup extension to avoid setting data context in code behind.
    /// Returns a view model instance based on the provided type.
    /// </summary>
    public class DISourceExtension : MarkupExtension
    {
        #region Properties

        /// <summary>
        /// Gets or sets a view model resolver.
        /// </summary>
        public static Func<Type, object> Resolver { get; set; }

        /// <summary>
        /// Gets or sets a view model type to be requested.
        /// </summary>
        public Type Type { get; set; }

        #endregion Properties

        #region Methods

        /// <inheritdoc/>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Resolver?.Invoke(Type);
        }

        #endregion Methods
    }
}
