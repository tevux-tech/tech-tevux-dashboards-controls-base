namespace Tech.Tevux.Dashboards.Controls;
public partial class TextualOutputControlBase {
    public static readonly DependencyProperty PrefixProperty = DependencyProperty.Register(
        nameof(Prefix),
        typeof(string),
        typeof(TextualOutputControlBase),
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
