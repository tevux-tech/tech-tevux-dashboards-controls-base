namespace Tech.Tevux.Dashboards.Controls;

public partial class InputOutputControlBase {
    public static readonly DependencyProperty RulesProperty = DependencyProperty.Register(
        nameof(Rules),
        typeof(string),
        typeof(InputOutputControlBase),
        new PropertyMetadata("", (d, e) => {
            (d as InputOutputControlBase)?.Reconfigure();
        }));

    [ExposedOption(OptionType.MultiLineText)]
    [DisplayName("Rules")]
    [Category(OptionCategory.Visuals)]
    public string Rules {
        get { return (string)GetValue(RulesProperty); }
        set { SetValue(RulesProperty, value); }
    }
}
