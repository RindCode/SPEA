// ==================================================================================================
// <copyright file="CommandCollection.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.Commands
{
    using System.Windows;

    /// <summary>
    /// Represents a collection of <see cref="CommandDescriptor"/> objects.
    /// </summary>
    public sealed class CommandCollection : FreezableCollection<CommandDescriptor>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandCollection"/> class.
        /// </summary>
        public CommandCollection()
            : base()
        {
            // Blank.
        }
    }
}
