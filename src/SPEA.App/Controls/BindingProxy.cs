// ==================================================================================================
// <copyright file="BindingProxy.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.Controls
{
    using System.Windows;

    /// <summary>
    /// Represents a bindable proxy object.
    /// </summary>
    /// <remarks>
    /// <para>
    /// <see cref="BindingProxy"/> extends <see cref="Freezable"/> type, which has a useful
    /// feature to inherit <see cref="FrameworkElement.DataContext"/> even if the element is not a part
    /// of the visual or logical tree.
    /// </para>
    /// <para>
    /// To use this proxy element, define it in the object's resources and bind <see cref="Data"/>
    /// dependency property to the object's <see cref="FrameworkElement.DataContext"/> using the following syntax:
    /// <code>
    /// &lt;SomeControlObject.Resources&gt;
    ///     &lt;BindingProxy x:Key="SomeControlObjectProxy" Data="{Binding}"/&gt;
    /// &lt;/SomeControlObject.Resources&gt;
    /// </code>
    /// </para>
    /// </remarks>
    public class BindingProxy : Freezable
    {
        // See more: https://thomaslevesque.com/2011/03/21/wpf-how-to-bind-to-data-when-the-datacontext-is-not-inherited/

        #region Dependency Properties

        /// <summary>
        /// DependencyProperty for <see cref="Data"/> property.
        /// </summary>
        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register(
                "Data",
                typeof(object),
                typeof(BindingProxy),
                new UIPropertyMetadata(null));

        /// <summary>
        /// Gets or sets the <see cref="BindingProxy"/> data.
        /// </summary>
        public object Data
        {
            get { return (object)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        #endregion Dependency Properties

        #region Methods

        /// <inheritdoc/>
        protected override Freezable CreateInstanceCore()
        {
            return new BindingProxy();
        }

        #endregion Methods
    }
}
