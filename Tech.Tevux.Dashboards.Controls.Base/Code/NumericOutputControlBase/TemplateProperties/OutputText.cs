namespace Tech.Tevux.Dashboards.Controls;

public partial class NumericOutputControlBase {
    public static readonly DependencyProperty OutputTextProperty = DependencyProperty.Register(
        nameof(OutputText),
        typeof(string),
        typeof(NumericOutputControlBase),
        new PropertyMetadata("..."));

    public string OutputText {
        get { return (string)GetValue(OutputTextProperty); }
        set { SetValue(OutputTextProperty, value); }
    }
}
