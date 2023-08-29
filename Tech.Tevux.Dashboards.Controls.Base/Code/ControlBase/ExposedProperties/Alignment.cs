namespace Tech.Tevux.Dashboards.Controls;

public partial class ControlBase {
    public static readonly DependencyProperty AlignmentProperty = DependencyProperty.Register(
        nameof(Alignment),
        typeof(string),
        typeof(ControlBase),
        new PropertyMetadata("Middle", (d, e) => {
            ((ControlBase)d).Reconfigure();
        }));

    [ExposedChoice(typeof(Alignment))]
    [Category(OptionCategory.Visuals)]
    public string Alignment {
        get { return (string)GetValue(AlignmentProperty); }
        set { SetValue(AlignmentProperty, value); }
    }
}
