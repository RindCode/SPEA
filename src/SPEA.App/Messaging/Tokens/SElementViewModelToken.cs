// ==================================================================================================
// <copyright file="SElementViewModelToken.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.Messaging.Tokens
{
    using System;

    /// <summary>
    /// Represents a message token used between <see cref="ViewModels.SElements.SElementViewModel"/>
    /// and <see cref="ViewModels.SElements.SElementInfoViewModel"/> view models.
    /// </summary>
    public class SElementViewModelToken : IEquatable<SElementViewModelToken>
    {
        #region Fields

        private readonly Guid _guid;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SElementViewModelToken"/> class.
        /// </summary>
        /// <param name="guid">A <see cref="Guid"/> of the current token.</param>
        public SElementViewModelToken(Guid guid)
        {
            _guid = guid;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets a unique GUID of this message.
        /// </summary>
        public Guid Guid => _guid;

        #endregion Properties

        #region Operators

        /// <summary>
        /// Returns <see langword="true"/> if the left <see cref="SElementViewModelToken"/> is equal to the right one,
        /// otherwise returns <see langword="false"/>.
        /// </summary>
        /// <param name="left">The left <see cref="SElementViewModelToken"/> to compare.</param>
        /// <param name="right">The right <see cref="SElementViewModelToken"/> to compare.</param>
        /// <returns><see langword="true"/> if both <see cref="SElementViewModelToken"/> are equal, otherwise returns <see langword="false"/>.</returns>
        public static bool operator ==(SElementViewModelToken left, SElementViewModelToken right)
        {
            if (left is null)
            {
                if (right is null)
                {
                    return true;
                }

                return false;
            }

            return left.Equals(right);
        }

        /// <summary>
        /// Returns <see langword="true"/> if the left <see cref="SElementViewModelToken"/> is NOT equal to the right one,
        /// otherwise returns <see langword="false"/>.
        /// </summary>
        /// <param name="left">The left <see cref="SElementViewModelToken"/> to compare.</param>
        /// <param name="right">The right <see cref="SElementViewModelToken"/> to compare.</param>
        /// <returns><see langword="true"/> if both <see cref="SElementViewModelToken"/> are NOT equal, otherwise returns <see langword="false"/>.</returns>
        public static bool operator !=(SElementViewModelToken left, SElementViewModelToken right) => !(left == right);

        #endregion Operators

        #region Methods

        /// <inheritdoc/>
        public bool Equals(SElementViewModelToken? other)
        {
            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (_guid == other.Guid)
            {
                return true;
            }

            return false;
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            return Equals(obj as SElementViewModelToken);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion Methods
    }
}
