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
    using SPEA.App.Controllers;
    using SPEA.App.ViewModels;
    using SPEA.App.ViewModels.Factories;
    using SPEA.App.ViewModels.Interfaces;

    /// <summary>
    /// Application service collection for Dependency Injection purposes.
    /// </summary>
    public static class ServiceProvider
    {
        /// <summary>
        /// Returns the application service collection.
        /// </summary>
        /// <returns><see cref="IServiceCollection"/> instance.</returns>
        public static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            // Note to a nooby myself.
            // The DI service provider will automatically check whether all the necessary services are registered,
            // then it will retrieve them and invoke the constructor for the registered  concrete type, to get the instance to return.
            // So we just pass the required types inside the constructors and all dependencies will be resolved automatically.

            // Services.
            services.AddSingleton<IMessenger>(WeakReferenceMessenger.Default);

            // Controllers
            services.AddSingleton<SDocumentsManager>();

            // View Models
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<MainMenuViewModel>();
            services.AddSingleton<ProjectTreeViewModel>();
            services.AddTransient<NewSectionViewModel>();
            services.AddSingleton<SectionEditorSettingsViewModel>();

            // Factories | View Models
            services.AddTransient<ISDocumentViewModelFactory, SDocumentViewModelFactory>();

            return services.BuildServiceProvider();
        }
    }
}
