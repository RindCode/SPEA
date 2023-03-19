// ==================================================================================================
// <copyright file="SElementInfoViewModel.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.ViewModels.SElements
{
    using System;
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Messaging;
    using CommunityToolkit.Mvvm.Messaging.Messages;

    /// <summary>
    /// Represents <see cref="Shapes.SRectPrimitive"/> entity information and acts
    /// as a data wrapper for binding purposes.
    /// </summary>
    public class SElementInfoViewModel : ObservableObject
    {
        private readonly IMessenger _messenger;
        private bool _isUpdatingFromMessage = false;
        private string _name;
        private Type _dataType;
        private object _value;
        private bool _isReadOnly;

        /// <summary>
        /// Initializes a new instance of the <see cref="SElementInfoViewModel"/> class.
        /// </summary>
        /// <param name="messenger">A reference to <see cref="IMessenger"/> instance.</param>
        /// <param name="name">The item's name.</param>
        /// <param name="dataType">The data type.</param>
        /// <param name="value">The item's value.</param>
        /// <param name="isReadOnly">The item is read-only.</param>
        public SElementInfoViewModel(IMessenger messenger, string name, Type dataType, object value, bool isReadOnly = false)
        {
            _messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
            ArgumentException.ThrowIfNullOrEmpty(nameof(name), name);

            _name = name;
            _dataType = dataType;
            _value = value;
            _isReadOnly = isReadOnly;

            Messenger.Register<PropertyChangedMessage<object>>(this, (r, m) => OnValueUpdated(m));
        }

        /// <summary>
        /// Gets a messenger reference.
        /// </summary>
        public IMessenger Messenger => _messenger;

        /// <summary>
        /// Gets or sets entry name.
        /// </summary>
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        /// <summary>
        /// Gets or sets the value type.
        /// </summary>
        public Type DataType
        {
            get => _dataType;
            set => SetProperty(ref _dataType, value);
        }

        /// <summary>
        /// Gets or sets the actual entry value.
        /// </summary>
        public object Value
        {
            get => _value;
            set
            {
                if (_isUpdatingFromMessage)
                {
                    // The request comes from another VM using messaging.
                    SetProperty(ref _value, value);
                }
                else
                {
                    // The request comes from the UI layer (propagate, no actual update here).
                    var oldValue = Convert.ChangeType(_value, DataType);
                    Messenger.Send(new PropertyChangedMessage<object>(this, Name, oldValue, value));
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this item is read-only.
        /// </summary>
        public bool IsReadOnly
        {
            get => _isReadOnly;
            set => SetProperty(ref _isReadOnly, value);
        }

        private void OnValueUpdated(PropertyChangedMessage<object> message)
        {
            _isUpdatingFromMessage = true;

            if (message.Sender is SElementViewModelBase && message.PropertyName == Name)
            {
                Value = message.NewValue;
            }

            _isUpdatingFromMessage = false;
        }
    }
}
