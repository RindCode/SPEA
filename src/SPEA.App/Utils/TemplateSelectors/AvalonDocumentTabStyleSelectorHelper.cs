// ==================================================================================================
// <copyright file="AvalonDocumentTabStyleSelectorHelper.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.Utils.TemplateSelectors
{
    using System.Windows;
    using System.Windows.Controls;
    using SPEA.App.ViewModels.SDocument;

    /// <summary>
    /// <see cref="DataTemplateSelector"/> for AvalonDock document tabs style.
    /// </summary>
    public class AvalonDocumentTabStyleSelectorHelper : StyleSelector
    {
        #region Preperties

        /// <summary>
        /// Gets or sets AvalonDock tabs style <see cref="DataTemplate"/>.
        /// </summary>
        public Style AvalonDocumentTabStyle { get; set; }

        #endregion properties

        #region Methods

        /// <inheritdoc/>
        public override Style SelectStyle(object item, DependencyObject container)
        {
            if (item is SDocumentViewModel)
            {
                return AvalonDocumentTabStyle;
            }

            return base.SelectStyle(item, container);
        }

        #endregion Methods
    }
}
