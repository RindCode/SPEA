// ==================================================================================================
// <copyright file="AssemblyInfo.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

using System.Windows;
using System.Windows.Markup;

[assembly: ThemeInfo(
    ResourceDictionaryLocation.None, // where theme specific resource dictionaries are located
                                     // (used if a resource is not found in the page,
                                     // or application resource dictionaries)
    ResourceDictionaryLocation.SourceAssembly) // where the generic resource dictionary is located
                                               // (used if a resource is not found in the page,
                                               // app, or any theme specific resource dictionaries)
]

#if DEBUG
[assembly: XmlnsDefinition("debug-mode", "Namespace")]
#endif

