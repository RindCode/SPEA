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
    using SPEA.App.ViewModels;
    using SPEA.App.ViewModels.SDocument;
    using SPEA.App.ViewModels.Windows;

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
            services.AddSingleton<SDocumentsManagerViewModel>();
            services.AddSingleton<CommandsManager>();

            // View Models
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<MainMenuViewModel>();
            services.AddSingleton<ProjectTreeViewModel>();
            services.AddSingleton<AddGeometryViewModel>();
            services.AddTransient<NewSectionViewModel>();
            services.AddTransient<AddPrimitiveRectViewModel>();

            return services.BuildServiceProvider();
        }

        #endregion Methods
    }
}
