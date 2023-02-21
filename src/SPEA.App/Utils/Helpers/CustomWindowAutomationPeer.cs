// ==================================================================================================
// <copyright file="CustomWindowAutomationPeer.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.Utils.Helpers
{
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Automation.Peers;

    /// <summary>
    /// Represents a custom fake AutomationPeer.
    /// See <see cref="Views.MainWindow"/> for the details.
    /// </summary>
    public class CustomWindowAutomationPeer : FrameworkElementAutomationPeer
    {
        // Implementation:
        // https://stackoverflow.com/questions/17297539/can-ui-automation-be-disabled-for-an-entire-wpf-4-0-app

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomWindowAutomationPeer"/> class.
        /// </summary>
        /// <param name="owner">Owner reference.</param>
        public CustomWindowAutomationPeer(FrameworkElement owner)
            : base(owner)
        {
            // Blank.
        }

        /// <inheritdoc/>
        protected override string GetNameCore()
        {
            return "CustomWindowAutomationPeer";
        }

        /// <inheritdoc/>
        protected override AutomationControlType GetAutomationControlTypeCore()
        {
            return AutomationControlType.Window;
        }

        /// <inheritdoc/>
        protected override List<AutomationPeer> GetChildrenCore()
        {
            return new List<AutomationPeer>();
        }
    }
}
