// ==================================================================================================
// <copyright file="SElementInfo.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.Models.SElements
{
    using CommunityToolkit.Mvvm.ComponentModel;

    /// <summary>
    /// Represents <see cref="Shapes.SRectPrimitive"/> entity information.
    /// </summary>
    public class SElementInfo : ObservableObject
    {
        private string _name;
        private object _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="SElementInfo"/> class.
        /// </summary>
        /// <param name="name">The item's name.</param>
        /// <param name="value">The item's value.</param>
        public SElementInfo(string name, object value)
        {
            _name = name;
            _value = value;
        }

        /// <summary>
        /// Gets or sets entry name.
        /// </summary>
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        /// <summary>
        /// Gets or sets entry value.
        /// </summary>
        public object Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }
    }
}
