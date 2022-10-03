namespace Tech.Tevux.Dashboards.Controls;

public partial class InputOutputControlBase {
    public static readonly DependencyProperty FormatProperty = DependencyProperty.Register(
        nameof(Format),
        typeof(string),
        typeof(InputOutputControlBase),
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
