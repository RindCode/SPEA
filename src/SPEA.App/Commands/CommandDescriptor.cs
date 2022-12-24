// ==================================================================================================
// <copyright file="CommandDescriptor.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.Commands
{
    using System;
    using System.Windows;
    using System.Windows.Input;

    /// <summary>
    /// Represents a bindable command and exposes <see cref="ICommandSource"/> interface for
    /// XAML markup binding convenience. This class should not be used outside of <see cref="CommandGroup"/>.
    /// </summary>
    public class CommandDescriptor : Freezable, ICommandSource
    {
        #region Dependency Properties

        /// <summary>
        /// <see cref="DependencyProperty"/> for <see cref="Command"/> property.
        /// </summary>
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(
                "Command",
                typeof(ICommand),
                typeof(CommandDescriptor),
                new PropertyMetadata(default(ICommand)));

        /// <summary>
        /// <see cref="DependencyProperty"/> for <see cref="CommandParameter"/> property.
        /// </summary>
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register(
                "CommandParameter",
                typeof(object),
                typeof(CommandDescriptor),
                new PropertyMetadata(default(object)));

        /// <summary>
        /// <see cref="DependencyProperty"/> for <see cref="CommandTarget"/> property.
        /// </summary>
        public static readonly DependencyProperty CommandTargetProperty =
            DependencyProperty.Register(
                "CommandTarget",
                typeof(IInputElement),
                typeof(CommandDescriptor),
                new PropertyMetadata(default(IInputElement)));

        #endregion Dependency Properties

        #region Fields

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandDescriptor"/> class.
        /// </summary>
        public CommandDescriptor()
            : base()
        {
            // blank
        }

        #endregion Constructors

        #region Events

        /////// <summary>
        /////// Occurs when changes occur that affect whether or not the command should execute.
        /////// </summary>
        ////public event EventHandler CanExecuteChanged;

        #endregion Events

        #region ICommandSource

        /// <summary>
        /// Gets or sets the command.
        /// </summary>
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        /// <summary>
        /// Gets or sets the command parameter.
        /// </summary>
        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        /// <summary>
        /// Gets or sets the command target.
        /// </summary>
        public IInputElement CommandTarget
        {
            get { return (IInputElement)GetValue(CommandTargetProperty); }
            set { SetValue(CommandTargetProperty, value); }
        }

        #endregion ICommandSource

        #region Properties

        #endregion Properties

        #region Methods

        ////// Command dependency property callback.
        ////private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        ////{
        ////    CommandDescriptor cd = (CommandDescriptor)d;
        ////    cd.OnCommandChanged((ICommand)e.OldValue, (ICommand)e.NewValue);
        ////}

        ////// Calls appropriate methods to remove an old command or add a new one.
        ////// The Command property cannot simply be overwritten when a new command is added,
        ////// because the event handling methods associated with the previous command,
        ////// if there was one, must be removed first.
        ////private void OnCommandChanged(ICommand oldCommand, ICommand newCommand)
        ////{
        ////    if (oldCommand != null)
        ////    {
        ////        UnhookCommand(oldCommand);
        ////    }

        ////    if (newCommand != null)
        ////    {
        ////        HookCommand(newCommand);
        ////    }
        ////}

        ////// Removes the event handling method associated with an old command.
        ////private void UnhookCommand(ICommand command)
        ////{
        ////    EventHandler handler = Command_CanExecuteChanged;
        ////    command.CanExecuteChanged -= handler;
        ////}

        ////// Adds a new event handling method associated with a new command.
        ////private void HookCommand(ICommand command)
        ////{
        ////    EventHandler handler = new (Command_CanExecuteChanged);
        ////    command.CanExecuteChanged += handler;
        ////}

        ////// Does nothings...
        ////private void Command_CanExecuteChanged(object sender, EventArgs e)
        ////{
        ////    // TODO: remove? It's just a command wrapper for XAML binding.
        ////}

        #endregion Methods

        #region Overridden Methods

        /// <inheritdoc/>
        protected override Freezable CreateInstanceCore()
        {
            return new CommandDescriptor();
        }

        #endregion Overridden Methods
    }
}
