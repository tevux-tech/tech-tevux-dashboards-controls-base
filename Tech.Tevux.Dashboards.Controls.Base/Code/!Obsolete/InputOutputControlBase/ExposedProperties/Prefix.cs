namespace Tech.Tevux.Dashboards.Controls;
public partial class InputOutputControlBase {
    public static readonly DependencyProperty PrefixProperty = DependencyProperty.Register(
        nameof(Prefix),
        typeof(string),
        typeof(InputOutputControlBase),
        new PropertyMetadata("", (d, e) => {
            (d as InputOutputControlBase)?.Reconfigure();
        }));

    [ExposedOption(OptionType.SingleLineText)]
    [Category(OptionCategory.Visuals)]
    public string Prefix {
        get { return (string)GetValue(PrefixProperty); }
        set { SetValue(PrefixProperty, value); }
    }
}
