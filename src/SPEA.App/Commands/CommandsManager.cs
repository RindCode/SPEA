// ==================================================================================================
// <copyright file="CommandsManager.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Input;
    using CommunityToolkit.Mvvm.ComponentModel;

    /// <summary>
    /// Represents a centralized entry point to operate application commands.
    /// </summary>
    /// <remarks>
    /// <example>
    /// If XAML binding is required, a command can be accessed by its registered name
    /// using the indexer as shown below.
    /// <code>
    ///     Command="{Binding CommandsManager[COMMAND_NAME].Command}"
    /// </code>
    /// Note, that the indexer returns <see cref="RegisteredCommand"/>,
    /// but not an <see cref="ICommand"/> object, thus <see cref="RegisteredCommand.Command"/> property
    /// must be used to access the actual command reference.
    /// </example>
    /// </remarks>
    public class CommandsManager : ObservableObject
    {
        // Based on the the idea (MIT license at the time of writing this):
        // https://github.com/MarcArmbruster/WpfCommandAggregator

        #region Fields

        private readonly Dictionary<string, RegisteredCommand> _registeredCommands;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandsManager"/> class.
        /// </summary>
        public CommandsManager()
        {
            _registeredCommands = new Dictionary<string, RegisteredCommand>();
        }

        #endregion Constructors

        #region Destructor

        /// <summary>
        /// Finalizes an instance of the <see cref="CommandsManager"/> class.
        /// </summary>
        ~CommandsManager()
        {
            RemoveAllCommands();
        }

        #endregion Destructor

        #region Methods

        /// <summary>
        /// Gets the registered command by using the specified name (key).
        /// </summary>
        /// <remarks>
        /// <para>
        /// The indexer implementation is required to simplify markup data binding.
        /// It can be used only for OneWay mode (read-only).
        /// </para>
        /// <para>
        /// Every command must be registered first prior using this method.
        /// In an unregistered command was requested, then <see cref="InvalidOperationException"/>
        /// will be thrown.
        /// </para>
        /// </remarks>
        /// <param name="name">A unique command name (key).</param>
        /// <returns><see cref="RegisteredCommand"/> instance.</returns>
        public RegisteredCommand? this[string name]
        {
            get => GetRegisteredCommand(name);
        }

        /// <summary>
        /// Registers a new command.
        /// </summary>
        /// <param name="name">A unique command name (key).</param>
        /// <param name="command"><see cref="ICommand"/> instance to be registered.</param>
        public void RegisterCommand(string name, ICommand command)
        {
            RegisterCommand(name, command, new CommandMetadata());
        }

        /// <summary>
        /// Registers a new command.
        /// </summary>
        /// <param name="name">A unique command name (key).</param>
        /// <param name="command"><see cref="ICommand"/> instance to be registered.</param>
        /// <param name="metadata">Command metadata.</param>
        public void RegisterCommand(string name, ICommand command, CommandMetadata metadata)
        {
            AddCommand(name, command, metadata);
        }

        /// <summary>
        /// Unregisters an existing command.
        /// </summary>
        /// <param name="name">A unique command name (key).</param>
        public void UnregisterCommand(string name)
        {
            RemoveCommand(name);
        }

        /// <summary>
        /// Gets the requested command by using the specified name (key).
        /// </summary>
        /// <remarks>
        /// The method will return <see cref="RegisteredCommand"/> instance, rather than <see cref="ICommand"/>.
        /// The command itself is accessible through the <see cref="RegisteredCommand"/> properties.
        /// </remarks>
        /// <param name="name">A unique command name (key).</param>
        /// <returns><see cref="RegisteredCommand"/> instance.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="name"/> is <see langword="null"/>.</exception>
        /// <exception cref="InvalidOperationException">If a given name (key) is not registered.</exception>
        public RegisteredCommand? GetRegisteredCommand(string name)
        {
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));

            var isRegistered = _registeredCommands.TryGetValue(name, out RegisteredCommand? value);
            if (!isRegistered)
            {
                throw new InvalidOperationException($"The requested command \"{name}\" is not registered.");
            }

            return value;
        }

        /// <summary>
        /// Gets the command metadata using the specified name (key).
        /// </summary>
        /// <param name="name">A unique command name (key).</param>
        /// <returns><see cref="CommandMetadata"/> instance.</returns>
        public CommandMetadata? GetRegisteredCommandOptions(string name)
        {
            var rc = GetRegisteredCommand(name);
            return rc?.Metadata;
        }

        /// <summary>
        /// Gets the <see cref="ICommand"/> instance using the specified name (key).
        /// </summary>
        /// <param name="name">A unique command name (key).</param>
        /// <returns><see cref="ICommand"/> instance.</returns>
        public ICommand? GetCommand(string name)
        {
            var rc = GetRegisteredCommand(name);
            return rc?.Command;
        }

        // Adds a new command into a dictionary of registered commands.
        private void AddCommand(string name, ICommand command, CommandMetadata metadata)
        {
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));
            ArgumentNullException.ThrowIfNull(command, nameof(command));
            ArgumentNullException.ThrowIfNull(metadata, nameof(metadata));

            var rc = new RegisteredCommand(command, metadata);
            if (_registeredCommands.ContainsKey(name))
            {
                return; // do nothing if a command was added before
            }

            var wasAdded = _registeredCommands.TryAdd(name, rc);
            if (!wasAdded)
            {
                throw new InvalidOperationException($"The requested command \"{name}\" cannot be added.");
            }
        }

        // Removes a command from a dictionary of registered commands.
        private void RemoveCommand(string name)
        {
            var removed = _registeredCommands.Remove(name);
            if (removed == false)
            {
                throw new InvalidOperationException($"Unable to remove the command: {name}");
            }
        }

        // Removes all registered commands from the internal dictionary.
        private void RemoveAllCommands()
        {
            _registeredCommands?.Clear();
        }

        #endregion Methods
    }
}
