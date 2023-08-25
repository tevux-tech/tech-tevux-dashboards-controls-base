namespace Tech.Tevux.Dashboards.Controls;

public partial class TextualOutputControlBase {
    public static readonly DependencyProperty TextualValueProperty = DependencyProperty.Register(
        nameof(TextualValue),
        typeof(string),
        typeof(TextualOutputControlBase),
        new PropertyMetadata("", (d, e) => {
            (d as TextualOutputControlBase)?.ApplyAppearanceRules();
        }));

    public string TextualValue {
        get { return (string)GetValue(TextualValueProperty); }
        set { SetValue(TextualValueProperty, value); }
    }
}
