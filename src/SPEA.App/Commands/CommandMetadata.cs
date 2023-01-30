// ==================================================================================================
// <copyright file="CommandMetadata.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.Commands
{
    using System;

    /// <summary>
    /// Represens flags which can be used inside <see cref="CommandMetadata"/>
    /// constructor to set up its options.
    /// </summary>
    [Flags]
    public enum CommandsMetadataOptions : int
    {
        /// <summary>
        /// No flags.
        /// </summary>
        None = 0,

        /// <summary>
        /// Indicates that the command may be exposed to a public API.
        /// </summary>
        ExposedToPublicApi = 1,
    }

    /// <summary>
    /// Represents metadata container for commands registered with <see cref="CommandsManager"/>.
    /// </summary>
    public class CommandMetadata
    {
        #region Fields

        private CommandsMetadataOptions _flags;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandMetadata"/> class.
        /// </summary>
        public CommandMetadata()
            : this(CommandsMetadataOptions.None)
        {
            // Blank.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandMetadata"/> class.
        /// </summary>
        /// <param name="options">Metadata options.</param>
        public CommandMetadata(CommandsMetadataOptions options)
        {
            _flags = options;
            TranslateFlags(options);
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the current instance options.
        /// </summary>
        public CommandsMetadataOptions Options => _flags;

        /// <summary>
        /// Gets or sets a value indicating whether the command is exposed
        /// to public API and thus may be called from outside.
        /// </summary>
        public bool ExposedToPublicApi
        {
            get => _flags.HasFlag(CommandsMetadataOptions.ExposedToPublicApi);
            set => WriteFlag(CommandsMetadataOptions.ExposedToPublicApi, value);
        }

        #endregion Properties

        #region Methods

        // Sets a flag.
        private void WriteFlag(CommandsMetadataOptions flag, bool value)
        {
            if (value)
            {
                _flags |= flag;
            }
            else
            {
                _flags &= ~flag;
            }
        }

        // Translates flags to state sets.
        private void TranslateFlags(CommandsMetadataOptions flags)
        {
            if (flags.HasFlag(CommandsMetadataOptions.ExposedToPublicApi))
            {
                ExposedToPublicApi = true;
            }
        }

        #endregion Methods
    }
}
