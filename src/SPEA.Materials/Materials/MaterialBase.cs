// ==================================================================================================
// <copyright file="MaterialBase.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Core.Materials
{
    /// <summary>
    /// A base class for any generic material.
    /// Any material must derive from this class.
    /// </summary>
    public abstract class MaterialBase
    {
        #region Fields

        private string _name;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialBase"/> class.
        /// </summary>
        /// <param name="name">Material name.</param>
        public MaterialBase(string name)
        {
            _name = name;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets material name.
        /// </summary>
        public string Name
        {
            get => _name;
            set => _name = value;
        }

        #endregion Properties
    }
}
