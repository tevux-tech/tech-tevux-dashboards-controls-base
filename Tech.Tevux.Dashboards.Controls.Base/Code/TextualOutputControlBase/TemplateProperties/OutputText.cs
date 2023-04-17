namespace Tech.Tevux.Dashboards.Controls;

public partial class TextualOutputControlBase {
    public static readonly DependencyProperty OutputTextProperty = DependencyProperty.Register(
        nameof(OutputText),
        typeof(string),
        typeof(TextualOutputControlBase),
        new PropertyMetadata(""));

    public string OutputText {
        get { return (string)GetValue(OutputTextProperty); }
        set { SetValue(OutputTextProperty, value); }
    }
}
