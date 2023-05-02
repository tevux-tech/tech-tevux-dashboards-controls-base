namespace Tech.Tevux.Dashboards.Controls;

public partial class ControlBase {
    public static readonly DependencyProperty TextSizeProperty = DependencyProperty.Register(
        nameof(TextSize),
        typeof(double),
        typeof(ControlBase),
        new PropertyMetadata(15.0, (d, e) => {
            ((ControlBase)d).Reconfigure();
        }));

    [ExposedOption(OptionType.Number)]
    [Category(OptionCategory.Visuals)]
    public double TextSize {
        get { return (double)GetValue(TextSizeProperty); }
        set { SetValue(TextSizeProperty, value); }
    }
}
