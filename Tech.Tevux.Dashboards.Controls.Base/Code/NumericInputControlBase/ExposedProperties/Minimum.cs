namespace Tech.Tevux.Dashboards.Controls;

public partial class NumericInputControlBase {
    public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register(
        nameof(Minimum),
        typeof(decimal),
        typeof(NumericInputControlBase),
        new PropertyMetadata(0m, (d, e) => { ((NumericInputControlBase)d).Reconfigure(); }));

    [ExposedNumber]
    [Category(OptionCategory.Main)]
    public decimal Minimum {
        get { return (decimal)GetValue(MinimumProperty); }
        set { SetValue(MinimumProperty, value); }
    }
}
