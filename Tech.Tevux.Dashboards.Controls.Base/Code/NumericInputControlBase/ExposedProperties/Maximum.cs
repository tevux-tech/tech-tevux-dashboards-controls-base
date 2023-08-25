namespace Tech.Tevux.Dashboards.Controls;

public partial class NumericInputControlBase {
    public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register(
        nameof(Maximum),
        typeof(decimal),
        typeof(NumericInputControlBase),
        new PropertyMetadata(1000000m, (d, e) => { ((NumericInputControlBase)d).Reconfigure(); }));


    [ExposedNumber]
    [Category(OptionCategory.Main)]
    public decimal Maximum {
        get { return (decimal)GetValue(MaximumProperty); }
        set { SetValue(MaximumProperty, value); }
    }
}
