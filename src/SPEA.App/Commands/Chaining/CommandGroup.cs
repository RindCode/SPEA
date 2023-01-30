// ==================================================================================================
// <copyright file="CommandGroup.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.Commands.Chaining
{
    using System;
    using System.Collections.Specialized;
    using System.Windows;
    using System.Windows.Input;

    /// <summary>
    /// Represents a group of commands to be invoked sequentially. Implements <see cref="ICommand"/> interface
    /// and can be used similar to any other command, but instead will invoke all commands stored within.
    /// </summary>
    /// <remarks>
    /// <para>
    /// For XAML markup and binding convenience all commands are wrapped by <see cref="CommandDescriptor"/> object,
    /// which represents a single lightweight command source derived from <see cref="Freezable"/> and implementing
    /// <see cref="ICommand"/> interface. Commands are stored inside of <see cref="Commands"/>
    /// dependency property which has a type of <see cref="CommandCollection"/>.
    /// </para>
    /// <example>
    /// The example below shows the idea of using a <see cref="CommandGroup"/> in XAML markup.
    /// In this example two commands will be invoked in order they're stored in the collection
    /// or defined in XAML code.
    /// <code>
    /// &lt;Button.Command&gt;
    ///     &lt;cmd:CommandGroup>
    ///         &lt;cmd:CommandGroup.Commands&gt;
    ///             &lt;cmd:CommandDescriptor
    ///                 Command="{Binding Path=DataContext.SomeCommand, RelativeSource={RelativeSource AncestorType=ctrl:Window}}"
    ///                 CommandParameter="{Binding ElementName=SomeTexBox, Path=Text}"/>
    ///             &lt;cmd:CommandDescriptor
    ///                 Command="{Binding Path=DataContext.AnotherCommand, RelativeSource={RelativeSource AncestorType=ctrl:Window}}"
    ///                 CommandParameter="{Binding RelativeSource={RelativeSource AncestorType=ctrl:Window}}"/>
    ///         &lt;/cmd:CommandGroup.Commands&gt;
    ///     &lt;/cmd:CommandGroup&gt;
    /// &lt;/Button.Command&gt;
    /// </code>
    /// </example>
    /// </remarks>
    public class CommandGroup : Freezable, ICommand
    {
        // Useful references:
        // (1) https://www.codeproject.com/Articles/25808/Aggregating-WPF-Commands-with-CommandGroup
        // (2a) http://drwpf.com/blog/2008/05/22/leveraging-freezables-to-provide-an-inheritance-context-for-bindings/
        // (2b) https://docs.microsoft.com/en-us/archive/blogs/mikehillberg/model-see-model-do-and-the-poo-is-optional
        // (3) https://docs.microsoft.com/en-us/archive/blogs/nickkramer/whats-an-inheritance-context

        // DP, Mid 2022 note:

        // Using a simple property with backing storage and INotifyPropertyChange didn't work
        // and led to binding errors when a Freezable object couldn't resolve DataContext,
        // which is usually done internally by it (check the references above).
        // I'm not sure how it works internally.

        // Initially this class was derived from FreezableCollection<T> and it worked fine,
        // but later it was decided to implement a separate class for the collection itself
        // to avoid the problems described above. Using ObservableCollection<T> didn't
        // work either due to same problems with DataContext inheritance.

        // This approach allows to avoid using a binding proxy object each time
        // we want to define a command sequence in XAML code.

        #region Dependency Properties

        /// <summary>
        /// <see cref="DependencyProperty"/> for <see cref="Commands"/> property.
        /// </summary>
        public static readonly DependencyProperty CommandsProperty =
            DependencyProperty.Register(
                "Commands",
                typeof(CommandCollection),
                typeof(CommandGroup),
                new PropertyMetadata(default(object)));

        #endregion Dependency Properties

        #region Fields

        // Backing field for the event delegate.
        ////private readonly EventHandler _canExecuteChanged;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandGroup"/> class.
        /// </summary>
        public CommandGroup()
            : base()
        {
            // Set empty collection in ctor, not in DP, otherwise PropertyMetadata will freeze it,
            // and runtime will throw InvalidOperationException on view's InitializeComponent().
            Commands = new CommandCollection();

            INotifyCollectionChanged ncc = Commands;
            if (ncc != null)
            {
                ncc.CollectionChanged += Commands_CollectionChanged;
            }
        }

        #endregion Constructors

        #region ICommand

        /// <inheritdoc/>
        public event EventHandler CanExecuteChanged;
        ////{
        ////    add => CommandsManager.RequerySuggested += value;
        ////    remove => CommandsManager.RequerySuggested -= value;
        ////}

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <remarks>
        /// The method will iterate through all commands in the collection and call their <see cref="ICommand.CanExecute(object)"/>.
        /// If any of them returns <see langword="false"/> or any of the commands is set to <see langword="null"/>,
        /// this method will return <see langword="false"/> as well.
        /// Parameter is always ignored since this class is just a wrapper for the actual commands,
        /// and their parameters are provided through <see cref="CommandDescriptor"/> object and data binding.
        /// </remarks>
        /// <param name="parameter">
        /// Data used by the command. If the command does not require data to be passed, this object can be set to <see langword="null"/>.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if this command can be executed; otherwise, <see langword="false"/>.
        /// </returns>
        public bool CanExecute(object parameter)
        {
            foreach (CommandDescriptor cd in Commands)
            {
                if (cd.Command == null || !cd.Command.CanExecute(cd.CommandParameter))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <remarks>
        /// The method will call commands in the order they are stored in the collection.
        /// Parameter is always ignored since this class is just a wrapper for the actual commands,
        /// and their parameters are provided through <see cref="CommandDescriptor"/> object.
        /// </remarks>
        /// <param name="parameter">
        /// Data used by the command. If the command does not require data to be passed, this object can be set to <see langword="null"/>.
        /// </param>
        public void Execute(object parameter)
        {
            foreach (CommandDescriptor cd in Commands)
            {
                if (cd.Command != null)
                {
                    cd.Command.Execute(cd.CommandParameter);
                }
            }
        }

        /// <summary>
        /// Raises <see cref="ICommand.CanExecuteChanged"/> event so every subscriber (<see cref="CommandGroup"/> invoker)
        /// can requery <see cref="ICommand.CanExecute(object)"/> to check if <see cref="CommandGroup"/> can be executed.
        /// </summary>
        protected virtual void OnCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            handler?.Invoke(this, EventArgs.Empty);
        }

        #endregion ICommand

        #region Properties

        /// <summary>
        /// Gets or sets a collection of commands to be invoked.
        /// The collection is derived from <see cref="FreezableCollection{T}"/> to enable ElementName and DataContext bindings.
        /// </summary>
        public CommandCollection Commands
        {
            get { return (CommandCollection)GetValue(CommandsProperty); }
            set { SetValue(CommandsProperty, value); }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Provides derived classes an opportunity to handle changes to the <see cref="Commands"/> collection.
        /// </summary>
        /// <param name="sender">A reference to the object that raised the event.</param>
        /// <param name="e">Event data.</param>
        protected virtual void Commands_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // Add handlers for all new items.
            if (e.NewItems != null && e.NewItems.Count > 0)
            {
                foreach (CommandDescriptor cd in e.NewItems)
                {
                    if (cd.Command != null)
                    {
                        cd.Command.CanExecuteChanged += OnChildCommandCanExecuteChanged;
                    }
                }
            }

            // Remove handlers for all replaced, removed or moved items.
            if (e.OldItems != null && e.OldItems.Count > 0)
            {
                foreach (CommandDescriptor cd in e.OldItems)
                {
                    if (cd.Command != null)
                    {
                        cd.Command.CanExecuteChanged -= OnChildCommandCanExecuteChanged;
                    }
                }
            }

            OnCanExecuteChanged();
        }

        /// <summary>
        /// Calls <see cref="OnCanExecuteChanged"/> each time a child command
        /// raises its <see cref="ICommand.CanExecuteChanged"/> event.
        /// </summary>
        /// <param name="sender">A reference to the object that raised the event.</param>
        /// <param name="e">Event data.</param>
        protected virtual void OnChildCommandCanExecuteChanged(object sender, EventArgs e)
        {
            OnCanExecuteChanged();
        }

        #endregion Methods

        #region Overridden Methods

        /// <inheritdoc/>
        protected override Freezable CreateInstanceCore()
        {
            return new CommandGroup();
        }

        #endregion Overridden Methods
    }
}
