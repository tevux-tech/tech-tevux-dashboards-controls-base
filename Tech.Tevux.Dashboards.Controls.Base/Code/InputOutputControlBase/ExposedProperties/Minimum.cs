namespace Tech.Tevux.Dashboards.Controls;

public partial class InputOutputControlBase {
    public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register(
        nameof(Minimum),
        typeof(decimal),
        typeof(InputOutputControlBase),
        new PropertyMetadata(0m));

    [ExposedOption(OptionType.Number)]
    [Category(OptionCategory.Main)]
    public decimal Minimum {
        get { return (decimal)GetValue(MinimumProperty); }
        set { SetValue(MinimumProperty, value); }
    }
}
