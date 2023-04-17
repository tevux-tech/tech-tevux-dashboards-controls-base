namespace Tech.Tevux.Dashboards.Controls;

public partial class InputOutputControlBase {
    public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register(
        nameof(Maximum),
        typeof(decimal),
        typeof(InputOutputControlBase),
        new PropertyMetadata(1000000m));


    [ExposedOption(OptionType.Number)]
    [Category(OptionCategory.Main)]
    public decimal Maximum {
        get { return (decimal)GetValue(MaximumProperty); }
        set { SetValue(MaximumProperty, value); }
    }
}
