namespace Tech.Tevux.Dashboards.Controls;

public partial class NumericOutputControlBase {
    public static readonly DependencyProperty NumericValueProperty = DependencyProperty.Register(
        nameof(NumericValue),
        typeof(decimal),
        typeof(NumericOutputControlBase),
        new PropertyMetadata(0m, (d, e) => {
            (d as NumericOutputControlBase)?.ApplyAppearanceRules();
        }));

    public decimal NumericValue {
        get { return (decimal)GetValue(NumericValueProperty); }
        set { SetValue(NumericValueProperty, value); }
    }
}
