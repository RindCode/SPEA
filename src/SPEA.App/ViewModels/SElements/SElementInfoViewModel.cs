// ==================================================================================================
// <copyright file="SElementInfoViewModel.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.ViewModels.SElements
{
    using System;
    using System.Diagnostics;
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Messaging;
    using CommunityToolkit.Mvvm.Messaging.Messages;
    using SPEA.App.Messaging.Tokens;

    /// <summary>
    /// Represents <see cref="Shapes.SRectPrimitive"/> entity information and acts
    /// as a data wrapper for binding purposes.
    /// </summary>
    /// <remarks>
    /// This view model is intended to be connected with the <see cref="SElementViewModel"/> view model and
    /// only acts as a loosely referenced object, synchronized with the main view model instance.
    /// </remarks>
    public class SElementInfoViewModel : ObservableObject
    {
        // This class should not be used as a standalone view model,
        // neither should update any data (Value prop) directly, but rather
        // from a message coming from the main view model, since the validation
        // is also done it there.

        #region Fields

        private readonly IMessenger _messenger;
        private readonly SElementViewModelToken _token;
        private bool _isUpdatingFromMessage = false;
        private string _name;
        private Type _dataType;
        private object _value;
        private bool _isReadOnly;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SElementInfoViewModel"/> class.
        /// </summary>
        /// <param name="messenger">A reference to <see cref="IMessenger"/> instance.</param>
        /// <param name="token">A unique token requied for messaging communication with the main <see cref="SElementViewModel"/>.</param>
        /// <param name="name">The item's name.</param>
        /// <param name="dataType">The data type.</param>
        /// <param name="value">The item's value.</param>
        /// <param name="isReadOnly">The item is read-only.</param>
        public SElementInfoViewModel(IMessenger messenger, SElementViewModelToken token, string name, Type dataType, object value, bool isReadOnly = false)
        {
            _messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
            _token = token;
            ArgumentException.ThrowIfNullOrEmpty(nameof(name), name);

            _name = name;
            _dataType = dataType;
            _value = value;
            _isReadOnly = isReadOnly;

            Messenger.Register<PropertyChangedMessage<object>, SElementViewModelToken>(this, token, (r, m) => OnValueUpdated(m));
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets a messenger reference.
        /// </summary>
        public IMessenger Messenger => _messenger;

        /// <summary>
        /// Gets the message unique token.
        /// </summary>
        public SElementViewModelToken Token => _token;

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
                    // The request comes from the UI layer (propagate, no actual update here yet).
                    var oldValue = Convert.ChangeType(_value, DataType);  // copy
                    Messenger.Send(new PropertyChangedMessage<object>(this, Name, oldValue, value), Token);
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

        #endregion Properties

        #region Methods

        // Handles PropertyChangedMessage message.
        private void OnValueUpdated(PropertyChangedMessage<object> message)
        {
            _isUpdatingFromMessage = true;

            if (message.Sender is SElementViewModel && message.PropertyName == Name)
            {
                Value = message.NewValue;
            }

            _isUpdatingFromMessage = false;
        }

        #endregion Methods
    }
}
