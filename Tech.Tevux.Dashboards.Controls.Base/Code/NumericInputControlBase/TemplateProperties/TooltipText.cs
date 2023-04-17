namespace Tech.Tevux.Dashboards.Controls;

public partial class NumericInputControlBase {
    public static readonly DependencyProperty TooltipTextProperty = DependencyProperty.Register(
        nameof(TooltipText),
        typeof(string),
        typeof(NumericInputControlBase),
        new PropertyMetadata(""));

    public string TooltipText {
        get { return (string)GetValue(TooltipTextProperty); }
        set { SetValue(TooltipTextProperty, value); }
    }
}
