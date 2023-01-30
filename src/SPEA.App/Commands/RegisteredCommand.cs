// ==================================================================================================
// <copyright file="RegisteredCommand.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.Commands
{
    using System.Windows.Input;

    /// <summary>
    /// Encapsulates a registered command with some additional data.
    /// </summary>
    public class RegisteredCommand
    {
        #region Fields

        private readonly ICommand _command;
        private readonly CommandMetadata _metadata;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisteredCommand"/> class.
        /// </summary>
        /// <param name="command">A command to be registered.</param>
        public RegisteredCommand(ICommand command)
            : this(command, new CommandMetadata())
        {
            // Blank.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisteredCommand"/> class.
        /// </summary>
        /// <param name="command">A command to be registered.</param>
        /// <param name="metadata">Command metadata.</param>
        public RegisteredCommand(ICommand command, CommandMetadata metadata)
        {
            _command = command;
            _metadata = metadata;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the registered command.
        /// </summary>
        public ICommand Command => _command;

        /// <summary>
        /// Gets the command metadata.
        /// </summary>
        public CommandMetadata Metadata => _metadata;

        #endregion Properties
    }
}
