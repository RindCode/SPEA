// ==================================================================================================
// <copyright file="SDocumentsManager.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Windows;
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using SPEA.App.Utils.Helpers;
    using SPEA.App.Utils.Services;
    using SPEA.App.ViewModels.Interfaces;

    /// <summary>
    /// A conroller that provides a centralized entry point for handling requests
    /// for a <see cref="ISDocumentViewModel"/> collection and its managing.
    /// </summary>
    public class SDocumentsManager : ObservableObject
    {
        #region Fields

        // Backing field for SelectedDocument.
        private ISDocumentViewModel _selectedDocument;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SDocumentsManager"/> class.
        /// </summary>
        public SDocumentsManager()
        {
            AddDocumentCommand = new RelayCommand<ISDocumentViewModel>(
                AddDocument);

            CloseDocumentCommand = new RelayCommand<ISDocumentViewModel>(
                ExecuteCloseDocument,
                (vm) => vm != null);

            CloseAllDocumentsCommand = new RelayCommand(
                ExecuteCloseAllDocuments,
                () => SDocumentsCollection.Count > 0);

            CloseAllButCommand = new RelayCommand<ISDocumentViewModel>(
                ExecuteCloseAllBut,
                (vm) => vm != null && SDocumentsCollection.Count > 1);

            // Event Handlers
            SDocumentsCollection.CollectionChanged += SDocumentViewModelCollection_CollectionChanged;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets a collection of created cross-sections.
        /// </summary>
        public ObservableCollection<ISDocumentViewModel> SDocumentsCollection { get; private set; } = new ObservableCollection<ISDocumentViewModel>();

        /// <summary>
        /// Gets or sets currently active document.
        /// </summary>
        public ISDocumentViewModel SelectedDocument
        {
            get => _selectedDocument;
            set
            {
                SetProperty(ref _selectedDocument, value);
            }
        }

        #endregion Properties

        #region Commands

        /// <summary>
        /// Gets a command which adds a document to the collection.
        /// </summary>
        public RelayCommand<ISDocumentViewModel> AddDocumentCommand { get; private set; }

        /// <summary>
        /// Gets a command which closes the selected document.
        /// </summary>
        public RelayCommand<ISDocumentViewModel> CloseDocumentCommand { get; private set; }

        /// <summary>
        /// Gets a command which closes all documents.
        /// </summary>
        public RelayCommand CloseAllDocumentsCommand { get; private set; }

        /// <summary>
        /// Gets a command which closes all documents, but the selected one.
        /// </summary>
        public RelayCommand<ISDocumentViewModel> CloseAllButCommand { get; private set; }

        #endregion Commands

        #region Commands Logic

        /// <summary>
        /// Provides derived classes an opportunity to handle changes in <see cref="SDocumentsCollection"/>.
        /// </summary>
        /// <param name="sender">A reference to the object that raised the event.</param>
        /// <param name="e">Event data.</param>
        protected virtual void SDocumentViewModelCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            CloseAllDocumentsCommand.NotifyCanExecuteChanged();
            CloseAllButCommand.NotifyCanExecuteChanged();
        }

        /// <summary>
        /// Implements <see cref="CloseDocumentCommand"/> command logic.
        /// </summary>
        private void ExecuteCloseDocument(ISDocumentViewModel doc)
        {
            RemoveDocumentWithConfirmation(doc);
        }

        /// <summary>
        /// Implements <see cref="CloseAllDocumentsCommand"/> command logic.
        /// </summary>
        private void ExecuteCloseAllDocuments()
        {
            RemoveAllDocumentsWithConfirmation();
        }

        /// <summary>
        /// Implements <see cref="CloseAllButCommand"/> command logic.
        /// </summary>
        private void ExecuteCloseAllBut(ISDocumentViewModel doc)
        {
            foreach (var item in SDocumentsCollection)
            {
                if (item != SelectedDocument)
                {
                    item.RequestCloseDocumentCommand.Execute(null);
                }
            }
        }

        #endregion Commands Logic

        #region Methods

        /// <summary>
        /// Adds a section document into the collection.
        /// </summary>
        /// <param name="doc">The document to be added.</param>
        internal void AddDocument(ISDocumentViewModel doc)
        {
            if (doc == null)
            {
                throw new ArgumentNullException(nameof(doc));
            }

            SDocumentsCollection.Add(doc);
            SelectedDocument = SDocumentsCollection[^1];
        }

        /// <summary>
        /// Removes the requested section document from the collection, disposes it
        /// and updates the current (active) document index.
        /// The method does not check the document for unsaved changes.
        /// </summary>
        /// <param name="doc">The document to be removed.</param>
        internal void RemoveDocument(ISDocumentViewModel doc)
        {
            int index;
            if (doc == SelectedDocument)
            {
                if (SDocumentsCollection.Count > 1)
                {
                    // New selected doc is the one located to the right => same index after removal.
                    index = SDocumentsCollection.IndexOf(doc);
                    if (index >= SDocumentsCollection.Count - 1)
                    {
                        // Handle the case if the selected doc == last one (no doc to the right).
                        index = SDocumentsCollection.Count - 2;
                    }
                }
                else
                {
                    index = -1;  // artificial
                }
            }
            else
            {
                if (SelectedDocument == null)
                {
                    if (SDocumentsCollection.Count > 1)
                    {
                        index = SDocumentsCollection.Count - 1;
                    }
                    else
                    {
                        index = -1;  // artificial
                    }
                }
                else
                {
                    var selectedDocIndex = SDocumentsCollection.IndexOf(SelectedDocument);
                    if (SDocumentsCollection.IndexOf(doc) > selectedDocIndex)
                    {
                        index = selectedDocIndex;
                    }
                    else
                    {
                        index = selectedDocIndex - 1;
                    }
                }
            }

            SDocumentsCollection?.Remove(doc);
            doc.Model.Dispose();

            SelectedDocument = index >= 0 ? SDocumentsCollection[index] : null;
        }

        /// <summary>
        /// Removes the requested section document from the collection, disposes it
        /// and updates the current (active) document index.
        /// The method will check the document for unsaved changes prior removing.
        /// </summary>
        /// <param name="doc">The document to be removed.</param>
        /// <returns>The confirmation result as the instance of <see cref="MessageBoxResult"/>.</returns>
        internal MessageBoxResult RemoveDocumentWithConfirmation(ISDocumentViewModel doc)
        {
            if (doc.IsSaveRequired)
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
        /// Iterates through the collection of documents and removes them one by one.
        /// </summary>
        /// <returns>Returns <see langword="true"/> if one of the documents was canceled for removal, otherwise <see langword="false"/>.</returns>
        internal bool RemoveAllDocumentsWithConfirmation()
        {
            // Use for-loop to avoid the exception thrown due to the collection being modified.
            var count = SDocumentsCollection.Count;
            for (int i = 0; i < count; i++)
            {
                SelectedDocument = SDocumentsCollection.First();
                var result = RemoveDocumentWithConfirmation(SelectedDocument);
                if (result == MessageBoxResult.Cancel)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Iterates through the collection of documents and removes them one by one. but the requested one.
        /// </summary>
        /// <returns>Returns <see langword="true"/> if one of the documents was canceled for removal, otherwise <see langword="false"/>.</returns>
        internal bool RemoveAllButWithConfirmation(ISDocumentViewModel sdoc)
        {
            // Use for-loop to avoid the exception thrown due to the collection being modified.
            var count = SDocumentsCollection.Count;
            while (count > 1)
            {
                var toRemove = SDocumentsCollection.First();
                if (toRemove == sdoc)
                {
                    continue;
                }

                var result = RemoveDocumentWithConfirmation(toRemove);
                if (result == MessageBoxResult.Cancel)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Saves the given document using the filepath parameter.
        /// </summary>
        /// <param name="doc">Document to be saved.</param>
        /// <param name="filepath">File path the document will be saved with.</param>
        /// <returns>Returns <see langword="true"/> if saved successfully, otherwise - <see langword="false"/>.</returns>
        internal bool SaveDocument(ISDocumentViewModel doc, string filepath)
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
        internal bool SaveDocumentWithDialog(ISDocumentViewModel doc, string filename)
        {
            // TODO: implementation
            return SaveDocument(doc, filename);
        }

        #endregion Methods
    }
}
