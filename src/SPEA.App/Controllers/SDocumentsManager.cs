// ==================================================================================================
// <copyright file="SDocumentsManager.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.Controllers
{
    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Windows;
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using SPEA.App.Commands;
    using SPEA.App.Utils.Helpers;
    using SPEA.App.Utils.Services;
    using SPEA.App.ViewModels;

    /// <summary>
    /// A conroller that provides a centralized entry point for handling requests
    /// for a <see cref="SDocumentViewModel"/> collection and its managing.
    /// </summary>
    public class SDocumentsManager : ObservableObject
    {
        #region Fields

        private readonly CommandsManager _commandsManager;
        private readonly string _closeDocumentCmd = "CloseDocument";
        private readonly string _closeAllDocumentsCmd = "CloseAllDocuments";
        private readonly string _closeOthersCmd = "CloseOthers";
        private SDocumentViewModel _selectedDocument;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SDocumentsManager"/> class.
        /// </summary>
        /// <param name="commandsManager">A reference to <see cref="CommandsManager"/> instance.</param>
        public SDocumentsManager(CommandsManager commandsManager)
        {
            _commandsManager = commandsManager ?? throw new ArgumentNullException(nameof(commandsManager));

            CommandsManager.RegisterCommand(_closeDocumentCmd, new RelayCommand(ExecuteCloseDocument, () => SelectedDocument != null));
            CommandsManager.RegisterCommand(_closeAllDocumentsCmd, new RelayCommand(ExecuteCloseAllDocuments, () => SDocumentsCollection?.Count > 0));
            CommandsManager.RegisterCommand(_closeOthersCmd, new RelayCommand(ExecuteCloseOthers, () => SelectedDocument != null && SDocumentsCollection?.Count > 1));
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets a commands manager reference.
        /// </summary>
        public CommandsManager CommandsManager => _commandsManager;

        /// <summary>
        /// Gets a collection of created cross-sections.
        /// </summary>
        public ObservableCollection<SDocumentViewModel> SDocumentsCollection { get; private set; } = new ObservableCollection<SDocumentViewModel>();

        /// <summary>
        /// Gets or sets currently active document.
        /// </summary>
        public SDocumentViewModel SelectedDocument
        {
            get => _selectedDocument;
            set
            {
                SetProperty(ref _selectedDocument, value);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the internal collection contains at least one element.
        /// </summary>
        public bool HasItems => SDocumentsCollection?.Count > 0;

        /// <summary>
        /// Gets a value indicating whether the internal collection is empty.
        /// </summary>
        public bool IsEmpty => SDocumentsCollection?.Count == 0;

        #endregion Properties

        #region Methods

        /// <summary>
        /// Adds a section document into the collection.
        /// </summary>
        /// <param name="doc">A document to be added.</param>
        public void AddDocument(SDocumentViewModel doc)
        {
            if (doc == null)
            {
                throw new ArgumentNullException(nameof(doc));
            }

            SDocumentsCollection.Add(doc);
            SelectedDocument = SDocumentsCollection[^1];
            InvalidateCommandsCanExecute();
        }

        /// <summary>
        /// Removes the requested section document from the collection, disposes it
        /// and updates the current (active) document index.
        /// The method does not check the document for unsaved changes.
        /// </summary>
        /// <param name="doc">A document to be removed.</param>
        public void RemoveDocument(SDocumentViewModel doc)
        {
            if (doc == null)
            {
                throw new ArgumentNullException(nameof(doc));
            }

            var removed = SDocumentsCollection?.Remove(doc);
            if (removed.HasValue && removed.Value == false)
            {
                throw new InvalidOperationException($"Unable to remove the document. Name={doc.Name}");
            }

            doc.Dispose();

            SelectedDocument = SDocumentsCollection.Count == 0 ? null : SDocumentsCollection[^1];
            InvalidateCommandsCanExecute();
        }

        /// <summary>
        /// Removes the requested section document from the collection, disposes it
        /// and updates the current (active) document index.
        /// </summary>
        /// <remarks>
        /// The method will check the document for unsaved changes prior removing.
        /// If there are unsaved changes, a confirmation dialog will appear.
        /// </remarks>
        /// <param name="doc">A document to be removed.</param>
        /// <returns>The confirmation result as the instance of <see cref="MessageBoxResult"/>.</returns>
        public MessageBoxResult RemoveDocumentWithConfirmation(SDocumentViewModel doc)
        {
            if (doc == null)
            {
                throw new ArgumentNullException(nameof(doc));
            }

            if (doc.IsDirty)
            {
                var message = $"{ResourcesHelper.GetApplicationResource<string>("S.MessageBox.SDocument.UnsavedChanges_1")} \"{doc.Name}\"\n\n" +
                              $"{ResourcesHelper.GetApplicationResource<string>("S.MessageBox.SDocument.UnsavedChanges_2")}";
                var title = ResourcesHelper.GetApplicationResource<string>("S.App.Title");
                var result = MessageBoxService.Show(message, title, MessageBoxButton.YesNoCancel, MessageBoxImage.None, MessageBoxResult.Cancel);

                switch (result)
                {
                    case MessageBoxResult.Yes:
                        SaveDocumentWithDialog(doc, doc.Name);
                        RemoveDocument(doc);
                        break;
                    case MessageBoxResult.No:
                        RemoveDocument(doc);
                        break;
                    case MessageBoxResult.Cancel:
                    default:
                        break;
                }

                return result;
            }

            RemoveDocument(doc);
            return MessageBoxResult.None;
        }

        /// <summary>
        /// Saves the given document using the filepath parameter.
        /// </summary>
        /// <param name="doc">Document to be saved.</param>
        /// <param name="filepath">File path the document will be saved with.</param>
        /// <returns>Returns <see langword="true"/> if saved successfully, otherwise - <see langword="false"/>.</returns>
        public bool SaveDocument(SDocumentViewModel doc, string filepath)
        {
            // TODO: implementation
            return true;
        }

        /// <summary>
        /// Saves the given document using file dialog window.
        /// </summary>
        /// <param name="doc">Document to be saved.</param>
        /// <param name="filename">The document name.</param>
        /// <returns>Returns <see langword="true"/> if saved successfully, otherwise - <see langword="false"/>.</returns>
        public bool SaveDocumentWithDialog(SDocumentViewModel doc, string filename)
        {
            // TODO: implementation
            return SaveDocument(doc, filename);
        }

        #endregion Methods

        #region Commands Logic

        // Must be called whenever a command's CanExecute state is changed.
        private void InvalidateCommandsCanExecute()
        {
            var cmd = CommandsManager[_closeDocumentCmd].Command as RelayCommand;
            cmd?.NotifyCanExecuteChanged();

            cmd = CommandsManager[_closeAllDocumentsCmd].Command as RelayCommand;
            cmd?.NotifyCanExecuteChanged();

            cmd = CommandsManager[_closeOthersCmd].Command as RelayCommand;
            cmd?.NotifyCanExecuteChanged();
        }

        // "Close" command.
        private void ExecuteCloseDocument()
        {
            RemoveDocumentWithConfirmation(SelectedDocument);
        }

        // "Close All" command.
        private void ExecuteCloseAllDocuments()
        {
            var count = SDocumentsCollection.Count;
            for (int i = 0; i < count; i++)
            {
                SelectedDocument = SDocumentsCollection.First();
                var result = RemoveDocumentWithConfirmation(SelectedDocument);
                if (result == MessageBoxResult.Cancel)
                {
                    return;  // break sequence if Cancel was requested
                }
            }
        }

        // "Closee All" (or "Close All But This") command.
        private void ExecuteCloseOthers()
        {
            var count = SDocumentsCollection.Count;
            for (int i = 0; i < count; i++)
            {
                var current = SDocumentsCollection[i];
                if (current != SelectedDocument)
                {
                    var result = RemoveDocumentWithConfirmation(current);
                    if (result == MessageBoxResult.Cancel)
                    {
                        return;  // break sequence if Cancel was requested
                    }

                    count = SDocumentsCollection.Count;
                    i -= 1;
                }
            }
        }

        #endregion Commands Logic
    }
}
