namespace Tech.Tevux.Dashboards.Controls;

public partial class TextualOutputControlBase {
    public static readonly DependencyProperty RulesProperty = DependencyProperty.Register(
        nameof(Rules),
        typeof(string),
        typeof(TextualOutputControlBase),
        new PropertyMetadata("", (d, e) => {
            (d as TextualOutputControlBase)?.Reconfigure();
        }));

    [ExposedOption(OptionType.MultiLineText)]
    [DisplayName("Rules")]
    [Category(OptionCategory.Visuals)]
    public string Rules {
        get { return (string)GetValue(RulesProperty); }
        set { SetValue(RulesProperty, value); }
    }
}
