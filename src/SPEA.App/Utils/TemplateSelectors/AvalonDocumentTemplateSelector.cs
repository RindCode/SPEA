// ==================================================================================================
// <copyright file="AvalonDocumentTemplateSelector.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.Utils.TemplateSelectors
{
    using System.Windows;
    using System.Windows.Controls;
    using SPEA.App.ViewModels;

    /// <summary>
    /// <see cref="DataTemplateSelector"/> for AvalonDock document.
    /// </summary>
    public class AvalonDocumentTemplateSelector : DataTemplateSelector
    {
        #region Properties

        /// <summary>
        /// Gets or sets AvalonDock document <see cref="DataTemplate"/>.
        /// </summary>
        public DataTemplate AvalonDocumentMetallicDataTemplate { get; set; }

        #endregion Properties

        #region Methods

        /// <inheritdoc/>
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is SDocumentMetallicViewModel)
            {
                return AvalonDocumentMetallicDataTemplate;
            }

            return base.SelectTemplate(item, container);
        }

        #endregion Methods
    }
}
