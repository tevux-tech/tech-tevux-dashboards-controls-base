namespace Tech.Tevux.Dashboards.Controls;

public partial class TextualOutputControlBase {
    public static readonly DependencyProperty CombinedFormatProperty = DependencyProperty.Register(
        nameof(CombinedFormat),
        typeof(string),
        typeof(TextualOutputControlBase),
        new PropertyMetadata("{0}"));

    public string CombinedFormat {
        get { return (string)GetValue(CombinedFormatProperty); }
        set { SetValue(CombinedFormatProperty, value); }
    }
}
