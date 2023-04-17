namespace Tech.Tevux.Dashboards.Controls;

public partial class TextualOutputControlBase {
    public static readonly DependencyProperty FormatProperty = DependencyProperty.Register(
        nameof(Format),
        typeof(string),
        typeof(TextualOutputControlBase),
        new PropertyMetadata("0.0", (d, e) => {
            (d as InputOutputControlBase)?.Reconfigure();
        }));

    [ExposedOption(OptionType.SingleLineText)]
    [Category(OptionCategory.Visuals)]
    public string Format {
        get { return (string)GetValue(FormatProperty); }
        set { SetValue(FormatProperty, value); }
    }
}
