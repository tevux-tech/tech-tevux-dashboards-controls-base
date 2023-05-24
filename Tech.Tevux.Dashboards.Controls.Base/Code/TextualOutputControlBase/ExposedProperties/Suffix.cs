namespace Tech.Tevux.Dashboards.Controls;

public partial class TextualOutputControlBase {
    public static readonly DependencyProperty SuffixProperty = DependencyProperty.Register(
        nameof(Suffix),
        typeof(string),
        typeof(TextualOutputControlBase),
        new PropertyMetadata("", (d, e) => {
            (d as TextualOutputControlBase)?.Reconfigure();
        }, (d, incomingValue) => {
            if (incomingValue is null) {
                return "";
            } else {
                return incomingValue;
            }
        }));

    [ExposedOption(OptionType.SingleLineText)]
    [Category(OptionCategory.Visuals)]
    public string Suffix {
        get { return (string)GetValue(SuffixProperty); }
        set { SetValue(SuffixProperty, value); }
    }
}
