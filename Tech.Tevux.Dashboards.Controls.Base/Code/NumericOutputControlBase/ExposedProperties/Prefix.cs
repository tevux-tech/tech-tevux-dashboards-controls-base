namespace Tech.Tevux.Dashboards.Controls;
public partial class NumericOutputControlBase {
    public static readonly DependencyProperty PrefixProperty = DependencyProperty.Register(
        nameof(Prefix),
        typeof(string),
        typeof(NumericOutputControlBase),
        new PropertyMetadata("", (d, e) => {
            (d as NumericOutputControlBase)?.Reconfigure();
        }));

    [ExposedOption(OptionType.SingleLineText)]
    [Category(OptionCategory.Visuals)]
    public string Prefix {
        get { return (string)GetValue(PrefixProperty); }
        set { SetValue(PrefixProperty, value); }
    }
}
