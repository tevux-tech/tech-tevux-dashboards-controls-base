namespace Tech.Tevux.Dashboards.Controls;

public partial class NumericOutputControlBase {
    public static readonly DependencyProperty SuffixProperty = DependencyProperty.Register(
        nameof(Suffix),
        typeof(string),
        typeof(NumericOutputControlBase),
        new PropertyMetadata("", (d, e) => {
            (d as NumericOutputControlBase)?.Reconfigure();
        }, (d, incomingValue) => {
            if (incomingValue is null) {
                return "";
            } else {
                return incomingValue;
            }
        }));

    [ExposedSingleLineText]
    [Category(OptionCategory.Visuals)]
    public string Suffix {
        get { return (string)GetValue(SuffixProperty); }
        set { SetValue(SuffixProperty, value); }
    }
}
