namespace SPEA.App.Extensions.Markup
{
    using System;
    using System.Windows;
    using System.Windows.Markup;

    /// <summary>
    /// Provides a XAML markup extension that allows to collapse
    /// an element when the application was built as debug.
    /// </summary>
    public class CollapseWhenDebugExtension : MarkupExtension
    {
        static CollapseWhenDebugExtension()
        {
#if DEBUG
            Value = Visibility.Visible;
#else
            Value = Visibility.Collapsed;
#endif
        }

        public static Visibility Value { get; }

        /// <inheritdoc/>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Value;
        }
    }
}
