// ==================================================================================================
// <copyright file="ServiceProvider.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.Utils.Services
{
    using System;
    using CommunityToolkit.Mvvm.Messaging;
    using Microsoft.Extensions.DependencyInjection;
    using SPEA.App.Commands;
    using SPEA.App.Controllers;
    using SPEA.App.ViewModels;
    using SPEA.App.ViewModels.Factories;
    using SPEA.App.ViewModels.Interfaces;

    /// <summary>
    /// Application service collection for Dependency Injection purposes.
    /// </summary>
    public static class ServiceProvider
    {
        #region Methods

        /// <summary>
        /// Returns the application service collection.
        /// </summary>
        /// <returns><see cref="IServiceCollection"/> instance.</returns>
        public static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            // Services.
            services.AddSingleton<IMessenger>(WeakReferenceMessenger.Default);

            // Controllers
            services.AddSingleton<SDocumentsManager>();
            services.AddSingleton<CommandsManager>();

            // View Models
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<MainMenuViewModel>();
            services.AddSingleton<ProjectTreeViewModel>();
            services.AddTransient<NewSectionViewModel>();

            // Factories | View Models
            services.AddTransient<ISDocumentViewModelFactory, SDocumentViewModelFactory>();

            return services.BuildServiceProvider();
        }

        #endregion Methods
    }
}
