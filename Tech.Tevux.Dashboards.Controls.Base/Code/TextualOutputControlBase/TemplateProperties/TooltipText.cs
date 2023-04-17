namespace Tech.Tevux.Dashboards.Controls;

public partial class TextualOutputControlBase {
    public static readonly DependencyProperty TooltipTextProperty = DependencyProperty.Register(
        nameof(TooltipText),
        typeof(string),
        typeof(TextualOutputControlBase),
        new PropertyMetadata(""));

    public string TooltipText {
        get { return (string)GetValue(TooltipTextProperty); }
        set { SetValue(TooltipTextProperty, value); }
    }
}
